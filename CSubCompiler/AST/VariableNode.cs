using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class VariableNode : SubExpressionNode
    {
        public IdentifierNode Identifier
        {
            get;
            set;
        }

        public VariableNode(IdentifierNode identifier, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return Identifier.ToString();
        }

        public static VariableNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            IdentifierNode identifier = IdentifierNode.Parse(tokens, ref i);
            return new VariableNode(identifier, startToken, startIndex);
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

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            throw new NotImplementedException();
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
