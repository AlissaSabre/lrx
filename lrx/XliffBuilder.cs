using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lrx
{
    /// <summary>
    /// Creates an XLIFF document from a series of LocRes entries.
    /// </summary>
    public class XliffBuilder
    {
        /// <summary>
        /// The LocRes file name.
        /// </summary>
        public string Origin;

        /// <summary>
        /// Source language.
        /// </summary>
        public string SourceLang;

        /// <summary>
        /// Target language.
        /// </summary>
        public string TargetLang;

        /// <summary>
        /// File format of LocRes file.
        /// </summary>
        public LocResFormat LocResFormat;

        private readonly List<XElement> Units = new List<XElement>();

        /// <summary>
        /// Serial number for XLIFF trans-unit elements.
        /// </summary>
        private int id = 1;

        /// <summary>
        /// Adds a LocRes entry to this XLIFF.
        /// </summary>
        /// <param name="name">LocRes namespace.</param>
        /// <param name="key">LocRes key.</param>
        /// <param name="hash">LocRes source hash.</param>
        /// <param name="source">Source text.</param>
        /// <param name="target">Target text when aligning, otherwise null.</param>
        public void Add(string name, string key, int hash, string source, string target = null)
        {
            // XLIFF spec defines semantics of trans-unit/@resname. 
            // I believe it is where the spec intends to store info like LocRes key (or namespace-key combination) to.
            // Many CAT tools handle it in a reasonable way.
            // However, some CAT tool handles context better than @resname.
            // That's why I store the same info twice.
            Units.Add(new XElement(X.TU,
                new XAttribute(X.ID, id++),
                new XAttribute(X.RESNAME, key),
                new XElement(X.SOURCE, new XAttribute(X.XLANG, SourceLang), source),
                (target == null ? null : new XElement(X.TARGET, new XAttribute(X.XLANG, TargetLang), target)),
                new XElement(X.CGROUP,
                    (name == null ? null : new XElement(X.CONTEXT, new XAttribute(X.CTYPE, X.LOCRES_NAME), name)),
                    new XElement(X.CONTEXT, new XAttribute(X.CTYPE, X.LOCRES_KEY), key),
                    new XElement(X.CONTEXT, new XAttribute(X.CTYPE, X.LOCRES_HASH), hash.ToString("X08", CultureInfo.InvariantCulture)))));
        }

        /// <summary>
        /// Gets an XLIFF document.
        /// </summary>
        /// <returns></returns>
        public XElement GetDocument()
        {
            return new XElement(X.XLIFF,
                new XAttribute(X.VERSION, "1.2"),
                new XAttribute(X.XNS, X.XLIFF_NS.NamespaceName),
                new XElement(X.FILE,
                    new XAttribute(X.ORIGINAL, Origin),
                    new XAttribute(X.SOURCELANG, SourceLang),
                    new XAttribute(X.TARGETLANG, TargetLang),
                    new XAttribute(X.DATATYPE, LocResFormat == LocResFormat.New ? X.LOCRES_FORMAT_NEW : X.LOCRES_FORMAT_OLD),
                    new XAttribute(X.XSPACE, X.PRESERVE),
                    new XElement(X.BODY,
                        Units)));
        }
    }
}
