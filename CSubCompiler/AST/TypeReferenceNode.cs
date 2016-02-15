using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class TypeReferenceNode
    {
        public string Name { get; set; }
        public bool Struct { get; set; }
        public int PointerDepth { get; set; }

        public TypeReferenceNode(string name, bool isStruct)
        {
            Name = name;
            Struct = isStruct;
            PointerDepth = 0;
        }
        public TypeReferenceNode(string name, bool isStruct, int pointerDepth)
        {
            Name = name;
            Struct = isStruct;
            PointerDepth = pointerDepth;
        }

        public static bool IsTypeReferenceNode(Token[] tokens, int i)
        {
            if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "struct"))
                i++;
            if (!Parser.Check(tokens, i, TokenType.AlphaNum) || Language.Keywords.IsNonTypeKeyword(tokens[i].Literal))
                return false;
            i++;
            for (; Parser.Check(tokens, i, TokenType.Star); i++) ; //Pass over pointer specifier, TODO: Make sure this doesn't accidently pass multiplication as a type reference in any cases
            if (Parser.Check(tokens, i, TokenType.LeftParen))
                return false;
            else
                return true;
        }
        public static TypeReferenceNode Parse(Token[] tokens, ref int i)
        {
            bool isStruct = false;
            if (tokens[i].Type == TokenType.AlphaNum && tokens[i].Literal == "struct")
            {
                isStruct = true;
                i++; //Consume struct keyword
            }
            string name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            int pointerDepth = 0;
            for (; Parser.Check(tokens, i, TokenType.Star); pointerDepth++, i++) ;
            return new TypeReferenceNode(name, isStruct, pointerDepth);
        }
    }
}
