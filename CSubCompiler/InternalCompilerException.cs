using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    internal class InternalCompilerException : Exception
    {
        public InternalCompilerException(string message) : base(message)
        {

        }
    }
}
