using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class StatementNode : Node
    {
        public static StatementNode Parse(Token[] tokens, ref int i)
        {
            if (IfNode.IsIf(tokens, i))
            {
                return IfNode.Parse(tokens, ref i);
            }
            else if (WhileNode.IsWhile(tokens, i))
            {
                return WhileNode.Parse(tokens, ref i);
            }
            else if (ForNode.IsFor(tokens, i))
            {
                return ForNode.Parse(tokens, ref i);
            }
            else if (BreakNode.IsBreak(tokens, i))
            {
                return BreakNode.Parse(tokens, ref i);
            }
            else if (ContinueNode.IsContinue(tokens, i))
            {
                return ContinueNode.Parse(tokens, ref i);
            }
            else if (VariableDeclarationNode.IsVariableDeclaration(tokens, i))
            {
                VariableDeclarationNode node = VariableDeclarationNode.Parse(tokens, ref i);
                Parser.Expect(tokens, ref i, TokenType.Semicolon);
                return node;
            }
            else //Assume expression if no previous match
            {
                ExpressionStatementNode node = ExpressionStatementNode.Parse(tokens, ref i);
                Parser.Expect(tokens, ref i, TokenType.Semicolon);
                return node;
            }
        }
        /// <summary>
        /// Parses a statement, excluding statement types that begin a new block (if/while/for/etc.), as well as excluding the semicolon.
        /// </summary>
        public static StatementNode ParseEmbedded(Token[] tokens, ref int i)
        {
            if (VariableDeclarationNode.IsVariableDeclaration(tokens, i))
            {
                VariableDeclarationNode node = VariableDeclarationNode.Parse(tokens, ref i);
                return node;
            }
            else //Assume expression if no previous match
            {
                ExpressionStatementNode node = ExpressionStatementNode.Parse(tokens, ref i);
                return node;
            }
        }
    }
}
