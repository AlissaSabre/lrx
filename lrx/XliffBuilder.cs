using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lrx
{
    public class XliffBuilder
    {
        public string Origin;

        public string SourceLang;

        public string TargetLang;

        public LocResFormat LocResFormat;

        private readonly List<XElement> Units = new List<XElement>();

        private int id = 1;

        public void Add(string name, string key, int hash, string source, string target)
        {
            Units.Add(new XElement(X.TU,
                new XAttribute(X.ID, id++),
                new XAttribute(X.RESNAME, key),
                new XElement(X.SOURCE, new XAttribute(X.XLANG, SourceLang), source),
                (target == null ? null : new XElement(X.TARGET, new XAttribute(X.XLANG, TargetLang), target)),
                new XElement(X.CGROUP,
                    (name == null ? null : new XElement(X.CONTEXT, new XAttribute(X.CTYPE, X.LOCRES_NAME), name)),
                    new XElement(X.CONTEXT, new XAttribute(X.CTYPE, X.LOCRES_KEY), key),
                    new XElement(X.CONTEXT, new XAttribute(X.CTYPE, X.LOCRES_HASH), hash.ToString("X08")))));
        }

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
