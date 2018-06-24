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
                new XAttribute("id", id++),
                new XAttribute("resname", key),
                new XElement(X.SOURCE, new XAttribute(X.XLANG, SourceLang), source),
                (target == null ? null : new XElement(X.TARGET, new XAttribute(X.XLANG, TargetLang), target)),
                new XElement(X.CGROUP,
                    (name == null ? null : new XElement(X.CONTEXT, new XAttribute("context-type", "x-locres-namespace"), name)),
                    new XElement(X.CONTEXT, new XAttribute("context-type", "x-locres-key"), key),
                    new XElement(X.CONTEXT, new XAttribute("context-type", "x-locres-hash"), hash.ToString("X08")))));
        }

        public XElement GetDocument()
        {
            return new XElement(X.XLIFF,
                new XAttribute("version", "1.2"),
                new XAttribute(X.XNS, X.XLIFF_NS.NamespaceName),
                new XElement(X.FILE,
                    new XAttribute("original", Origin),
                    new XAttribute("source-language", SourceLang),
                    new XAttribute("target-language", TargetLang),
                    new XAttribute("datatype", LocResFormat == LocResFormat.New ? "x-locres-1" : "x-locres-0"),
                    new XAttribute(X.XSPACE, "preserve"),
                    new XElement(X.BODY,
                        Units)));
        }
    }
}
