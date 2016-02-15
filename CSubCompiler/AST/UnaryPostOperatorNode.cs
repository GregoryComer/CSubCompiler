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

        public static bool IsUnaryPostOperator(Token[] tokens, int i)
        {
            return Operators.IsUnaryPostOperator(tokens, i);
        }

        public static UnaryPostOperatorNode ParseWithOperand(Token[] tokens, ref int i, ISubExpressionNode operand)
        {
            UnaryPostOperatorType opType = Operators.UnaryPostOperatorTokenTable[tokens[i].Type];
            int opPrecedence = Operators.UnaryPostOperatorPrecedenceTable[opType];
            i++; //Consume operator token
            return new UnaryPostOperatorNode(opType, operand);
        }
    }
}