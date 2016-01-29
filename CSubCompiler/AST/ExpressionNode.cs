using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class ExpressionNode : ISubExpressionNode
    {
        ISubExpressionNode Value;

        public ExpressionNode(ISubExpressionNode value)
        {
            Value = value;
        }

        public static ExpressionNode Parse(Token[] tokens, ref int i)
        {
            ISubExpressionNode exp = ParseQ(tokens, ref i, Operators.MinPrecedence);
            return new ExpressionNode(exp);
        }

        public static ISubExpressionNode ParseQ(Token[] tokens, ref int i, int q) //Predence climbing algorithm (q represents minimum precedence handled by this loop)
        {
            ISubExpressionNode val = ParseSubExpression(tokens, ref i, q);
            while (i < tokens.Length && BinaryOperatorNode.IsBinaryOperator(tokens, i))
            {
                BinaryOperatorType opType = Operators.BinaryOperatorTokenTable[tokens[i].Type];
                OperatorAssociativity opAssoc = Operators.BinaryOperatorAssociativityTable[opType];
                int opPrecedence = Operators.BinaryOperatorPrecedenceTable[opType];
                if (opPrecedence <= q) //Lower precedence values indicate higher precedence
                {
                    i++; //Consume operator
                    ISubExpressionNode right = ParseQ(tokens, ref i, (opAssoc == OperatorAssociativity.Left) ? opPrecedence - 1 : opPrecedence);
                    if (opAssoc == OperatorAssociativity.Left)
                    {
                        val = new BinaryOperatorNode(opType, val, right);
                    }
                    else //Right associative
                    {
                        val = new BinaryOperatorNode(opType, val, right); //TEMPORARY, TODO: DETERMINE IF WORKS
                    }
                }
                else
                {
                    break; //Todo: Refactor this?
                }
            }
            return val;
        }

        public static ISubExpressionNode ParseSubExpression(Token[] tokens, ref int i, int q)
        {
            if (tokens[i].Type == TokenType.LeftParen) //Expression in parenthesis
            {
                ExpressionNode exp = Parse(tokens, ref i);
                Parser.Expect(tokens, ref i, TokenType.RightParen);
                return exp;
            }
            else if (tokens[i].Type == TokenType.Int) //Int literals
            {
                IntLiteralNode literal = IntLiteralNode.Parse(tokens, ref i);
                return literal;
            }
            else if (VariableNode.IsVariable(tokens, i))
            {
                VariableNode var = VariableNode.Parse(tokens, ref i);
                return var;
            }
            else if (FunctionCallNode.IsFunctionCall(tokens, i))
            {
                FunctionCallNode fcall = FunctionCallNode.Parse(tokens, ref i);
                return fcall;
            }
            else if (UnaryPreOperatorNode.IsUnaryPreOperator(tokens, i))
            {
                UnaryPreOperatorNode unaryPreOp = UnaryPreOperatorNode.Parse(tokens, ref i);
                return unaryPreOp;
            }
            else
            {
                throw new ParserException("Unexpected token.", i, tokens[i]);
            }
        }
    }
}
