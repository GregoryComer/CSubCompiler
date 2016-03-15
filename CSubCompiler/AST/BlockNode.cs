using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public class BlockNode : Node
    {
        StatementNode[] Statements
        {
            get;
            set;
        }

        public BlockNode(StatementNode[] statements, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Statements = statements;
        }

        public void GenerateILInternal(ILGenerationContext context)
        {

        }

        public static BlockNode Parse(Token[] tokens, ref int i)
        {
            Token token = tokens[i];
            int tokenIndex = i;

            List<StatementNode> statements = new List<StatementNode>();
            if (Parser.CheckBounds(tokens, i) && tokens[i].Type == TokenType.LeftCurlyBrace)
            {
                i++; //Consume '{'
                while (Parser.CheckBounds(tokens, i) && tokens[i].Type != TokenType.RightCurlyBrace)
                {
                    StatementNode statement = StatementNode.Parse(tokens, ref i);
                    statements.Add(statement);
                }
                Parser.Expect(tokens, ref i, TokenType.RightCurlyBrace);
            }
            else //Single line block
            {
                StatementNode statement = StatementNode.Parse(tokens, ref i);
                statements.Add(statement);
            }
            return new BlockNode(statements.ToArray(), token, tokenIndex);
        }
    }
}
