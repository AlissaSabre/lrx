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
    public static class X
    {
        public static readonly XNamespace XLIFF_NS = XNamespace.Get("urn:oasis:names:tc:xliff:document:1.2");

        public static readonly XName XLIFF = XLIFF_NS + "xliff";

        public static readonly XName FILE = XLIFF_NS + "file";

        public static readonly XName BODY = XLIFF_NS + "body";

        public static readonly XName TU = XLIFF_NS + "trans-unit";

        public static readonly XName SOURCE = XLIFF_NS + "source";

        public static readonly XName TARGET = XLIFF_NS + "target";

        public static readonly XName CGROUP = XLIFF_NS + "context-group";

        public static readonly XName CONTEXT = XLIFF_NS + "context";

        public static readonly XName XNS = XNamespace.Xml + "ns";

        public static readonly XName XLANG = XNamespace.Xml + "lang";

        public static readonly XName XSPACE = XNamespace.Xml + "space";


    }
}
