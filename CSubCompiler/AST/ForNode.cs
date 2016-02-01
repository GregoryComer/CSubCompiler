using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class ForNode : StatementNode
    {
        public StatementNode Initialization //Can be null
        {
            get;
            set;
        }
        public ExpressionNode Condition //Can be null
        {
            get;
            set;
        }
        public StatementNode Afterthought //Can be null
        {
            get;
            set;
        }
        public BlockNode Body
        {
            get;
            set;
        }

        public ForNode(StatementNode initialization, ExpressionNode condition, StatementNode afterthought, BlockNode body)
        {
            Initialization = initialization;
            Condition = condition;
            Afterthought = afterthought;
            Body = body;
        }

        public static bool IsFor(Token[] tokens, int i)
        {
            return tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "for";
        }

        public static new ForNode Parse(Token[] tokens, ref int i)
        {
            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "for");
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            StatementNode initialization = null;
            ExpressionNode condition = null;
            StatementNode afterthought = null;
            if (tokens[i].Type != TokenType.Semicolon) //Check for initialization statement
            {
                initialization = StatementNode.ParseEmbedded(tokens, ref i);
            }
            Parser.Expect(tokens, ref i, TokenType.Semicolon);
            if (tokens[i].Type != TokenType.Semicolon) //Check for condition expression
            {
                condition = ExpressionNode.Parse(tokens, ref i);
            }
            Parser.Expect(tokens, ref i, TokenType.Semicolon);
            if (tokens[i].Type != TokenType.RightParen) //Check for afterthought statement
            {
                afterthought = StatementNode.ParseEmbedded(tokens, ref i);
            }
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            BlockNode body = BlockNode.Parse(tokens, ref i);
            return new ForNode(initialization, condition, afterthought, body);
        }
    }
}
