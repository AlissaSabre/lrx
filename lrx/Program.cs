using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mono.Options;

namespace lrx
{
    class Program
    {
        private enum Op { Import, Align, Export, Help };
        private enum Format { @auto = LocResFormat.Auto, @old = LocResFormat.Old, @new = LocResFormat.New }; // XXX

        private const string NAME = "lrx";

        static void Main(string[] args)
        {
            var diag = new Diagnoser();
            Op? op = null;
            string slang = null;
            string tlang = null;
            Format? fmt = null;

            var options = new OptionSet
            {
                { "i|import", "Import a source LocRes into an XLIFF.", v => op = Op.Import },
                { "a|align", "Align two (source and target) LocRes into an XLIFF.", v => op = Op.Align },
                { "e|export", "Export an XLIFF to a LocRes.", v => op = Op.Export },
                { "h|help", "Show usage and terminate.", v => op = Op.Help },

                { "s|slang=", "Source language, e.g., en.", v => slang = v },
                { "t|tlang=", "Target language, e.g., fr.", v => tlang = v },
                { "f|format=", "LocRes file format.  VALUE is auto, old or new.  Default is auto.", (Format v) => fmt = v },
            };

            List<string> files = null;
            try
            {
                files = options.Parse(args);
                switch (op)
                {
                    case Op.Import:
                        if (slang == null) diag.Add("Import requires a source language (-s).");
                        if (tlang == null) diag.Add("Import requires a target language (-t).");
                        if (files.Count != 2) diag.Add("Import requires two files, source LocRes and output XLIFF.");
                        if (fmt != null) diag.Add("Import doesn't allow -f option.");
                        break;
                    case Op.Align:
                        if (slang == null) diag.Add("Align requires a source language (-s).");
                        if (tlang == null) diag.Add("Align requires a target language (-t).");
                        if (files.Count != 3) diag.Add("Align requires three files, source LocRes, target LocRes and output XLIFF.");
                        if (fmt != null) diag.Add("Align doesn't allow -f option.");
                        break;
                    case Op.Export:
                        if (files.Count != 2) diag.Add("Export requires two files, input XLIFF and target LocRes.");
                        if (slang != null) diag.Add("Export doesn't allow -s option.");
                        if (tlang != null) diag.Add("Export doesn't allow -t option.");
                        break;
                    case Op.Help:
                        break;
                    default: /* most likely null */
                        diag.Add("One of Import (-i), Align (-a), Export (-e), or Help (-h) is required.");
                        break;
                }
            }
            catch (OptionException e)
            {
                diag.Add(e.Message);
            }

            if (diag.Messages.Count > 0)
            {
                Console.Error.WriteLine("Error on the command line options/parameters.");
                foreach (var msg in diag.Messages) Console.Error.WriteLine("  " + msg);
                Console.Error.WriteLine("use \"{0} --help\" to see command line syntax.", NAME);
                Environment.Exit(1);
            }

            switch (op)
            {
                case Op.Import:
                    Converter.Import(files[0], files[1], slang, tlang);
                    break;
                case Op.Align:
                    Converter.Align(files[0], files[1], files[2], slang, tlang);
                    break;
                case Op.Export:
                    Converter.Export(files[0], files[1], (LocResFormat)(fmt ?? Format.@auto));
                    break;
                case Op.Help:
                    {
                        Console.Error.WriteLine("Usage: {0} command [options] files...", NAME);
                        Console.Error.WriteLine("  To import: {0} -i -s source-language -t target-language source.locres output.xliff", NAME);
                        Console.Error.WriteLine("  To align:  {0} -a -s source-language -t target-language source.locres target.locres output.xloiff", NAME);
                        Console.Error.WriteLine("  To export: {0} -e -s [-f format] input.xliff output-target.locres", NAME);
                        Console.Error.WriteLine("Options:");
                        options.WriteOptionDescriptions(Console.Error);
                        Console.Error.WriteLine("Example:");
                        Console.Error.WriteLine("  {0} -a -s en -t fr en/Tutorial.locres fr/Tutorial.locres Tutorial-en-fr.xliff", NAME);
                        Console.Error.WriteLine();
                    }
                    break;
            }
        }
    }

    class Diagnoser
    {
        public readonly List<string> Messages = new List<string>();
        
        public void Add(string fmt, params object[] args)
        {
            Messages.Add(string.Format(fmt, args));
        }
    }
}
