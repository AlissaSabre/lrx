using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lrx
{
    public static class Converter
    {
        public static void Import(string sourceLocRes, string outputXliff, string sourceLang, string targetLang)
        {
            var locres = LocRes.Read(sourceLocRes);
            var builder = new XliffBuilder() { SourceLang = sourceLang, TargetLang = targetLang, Origin = sourceLocRes, LocResFormat = locres.Format };
            foreach (var table in locres.Tables)
            {
                foreach (var entry in table.Entries)
                {
                    builder.Add(table.Name, entry.Key, entry.Hash, entry.Text);
                }
            }
            var xliff = builder.GetDocument();
            xliff.Save(outputXliff);
        }

        public static void Align(string sourceLocRes, string targetLocRes, string outputXliff, string sourceLang, string targetLang)
        {
            var source = LocRes.Read(sourceLocRes);
            var target = LocRes.Read(targetLocRes);
            var format = (LocResFormat)Math.Max((int)source.Format, (int)target.Format);
            var builder = new XliffBuilder() { SourceLang = sourceLang, TargetLang = targetLang, Origin = sourceLocRes, LocResFormat = format };
            foreach (var table in source.Tables)
            {
                foreach (var entry in table.Entries)
                {
                    string target_text;
                    target.Lookup(table.Name, entry.Key, entry.Hash, out target_text);
                    builder.Add(table.Name, entry.Key, entry.Hash, entry.Text, target_text);
                }
            }
            var xliff = builder.GetDocument();
            xliff.Save(outputXliff);
        }

        public static void Export(string inputXliff, string outputLocRes, LocResFormat format)
        {
            var cruncher = new XliffCruncher();
            cruncher.Read(inputXliff);
            var locres = cruncher.Crunch();
            if (format != LocResFormat.Auto)
            {
                locres.Format = format;
            }
            locres.Save(outputLocRes);
        }
    }
}
