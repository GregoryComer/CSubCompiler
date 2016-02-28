using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public interface ISubExpressionNode
    {
        void GenerateIL(ILGenerationContext context, List<IILInstruction> output);
        ILTypeSpecifier GetResultType(ILGenerationContext context);
    }
}
