using System;
using System.Collections.Generic;
using System.Linq;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPostOperators
{
    /*public class UnaryPostOperatorNodeOld : OperatorNode
    {
        public UnaryPostOperatorType OperatorType;
        public SubExpressionNode Operand;

        public UnaryPostOperatorNodeOld(UnaryPostOperatorType operatorType, SubExpressionNode operand, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            OperatorType = operatorType;
            Operand = operand;
        }

        public override string ToString()
        {
            return OperatorType.ToString();
        }

        public static bool IsUnaryPostOperator(Token[] tokens, int i)
        {
            return Operators.IsUnaryPostOperator(tokens, i);
        }

        public static UnaryPostOperatorNodeOld ParseWithOperand(Token[] tokens, ref int i, SubExpressionNode operand)
        {
            UnaryPostOperatorType opType = Operators.UnaryPostOperatorTokenTable[tokens[i].Type];
            int opPrecedence = Operators.UnaryPostOperatorPrecedenceTable[opType];
            i++; //Consume operator token
            return new UnaryPostOperatorNodeOld(opType, operand, tokens[i - 1], i - 1);
        }

        public override void GenerateIL(ILGenerationContext context)
        {
            Operand.GenerateIL(context);
            base.GenerateIL(context);
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            ILType opType = Operand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<UnaryPostOperatorType>, Action<ILGenerationContext, ILType>>
            {
                { new UnaryPostOperatorType[] { UnaryPostOperatorType.DoubleMinus }, GenerateIL_DoubleMinus },
                { new UnaryPostOperatorType[] { UnaryPostOperatorType.DoublePlus }, GenerateIL_DoublePlus }
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
                        context.Output.Write(new ILLoadF { Address = new ILAddressingReference() });
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
                        context.Output.Write(new ILLoadF { Address = ILAddressingReference.CreateConstant(1.0f), Size = (FloatOperandSize)Types.GetBaseTypeSize(baseType.Type) });
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
        #endregion

        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType operandType = Operand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<UnaryPostOperatorType>, Func<ILType, ILType>>
            {
                { new UnaryPostOperatorType[] { UnaryPostOperatorType.DoubleMinus, UnaryPostOperatorType.DoublePlus }, opType =>
                    {
                        switch (opType.Category)
                        {
                            case ILTypeCategory.Base:
                            case ILTypeCategory.Pointer:
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
    }*/
}