using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: ADD PROPER ERROR HANDLING TO LOOPS WHEN I EXCEEDS CODE LENGTH

namespace CSubCompiler
{
    public static class Lexer
    {
        delegate Token LexerMethod(string raw, ref int i);
        static Dictionary<char, LexerMethod> LexerMap = new Dictionary<char,LexerMethod>()
        {
            { '&', ProcessAmpersand },
            { '^', ProcessCaret },
            { ',', ProcessComma },
            { '/', ProcessDivide },
            { '.', ProcessDot },
            { '=', ProcessEqual },
            { '!', ProcessExclamation },
            { '>', ProcessGreater },
            { '{', ProcessLeftCurlyBrace },
            { '[', ProcessLeftSquareBrace },
            { '(', ProcessLeftParen },
            { '<', ProcessLess },
            { '-', ProcessMinus },
            { '%', ProcessMod },
            { '|', ProcessPipe },
            { '+', ProcessPlus },
            { '}', ProcessRightCurlyBrace },
            { ']', ProcessRightSquareBrace },
            { ')', ProcessRightParen },
            { ';', ProcessSemicolon },
            { '*', ProcessStar },
            { '~', ProcessTilde }
        };

        public static Token[] Lex(string raw)
        {
            List<Token> tokens = new List<Token>();
            int i = 0;
            while (i < raw.Length)
            {
                Token t = ProcessNew(raw, ref i);
                tokens.Add(t);
            }
            return tokens.ToArray();
        }

