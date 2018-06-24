using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lrx
{
    public class XliffCruncher
    {
        public XElement Xliff;

        public void Read(string filename)
        {
            var xliff = XElement.Load(filename);
            if (xliff.Name != XLF.XLIFF) throw new IOException();
        }

        public LocRes Crunch()
        {
            var names = Xliff.Descendants(XLF.TU).Select(tu => GetContext(tu, "x-locres-namespace")).Distinct().ToArray();
            var tables = new List<LocRes.Table>(names.Length);
            foreach (var n in names)
            {
                var entries = new List<LocRes.Entry>();
                foreach (var tu in Xliff.Descendants(XLF.TU).Where(tu => GetContext(tu, "x-locres-namespace") == n))
                {
                    var text = (string)tu.Element(XLF.TARGET);
                    if (text == null) continue;

                    var key = GetContext(tu, "x-locres-key");
                    var hash_text = GetContext(tu, "x-locres-hash");
                    if (key == null || hash_text == null) throw new FormatException();
                    int hash;
                    if (!int.TryParse(hash_text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hash))
                    {
                        throw new FormatException();
                    }

                    entries.Add(new LocRes.Entry() { Key = key, Hash = hash, Text = text });
                }
                tables.Add(new LocRes.Table() { Name = n, Entries = entries });
            }

            return new LocRes() { Tables = tables };
        }

        /// <summary>
        /// Returns a context value for a trans-unit element.
        /// </summary>
        /// <param name="tu">trans-unit element.</param>
        /// <param name="context_type">context-type.</param>
        /// <returns>Content string of a context element whose context-type matches <paramref name="context_type"/>, or null if no such context exists.</returns>
        private static string GetContext(XElement tu, string context_type)
        {
            return (string)tu.Element(XLF.CGROUP)?.Elements(XLF.CONTEXT)?.FirstOrDefault(c => (string)c.Attribute("context-type") == context_type);
        }
    }
}
