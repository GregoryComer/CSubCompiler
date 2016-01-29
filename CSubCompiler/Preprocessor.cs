using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: REMOVE COMMENTS

namespace CSubCompiler
{
    public static class Preprocessor
    {
        public static string Preprocess(string text)
        {
            //Todo: Implement
            //As a temporary measure, this method removes all preprocessing directives.
            return RemovePreprocessorDirectives(text);
        }

        //DEBUG METHOD
        static string RemovePreprocessorDirectives(string text)
        {
            StringBuilder output = new StringBuilder();
            int i = 0;
            while (i < text.Length)
            {
                int startIndex = i;
                for (; (i < text.Length) && ((text[i] != '\n' && text[i] != '\r') || (text[i] == '\r' && text[i + 1] == '\n')); i++) ;
                if (text[startIndex] != '#')
                {
                    output.Append(text.Substring(startIndex, i - startIndex));
                }
                for (; (i < text.Length) && (text[i] == '\n' || text[i] == '\r'); i++) ; //Skip over newline
            }
            return output.ToString().Trim();
        }
    }
}
