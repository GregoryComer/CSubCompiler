using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public abstract class LiteralNode : ISubExpressionNode
    {
        public abstract void GenerateIL(ILGenerationContext context, List<IILInstruction> output);
        public abstract ILTypeSpecifier GetResultType(ILGenerationContext context);
    }
}
