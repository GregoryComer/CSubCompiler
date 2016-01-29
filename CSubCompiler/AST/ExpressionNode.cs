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

        public static ISubExpressionNode ParseQ(Token[] tokens, ref int i, int q)
        {
            ISubExpressionNode val = ParseP(tokens, ref i, q);
            while (i < tokens.Length && BinaryOperatorNode.IsBinaryOperator(tokens, i))
            {
                BinaryOperatorType opType = Operators.BinaryOperatorTokenTable[tokens[i].Type];
                OperatorAssociativity opAssoc = Operators.BinaryOperatorAssociativityTable[opType];
                int opPrecedence = Operators.BinaryOperatorPrecedenceTable[opType];
                if (opPrecedence <= q)
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

        public static ISubExpressionNode ParseP(Token[] tokens, ref int i, int q)
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
                throw new ParserException("Unexpected token.", tokens[i].CodeIndex, i, tokens[i]);
            }
        }

#region Old Code
        /*public static ExpressionNode Parse(Token[] tokens, ref int i)
{
    return new ExpressionNode(ParseInternal(tokens, ref i, Operators.MinPrecedence)); //Placeholder
}

static ISubExpressionNode ParseInternal(Token[] tokens, ref int i, int minPrecedence)
{
    ISubExpressionNode leftOperand = ParseSubexpression(tokens, ref i, minPrecedence);
    if (CSubCompiler.Language.Operators.IsBinaryOperator(tokens, i) && !(CSubCompiler.Language.Operators.IsUnaryPreOperator(tokens, i) && tokens[i + 1].WhitespaceBefore) && Operators.BinaryOperatorPrecedenceTable[Operators.BinaryOperatorTokenTable[tokens[i].Type]] <= minPrecedence)
    {
        BinaryOperatorType op = Operators.BinaryOperatorTokenTable[tokens[i].Type];
        OperatorAssociativity assoc = Operators.BinaryOperatorAssociativityTable[op];
        i++; //Skip over operator
        ISubExpressionNode rightOperand = ParseInternal(tokens, ref i, (assoc == OperatorAssociativity.Left) ? i + 1 : i);
        BinaryOperatorNode opNode = new BinaryOperatorNode(op, leftOperand, rightOperand);
        return opNode;
    }
    else
    {
        return new ExpressionNode(leftOperand);
    }
}

static ISubExpressionNode ParseSubexpression(Token[] tokens, ref int i, int minPrecedence)
{
    if (tokens[i].Type == TokenType.LeftParen) //Recursively parse expression in parenthesis
    {
        return Parse(tokens, ref i);
    }
    else if (FunctionCallNode.IsFunctionCall(tokens, i))
    {
        return FunctionCallNode.Parse(tokens, ref i);
    }
    else if (VariableNode.IsVariable(tokens, i))
    {
        return VariableNode.Parse(tokens, ref i);
    }
    else if (tokens[i].Type == TokenType.String) //String literal
    {
        return StringLiteralNode.Parse(tokens, ref i);
    }
    else if (tokens[i].Type == TokenType.Char) //Char literal
    {
        return CharLiteralNode.Parse(tokens, ref i);
    }
    else if (tokens[i].Type == TokenType.Int)
    {
        return IntLiteralNode.Parse(tokens, ref i);
    }
    else if (tokens[i].Type == TokenType.Float)
    {
        return FloatLiteralNode.Parse(tokens, ref i);
    }
    else
    {
        throw new ParserException("Unexpected token in expression.", tokens[i].CodeIndex, i, tokens[i]);
    }
}*/
#endregion
    }
}
