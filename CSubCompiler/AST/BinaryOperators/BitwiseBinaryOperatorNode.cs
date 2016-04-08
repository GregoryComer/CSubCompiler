using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST.BinaryOperators
{
    public abstract class BitwiseBinaryOperatorNode : BinaryOperatorNode
    {
        protected BitwiseBinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {

        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType leftType = Left.GetResultType(context);
            ILType rightType = Right.GetResultType(context);

            if (leftType.Category == ILTypeCategory.Struct || rightType.Category == ILTypeCategory.Struct)
            {
                throw new ParserException("Invalid operation on struct type.", TokenIndex, Token);
            }
            if (leftType.Category == ILTypeCategory.Pointer || rightType.Category == ILTypeCategory.Pointer) // Pointer Arithmetic Is Handled In BasicArithmeticBinaryOperatorNode
            {
                throw new ParserException("Invalid operation on pointer type.", TokenIndex, Token);
            }

            ILBaseType leftBaseType = (ILBaseType)leftType;
            ILBaseType rightBaseType = (ILBaseType)rightType;

            var promotedType = Promote(leftBaseType.Type, rightBaseType.Type);

            return new ILBaseType(promotedType);
        }
    }
}
