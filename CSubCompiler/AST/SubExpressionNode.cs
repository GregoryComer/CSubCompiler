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
        public virtual bool IsLValue(ILGenerationContext context) { throw new NotImplementedException("Hit fallback IsLValue method."); }
        public virtual ILAddressingReference GenerateLValue(ILGenerationContext context) { throw new NotImplementedException("Hit fallback GenerateLValue method."); }
    }
}
