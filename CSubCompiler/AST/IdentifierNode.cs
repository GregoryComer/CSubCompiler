using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class IdentifierNode : Node
    {
        JoinType[] Joins
        {
            get;
            set;
        }
        string[] Parts
        {
            get;
            set;
        }

        public IdentifierNode(string[] parts, JoinType[] joins)
        {
            if (joins.Length != parts.Length - 1)
            {
                throw new InternalCompilerException("Error constructing IdentifierNode: Part count must be one greater than join count.");
            }

            Joins = joins;
            Parts = parts;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(Parts[0]);
            for (int i = 0; i < Joins.Length; i++)
            {
                str.Append((Joins[i] == JoinType.Arrow) ? "->" : ".");
                str.Append(Parts[i + 1]);
            }
            return str.ToString();
        }

        public static IdentifierNode Parse(Token[] tokens, ref int i)
        {
            List<string> parts = new List<string>();
            List<JoinType> joins = new List<JoinType>();

            if (tokens[i].Type != TokenType.AlphaNum)
            {
                throw new InternalCompilerException("Attempt to parse IdentifierNode starting at non-AlphaNum token."); //Should not occur, fallback.
            }

            parts.Add(tokens[i++].Literal);
            while (i < tokens.Length && (tokens[i].Type == TokenType.Dot || tokens[i].Type == TokenType.Arrow) && !tokens[i].WhitespaceBefore)
            {
                if (tokens[i].Type == TokenType.Dot)
                {
                    joins.Add(JoinType.Dot);
                }
                else if (tokens[i].Type == TokenType.Arrow)
                {
                    joins.Add(JoinType.Arrow);
                }
                i++; //Advance over join token
                if (tokens[i].Type != TokenType.AlphaNum) //Joins should be followed by AlphaNum
                {
                    throw new ParserException(string.Format("Expected identifier following \"{0}\"", tokens[i - 1].Literal), i, tokens[i]);
                }
                parts.Add(tokens[i].Literal);
                i++; //Advance over AlphaNum part
            }

            return new IdentifierNode(parts.ToArray(), joins.ToArray());
        }

        public static bool IsIdentifier(Token[] tokens, int i) //Todo: Determine if below logic is sufficient and unambigous
        {
            return (tokens[i].Type == TokenType.AlphaNum) && !Keywords.IsKeyword(tokens[i].Literal);
        }

        /// <summary>
        /// Returns the length of an identifier starting at token index i. Returns -1 if the element at index i is not an identifier.
        /// </summary>
        public static int PeekIdentifierLength(Token[] tokens, int i)
        {
            int startIndex = 0;

            if (tokens[i].Type != TokenType.AlphaNum)
            {
                return -1;
            }
            i++; //Pass over initial token
            while (i < tokens.Length && (tokens[i].Type == TokenType.Dot || tokens[i].Type == TokenType.Arrow) && !tokens[i].WhitespaceBefore)
            {
                i++; //Advance over join token
                if (tokens[i].Type != TokenType.AlphaNum) //Joins should be followed by AlphaNum
                {
                    return -1;
                }
                i++; //Advance over AlphaNum part
            }

            return i - startIndex;
        }

        /// <summary>
        /// Represents the connection between two identifier parts.
        /// </summary>
        public enum JoinType
        {
            Arrow,
            Dot
        }
    }
}
