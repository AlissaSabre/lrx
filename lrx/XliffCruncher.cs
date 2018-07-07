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
    /// <summary>
    /// Reads an XLIFF and turns it into LocRes infoset.
    /// </summary>
    /// <remarks>
    /// This class works only on XLIFF that is originally produced by lrx tool.
    /// It can't handle general XLIFF file.
    /// </remarks>
    public class XliffCruncher
    {
        private XElement Xliff;

        /// <summary>
        /// Reads an XLIFF file for further processing.
        /// </summary>
        /// <param name="filename">Path name of XLIFF file.</param>
        public void Read(string filename)
        {
            var xliff = XElement.Load(filename);
            if (xliff.Name != X.XLIFF) throw Exception("Not an XLIFF file.");
            Xliff = xliff;
        }

        /// <summary>
        /// Takes an XLIFF and turns it into LocRes infoset.
        /// </summary>
        /// <returns>A LocRes inforset.</returns>
        public LocRes Crunch()
        {
            LocResFormat format;
            switch ((string)Xliff.Element(X.FILE).Attribute(X.DATATYPE))
            {
                case X.LOCRES_FORMAT_OLD: format = LocResFormat.Old; break;
                case X.LOCRES_FORMAT_NEW: format = LocResFormat.New; break;
                default:
                    throw Exception("Unknown datatype");
            }

            // In LocRes file, all texts that share a same namespace are put together in one place.
            // lrx creates an XLIFF in which trans-unit's sharing a same namespace appear contiguously.
            // However, there is a rumor that some CAT tool reorders trans-unit in its own circumstance upon translation.
            // So, I chose to go a safer way.

            var names = Xliff.Descendants(X.TU).Select(tu => GetContext(tu, X.LOCRES_NAME)).Distinct().ToArray();
            var tables = new List<LocRes.Table>(names.Length);
            foreach (var n in names)
            {
                var entries = new List<LocRes.Entry>();
                foreach (var tu in Xliff.Descendants(X.TU).Where(tu => GetContext(tu, X.LOCRES_NAME) == n))
                {
                    var text = (string)tu.Element(X.TARGET);
                    if (text == null)
                    {
                        // Absence of target element indicates this trans-unit is not translated yet.
                        // It seems that untranslated texts are excluded in a genuine LocRes.
                        continue;
                    }

                    var key = GetContext(tu, X.LOCRES_KEY);
                    if (key == null) throw Exception("No key specified in context for trans-unit {0}.", tu.Attribute(X.ID));

                    var hash_text = GetContext(tu, X.LOCRES_HASH);
                    if (hash_text == null) throw Exception("No hash specified in context for trans-unit {0}.", tu.Attribute(X.ID));

                    int hash;
                    if (!int.TryParse(hash_text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hash))
                    {
                        throw Exception("Wrong hash value format for trans-unit {0}", tu.Attribute(X.ID));
                    }

                    entries.Add(new LocRes.Entry() { Key = key, Hash = hash, Text = text });
                }
                tables.Add(new LocRes.Table() { Name = n, Entries = entries });
            }

            return new LocRes() { Tables = tables, Format = format };
        }

        /// <summary>
        /// Returns a context value for a trans-unit element.
        /// </summary>
        /// <param name="tu">trans-unit element.</param>
        /// <param name="context_type">context-type.</param>
        /// <returns>Content string of a context element whose context-type matches <paramref name="context_type"/>, or null if no such context exists.</returns>
        private static string GetContext(XElement tu, string context_type)
        {
            return (string)tu.Element(X.CGROUP)?.Elements(X.CONTEXT)?.FirstOrDefault(c => (string)c.Attribute(X.CTYPE) == context_type);
        }

        /// <summary>
        /// Creates an Exception with a formatted message that is ready to throw.
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Exception Exception(string fmt, params object[] args)
        {
            return new FormatException(string.Format(fmt, args));
        }
    }
}
