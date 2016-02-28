using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.Language
{
    public static class Keywords
    {
        public static string[] KeywordTable = { "auto", "break", "case", "char", "const", "continue", "default", "do", "else", "enum", "extern", "float", "for", "goto", "if", "int", "long", "register", "return", "short", "signed", "sizeof", "static", "struct", "switch", "typedef", "union", "void", "volatile", "while" };
        public static string[] TypeKeywordTable = { "char", "float", "int", "void" };

        public static bool IsKeyword(string value)
        {
            return KeywordTable.Contains(value);
        }
        public static bool IsNonTypeKeyword(string value)
        {
            return KeywordTable.Contains(value) && !TypeKeywordTable.Contains(value);
        }
    }
}
