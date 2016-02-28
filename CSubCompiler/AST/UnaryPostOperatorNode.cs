using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class UnaryPostOperatorNode : OperatorNode
    {
        public UnaryPostOperatorType OperatorType;
        public ISubExpressionNode Operand;

        public int TokenIndex;
        public Token Token;

        public UnaryPostOperatorNode(UnaryPostOperatorType operatorType, ISubExpressionNode operand, int tokenIndex, Token token)
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

        public static bool IsUnaryPostOperator(Token[] tokens, int i)
        {
            return Operators.IsUnaryPostOperator(tokens, i);
        }

        public static UnaryPostOperatorNode ParseWithOperand(Token[] tokens, ref int i, ISubExpressionNode operand)
        {
            UnaryPostOperatorType opType = Operators.UnaryPostOperatorTokenTable[tokens[i].Type];
            int opPrecedence = Operators.UnaryPostOperatorPrecedenceTable[opType];
            i++; //Consume operator token
            return new UnaryPostOperatorNode(opType, operand, i - 1, tokens[i - 1]);
        }

        public override void GenerateIL(ILGenerationContext context, List<IILInstruction> output)
        {
            throw new NotImplementedException();
        }

        public override ILTypeSpecifier GetResultType(ILGenerationContext context)
        {
            ILTypeSpecifier operandType = Operand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<UnaryPostOperatorType>, Func<ILTypeSpecifier, ILTypeSpecifier>>
            {
                { new UnaryPostOperatorType[] { UnaryPostOperatorType.DoubleMinus, UnaryPostOperatorType.DoublePlus }, opType =>
                    {
                        switch (opType.Category)
                        {
                            case ILTypeCategory.Base:
                                return opType;
                            case ILTypeCategory.Enum:
                                return opType;
                            case ILTypeCategory.Struct:
                                throw new ParserException(string.Format("Invalid operation {0} on type struct.", OperatorType.ToString()), TokenIndex, Token);
                            default:
                                throw new InternalCompilerException("Unexpected operator type in type computation.");
                        }
                    }
                }
            };
            return handlers.First(n => n.Key.Contains(OperatorType)).Value(operandType);
        }
    }
}