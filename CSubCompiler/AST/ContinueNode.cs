using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class ContinueNode : StatementNode
    {
        public ContinueNode(Token token, int tokenIndex) : base(token, tokenIndex)
        {

        }

        public static bool IsContinue(Token[] tokens, int i)
        {
            return tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "continue";
        }

        public static new ContinueNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "continue");
            return new ContinueNode(startToken, startIndex);
        }
    }
}
