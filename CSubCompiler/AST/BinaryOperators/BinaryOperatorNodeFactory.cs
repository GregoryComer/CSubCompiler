using CSubCompiler.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST.BinaryOperators
{
    public static class BinaryOperatorNodeFactory
    {
        public static BinaryOperatorNode Create(BinaryOperatorType type, SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex)
        {
            switch (type)
            {
                case BinaryOperatorType.Ampersand:
                    return new AmpersandBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.AmpersandEqual:
                    return new AmpersandEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Caret:
                    return new CaretBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.CaretEqual:
                    return new CaretEqualBinaryOperaratorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Divide:
                    return new DivideBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.DivideEqual:
                    return new DivideEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Minus:
                    return new MinusBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.MinusEqual:
                    return new MinusEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Mod:
                    return new ModBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.ModEqual:
                    return new ModEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Pipe:
                    return new PipeBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.PipeEqual:
                    return new PipeEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Plus:
                    return new PlusBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.PlusEqual:
                    return new PlusEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.ShiftLeft:
                    return new ShiftLeftBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.ShiftLeftEqual:
                    return new ShiftLeftEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.ShiftRight:
                    return new ShiftRightBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.ShiftRightEqual:
                    return new ShiftRightEqualBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.Star:
                    return new StarBinaryOperatorNode(left, right, token, tokenIndex);
                case BinaryOperatorType.StarEqual:
                    return new StarEqualBinaryOperatorNode(left, right, token, tokenIndex);
                default:
                    throw new InternalCompilerException("Unknown BinaryOperatorType.");
            }
        }
    }
}
