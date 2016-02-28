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

        public BlockNode(StatementNode[] statements)
        {
            Statements = statements;
        }

        public void GenerateIL(ILGenerationContext context, List<IILInstruction> output)
        {

        }

        public static BlockNode Parse(Token[] tokens, ref int i)
        {
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
            return new BlockNode(statements.ToArray());
        }
    }
}
