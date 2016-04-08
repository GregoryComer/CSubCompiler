using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.BinaryOperators
{
    public abstract class BinaryOperatorNode : OperatorNode
    {
        public SubExpressionNode Left { get; set; }
        public SubExpressionNode Right { get; set; }
        public abstract BinaryOperatorType OperatorType { get; }

        protected BinaryOperatorNode(SubExpressionNode left, SubExpressionNode right, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return OperatorType.ToString();
        }

        public static bool IsBinaryOperator(Token[] tokens, int i)
        {
            return Operators.IsBinaryOperator(tokens, i);
        }

        #region Util
        protected BaseType Promote(BaseType a, BaseType b)
        {
            if (a == BaseType.Double || b == BaseType.Double)
                return BaseType.Double;
            if (a == BaseType.Float || b == BaseType.Float)
                return BaseType.Float;
            if (a == BaseType.ULong || b == BaseType.ULong)
                return BaseType.ULong;
            int aSize = Types.GetBaseTypeSize(a);
            int bSize = Types.GetBaseTypeSize(b);
            if (Types.IsUnsigned(a) && Types.IsSigned(b))
            {
                if (aSize < bSize)
                    return b;
                else
                    Types.GetIntegralTypeBySize(Math.Max(aSize, bSize), false);
            }
            if (Types.IsSigned(a) && Types.IsUnsigned(b))
            {
                if (bSize < aSize)
                    return a;
                else
                    return Types.GetIntegralTypeBySize(Math.Max(aSize, bSize), false);
            }
            else
            {
                bool isSigned = Types.IsSigned(a);
                return Types.GetIntegralTypeBySize(Math.Max(aSize, bSize), isSigned);
            }
        } 
        #endregion
    }
}
