using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class FunctionCallNode : ISubExpressionNode
    {
        public ExpressionNode[] Arguments
        {
            get;
            set;
        }
        public IdentifierNode Identifier
        {
            get;
            set;
        }

        public FunctionCallNode(IdentifierNode identifier, ExpressionNode[] arguments)
        {
            Arguments = arguments;
            Identifier = identifier;
        }

        public static FunctionCallNode Parse(Token[] tokens, ref int i)
        {
            IdentifierNode identifier = IdentifierNode.Parse(tokens, ref i);
            if (tokens[i].Type != TokenType.LeftParen)
            {
                throw new InternalCompilerException("Attempted to parse FunctionCallNode without opening paren.");
            }
            i++; //Pass over opening paren
            List<ExpressionNode> args = new List<ExpressionNode>();
            while (i < tokens.Length && tokens[i].Type != TokenType.RightParen)
            {
                ExpressionNode arg = ExpressionNode.Parse(tokens, ref i);
                args.Add(arg);
                if (tokens[i].Type != TokenType.Comma && tokens[i].Type != TokenType.RightParen)
                {
                    throw new ParserException("Expected delimiter or closing paren.", i, tokens[i]);
                }
                if (tokens[i].Type == TokenType.Comma)
                {
                    i++;
                }
            }
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            return new FunctionCallNode(identifier, args.ToArray());
        }

        public static bool IsFunctionCall(Token[] tokens, int i)
        {
            if (tokens[i].Type != TokenType.AlphaNum || Keywords.IsKeyword(tokens[i].Literal))
            {
                return false;
            }
            for (; i < tokens.Length && !tokens[i].WhitespaceBefore && (tokens[i].Type == TokenType.AlphaNum || tokens[i].Type == TokenType.Dot || tokens[i].Type == TokenType.Arrow) && !Keywords.IsKeyword(tokens[i].Literal); i++) ;
            if (i >= tokens.Length && tokens[i].Type != TokenType.LeftParen)
            {
                return false; //It's a variable
            }
            else
            {
                return true; //It's a function call
            }
        }
    }
}
