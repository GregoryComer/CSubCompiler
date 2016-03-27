using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public abstract class SubExpressionNode : Node
    {
        public SubExpressionNode(Token token, int tokenIndex) : base(token, tokenIndex)
        {

        }

        public abstract ILType GetResultType(ILGenerationContext context);
        public abstract bool IsLValue(ILGenerationContext context);
        public abstract ILAddressingReference GenerateLValue(ILGenerationContext context);
    }
}
