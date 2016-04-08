using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public class MinusUnaryPreOperatorNode : UnaryPreOperatorNode
    {
        public MinusUnaryPreOperatorNode(SubExpressionNode operand, Token token, int tokenIndex) : base(UnaryPreOperatorType.Minus, operand, token, tokenIndex)
        {
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            ILBaseType operandBaseType = operandType as ILBaseType;
            if (operandBaseType == null)
                throw new ParserException("Invalid operation on non-numeric type.", TokenIndex, Token);
            return operandBaseType;
        }
    }
}
