using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPostOperators
{
    public class DoublePlusUnaryPostOperatorNode : UnaryPostOperatorNode
    {
        public DoublePlusUnaryPostOperatorNode(SubExpressionNode operand, Token token, int tokenIndex) : base(UnaryPostOperatorType.DoublePlus, operand, token, tokenIndex)
        {
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            switch (operandType.Category)
            {
                case ILTypeCategory.Base:
                    return operandType;
                case ILTypeCategory.Struct:
                    throw new ParserException("Invalid operation on struct type.", TokenIndex, Token);
                case ILTypeCategory.Pointer:
                    return operandType;
                default:
                    throw new InternalCompilerException("Unknown ILType category.");
            }
        }
    }
}