        #region Lexer Processing Methods
        static Token ProcessNew(string raw, ref int i)
        {
            int startIndex = i;
            Token t;
            bool whitespaceBefore = ProcessWhitespace(raw, ref i);
            char first = raw[i];

            if (char.IsLetter(first) || first == '_') //Handle keywords and identifiers
            {
                t = ProcessAlphaNum(raw, ref i);
            }
            else if (first == '"') //Handle strings
            {
                t = ProcessString(raw, ref i);
            }
            else if (char.IsNumber(first)) //Handle ints and floats (that do not begin with '.')
            {
                t = ProcessNumber(raw, ref i);
            }
            else if (LexerMap.ContainsKey(first)) //Handle misc. symbols
            {
                t = LexerMap[first](raw, ref i);
            }
            else //Unknown token, error
            {
                throw new LexerException(string.Format("Unrecognized token beginning at index {0}: \"{1}\".", i, GetCodePreview(raw, i, 10)), i);
            }

            t.CodeIndex = startIndex;
            t.WhitespaceBefore = whitespaceBefore;
            return t;
        }
        static Token ProcessAlphaNum(string raw, ref int i)
        {
            int startIndex = i;
            for (; i < raw.Length && (char.IsLetterOrDigit(raw[i]) || raw[i] == '_'); i++) ;
            string val = raw.Substring(startIndex, i - startIndex);
            return new Token(val, TokenType.AlphaNum);
        }
        static Token ProcessAmpersand(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //&=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.AmpersandEqual);
                i += 2;
                return t;
            }
            else if (raw[i + 1] == '&') //&&
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.DoubleAmpersand);
                i += 2;
                return t;
            }
            else //&
            {
                Token t = new Token(raw[i].ToString(), TokenType.Ampersand);
                i += 1;
                return t;
            }
        }
        static Token ProcessCaret(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //^=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.CaretEqual);
                i += 2;
                return t;
            }
            else //^
            {
                Token t = new Token(raw[i].ToString(), TokenType.Caret);
                i += 1;
                return t;
            }
        }
        static Token ProcessChar(string raw, ref int i)
        {
            char literal;
            if (raw[i] == '\\') //Handle escape characters
            {
                i++;
                switch (raw[i])
                {
                    case 'a':
                        literal = '\x07';
                        break;
                    case 'b':
                        literal = '\x08';
                        break;
                    case 'f':
                        literal = '\x0C';
                        break;
                    case 'n':
                        literal = '\x0A';
                        break;
                    case 'r':
                        literal = '\x0D';
                        break;
                    case 't':
                        literal = '\x09';
                        break;
                    case 'v':
                        literal = '\x0B';
                        break;
                    case '\\':
                        literal = '\\';
                        break;
                    case '\'':
                        literal = '\'';
                        break;
                    case '\"':
                        literal = '"';
                        break;
                    case '?':
                        literal = '?';
                        break;
                    default:
                        if (raw[i] == 'x') //Hex literal
                        {
                            int hexStartIndex = i;
                            for (; IsHexDigit(raw[i]); i++) ;
                            string hexLiteral = raw.Substring(hexStartIndex, i - hexStartIndex);
                            literal = (char)Convert.ToInt32(hexLiteral, 16);
                        }
                        else //Octal literals are not supported
                        {
                            throw new LexerException(string.Format("Unknown escape sequence at index {0}.", i), i);
                        }
                        break;
                }
            }
            else
            {
                literal = raw[i];
            }
            return new Token(literal.ToString(), TokenType.Char);
        }
        static Token ProcessComma(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.Comma);
        }
        static Token ProcessDivide(string raw, ref int i)
        {
            if (raw[i + 1] == '=') // /=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.DivideEqual);
                i += 2;
                return t;
            }
            else // /
            {
                Token t = new Token(raw[i].ToString(), TokenType.Divide);
                i += 1;
                return t;
            }
        }
        static Token ProcessDot(string raw, ref int i)
        {
            if (char.IsNumber(raw[i + 1])) //Float literal
            {
                int literalStart = i;
                i += 1; //Skip over '.'
                for (; char.IsNumber(raw[i]); i++) ;
                return new Token(raw.Substring(literalStart, i - literalStart), TokenType.Float);
            }
            else //Dot
            {
                Token t = new Token(raw[i].ToString(), TokenType.Dot);
                i += 1;
                return t;
            }
        }
        static Token ProcessEqual(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //Token is '=='
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.DoubleEqual);
                i += 2;
                return t;
            }
            else //Token is '='
            {
                Token t = new Token(raw[i].ToString(), TokenType.Equal);
                i += 1;
                return t;
            }
        }
        static Token ProcessExclamation(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //Token is '!='
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.NotEqual);
                i += 2;
                return t;
            }
            else //Token is '!'
            {
                Token t = new Token(raw[i].ToString(), TokenType.Exclamation);
                i += 1;
                return t;
            }
        }
        static Token ProcessGreater(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //Token is '>='
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.GreaterThanEqual);
                i += 2;
                return t;
            }
            else if (raw.Substring(i, 3) == ">>=") //>>=
            {
                Token t = new Token(raw.Substring(i, 3), TokenType.ShiftRightEqual);
                i += 3;
                return t;
            }
            else if (raw.Substring(i, 2) == ">>")//>>
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.ShiftRight);
                i += 2;
                return t;
            }
            else //Token is '>'
            {
                Token t = new Token(raw[i].ToString(), TokenType.GreaterThan);
                i += 1;
                return t;
            }
        }
        static Token ProcessLeftCurlyBrace(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.LeftCurlyBrace);
        }
        static Token ProcessLeftSquareBrace(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.LeftSquareBrace);
        }
        static Token ProcessLeftParen(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.LeftParen);
        }
        static Token ProcessLess(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //Token is '<='
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.LessThanEqual);
                i += 2;
                return t;
            }
            else if (raw.Substring(i, 3) == "<<=") //<<=
            {
                Token t = new Token(raw.Substring(i, 3), TokenType.ShiftLeftEqual);
                i += 3;
                return t;
            }
            else if (raw.Substring(i, 2) == "<<")//<<
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.ShiftLeft);
                i += 2;
                return t;
            }
            else //Token is '<'
            {
                Token t = new Token(raw[i].ToString(), TokenType.LessThan);
                i += 1;
                return t;
            }
        }
        static Token ProcessMinus(string raw, ref int i)
        {
            if (raw[i + 1] == '-') //--
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.DoubleMinus);
                i += 2;
                return t;
            }
            else if (raw[i + 1] == '=') //-=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.MinusEquals);
                i += 2;
                return t;
            }
            else if (raw[i + 1] == '>') //-=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.Arrow);
                i += 2;
                return t;
            }
            else //-
            {
                Token t = new Token(raw[i].ToString(), TokenType.Minus);
                i += 1;
                return t;
            }
        }
        static Token ProcessMod(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //%=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.PercentEqual);
                i += 2;
                return t;
            }
            else //%
            {
                Token t = new Token(raw[i].ToString(), TokenType.Percent);
                i += 1;
                return t;
            }
        }
        static Token ProcessNumber(string raw, ref int i)
        {
            StringBuilder startingDigits = new StringBuilder();
            int startIndex = i;
            for (; i < raw.Length && char.IsDigit(raw[i]); i++) ;
            if (i < raw.Length && raw[i] == '.') //Float literal
            {
                i++;
                for (; char.IsDigit(raw[i]); i++) ;
                return new Token(raw.Substring(startIndex, i - startIndex), TokenType.Float);
            }
            else //Int literal
            {
                return new Token(raw.Substring(startIndex, i - startIndex), TokenType.Int);
            }
        }
        static Token ProcessPipe(string raw, ref int i)
        {
            if (raw[i + 1] == '|') //||
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.DoublePipe);
                i += 2;
                return t;
            }
            else if (raw[i + 1] == '=') //|=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.PipeEquals);
                i += 2;
                return t;
            }
            else //|
            {
                Token t = new Token(raw[i].ToString(), TokenType.Pipe);
                i += 1;
                return t;
            }
        }
        static Token ProcessPlus(string raw, ref int i)
        {
            if (raw[i + 1] == '+') //++
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.DoublePlus);
                i += 2;
                return t;
            }
            else if (raw[i + 1] == '=') //+=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.PlusEquals);
                i += 2;
                return t;
            }
            else //+
            {
                Token t = new Token(raw[i].ToString(), TokenType.Plus);
                i += 1;
                return t;
            }
        }
        static Token ProcessRightCurlyBrace(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.RightCurlyBrace);
        }
        static Token ProcessRightSquareBrace(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.RightSquareBrace);
        }
        static Token ProcessRightParen(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.RightParen);
        }
        static Token ProcessSemicolon(string raw, ref int i)
        {
            return new Token(raw[i++].ToString(), TokenType.Semicolon);
        }
        static Token ProcessStar(string raw, ref int i)
        {
            if (raw[i + 1] == '=') //*=
            {
                Token t = new Token(raw.Substring(i, 2), TokenType.StarEquals);
                i += 2;
                return t;
            }
            else //*
            {
                Token t = new Token(raw[i].ToString(), TokenType.Star);
                i += 1;
                return t;
            }
        }
        static Token ProcessString(string raw, ref int i)
        {
            i++; //Skip over '"'
            StringBuilder literal = new StringBuilder();
            for (; raw[i] != '"'; i++)
            {
                if (raw[i] == '\\') //Handle escape characters
                {
                    i++;
                    switch (raw[i])
                    {
                        case 'a':
                            literal.Append('\x07');
                            break;
                        case 'b':
                            literal.Append('\x08');
                            break;
                        case 'f':
                            literal.Append('\x0C');
                            break;
                        case 'n':
                            literal.Append('\x0A');
                            break;
                        case 'r':
                            literal.Append('\x0D');
                            break;
                        case 't':
                            literal.Append('\x09');
                            break;
                        case 'v':
                            literal.Append('\x0B');
                            break;
                        case '\\':
                            literal.Append('\\');
                            break;
                        case '\'':
                            literal.Append('\'');
                            break;
                        case '\"':
                            literal.Append('"');
                            break;
                        case '?':
                            literal.Append('?');
                            break;
                        default:
                            if (raw[i] == 'x') //Hex literal
                            {
                                int hexStartIndex = i;
                                for (; IsHexDigit(raw[i]); i++) ;
                                string hexLiteral = raw.Substring(hexStartIndex, i - hexStartIndex);
                                literal.Append((char)Convert.ToInt32(hexLiteral, 16));
                            }
                            else //Octal literals are not supported
                            {
                                throw new LexerException(string.Format("Unknown escape sequence at index {0}: \"{1}\".", i, GetCodePreview(raw, i - 1, 10)), i);
                            }
                            break;
                    }
                }
                else
                {
                    literal.Append(raw[i]);
                }
            }
            i++; //Skip over closing quote
            return new Token(literal.ToString(), TokenType.String);
        }
        static Token ProcessTilde(string raw, ref int i)
        {
            Token t = new Token(raw[i].ToString(), TokenType.Tilde);
            i += 1;
            return t;
        }
        /// <summary>
        /// Advances index over whitespace.
        /// </summary>
        /// <returns>Returns true if whitespace (excluding newlines) precede the next token.</returns>
        static bool ProcessWhitespace(string raw, ref int i)
        {
            int startIndex = i;
            for (; char.IsWhiteSpace(raw[i]); i++) ;
            return (i != 0) && (startIndex != i) && (raw[i - 1] != '\r' && raw[i - 1] != '\n'); //Only mark token as having preceding whitespace if newline does not precede
        }
        #endregion

        #region Utility Methods
        static string GetCodePreview(string raw, int i, int len)
        {
            if (raw.Length >= i + len)
            {
                return raw.Substring(i, len) + "...";
            }
            else
            {
                return raw.Substring(i);
            }
        }
        static bool IsHexDigit(char c)
        {
            return char.IsNumber(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }
        #endregion
    }
}
