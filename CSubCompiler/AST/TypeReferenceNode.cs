using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class TypeReferenceNode
    {
        string Name;
        bool Struct;

        public TypeReferenceNode(string name, bool isStruct)
        {
            Name = name;
            Struct = isStruct;
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
            return new TypeReferenceNode(name, isStruct);
        }
    }
}
