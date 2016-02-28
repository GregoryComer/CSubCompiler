using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
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

        #region IL Generation
        public override ILTypeSpecifier GetResultType(ILGenerationContext context)
        {
            ILTypeSpecifier leftOperandType = LeftOperand.GetResultType(context);
            ILTypeSpecifier rightOperandType = RightOperand.GetResultType(context);
            var handlers = new Dictionary<IEnumerable<BinaryOperatorType>, Func<ILTypeSpecifier>>
            {
                
            };
            return handlers.First(n => n.Key.Contains(OperatorType)).Value();
        }

        public override void GenerateIL(ILGenerationContext context, List<IILInstruction> output)
        {
            RightOperand.GenerateIL(context, output);
            LeftOperand.GenerateIL(context, output);
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
        #endregion
    }
}
