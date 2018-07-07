using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lrx
{
    /// <summary>
    /// Format of LocRes file.
    /// </summary>
    public enum LocResFormat
    {
        Auto,
        Old,
        New,
    }

    /// <summary>
    /// Contents of a single .locres file (LocRes infoset).
    /// </summary>
    public class LocRes
    {
        /// <summary>
        /// Magic number at the beginning of a newer format.
        /// </summary>
        private static byte[] MAGIC =
        {
            0x0E, 0x14, 0x74, 0x75, 0x67, 0x4A, 0x03, 0xFC,
            0x4A, 0x15, 0x90, 0x9D, 0xC3, 0x37, 0x7F, 0x1B,
            0x01
        };

        /// <summary>
        /// LocRes file format.
        /// </summary>
        public LocResFormat Format;

        /// <summary>
        /// Series of namespace tables.
        /// </summary>
        public IEnumerable<Table> Tables;

        /// <summary>
        /// A namespace table.
        /// </summary>
        public class Table
        {
            /// <summary>
            /// Namespace name.
            /// </summary>
            public string Name;

            /// <summary>
            /// Series of entries in this namespace.
            /// </summary>
            public IEnumerable<Entry> Entries;
        }

        /// <summary>
        /// A text entry.
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// Text key.
            /// </summary>
            public string Key;

            /// <summary>
            /// Source text hash.
            /// </summary>
            public int Hash;

            /// <summary>
            /// Text.
            /// </summary>
            public string Text;
        }

        /// <summary>
        /// Reads the contents of a LocRes file.
        /// </summary>
        /// <param name="filename">Path name to the LocRes file.</param>
        /// <returns>LocRes infoset.</returns>
        public static LocRes Read(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                return Read(stream);
            }
        }

        /// <summary>
        /// Reads the contents of a LocRes stream.
        /// </summary>
        /// <param name="stream">Stream to read LocRes from.  It must be <see cref="Stream.CanSeek"/>.</param>
        /// <returns>LocRes infoset.</returns>
        public static LocRes Read(Stream stream)
        {
            var format = LocResFormat.Old;
            string[] strings = null;

            // See if this is in a newer format.
            var position = stream.Position;
            var preamble = new byte[MAGIC.Length];
            if (stream.Read(preamble, 0, preamble.Length) != preamble.Length)
            {
                throw new IOException();
            }
            if (Enumerable.SequenceEqual(MAGIC, preamble))
            {
                // This is a newer format LocRes.

                long offset = 0;
                for (int i = 0; i < 64; i += 8)
                {
                    offset |= (long)stream.ReadByte() << i;
                }
                if (offset < 0) throw new FormatException("Invalid string table offset.");

                position = stream.Position;
                stream.Position = offset;
                strings = LoadStrings(stream);
                format = LocResFormat.New;
            }

            // Seek back to the beginning of tables.
            stream.Position = position;

            // Read the tables.
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

        /// <summary>
        /// Loads the string pool from LocRes at the current position.
        /// </summary>
        /// <param name="stream">Stream to read the string pool from.</param>
        /// <returns>String pool.</returns>
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

        /// <summary>
        /// Saves this LocRes infoset into a file.
        /// </summary>
        /// <param name="filename">Path name.</param>
        public void Save(string filename)
        {
            using (var stream = File.OpenWrite(filename))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Saves this LocRes infoset into a Stream.
        /// </summary>
        /// <param name="stream">Stream to save infoset into.</param>
        public void Save(Stream stream)
        {
            switch (Format)
            {
                case LocResFormat.Auto:
                case LocResFormat.Old:
                    SaveOldFormat(stream);
                    break;
                case LocResFormat.New:
                    SaveNewFormat(stream);
                    break;
            }
        }

        private void SaveOldFormat(Stream stream)
        {
            SaveTables(stream, null);
        }

        private void SaveNewFormat(Stream stream)
        {
            stream.Write(MAGIC, 0, MAGIC.Length);

            var coder = new StringCoder();

            using (var memory = new MemoryStream())
            {
                SaveTables(memory, coder);
                long offset = MAGIC.Length + 8 + memory.Length;
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

        /// <summary>
        /// Maps a string to an index in a LocRes string pool, at the same time constructing the pool.
        /// </summary>
        private class StringCoder
        {
            private readonly Dictionary<string, int> Pool = new Dictionary<string, int>();

            /// <summary>
            /// Get an index for a string, possibly adding it into the pool.
            /// </summary>
            /// <param name="text">A string.</param>
            /// <returns>An index.</returns>
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

            /// <summary>
            /// Get the string pool as an array.
            /// </summary>
            /// <returns></returns>
            public string[] GetStringTable()
            {
                return Pool.OrderBy(p => p.Value).Select(p => p.Key).ToArray();
            }
        }

        private Dictionary<Tuple<string, string, int>, string> LookupTable;

        /// <summary>
        /// Looks up an entry in this LocRes pool.
        /// </summary>
        /// <param name="name">Namespace name.</param>
        /// <param name="key">Key value.</param>
        /// <param name="hash">Hash value.</param>
        /// <param name="text">The text if found.  Null otherwise.</param>
        /// <returns>True if found.  False otherwise.</returns>
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

    /// <summary>
    /// A set of extension methods to read/write a string in a LocRes serialization from/to a BinaryReader/BinaryWriter.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Reads a LocRes-serialized string from a <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="rd">BinaryReader to read a string from.</param>
        /// <returns>The string.  It may be null.</returns>
        public static string ReadCString(this BinaryReader rd)
        {
            var size = rd.ReadInt32();
            if (size > 0) throw new FormatException("Invalid length.");
            if (size == 0) return null;
            size = -size - 1;
            var array = new char[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = (char)rd.ReadUInt16();
            }
            if (rd.ReadUInt16() != 0) throw new FormatException("No terminating NUL.");
            return new String(array);
        }

        /// <summary>
        /// Writes a string into a <see cref="BinaryWriter"/> in LocRes serialization.
        /// </summary>
        /// <param name="wr">BinaryWriter to write a string into.</param>
        /// <param name="text">The string.  It can be null.</param>
        public static void WriteCString(this BinaryWriter wr, string text)
        {
            if (text == null)
            {
                wr.Write((Int32)0);
            }
            else
            {
                wr.Write((Int32)(-(text.Length + 1)));
                foreach (var c in text)
                {
                    wr.Write((UInt16)c);
                }
                wr.Write((UInt16)0);
            }
        }
    }
}
