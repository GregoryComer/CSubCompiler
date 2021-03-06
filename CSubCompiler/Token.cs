﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler
{
    public class Token
    {
        public Token(string literal, TokenType type)
        {
            Literal = literal;
            Type = type;
            WhitespaceBefore = false;
        }
        public Token(string literal, TokenType type, int codeIndex, bool whitespaceBefore)
        {
            Literal = literal;
            Type = type;
            CodeIndex = codeIndex;
            WhitespaceBefore = whitespaceBefore;
        }

        public string Literal;
        public TokenType Type;
        public bool WhitespaceBefore;
        public int CodeIndex;

        public override string ToString()
        {
            return Literal;
        }
    }

    public enum TokenType
    {
        AlphaNum,
        Ampersand,
        AmpersandEqual,
        Arrow,
        Caret,
        CaretEqual,
        Char,
        Comma,
        Divide,
        DivideEqual,
        Dot,
        DoubleAmpersand,
        DoubleEqual,
        DoubleMinus,
        DoublePipe,
        DoublePlus,
        Equal,
        Exclamation,
        Float,
        GreaterThan,
        GreaterThanEqual,
        Int,
        LeftCurlyBrace,
        LeftParen,
        LeftSquareBrace,
        LessThan,
        LessThanEqual,
        Minus,
        MinusEquals,
        NotEqual,
        Mod,
        ModEqual,
        Pipe,
        PipeEqual,
        Plus,
        PlusEqual,
        RightCurlyBrace,
        RightParen,
        RightSquareBrace,
        Semicolon,
        ShiftLeft,
        ShiftLeftEqual,
        ShiftRight,
        ShiftRightEqual,
        Star,
        StarEqual,
        String,
        Tilde
    }
}
