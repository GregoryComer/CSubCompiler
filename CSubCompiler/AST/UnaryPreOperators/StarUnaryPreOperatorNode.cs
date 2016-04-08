using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public class StarUnaryPreOperatorNode : UnaryPreOperatorNode
    {
        public StarUnaryPreOperatorNode(SubExpressionNode operand, Token token, int tokenIndex) : base(UnaryPreOperatorType.Star, operand, token, tokenIndex)
        {
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            ILPointerType pointerType = operandType as ILPointerType;
            if (pointerType == null)
                throw new ParserException("Unable to dereference non-pointer type.", TokenIndex, Token);
            return pointerType.InnerType.Type;
        }
    }
}
