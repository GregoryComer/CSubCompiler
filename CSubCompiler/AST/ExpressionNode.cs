using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
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

        public void GenerateIL(ILGenerationContext context, List<IILInstruction> output)
        {
            Value.GenerateIL(context, output);
        }

        public static ExpressionNode Parse(Token[] tokens, ref int i)
        {
            ISubExpressionNode exp = ParseQ(tokens, ref i, Operators.MinPrecedence);
            return new ExpressionNode(exp);
        }

        public static ISubExpressionNode ParseQ(Token[] tokens, ref int i, int q) //Predence climbing algorithm (q represents minimum precedence handled by this loop)
        {
            ISubExpressionNode val = ParseSubExpression(tokens, ref i, q);
            while (i < tokens.Length && (BinaryOperatorNode.IsBinaryOperator(tokens, i)) || UnaryPostOperatorNode.IsUnaryPostOperator(tokens, i))
            {
                if (BinaryOperatorNode.IsBinaryOperator(tokens, i))
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
                            val = new BinaryOperatorNode(opType, val, right, i, tokens[i]);
                        }
                        else //Right associative
                        {
                            val = new BinaryOperatorNode(opType, val, right, i, tokens[i]); //TEMPORARY, TODO: DETERMINE IF WORKS
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else if (UnaryPostOperatorNode.IsUnaryPostOperator(tokens, i))
                {
                    var opType = Operators.UnaryPostOperatorTokenTable[tokens[i].Type];
                    int opPrecedence = Operators.UnaryPostOperatorPrecedenceTable[opType];
                    var opAssoc = Operators.UnaryPostOperatorAssociativityTable[opType];
                    
                    if (opPrecedence <= q || (opPrecedence == q && opAssoc == OperatorAssociativity.Left))
                    {
                        val = UnaryPostOperatorNode.ParseWithOperand(tokens, ref i, val);
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
            ISubExpressionNode exp;
            if (tokens[i].Type == TokenType.LeftParen) //Expression in parenthesis
            {
                exp = Parse(tokens, ref i);
                Parser.Expect(tokens, ref i, TokenType.RightParen);
            }
            else if (tokens[i].Type == TokenType.Int) //Int literals
            {
                exp = IntLiteralNode.Parse(tokens, ref i);
            }
            else if (VariableNode.IsVariable(tokens, i))
            {
                exp = VariableNode.Parse(tokens, ref i);
            }
            else if (FunctionCallNode.IsFunctionCall(tokens, i))
            {
                exp = FunctionCallNode.Parse(tokens, ref i);
            }
            else if (UnaryPreOperatorNode.IsUnaryPreOperator(tokens, i))
            {
                exp = UnaryPreOperatorNode.Parse(tokens, ref i);
            }
            else
            {
                throw new ParserException("Unexpected token.", i, tokens[i]);
            }

            if (UnaryPostOperatorNode.IsUnaryPostOperator(tokens, i))
            {
                var opType = Operators.UnaryPostOperatorTokenTable[tokens[i].Type];
                int opPrecedence = Operators.UnaryPostOperatorPrecedenceTable[opType];
                var opAssoc = Operators.UnaryPostOperatorAssociativityTable[opType];

                if (opPrecedence <= q || (opPrecedence == q && opAssoc == OperatorAssociativity.Left))
                {
                    exp = UnaryPostOperatorNode.ParseWithOperand(tokens, ref i, exp);
                }
            }

            return exp;
        }

        public ILTypeSpecifier GetResultType(ILGenerationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
