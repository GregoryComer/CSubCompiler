using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class BreakNode : StatementNode
    {
        public BreakNode(Token token, int tokenIndex) : base(token, tokenIndex)
        {

        }

        public static bool IsBreak(Token[] tokens, int i)
        {
            return tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "break";
        }

        public static new BreakNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "break");
            return new BreakNode(startToken, startIndex);
        }
    }
}
