using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class VariableNode : ISubExpressionNode
    {
        public IdentifierNode Identifier
        {
            get;
            set;
        }

        public VariableNode(IdentifierNode identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return Identifier.ToString();
        }

        public static VariableNode Parse(Token[] tokens, ref int i)
        {
            IdentifierNode identifier = IdentifierNode.Parse(tokens, ref i);
            return new VariableNode(identifier);
        }

        public static bool IsVariable(Token[] tokens, int i)
        {
            if (tokens[i].Type != TokenType.AlphaNum || Keywords.IsKeyword(tokens[i].Literal))
            {
                return false;
            }
            i++; //Skip past first token
            for (; i < tokens.Length && !tokens[i].WhitespaceBefore && (tokens[i].Type == TokenType.AlphaNum || tokens[i].Type == TokenType.Dot || tokens[i].Type == TokenType.Arrow) && !Keywords.IsKeyword(tokens[i].Literal); i++) ;
            if (i >= tokens.Length || tokens[i].Type != TokenType.LeftParen)
            {
                return true; //It's a variable
            }
            else
            {
                return false; //It's a function call
            }
        }
    }
}
