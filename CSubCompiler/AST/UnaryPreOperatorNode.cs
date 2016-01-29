using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class UnaryPreOperatorNode : OperatorNode
    {
        public UnaryPreOperatorType OperatorType;
        public ISubExpressionNode Operand;

        public UnaryPreOperatorNode(UnaryPreOperatorType operatorType, ISubExpressionNode operand)
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
            ISubExpressionNode operand = ExpressionNode.ParseQ(tokens, ref i, opPrecedence);
            return new UnaryPreOperatorNode(opType, operand);
        }
    }
}
