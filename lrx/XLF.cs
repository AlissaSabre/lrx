using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lrx
{
    /// <summary>
    /// Defines some Linq to XML constants for XLIFF handling.
    /// </summary>
    public static class XLF
    {
        public static readonly XNamespace NS = XNamespace.Get("urn:oasis:names:tc:xliff:document:1.2");

        public static readonly XName XLIFF = NS + "xliff";

        public static readonly XName FILE = NS + "file";

        public static readonly XName BODY = NS + "body";

        public static readonly XName TU = NS + "trans-unit";

        public static readonly XName SOURCE = NS + "source";

        public static readonly XName TARGET = NS + "target";

        public static readonly XName CGROUP = NS + "context-group";

        public static readonly XName CONTEXT = NS + "context";

        public static readonly XName XNS = XNamespace.Xml + "ns";

        public static readonly XName XLANG = XNamespace.Xml + "lang";

        public static readonly XName XSPACE = XNamespace.Xml + "space";
    }
}
