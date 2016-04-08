using CSubCompiler.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST.BinaryOperators
{
    /// <summary>
    /// Base class for binary operators that support pointers (plus and minus).
    /// </summary>
    public abstract class PointerSupportedArithmeticBinaryOperatorNode : ArithmeticBinaryOperatorNode
    {
        public PointerSupportedArithmeticBinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {

        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType leftType = Left.GetResultType(context);
            ILType rightType = Right.GetResultType(context);

            if (leftType.Category == ILTypeCategory.Struct || rightType.Category == ILTypeCategory.Struct)
                throw new ParserException("Invalid operation on struct type.", TokenIndex, Token);
            else if (leftType.Category == ILTypeCategory.Pointer && rightType.Category == ILTypeCategory.Pointer)
                throw new ParserException("Cannot perform operation on pointer type and pointer type.", TokenIndex, Token);
            else if (leftType.Category == ILTypeCategory.Pointer && rightType.Category == ILTypeCategory.Base)
                return leftType;
            else if (leftType.Category == ILTypeCategory.Base && rightType.Category == ILTypeCategory.Pointer)
                return rightType;
            else //Both types are base types
                return base.GetResultType(context);
        }
    }
}
