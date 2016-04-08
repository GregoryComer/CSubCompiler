using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST.BinaryOperators
{
    public class DivideBinaryOperatorNode : ArithmeticBinaryOperatorNode
    {
        public DivideBinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(left, right, token, tokenIndex)
        {

        }

        public override BinaryOperatorType OperatorType { get { return BinaryOperatorType.Divide; } }
    }
}
