using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    public class LexerException : Exception
    {
        public LexerException(string message, int index) : base(message)
        {
            Index = index;
        }

        public int Index
        {
            get;
            set;
        }
    }
}
