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

        private readonly List<XElement> Units = new List<XElement>();

        private int id = 1;

        public void Add(string name, string key, int hash, string source, string target)
        {
            Units.Add(new XElement(XLF.TU,
                new XAttribute("id", id++),
                new XAttribute("resname", key),
                new XElement(XLF.SOURCE, new XAttribute(XLF.XLANG, SourceLang), source),
                (target == null ? null : new XElement(XLF.TARGET, new XAttribute(XLF.XLANG, TargetLang), target)),
                new XElement(XLF.CGROUP,
                    (name == null ? null : new XElement(XLF.CONTEXT, new XAttribute("context-type", "x-locrees-namespace"), name)),
                    new XElement(XLF.CONTEXT, new XAttribute("context-type", "x-locres-key"), key),
                    new XElement(XLF.CONTEXT, new XAttribute("context-type", "x-locres-hash"), hash.ToString("X08")))));
        }

        public XElement GetDocument()
        {
            return new XElement(XLF.XLIFF,
                new XAttribute("version", "1.2"),
                new XAttribute(XLF.XNS, XLF.NS.NamespaceName),
                new XElement(XLF.FILE,
                    new XAttribute("original", Origin),
                    new XAttribute("source-language", SourceLang),
                    new XAttribute("target-language", TargetLang),
                    new XAttribute("datatype", "x-locres"),
                    new XAttribute(XLF.XSPACE, "preserve"),
                    new XElement(XLF.BODY,
                        Units)));
        }
    }
}
