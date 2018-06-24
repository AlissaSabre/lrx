using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lrx
{
    public enum LocResFormat
    {
        Auto,
        Old,
        New,
    }

    public static class Converter
    {
        public static void Import(string sourceLocRes, string outputXliff, string sourceLang, string targetLang)
        {
            throw new NotImplementedException();
        }

        public static void Align(string sourceLocRes, string targetLocRes, string outputXliff, string sourceLang, string targetLang)
        {
            throw new NotImplementedException();
        }

        public static void Export(string inputXliff, string outputLocRes, LocResFormat format)
        {
            throw new NotImplementedException();
        }
    }
}
