using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPreOperators
{
    public abstract class UnaryPreOperatorNode : OperatorNode
    {
        public UnaryPreOperatorType OperatorType;
        public SubExpressionNode Operand;

        protected UnaryPreOperatorNode(UnaryPreOperatorType operatorType, SubExpressionNode operand, Token token, int tokenIndex) : base(token, tokenIndex)
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
    }
}
