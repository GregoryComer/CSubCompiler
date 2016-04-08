using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public class AmpersandUnaryPreOperatorNode : UnaryPreOperatorNode
    {
        public AmpersandUnaryPreOperatorNode(SubExpressionNode operand, Token token, int tokenIndex) : base(UnaryPreOperatorType.Ampersand, operand, token, tokenIndex)
        {
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            return new ILPointerType(operandType);
        }
    }
}
