using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zip
{
    /// <summary>
    /// Produces a ZIP archive containing release files.
    /// </summary>
    class Program
    {
        private const string Zip = "lrx.zip";

        private const string Prefix = "lrx/";

        private const string LRX = "../../../lrx/bin/Release/";

        private const string LRXW = "../../../lrxw/bin/Release/";

        private static readonly string[] Manifest =
        {
            LRX + "lrx.exe",
            LRX + "lrx.exe.config",
            LRX + "Mono.Options.dll",
            LRXW + "lrxw.exe",
            LRXW + "lrxw.exe.config",
        };

        static void Main(string[] args)
        {
            File.Delete(Zip);
            using (var zip = ZipFile.Open(Zip, ZipArchiveMode.Create))
            {
                foreach (var file in Manifest)
                {
                    var name = Prefix + Path.GetFileName(file);
                    zip.CreateEntryFromFile(file, name, CompressionLevel.Optimal);
                }
            }
        }
    }
}
