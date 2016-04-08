using CSubCompiler.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST.BinaryOperators
{
    public class StarBinaryOperatorNode : ArithmeticBinaryOperatorNode
    {
        public StarBinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {

        }

        public override BinaryOperatorType OperatorType { get { return BinaryOperatorType.Star; } }
    }
}
