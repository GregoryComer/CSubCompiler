using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class TypedefNode : TopLevelNode, ITypeDefinitionNode
    {
        public TypeReferenceNode Type
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public TypedefNode(TypeReferenceNode type, string name)
        {
            Type = type;
            Name = name;
        }

        public static bool IsTypedef(Token[] tokens, int i)
        {
            return Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "typedef");
        }
        public static new TypedefNode Parse(Token[] tokens, ref int i)
        {
            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "typedef");
            TypeReferenceNode typeReference;
            typeReference = TypeReferenceNode.Parse(tokens, ref i);
            string name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            if (Keywords.IsKeyword(name))
            {
                throw new ParserException("Invalid typedef name.", i, tokens[i]);
            }
            Parser.Expect(tokens, ref i, TokenType.Semicolon);
            return new TypedefNode(typeReference, name);
        }
    }
}
