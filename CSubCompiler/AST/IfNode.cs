using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class IfNode : StatementNode
    {
        public ExpressionNode Condition
        {
            get;
            set;
        }
        public BlockNode IfBlock
        {
            get;
            set;
        }
        public BlockNode ElseBlock //Can be null
        {
            get;
            set;
        }

        public IfNode(ExpressionNode condition, BlockNode ifBlock)
        {
            Condition = condition;
            IfBlock = ifBlock;
        }
        public IfNode(ExpressionNode condition, BlockNode ifBlock, BlockNode elseBlock)
        {
            Condition = condition;
            IfBlock = ifBlock;
            ElseBlock = elseBlock;
        }

        public static bool IsIf(Token[] tokens, int i)
        {
            return tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "if";
        }

        public static new IfNode Parse(Token[] tokens, ref int i)
        {
            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "if");
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            ExpressionNode condition = ExpressionNode.Parse(tokens, ref i);
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            BlockNode ifBlock = BlockNode.Parse(tokens, ref i);
            if (tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "else") //Has else block
            {
                BlockNode elseBlock = BlockNode.Parse(tokens, ref i);
                return new IfNode(condition, ifBlock, elseBlock);
            }
            else //No else block
            {
                return new IfNode(condition, ifBlock);
            }
        }
    }
}
