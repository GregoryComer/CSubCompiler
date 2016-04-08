using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public class TildeUnaryPreOperatorNode : UnaryPreOperatorNode
    {
        public TildeUnaryPreOperatorNode(SubExpressionNode operand, Token token, int tokenIndex) : base(UnaryPreOperatorType.Tilde, operand, token, tokenIndex)
        {
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            ILBaseType operandBaseType = operandType as ILBaseType;
            if (operandBaseType == null || !Types.IsIntegralType(operandBaseType.Type))
                throw new ParserException("Invalid operation on non-integral type.", TokenIndex, Token);
            return operandBaseType;
        }
    }
}
