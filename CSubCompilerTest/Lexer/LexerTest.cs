using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler;
using NUnit.Framework;

namespace CSubCompilerTest.Lexer
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void Lex_SimpleTokens()
        {
            TestLexToken("Test", TokenType.AlphaNum);
            TestLexToken("_Test_2", TokenType.AlphaNum);
            TestLexToken("&", TokenType.Ampersand);
            TestLexToken("&=", TokenType.AmpersandEqual);
            TestLexToken("&&", TokenType.DoubleAmpersand);
            TestLexToken("^", TokenType.Caret);
            TestLexToken("^=", TokenType.CaretEqual);
            TestLexToken(",", TokenType.Comma);
            TestLexToken("/", TokenType.Divide);
            TestLexToken("/=", TokenType.DivideEqual);
            TestLexToken(".", TokenType.Dot);
            TestLexToken("=", TokenType.Equal);
            TestLexToken("==", TokenType.DoubleEqual);
            TestLexToken("!", TokenType.Exclamation);
            TestLexToken("!=", TokenType.NotEqual);
            TestLexToken(">", TokenType.GreaterThan);
            TestLexToken(">=", TokenType.GreaterThanEqual);
            TestLexToken(">>", TokenType.ShiftRight);
            TestLexToken(">>=", TokenType.ShiftRightEqual);
            TestLexToken("{", TokenType.LeftCurlyBrace);
            TestLexToken("[", TokenType.LeftSquareBrace);
            TestLexToken("(", TokenType.LeftParen);
            TestLexToken("<", TokenType.LessThan);
            TestLexToken("<=", TokenType.LessThanEqual);
            TestLexToken("<<", TokenType.ShiftLeft);
            TestLexToken("<<=", TokenType.ShiftLeftEqual);
            TestLexToken("-", TokenType.Minus);
            TestLexToken("-=", TokenType.MinusEquals);
            TestLexToken("--", TokenType.DoubleMinus);
            TestLexToken("->", TokenType.Arrow);
            TestLexToken("%", TokenType.Mod);
            TestLexToken("%=", TokenType.ModEqual);
            TestLexToken("|", TokenType.Pipe);
            TestLexToken("|=", TokenType.PipeEqual);
            TestLexToken("||", TokenType.DoublePipe);
            TestLexToken("+", TokenType.Plus);
            TestLexToken("+=", TokenType.PlusEqual);
            TestLexToken("==", TokenType.DoubleEqual);
            TestLexToken("}", TokenType.RightCurlyBrace);
            TestLexToken("]", TokenType.RightSquareBrace);
            TestLexToken(")", TokenType.RightParen);
            TestLexToken(";", TokenType.Semicolon);
            TestLexToken("*", TokenType.Star);
            TestLexToken("*=", TokenType.StarEqual);
            TestLexToken("~", TokenType.Tilde);
        }

        [Test]
        public void Lex_String_Simple()
        {
            string testString = "Test string.";
            string code = $"\"{testString}\"";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.String, tokens[0].Type);
            Assert.AreEqual(testString, tokens[0].Literal);
        }

        [Test]
        public void Lex_String_EscapeChars()
        {
            TestStringEscapeChar("\\a", '\a');
            TestStringEscapeChar("\\b", '\b');
            TestStringEscapeChar("\\f", '\f');
            TestStringEscapeChar("\\n", '\n');
            TestStringEscapeChar("\\r", '\r');
            TestStringEscapeChar("\\t", '\t');
            TestStringEscapeChar("\\v", '\v');
            TestStringEscapeChar("\\\\", '\\');
            TestStringEscapeChar("\\'", '\'');
            TestStringEscapeChar("\\\"", '"');
            TestStringEscapeChar("\\?", '?');
        }

        [Test]
        public void Lex_String_HexEscapeChars()
        {
            TestStringEscapeChar("\\x20", '\x20');
            TestStringEscapeChar("\\x30", '\x30');
            TestStringEscapeChar("\\x40", '\x40');
        }

        [Test]
        public void Lex_String_Compound()
        {
            string testCode = "\"\\x25Test.\\r\\n\"";
            var tokens = CSubCompiler.Lexer.Lex(testCode);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.String, tokens[0].Type);
            Assert.AreEqual("\x25Test.\r\n", tokens[0].Literal);
        }

        [Test]
        public void Lex_Char_Simple()
        {
            TestChar("a", 'a');
            TestChar("b", 'b');
            TestChar("c", 'c');
        }

        [Test]
        public void Lex_Char_Escape()
        {
            TestChar("\\a", '\a');
            TestChar("\\b", '\b');
            TestChar("\\f", '\f');
            TestChar("\\n", '\n');
            TestChar("\\r", '\r');
            TestChar("\\t", '\t');
            TestChar("\\v", '\v');
            TestChar("\\\\", '\\');
            TestChar("\\'", '\'');
            TestChar("\\\"", '"');
            TestChar("\\?", '?');
        }

        [Test]
        public void Lex_Char_HexEscape()
        {
            TestChar("\\x20", '\x20');
            TestChar("\\x30", '\x30');
            TestChar("\\x40", '\x40');
        }

        [Test]
        public void Lex_WhitespaceBefore()
        {
            string code = " a";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.IsTrue(tokens[0].WhitespaceBefore);
        }

        [Test]
        public void Lex_WhitespaceBefore_None()
        {
            string code = "a";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.IsFalse(tokens[0].WhitespaceBefore);
        }

        [Test]
        public void Lex_Int()
        {
            string code = "152";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.Int, tokens[0].Type);
            Assert.AreEqual("152", tokens[0].Literal);
        }

        [Test]
        public void Lex_Float_StartWithDigit()
        {
            string code = "5.23";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.Float, tokens[0].Type);
            Assert.AreEqual("5.23", tokens[0].Literal);
        }

        [Test]
        public void Lex_Float_StartWithDot()
        {
            string code = ".23";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.Float, tokens[0].Type);
            Assert.AreEqual(".23", tokens[0].Literal);
        }

        [Test]
        public void Lex_SimpleStatement()
        {
            string code = "x = 1 + 2";
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(5, tokens.Length);
            Assert.AreEqual(TokenType.AlphaNum, tokens[0].Type);
            Assert.AreEqual(TokenType.Equal, tokens[1].Type);
            Assert.AreEqual(TokenType.Int, tokens[2].Type);
            Assert.AreEqual(TokenType.Plus, tokens[3].Type);
            Assert.AreEqual(TokenType.Int, tokens[4].Type);
            Assert.AreEqual("x", tokens[0].Literal);
            Assert.AreEqual("=", tokens[1].Literal);
            Assert.AreEqual("1", tokens[2].Literal);
            Assert.AreEqual("+", tokens[3].Literal);
            Assert.AreEqual("2", tokens[4].Literal);
        }

        #region Util
        public void TestLexToken(string code, TokenType type)
        {
            var tokens = CSubCompiler.Lexer.Lex(code);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(type, tokens[0].Type);
        }

        public void TestStringEscapeChar(string literal, char expected)
        {
            string testCode = $"\"{literal}\"";
            var tokens = CSubCompiler.Lexer.Lex(testCode);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.String, tokens[0].Type);
            Assert.AreEqual(expected.ToString(), tokens[0].Literal);
        }

        public void TestChar(string literal, char expected)
        {
            string testCode = $"'{literal}'";
            var tokens = CSubCompiler.Lexer.Lex(testCode);
            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(TokenType.Char, tokens[0].Type);
            Assert.AreEqual(expected, tokens[0].Literal[0]);
        }
        #endregion
    }
}
