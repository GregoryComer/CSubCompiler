using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class WhileNode : Node
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

        public WhileNode(ExpressionNode condition, BlockNode body)
        {
            Condition = condition;
            Body = body;
        }

        public static bool IsWhile(Token[] tokens, int i)
        {
            return tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "while";
        }

        public static WhileNode ParseWhile(Token[] tokens, ref int i)
        {
            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "while");
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            ExpressionNode condition = ExpressionNode.Parse(tokens, ref i);
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            BlockNode whileBlock = BlockNode.Parse(tokens, ref i);
            return new WhileNode(condition, whileBlock);
        }
    }
}
