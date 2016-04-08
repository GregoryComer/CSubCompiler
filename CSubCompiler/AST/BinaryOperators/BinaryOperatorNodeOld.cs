using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.BinaryOperators
{
    /*public class BinaryOperatorNodeOld : OperatorNode
    {
        public BinaryOperatorType OperatorType
        {
            get;
            set;
        }
        public SubExpressionNode LeftOperand
        {
            get;
            set;
        }
        public SubExpressionNode RightOperand
        {
            get;
            set;
        }

        public BinaryOperatorNodeOld(BinaryOperatorType operatorType, SubExpressionNode leftOperand, SubExpressionNode rightOperand, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            OperatorType = operatorType;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public override string ToString()
        {
            return OperatorType.ToString();
        }

        public static bool IsBinaryOperator(Token[] tokens, int i)
        {
            return Operators.IsBinaryOperator(tokens, i);
        }

        #region IL Generation
        public override ILType GetResultType(ILGenerationContext context)
        {
            ILType leftOperandType = LeftOperand.GetResultType(context);
            ILType rightOperandType = RightOperand.GetResultType(context);
            Sort(ref leftOperandType, ref rightOperandType); //Putting the input types in a specific order simplifies code below (Sort Order: Pointer, Integral, Float)
            var handlers = new Dictionary<IEnumerable<BinaryOperatorType>, Func<ILType>>
            {
                //Integral Types Only
                {new BinaryOperatorType[] { BinaryOperatorType.Ampersand, BinaryOperatorType.AmpersandEqual, BinaryOperatorType.Caret, BinaryOperatorType.CaretEqual, BinaryOperatorType.DoubleAmpersand, BinaryOperatorType.DoublePipe, BinaryOperatorType.Mod, BinaryOperatorType.ModEqual, BinaryOperatorType.Pipe, BinaryOperatorType.PipeEqual, BinaryOperatorType.ShiftLeft, BinaryOperatorType.ShiftLeftEqual, BinaryOperatorType.ShiftRight, BinaryOperatorType.ShiftRightEqual }, () =>
                    {
                        if ((leftOperandType.Category != ILTypeCategory.Base) || (rightOperandType.Category != ILTypeCategory.Base))
                            throw new ParserException(string.Format("Unable to apply operator {0} to non-integral type.", OperatorType), TokenIndex, Token);
                        BaseType leftBaseType = (leftOperandType as ILBaseType).Type;
                        BaseType rightBaseType = (rightOperandType as ILBaseType).Type;
                        if (!Types.IsIntegralType(leftBaseType) || !Types.IsIntegralType(rightBaseType))
                            throw new ParserException(string.Format("Unable to apply operator {0} to non-integral type.", OperatorType), TokenIndex, Token);
                        int opSizeLeft = Types.GetBaseTypeSize(leftBaseType);
                        int opSizeRight = Types.GetBaseTypeSize(rightBaseType);
                        int resultSize = Math.Max(opSizeLeft, opSizeRight);
                        BaseType resultType = Types.GetIntegralTypeBySize(resultSize, false);
                        return new ILBaseType(resultType);
                    }
                },
                //Integral, Float Types
                { new BinaryOperatorType[] { BinaryOperatorType.Divide, BinaryOperatorType.DivideEqual, BinaryOperatorType.DoubleEqual, BinaryOperatorType.Equal, BinaryOperatorType.Greater, BinaryOperatorType.GreaterEqual, BinaryOperatorType.Less, BinaryOperatorType.LessEqual, BinaryOperatorType.NotEqual, BinaryOperatorType.Star, BinaryOperatorType.StarEqual }, () =>
                    {
                        if ((leftOperandType.Category != ILTypeCategory.Base) || (rightOperandType.Category != ILTypeCategory.Base))
                            throw new ParserException(string.Format("Unable to apply operator {0} to non-integral type.", OperatorType), TokenIndex, Token);
                        return GetBaseTypeResultType(leftOperandType,rightOperandType);
                    }
                },
                //Integral, Float, Pointer Types
                { new BinaryOperatorType[] { BinaryOperatorType.Minus, BinaryOperatorType.MinusEqual, BinaryOperatorType.Plus, BinaryOperatorType.PlusEqual }, () =>
                    {
                        return GetBaseTypeResultType(leftOperandType, rightOperandType);
                    }
                }
            };
            return handlers.First(n => n.Key.Contains(OperatorType)).Value();
        }

        private ILType GetBaseTypeResultType(ILType leftType, ILType rightType) //Assumes args are sorted
        {
            BaseType leftBaseType = leftType.GetBaseType();
            BaseType rightBaseType = rightType.GetBaseType();
            if (leftBaseType != BaseType.Pointer)
            {
                if (Types.IsIntegralType(leftBaseType) && Types.IsIntegralType(rightBaseType)) //Both Integral Types
                {
                    int opSizeLeft = Types.GetBaseTypeSize(leftBaseType);
                    int opSizeRight = Types.GetBaseTypeSize(rightBaseType);
                    int resultSize = Math.Max(opSizeLeft, opSizeRight);
                    BaseType resultType = Types.GetIntegralTypeBySize(resultSize, false);
                    return new ILBaseType(resultType);
                }
                else if (Types.IsIntegralType(leftBaseType) && Types.IsFloatType(rightBaseType)) //Mixed Types
                {
                    int floatSize = 0;
                    if (Types.IsFloatType(leftBaseType))
                        floatSize = Types.GetBaseTypeSize(leftBaseType);
                    else
                        floatSize = Types.GetBaseTypeSize(rightBaseType);
                    BaseType resultType = Types.GetFloatTypeBySize(floatSize);
                    return new ILBaseType(resultType);
                }
                else if (Types.IsFloatType(leftBaseType) && Types.IsFloatType(rightBaseType)) //Both Float Types
                {
                    int opSizeLeft = Types.GetBaseTypeSize(leftBaseType);
                    int opSizeRight = Types.GetBaseTypeSize(rightBaseType);
                    int resultSize = Math.Max(opSizeLeft, opSizeRight);
                    BaseType resultType = Types.GetIntegralTypeBySize(resultSize, false);
                    return new ILBaseType(resultType);
                }
                else
                {
                    throw new InternalCompilerException("Unexpected combination of base types.");
                }
            }
            else //leftBaseType == BaseType.Pointer
            {
                if (Types.IsFloatType(rightBaseType))
                    throw new ParserException(string.Format("Cannot apply operator {0} to Pointer and {1}.", OperatorType, rightBaseType), TokenIndex, Token);
                return leftType;
            }
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            RightOperand.GenerateIL(context);
            LeftOperand.GenerateIL(context);
            switch (OperatorType)
            {
                case BinaryOperatorType.Ampersand:
                    break;
                case BinaryOperatorType.Caret:
                    break;
                case BinaryOperatorType.Divide:
                    break;
                case BinaryOperatorType.Minus:
                    break;
                case BinaryOperatorType.Mod:
                    break;
                case BinaryOperatorType.Pipe:
                    break;
                case BinaryOperatorType.Plus:
                    break;
                case BinaryOperatorType.ShiftLeft:
                    break;
                case BinaryOperatorType.ShiftRight:
                    break;
                case BinaryOperatorType.Star:
                    break;
            }
        }

        private BaseType GetBaseTypeFromILType(ILType typeSpec)
        {
            switch (typeSpec.Category)
            {
                case ILTypeCategory.Base:
                    return (typeSpec as ILBaseType).Type;
                case ILTypeCategory.Pointer:
                    return BaseType.Pointer;
                case ILTypeCategory.Struct:
                    throw new InternalCompilerException("Attempt to retrieve base type of struct.");
                default:
                    throw new InternalCompilerException("Attempt to retrieve base type of unknown type category.");
            }
        }

        private void Sort(ref ILType a, ref ILType b)
        {
            int aPriority = (a.Category == ILTypeCategory.Pointer) ? 0 :
                (a.Category != ILTypeCategory.Base) ? 3 :
                (Types.IsFloatType((a as ILBaseType).Type)) ? 2 :
                (Types.IsIntegralType((a as ILBaseType).Type)) ? 1 : 3;
            int bPriority = (b.Category == ILTypeCategory.Pointer) ? 0 :
                (b.Category != ILTypeCategory.Base) ? 3 :
                (Types.IsFloatType((b as ILBaseType).Type)) ? 2 :
                (Types.IsIntegralType((b as ILBaseType).Type)) ? 1 : 3;
            if (bPriority < aPriority)
                Swap(ref a, ref b);
        }
        private void Swap(ref ILType a, ref ILType b)
        {
            ILType buffer = a;
            a = b;
            b = buffer;
        }
        #endregion
    }*/
}
