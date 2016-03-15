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
        public SubExpressionNode Operand;

        public UnaryPreOperatorNode(UnaryPreOperatorType operatorType, SubExpressionNode operand, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            OperatorType = operatorType;
            Operand = operand;
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
            SubExpressionNode operand = ExpressionNode.ParseQ(tokens, ref i, opPrecedence);
            return new UnaryPreOperatorNode(opType, operand, tokens[i - 1], i - 1);
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<UnaryPreOperatorType>, Func<ILType, ILType>>
            {
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.DoubleMinus, UnaryPreOperatorType.DoublePlus }, opType =>
                    {
                        switch (opType.Category)
                        {
                            case ILTypeCategory.Base:
                            case ILTypeCategory.Pointer:
                                return opType;
                            case ILTypeCategory.Struct:
                                throw new ParserException(string.Format("Invalid operation \'{0}\' on type struct.", OperatorType.ToString()), TokenIndex, Token);
                            default:
                                throw new InternalCompilerException("Unexpected operator type in type computation.");
                        }
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Minus, UnaryPreOperatorType.Tilde }, opType =>
                    {
                        switch (opType.Category)
                        {
                            case ILTypeCategory.Base:
                                return opType;
                            case ILTypeCategory.Pointer:
                                throw new ParserException(string.Format("Invalid operation \'{0}\' on pointer type.", OperatorType.ToString()), TokenIndex, Token);
                            case ILTypeCategory.Struct:
                                throw new ParserException(string.Format("Invalid operation \'{0}\' on struct type.", OperatorType.ToString()), TokenIndex, Token);
                            default:
                                throw new InternalCompilerException("Unexpected operator type in type computation.");
                        }
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Ampersand }, opType =>
                    {
                        return new ILPointerType(opType);
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Star }, opType =>
                    {
                        if (opType.Category != ILTypeCategory.Pointer)
                            throw new ParserException("Cannot dereference non-pointer type.", TokenIndex, Token);
                        return (opType as ILPointerType).Type;
                    }
                },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Exclamation }, opType =>
                    {
                        if (opType.Category != ILTypeCategory.Base)
                            throw new ParserException("Unable to apply negation operator to non-integral type.", TokenIndex, Token);
                        if (!Types.IsIntegralType((opType as ILBaseType).Type))
                            throw new ParserException("Unable to apply negation operator to non-integral type.", TokenIndex, Token);
                        return opType;
                    }
                }
            };
            return handlers.First(n => n.Key.Contains(OperatorType)).Value(operandType);
        }

        public override void GenerateIL(ILGenerationContext context)
        {
            Operand.GenerateIL(context);
            base.GenerateIL(context);
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            ILType opType = Operand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<UnaryPreOperatorType>, Action<ILGenerationContext, ILType>>
            {
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Ampersand }, (a, b) => { } },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.DoubleMinus }, GenerateIL_DoubleMinus },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.DoublePlus }, GenerateIL_DoublePlus },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Exclamation }, GenerateIL_Exclamation },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Minus }, GenerateIL_Minus },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Star }, (a, b) => { } },
                { new UnaryPreOperatorType[] { UnaryPreOperatorType.Tilde }, GenerateIL_Tilde }
            };
            handlers.First(n => n.Key.Contains(OperatorType)).Value(context, opType);
        }

        #region IL Generation Methods
        private void GenerateIL_DoublePlus(ILGenerationContext context, ILType opType)
        {
            switch (opType.Category)
            {
                case ILTypeCategory.Base:
                    ILBaseType baseType = (ILBaseType)opType;
                    if (Types.IsIntegralType(baseType.Type))
                    {
                        context.Output.Write(new ILInc { OperandSize = (GeneralOperandSize)Types.GetBaseTypeSize(baseType.Type) });
                    }
                    else if (Types.IsFloatType(baseType.Type))
                    {
                        context.Output.Write(new ILLoadCF { Constant = 1.0f });
                        context.Output.Write(new ILAddF { });
                    }
                    else
                    {
                        throw new InternalCompilerException("Unexpected type.");
                    }
                    break;
                case ILTypeCategory.Pointer:
                    context.Output.Write(new ILInc { OperandSize = (GeneralOperandSize)Types.GetPointerSize() });
                    break;
                case ILTypeCategory.Struct:
                    throw new ParserException(string.Format("Invalid operation {0} on type struct.", OperatorType.ToString()), TokenIndex, Token);
                default:
                    throw new InternalCompilerException("Unexpected operator type in IL generation.");
            }
        }
        private void GenerateIL_DoubleMinus(ILGenerationContext context, ILType opType)
        {
            switch (opType.Category)
            {
                case ILTypeCategory.Base:
                    ILBaseType baseType = (ILBaseType)opType;
                    if (Types.IsIntegralType(baseType.Type))
                    {
                        context.Output.Write(new ILInc { OperandSize = (GeneralOperandSize)Types.GetBaseTypeSize(baseType.Type) });
                    }
                    else if (Types.IsFloatType(baseType.Type))
                    {
                        context.Output.Write(new ILLoadCF { Constant = 1.0f });
                        context.Output.Write(new ILAddF { });
                    }
                    else
                    {
                        throw new InternalCompilerException("Unexpected type.");
                    }
                    break;
                case ILTypeCategory.Pointer:
                    context.Output.Write(new ILInc { OperandSize = (GeneralOperandSize)Types.GetPointerSize() });
                    break;
                case ILTypeCategory.Struct:
                    throw new ParserException(string.Format("Invalid operation {0} on type struct.", OperatorType.ToString()), TokenIndex, Token);
                default:
                    throw new InternalCompilerException("Unexpected operator type in IL generation.");
            }
        }
        private void GenerateIL_Exclamation(ILGenerationContext context, ILType opType)
        {
            switch (opType.Category)
            {
                case ILTypeCategory.Base:
                    ILBaseType baseType = (ILBaseType)opType;
                    if (Types.IsIntegralType(baseType.Type))
                    {
                        context.Output.Write(new ILNBool { OperandSize = (GeneralOperandSize)Types.GetBaseTypeSize(baseType.Type) });
                    }
                    else if (Types.IsFloatType(baseType.Type))
                    {
                        throw new ParserException(string.Format("Invalid operation {0} on floating point type.", OperatorType.ToString()), TokenIndex, Token);
                    }
                    else
                    {
                        throw new InternalCompilerException("Unexpected type.");
                    }
                    break;
                case ILTypeCategory.Pointer:
                    throw new ParserException(string.Format("Invalid operation {0} on pointer type.", OperatorType.ToString()), TokenIndex, Token);
                case ILTypeCategory.Struct:
                    throw new ParserException(string.Format("Invalid operation {0} on struct type.", OperatorType.ToString()), TokenIndex, Token);
                default:
                    throw new InternalCompilerException("Unexpected operator type in IL generation.");
            }
        }
        private void GenerateIL_Minus(ILGenerationContext context, ILType opType)
        {
            switch (opType.Category)
            {
                case ILTypeCategory.Base:
                    ILBaseType baseType = (ILBaseType)opType;
                    if (Types.IsIntegralType(baseType.Type))
                    {
                        context.Output.Write(new ILNeg { OperandSize = (GeneralOperandSize)Types.GetBaseTypeSize(baseType.Type) });
                    }
                    else if (Types.IsFloatType(baseType.Type))
                    {
                        context.Output.Write(new ILNegF { });
                    }
                    else
                    {
                        throw new InternalCompilerException("Unexpected type.");
                    }
                    break;
                case ILTypeCategory.Pointer:
                    throw new ParserException(string.Format("Invalid operation {0} on pointer type.", OperatorType.ToString()), TokenIndex, Token);
                case ILTypeCategory.Struct:
                    throw new ParserException(string.Format("Invalid operation {0} on type struct.", OperatorType.ToString()), TokenIndex, Token);
                default:
                    throw new InternalCompilerException("Unexpected operator type in IL generation.");
            }
        }
        private void GenerateIL_Tilde(ILGenerationContext context, ILType opType)
        {
            switch (opType.Category)
            {
                case ILTypeCategory.Base:
                    ILBaseType baseType = (ILBaseType)opType;
                    if (Types.IsIntegralType(baseType.Type))
                    {
                        context.Output.Write(new ILNot { OperandSize = (GeneralOperandSize)Types.GetBaseTypeSize(baseType.Type) });
                    }
                    else if (Types.IsFloatType(baseType.Type))
                    {
                        throw new ParserException(string.Format("Invalid operation {0} on floating point type.", OperatorType.ToString()), TokenIndex, Token);
                    }
                    else
                    {
                        throw new InternalCompilerException("Unexpected type.");
                    }
                    break;
                case ILTypeCategory.Pointer:
                    throw new ParserException(string.Format("Invalid operation {0} on pointer type.", OperatorType.ToString()), TokenIndex, Token);
                case ILTypeCategory.Struct:
                    throw new ParserException(string.Format("Invalid operation {0} on type struct.", OperatorType.ToString()), TokenIndex, Token);
                default:
                    throw new InternalCompilerException("Unexpected operator type in IL generation.");
            }
        }
        #endregion
    }
}
