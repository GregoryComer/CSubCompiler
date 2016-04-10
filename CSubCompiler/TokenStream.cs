using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    public class TokenStream
    {
        private int _index;
        private readonly Token[] _tokens;

        public TokenStream(Token[] tokens)
        {
            _index = 0;
            _tokens = tokens;
        }

        public Token Consume()
        {
            return _tokens[_index++];
        }

        public Token Expect(TokenType type)
        {
            if (_tokens[_index].Type != type)
                throw new ParserException(
                    $"Unexpected token \"{_tokens[_index].Literal}\", expected token of type \"{type.ToString()}\".",
                    _index, _tokens[_index]);
            return _tokens[_index++];
        }

        public Token Expect(TokenType type, string literal)
        {
            if (_tokens[_index].Type != type)
                throw new ParserException(
                    $"Unexpected token \"{_tokens[_index].Literal}\", expected token of type \"{type.ToString()}\".",
                    _index, _tokens[_index]);
            if (_tokens[_index].Literal != literal)
                throw new ParserException(
                    $"Unexpected token \"{_tokens[_index].Literal}\", expected \"{literal.ToString()}\".",
                    _index, _tokens[_index]);
            return _tokens[_index++];
        }

        public Token Peek()
        {
            return _tokens[_index];
        }

        public Token Peek(int offset)
        {
            return _tokens[_index + offset];
        }

        public bool Check(TokenType type)
        {
            return _tokens[_index].Type == type;
        }

        public bool Check(TokenType type, int offset)
        {
            return _tokens[_index + offset].Type == type;
        }

        public bool Check(TokenType type, string literal)
        {
            return _tokens[_index].Type == type &&
                   _tokens[_index].Literal == literal;
        }

        public bool Check(TokenType type, string literal, int offset)
        {
            return _tokens[_index + offset].Type == type &&
                   _tokens[_index + offset].Literal == literal;
        }
    }
}
