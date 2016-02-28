using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class ILGenerationContext
    {
        public ILScope ScopeStack { get; set; }
        public Dictionary<string, ILStruct> StructTypes { get; set; }
        public Dictionary<string, ILTypeSpecifier> Typedefs { get; set; }
    }
}
