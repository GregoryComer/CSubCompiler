using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST.BinaryOperators
{
    public class ShiftRightBinaryOperatorNode : BitwiseBinaryOperatorNode
    {
        public ShiftRightBinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {
        }

        public override BinaryOperatorType OperatorType => BinaryOperatorType.ShiftRight;
    }
}
