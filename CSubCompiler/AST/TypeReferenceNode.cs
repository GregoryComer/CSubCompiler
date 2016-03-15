using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class TypeReferenceNode : Node
    {
        public string Name { get; set; }
        public TypeClassification Classification { get; set; }
        public TypeModifiers Modifiers { get; set; }
        /// <summary>
        /// For pointer types, the type pointed to.
        /// </summary>
        public TypeReferenceNode InnerType { get; set; }
        public ITypeDefinitionNode EmbeddedTypeDefinition { get; set; }

        public TypeReferenceNode(string name, TypeClassification classification, TypeModifiers modifiers, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Name = name;
            Classification = classification;
            Modifiers = modifiers;
        }
        public TypeReferenceNode(string name, TypeClassification classification, TypeModifiers modifiers, TypeReferenceNode inner, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Name = name;
            Classification = classification;
            Modifiers = modifiers;
            InnerType = inner;
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
            Token startToken = tokens[i];
            int startTokenIndex = i;

            TypeReferenceNode outer = null;
            TypeModifiers modifiers = ReadModifiers(tokens, ref i);
            if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "struct"))
            {
                i++; //Consume struct token
                string structName = null;
                if (Parser.Check(tokens, i, TokenType.AlphaNum) && !Keywords.IsKeyword(Parser.Peek(tokens, i).Literal))
                    structName = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
                StructDefinitionNode embeddedStruct = null;
                if (Parser.Check(tokens, i, TokenType.LeftCurlyBrace))
                {
                    embeddedStruct = StructDefinitionNode.ParseBodyOnly(tokens, ref i, structName, true); //Todo: Disallow methods in structs in functions
                }
                if (outer != null)
                    throw new ParserException("Unexpected token in type.", i, tokens[i]);
                outer = new TypeReferenceNode(structName, TypeClassification.Struct, modifiers, startToken, startTokenIndex);
                outer.EmbeddedTypeDefinition = embeddedStruct;
            }
            else
            {
                string typeName = "";
                if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "unsigned"))
                {
                    typeName += tokens[i].Literal;
                    i++;
                }
                //Read Modifiers
                string typeNameMain = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
                if (Keywords.IsNonTypeKeyword(typeNameMain))
                    throw new ParserException(string.Format("Unexpected keyword \"{0}\"", typeNameMain), i, tokens[i]);
                typeName += typeNameMain;
                if (outer != null)
                    throw new ParserException("Unexpected token in type.", i, tokens[i]);
                outer = new TypeReferenceNode(typeName, TypeClassification.Standard, modifiers, startToken, startTokenIndex);
            }

            while (Parser.Check(tokens, i, TokenType.Star) || IsModifier(tokens, i))
            {
                modifiers = ReadModifiers(tokens, ref i);
                Parser.Expect(tokens, ref i, TokenType.Star);
                outer = new TypeReferenceNode(null, TypeClassification.Pointer, modifiers, outer, startToken, startTokenIndex);
            }
            return outer;
        }

        internal static TypeModifiers ReadModifiers(Token[] tokens, ref int i)
        {
            TypeModifiers modifiers = TypeModifiers.None;
            while (true)
            {
                if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "const"))
                {
                    if ((modifiers & TypeModifiers.Const) == 0)
                        throw new ParserException("Multiple const modifiers for type.", i, tokens[i]);
                    modifiers &= TypeModifiers.Const;
                }
                else if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "extern"))
                {
                    if ((modifiers & TypeModifiers.Extern) == 0)
                        throw new ParserException("Multiple extern modifers for type.", i, tokens[i]);
                    modifiers &= TypeModifiers.Extern;
                }
                else if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "static"))
                {
                    if ((modifiers & TypeModifiers.Static) == 0)
                        throw new ParserException("Multiple static modifiers for type.", i, tokens[i]);
                    modifiers &= TypeModifiers.Static;
                }
                else if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "volatile"))
                {
                    if ((modifiers & TypeModifiers.Volatile) == 0)
                        throw new ParserException("Multiple volatile modifers for type.", i, tokens[i]);
                    modifiers &= TypeModifiers.Volatile;
                }
                else
                {
                    break;
                }
                i++;
            }
            return modifiers;
        }

        private static string[] ValidModifiers = { "extern", "static", "volatile", "const" };
        private static bool IsModifier(Token[] tokens, int i)
        {
            return ValidModifiers.Contains(tokens[i].Literal);
        }
    }

    public enum TypeClassification
    {
        Standard,
        Struct,
        Pointer
    }
}
