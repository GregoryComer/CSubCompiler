using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    public class ParserException : Exception
    {
        public ParserException(string message, int codeIndex, int tokenIndex, Token token) : base(message)
        {
            CodeIndex = codeIndex;
            TokenIndex = tokenIndex;
            Token = token;
        }

        public int CodeIndex
        {
            get;
            set;
        }
        public int TokenIndex
        {
            get;
            set;
        }
        public Token Token
        {
            get;
            set;
        }
    }
}
