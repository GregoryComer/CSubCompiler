using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class ExpressionNode : SubExpressionNode
    {
        SubExpressionNode Value;

        public ExpressionNode(SubExpressionNode value, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Value = value;
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            Value.GenerateIL(context);
        }

        public static ExpressionNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            SubExpressionNode exp = ParseQ(tokens, ref i, Operators.MinPrecedence);
            return new ExpressionNode(exp, startToken, startIndex);
        }

        public static SubExpressionNode ParseQ(Token[] tokens, ref int i, int q) //Predence climbing algorithm (q represents minimum precedence handled by this loop)
        {
            SubExpressionNode val = ParseSubExpression(tokens, ref i, q);
            while (i < tokens.Length && (BinaryOperatorNode.IsBinaryOperator(tokens, i) || UnaryPostOperatorNode.IsUnaryPostOperator(tokens, i)))
            {
                if (BinaryOperatorNode.IsBinaryOperator(tokens, i))
                {
                    BinaryOperatorType opType = Operators.BinaryOperatorTokenTable[tokens[i].Type];
                    OperatorAssociativity opAssoc = Operators.BinaryOperatorAssociativityTable[opType];
                    int opPrecedence = Operators.BinaryOperatorPrecedenceTable[opType];
                    if (opPrecedence <= q) //Lower precedence values indicate higher precedence
                    {
                        i++; //Consume operator
                        SubExpressionNode right = ParseQ(tokens, ref i, (opAssoc == OperatorAssociativity.Left) ? opPrecedence - 1 : opPrecedence);
                        if (opAssoc == OperatorAssociativity.Left)
                        {
                            val = new BinaryOperatorNode(opType, val, right, tokens[i], i);
                        }
                        else //Right associative
                        {
                            val = new BinaryOperatorNode(opType, val, right, tokens[i], i); //TEMPORARY, TODO: DETERMINE IF WORKS
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

        public static SubExpressionNode ParseSubExpression(Token[] tokens, ref int i, int q)
        {
            SubExpressionNode exp;
            if (CastNode.IsCast(tokens, i))
            {
                exp = CastNode.Parse(tokens, ref i);
            }
            else if (tokens[i].Type == TokenType.LeftParen) //Expression in parenthesis
            {
                Parser.Expect(tokens, ref i, TokenType.LeftParen);
                exp = Parse(tokens, ref i);
                Parser.Expect(tokens, ref i, TokenType.RightParen);
            }
            else if (tokens[i].Type == TokenType.Int) //Int literals
            {
                exp = IntLiteralNode.Parse(tokens, ref i);
            }
            else if (tokens[i].Type == TokenType.Float)
            {
                exp = FloatLiteralNode.Parse(tokens, ref i);
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

            if (Parser.CheckBoundsNoThrow(tokens, i) && UnaryPostOperatorNode.IsUnaryPostOperator(tokens, i))
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

        public static bool IsSubExpression(Token[] tokens, int i)
        {
            if (tokens[i].Type == TokenType.LeftParen) //Expression in parenthesis
            {
                return true; //TODO: UPDATE TO HANDLE CASTS
            }
            else if (tokens[i].Type == TokenType.Int) //Int literals
            {
                return true;
            }
            else if (tokens[i].Type == TokenType.Float)
            {
                return true;
            }
            else if (VariableNode.IsVariable(tokens, i))
            {
                return true;
            }
            else if (FunctionCallNode.IsFunctionCall(tokens, i))
            {
                return true;
            }
            else if (UnaryPreOperatorNode.IsUnaryPreOperator(tokens, i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public override ILType GetResultType(ILGenerationContext context)
        {
            return Value.GetResultType(context);
        }
    }
}
