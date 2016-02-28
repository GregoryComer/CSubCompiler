using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class UnaryPreOperatorNode : OperatorNode
    {
        public UnaryPreOperatorType OperatorType;
        public ISubExpressionNode Operand;

        public int TokenIndex;
        public Token Token;

        public UnaryPreOperatorNode(UnaryPreOperatorType operatorType, ISubExpressionNode operand, int tokenIndex, Token token)
        {
            OperatorType = operatorType;
            Operand = operand;
            TokenIndex = tokenIndex;
            Token = token;
        }

        public override string ToString()
        {
            return OperatorType.ToString();
        }

        public static bool IsUnaryPreOperator(Token[] tokens, int i)
        {
            return Operators.IsUnaryPreOperator(tokens, i);
        }

        public static UnaryPreOperatorNode Parse(Token[] tokens, ref int i)
        {
            UnaryPreOperatorType opType = Operators.UnaryPreOperatorTokenTable[tokens[i].Type];
            int opPrecedence = Operators.UnaryPreOperatorPrecedenceTable[opType];
            i++; //Consume operator token
            ISubExpressionNode operand = ExpressionNode.ParseQ(tokens, ref i, opPrecedence);
            return new UnaryPreOperatorNode(opType, operand, i - 1, tokens[i - 1]);
        }

        public override ILTypeSpecifier GetResultType(ILGenerationContext context)
        {
            ILTypeSpecifier operandType = Operand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<UnaryPreOperatorType>, Func<ILTypeSpecifier, ILTypeSpecifier>>
            {
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.DoubleMinus, UnaryPreOperatorType.DoublePlus, UnaryPreOperatorType.Minus, UnaryPreOperatorType.Tilde }, opType =>
                    {
                        switch (opType.Category)
                        {
                            case ILTypeCategory.Base:
                                return opType;
                            case ILTypeCategory.Enum:
                                return opType;
                            case ILTypeCategory.Struct:
                                throw new ParserException(string.Format("Invalid operation \'{0}\' on type struct.", OperatorType.ToString()), TokenIndex, Token);
                            default:
                                throw new InternalCompilerException("Unexpected operator type in type computation.");
                        }
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Ampersand }, opType =>
                    {
                        return new ILTypeSpecifier(opType.Name, opType.Category, opType.PointerDepth + 1);
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Star }, opType =>
                    {
                        if (opType.PointerDepth <= 0)
                            throw new ParserException("Cannot dereference non-pointer type.", TokenIndex, Token);
                        return new ILTypeSpecifier(opType.Name, opType.Category, opType.PointerDepth - 1);
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Exclamation }, opType =>
                    {
                        if (opType.Category != ILTypeCategory.Base)
                            throw new ParserException("Unable to apply negation operator to non-integral type.", TokenIndex, Token);
                        BaseType baseType = Types.GetBaseTypeByName(opType.Name);
                        if (!Types.IsIntegralType(baseType))
                            throw new ParserException("Unable to apply negation operator to non-integral type.", TokenIndex, Token);
                        return opType;
                    }
                }
            };
            return handlers.First(n => n.Key.Contains(OperatorType)).Value(operandType);
        }

        public override void GenerateIL(ILGenerationContext context, List<IILInstruction> output)
        {
            throw new NotImplementedException();
        }
    }
}
