using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class CharLiteralNode : LiteralNode
    {
        public char Value
        {
            get;
            set;
        }

        public CharLiteralNode(char value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static CharLiteralNode Parse(Token[] tokens, ref int i)
        {
            if (tokens[i].Literal.Length > 1)
            {
                throw new ParserException("Invalid char literal.", i, tokens[i]); //Should not occur. Any invalid char literals should be caught by lexer. This is a fallback.
            }
            char value = tokens[i].Literal[0];
            i++; //Consume token
            return new CharLiteralNode(value);
        }
    }
}
