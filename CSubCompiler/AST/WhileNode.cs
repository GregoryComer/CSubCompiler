using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class WhileNode : StatementNode
    {
        public ExpressionNode Condition
        {
            get;
            set;
        }
        public BlockNode Body
        {
            get;
            set;
        }

        public WhileNode(ExpressionNode condition, BlockNode body, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Condition = condition;
            Body = body;
        }

        public static bool IsWhile(Token[] tokens, int i)
        {
            return tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "while";
        }

        public static new WhileNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "while");
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            ExpressionNode condition = ExpressionNode.Parse(tokens, ref i);
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            BlockNode whileBlock = BlockNode.Parse(tokens, ref i);
            return new WhileNode(condition, whileBlock, startToken, startIndex);
        }
    }
}
