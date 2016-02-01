using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class BinaryOperatorNode : OperatorNode
    {
        public BinaryOperatorType OperatorType
        {
            get;
            set;
        }
        public ISubExpressionNode LeftOperand
        {
            get;
            set;
        }
        public ISubExpressionNode RightOperand
        {
            get;
            set;
        }

        public BinaryOperatorNode(BinaryOperatorType operatorType, ISubExpressionNode leftOperand, ISubExpressionNode rightOperand)
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
    }
}
