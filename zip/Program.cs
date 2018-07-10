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

        private static readonly string[] Manifest =
        {
            "../../../lrx/bin/Release/lrx.exe",
            "../../../lrx/bin/Release/lrx.exe.config",
            "../../../lrx/bin/Release/Mono.Options.dll",
            "../../../lrxw/bin/Release/lrxw.exe",
            "../../../lrxw/bin/Release/lrxw.exe.config",
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
