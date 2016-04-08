using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST.BinaryOperators
{
    public class ShiftLeftEqualBinaryOperatorNode : ShiftLeftBinaryOperatorNode
    {
        public ShiftLeftEqualBinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {
        }
    }
}
