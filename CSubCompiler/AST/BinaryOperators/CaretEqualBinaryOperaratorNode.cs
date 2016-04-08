using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST.BinaryOperators
{
    public class CaretEqualBinaryOperaratorNode : CaretBinaryOperatorNode
    {
        public CaretEqualBinaryOperaratorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {
        }
    }
}
