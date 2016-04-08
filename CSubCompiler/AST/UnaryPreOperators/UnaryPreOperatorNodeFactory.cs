using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public static class UnaryPreOperatorNodeFactory
    {
        public static UnaryPreOperatorNode Create(UnaryPreOperatorType type, SubExpressionNode operand, Token token, int tokenIndex)
        {
            switch (type)
            {
                case UnaryPreOperatorType.Ampersand:
                    return new AmpersandUnaryPreOperatorNode(operand, token, tokenIndex);
                case UnaryPreOperatorType.DoubleMinus:
                    return new DoubleMinusUnaryPreOperatorNode(operand, token, tokenIndex);
                case UnaryPreOperatorType.DoublePlus:
                    return new DoublePlusUnaryPreOperatorNode(operand, token, tokenIndex);
                case UnaryPreOperatorType.Exclamation:
                    return new ExclamationUnaryPreOperatorNode(operand, token, tokenIndex);
                case UnaryPreOperatorType.Minus:
                    return new MinusUnaryPreOperatorNode(operand, token, tokenIndex);
                case UnaryPreOperatorType.Star:
                    return new StarUnaryPreOperatorNode(operand, token, tokenIndex);
                case UnaryPreOperatorType.Tilde:
                    return new TildeUnaryPreOperatorNode(operand, token, tokenIndex);
                default:
                    throw new InternalCompilerException("Unknown UnaryPreOperatorType.");
            }
        }
    }
}
