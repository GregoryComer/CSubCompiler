using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    public class ParserException : Exception
    {
        public ParserException(string message, int tokenIndex, Token token) : base(message)
        {
            TokenIndex = tokenIndex;
            Token = token;
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
