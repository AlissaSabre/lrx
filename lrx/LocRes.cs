using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lrx
{
    /// <summary>
    /// Contents of a single .locres file.
    /// </summary>
    public class LocRes
    {
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
                        var text = rd.ReadCString();
                        entries[n] = new Entry() { Key = key, Hash = hash, Text = text };
                    }

                    tables[t] = new Table() { Name = name, Entries = entries };
                }

                return new LocRes() { Tables = tables };
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
                        wr.WriteCString(entry.Text);
                    }
                }
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
