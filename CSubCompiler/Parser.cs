﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    public static class Parser
    {
        public static bool Check(Token[] tokens, int i, TokenType type)
        {
            return tokens[i].Type == type;
        }
        public static bool CheckLiteral(Token[] tokens, int i, TokenType type, string literal)
        {
            return (tokens[i].Type == type) && (tokens[i].Literal == literal);
        }
        public static bool CheckLiteral(Token[] tokens, int i, TokenType type, IEnumerable<string> literals)
        {
            return (tokens[i].Type == type) && literals.Contains(tokens[i].Literal);
        }
        public static bool CheckBounds(Token[] tokens, int i)
        {
            if (i >= tokens.Length)
            {
                throw new ParserException("Unexpected end of stream.", i, tokens[i]);
            }
            return true;
        }
        public static bool CheckBoundsNoThrow(Token[] tokens, int i)
        {
            return (i < tokens.Length);
        }
        public static Token Expect(Token[] tokens, ref int i, TokenType type)
        {
            if (tokens[i].Type != type) //Throw exception if token type does not match expected
            {
                throw new ParserException(string.Format("Expected token of type {0}.", type.ToString()), i, tokens[i]);
            }
            else //Consume token
            {
                Token t = tokens[i];
                i++;
                return t;
            }
        }
        public static Token ExpectLiteral(Token[] tokens, ref int i, TokenType type, string literal)
        {
            if (tokens[i].Type != type) //Throw exception if token type does not match expected
            {
                throw new ParserException(string.Format("Expected token of type {0}.", type.ToString()), i, tokens[i]);
            }
            else if (tokens[i].Literal != literal)
            {
                throw new ParserException(string.Format("Expected literal \"{0}\".", literal.ToString()), i, tokens[i]);
            }
            else //Consume token
            {
                Token t = tokens[i];
                i++;
                return t;
            }
        }

        internal static Token Peek(Token[] tokens, int i)
        {
            return tokens[i];
        }
    }
}