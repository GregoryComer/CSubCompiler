using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class TypedefNode : TopLevelNode
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
        public StructDefinitionNode StructDefinition
        {
            get;
            set;
        }

        public TypedefNode(TypeReferenceNode type, string name)
        {
            Type = type;
            Name = name;
        }
        public TypedefNode(TypeReferenceNode type, string name, StructDefinitionNode structDefinition)
        {
            Type = type;
            Name = name;
            StructDefinition = structDefinition;
        }

        public static bool IsTypedef(Token[] tokens, int i)
        {
            return Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "typedef");
        }
        public static TypedefNode Parse(Token[] tokens, ref int i)
        {
            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "typedef");
            TypeReferenceNode typeReference;
            StructDefinitionNode structDefinition = null;
            if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "struct"))
            {
                if (Parser.Check(tokens, i + 1, TokenType.LeftCurlyBrace) || (Parser.Check(tokens, i, TokenType.AlphaNum) && Parser.Check(tokens, i + 2, TokenType.LeftCurlyBrace))) //It's a struct definition
                {
                    structDefinition = StructDefinitionNode.Parse(tokens, ref i);
                    typeReference = new TypeReferenceNode(structDefinition.Name, true);
                }
                else //It's a struct reference
                {
                    typeReference = TypeReferenceNode.Parse(tokens, ref i);
                }
            }
            else if (Parser.Check(tokens, i, TokenType.AlphaNum))
            {
                typeReference = TypeReferenceNode.Parse(tokens, ref i);
            }
            else
            {
                throw new ParserException("Expected type reference.", i, tokens[i]);
            }
            string name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            if (Keywords.IsKeyword(name))
            {
                throw new ParserException("Invalid typedef name.", i, tokens[i]);
            }
            Parser.Expect(tokens, ref i, TokenType.Semicolon);
            return new TypedefNode(typeReference, name, structDefinition);
        }
    }
}
