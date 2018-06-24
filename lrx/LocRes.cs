using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lrx
{
    public enum LocResFormat
    {
        Auto,
        Old,
        New,
    }

    /// <summary>
    /// Contents of a single .locres file.
    /// </summary>
    public class LocRes
    {
        private static byte[] MAGIC =
        {
            0x0E, 0x14, 0x74, 0x75, 0x67, 0x4A, 0x03, 0xFC,
            0x4A, 0x15, 0x90, 0x9D, 0xC3, 0x37, 0x7F, 0x1B,
            0x01
        };

        public LocResFormat Format;

        public IEnumerable<Table> Tables;

        public class Table
        {
            public string Name;

            public IEnumerable<Entry> Entries;
        }

        public class Entry
        {
            public string Key;

            public int Hash;

            public string Text;
        }

        public static LocRes Read(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                return Read(stream);
            }
        }

        public static LocRes Read(Stream stream)
        {
            var format = LocResFormat.Auto;
            string[] strings = null;

            var position = stream.Position;
            var preamble = new byte[MAGIC.Length];
            if (stream.Read(preamble, 0, preamble.Length) != preamble.Length)
            {
                throw new IOException();
            }
            if (Enumerable.SequenceEqual(MAGIC, preamble))
            {
                long offset = 0;
                for (int i = 0; i < 64; i += 8)
                {
                    offset |= (long)stream.ReadByte() << i;
                }
                if (offset < 0) throw new FormatException();

                stream.Position = offset;
                strings = LoadStrings(stream);

                stream.Position = position + MAGIC.Length + 8;
                format = LocResFormat.New;
            }
            else
            {
                stream.Position = position;
                format = LocResFormat.Old;
            }

            using (var rd = new BinaryReader(stream, Encoding.Unicode, true))
            {
                var table_count = rd.ReadInt32();
                var tables = new Table[table_count];
                for (int t = 0; t < table_count; t++)
                {
                    var name = rd.ReadCString();

                    var entry_count = rd.ReadInt32();
                    var entries = new Entry[entry_count];
                    for (int n = 0; n < entry_count; n++)
                    {
                        var key = rd.ReadCString();
                        var hash = rd.ReadInt32();
                        var text = strings == null ? rd.ReadCString() : strings[rd.ReadInt32()];
                        entries[n] = new Entry() { Key = key, Hash = hash, Text = text };
                    }

                    tables[t] = new Table() { Name = name, Entries = entries };
                }

                return new LocRes() { Tables = tables, Format = format };
            }
        }

        private static string[] LoadStrings(Stream stream)
        {
            using (var rd = new BinaryReader(stream, Encoding.Unicode, true))
            {
                var string_count = rd.ReadInt32();
                var strings = new string[string_count];
                for (int i = 0; i < string_count; i++)
                {
                    strings[i] = rd.ReadCString();
                }
                return strings;
            }
        }

        public void Save(string filename)
        {
            using (var stream = File.OpenWrite(filename))
            {
                Save(stream);
            }
        }

        public void Save(Stream stream)
        {
            switch (Format)
            {
                case LocResFormat.Auto:
                case LocResFormat.Old:
                    SaveTables(stream, null);
                    break;
                case LocResFormat.New:
                    SaveNewFormat(stream);
                    break;
            }
        }

        private void SaveNewFormat(Stream stream)
        {
            stream.Write(MAGIC, 0, MAGIC.Length);
            var coder = new StringCoder();

            using (var memory = new MemoryStream())
            {
                SaveTables(memory, coder);
                var offset = MAGIC.Length + 8 + memory.Length;
                for (int i = 0; i < 64; i += 8)
                {
                    stream.WriteByte((byte)(offset >> i));
                }
                memory.WriteTo(stream);
            }

            var strings = coder.GetStringTable();
            using (var wr = new BinaryWriter(stream))
            {
                wr.Write((Int32)strings.Length);
                foreach (var s in strings)
                {
                    wr.WriteCString(s);
                }
            }
        }

        private void SaveTables(Stream stream, StringCoder coder)
        {
            using (var wr = new BinaryWriter(stream, Encoding.Unicode, true))
            {
                wr.Write((Int32)Tables.Count());
                foreach (var table in Tables)
                {
                    wr.WriteCString(table.Name);
                    wr.Write((Int32)table.Entries.Count());
                    foreach (Entry entry in table.Entries)
                    {
                        wr.WriteCString(entry.Key);
                        wr.Write((Int32)entry.Hash);
                        if (coder == null)
                        {
                            wr.WriteCString(entry.Text);
                        }
                        else
                        {
                            wr.Write((Int32)coder.Encode(entry.Text));
                        }
                    }
                }
            }
        }

        private class StringCoder
        {
            private readonly Dictionary<string, int> Pool = new Dictionary<string, int>();

            public int Encode(string text)
            {
                int code;
                if (Pool.TryGetValue(text, out code))
                {
                    return code;
                }
                else
                {
                    return Pool[text] = Pool.Count;
                }
            }

            public string[] GetStringTable()
            {
                return Pool.OrderBy(p => p.Value).Select(p => p.Key).ToArray();
            }
        }

        private Dictionary<Tuple<string, string, int>, string> LookupTable;

        /// <summary>
        /// Looks up a matching entry in this LocRes pool.
        /// </summary>
        /// <param name="name">Namespace name.</param>
        /// <param name="key">Key value.</param>
        /// <param name="hash">Hash value.</param>
        /// <param name="text">Returns the text from a matching entry.</param>
        /// <returns>True if found.</returns>
        public bool Lookup(string name, string key, int hash, out string text)
        {
            if (LookupTable == null)
            {
                var lut = new Dictionary<Tuple<string, string, int>, string>();
                foreach (var table in Tables)
                {
                    foreach (var entry in table.Entries)
                    {
                        lut[Tuple.Create(table.Name, entry.Key, entry.Hash)] = entry.Text;
                    }
                }
                LookupTable = lut;
            }

            return LookupTable.TryGetValue(Tuple.Create(name, key, hash), out text);
        }
    }

    public static class ExtensionMethods
    {
        public static string ReadCString(this BinaryReader rd)
        {
            var size = rd.ReadInt32();
            if (size > 0) throw new FormatException();
            if (size == 0) return null;
            size = -size - 1;
            var array = new char[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = (char)rd.ReadUInt16();
            }
            if (rd.ReadUInt16() != 0) throw new FormatException();
            return new String(array);
        }

        public static void WriteCString(this BinaryWriter wr, string text)
        {
            if (text == null)
            {
                wr.Write((Int32)0);
            }
            else
            {
                wr.Write((Int32)(-text.Length - 1));
                foreach (var c in text)
                {
                    wr.Write((UInt16)c);
                }
                wr.Write((UInt16)0);
            }
        }
    }
}
