using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST.UnaryPostOperators
{
    public abstract class UnaryPostOperatorNode : OperatorNode
    {
        public UnaryPostOperatorType OperatorType;
        public SubExpressionNode Operand;

        protected UnaryPostOperatorNode(UnaryPostOperatorType operatorType, SubExpressionNode operand, Token token, int tokenIndex) : base(token, tokenIndex)
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
    }
}
