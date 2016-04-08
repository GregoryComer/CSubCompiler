using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public class DoublePlusUnaryPreOperatorNode : UnaryPreOperatorNode
    {
        public DoublePlusUnaryPreOperatorNode(SubExpressionNode operand, Token token, int tokenIndex) : base(UnaryPreOperatorType.DoubleMinus, operand, token, tokenIndex)
        {
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            switch (operandType.Category)
            {
                case ILTypeCategory.Base:
                    return operandType;
                case ILTypeCategory.Pointer:
                    return operandType;
                case ILTypeCategory.Struct:
                    throw new ParserException("Invalid operation on struct type.", TokenIndex, Token);
            }
            ILBaseType operandBaseType = operandType as ILBaseType;
            if (operandBaseType == null || !Types.IsIntegralType(operandBaseType.Type))
                throw new ParserException("Invalid operation on non-integral type.", TokenIndex, Token);
            return operandBaseType;
        }
    }
}
