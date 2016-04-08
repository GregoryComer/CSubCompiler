using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPostOperators
{
    public static class UnaryPostOperatorNodeFactory
    {
        public static UnaryPostOperatorNode Create(UnaryPostOperatorType type, SubExpressionNode operand, Token token, int tokenIndex)
        {
            switch (type)
            {
                case UnaryPostOperatorType.DoublePlus:
                    return new DoublePlusUnaryPostOperatorNode(operand, token, tokenIndex);
                case UnaryPostOperatorType.DoubleMinus:
                    return new DoubleMinusUnaryPostOperatorNode(operand, token, tokenIndex);
                default:
                    throw new InternalCompilerException("Unknown UnaryPostOperatorType.");
            }
        }
    }
}
