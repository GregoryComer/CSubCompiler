using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class UnaryPostOperatorNode : OperatorNode
    {
        public UnaryPostOperatorType OperatorType;
        public ISubExpressionNode Operand;

        public UnaryPostOperatorNode(UnaryPostOperatorType operatorType, ISubExpressionNode operand)
        {
            OperatorType = operatorType;
            Operand = operand;
        }

        public override string ToString()
        {
            return OperatorType.ToString();
        }
    }
}