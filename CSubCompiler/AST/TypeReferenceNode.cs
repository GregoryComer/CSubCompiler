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
        public TypeClassification Classification { get; set; }
        public bool Const { get; set; }
        public bool Extern { get; set; }
        public bool Static { get; set; }
        public bool Volatile { get; set; }
        /// <summary>
        /// For pointer types, the type pointed to.
        /// </summary>
        public TypeReferenceNode InnerType { get; set; }
        public ITypeDefinitionNode EmbeddedTypeDefinition { get; set; }

        public TypeReferenceNode(string name, TypeClassification classification, bool modStatic, bool modExtern, bool modVolatile)
        {
            Name = name;
            Classification = classification;
            Static = modStatic;
            Extern = modExtern;
            Volatile = modVolatile;
        }
        public TypeReferenceNode(string name, TypeClassification classification, bool modStatic, bool modExtern, bool modVolatile, TypeReferenceNode inner)
        {
            Name = name;
            Classification = classification;
            InnerType = inner;
            Static = modStatic;
            Extern = modExtern;
            Volatile = modVolatile;
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
            TypeReferenceNode outer = null;
            bool modConst, modExtern, modStatic, modVolatile;
            ReadModifiers(tokens, ref i, out modConst, out modExtern, out modStatic, out modVolatile);
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
                outer = new TypeReferenceNode(structName, TypeClassification.Struct, modStatic, modExtern, modVolatile);
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
                outer = new TypeReferenceNode(typeName, TypeClassification.Standard, modStatic, modExtern, modVolatile);
            }

            while (Parser.Check(tokens, i, TokenType.Star) || IsModifier(tokens, i))
            {
                ReadModifiers(tokens, ref i, out modConst, out modExtern, out modStatic, out modVolatile);
                Parser.Expect(tokens, ref i, TokenType.Star);
                outer = new TypeReferenceNode(null, TypeClassification.Pointer, modStatic, modExtern, modVolatile, outer);
            }
            return outer;
        }

        internal static void ReadModifiers(Token[] tokens, ref int i, out bool modConst, out bool modExtern, out bool modStatic, out bool modVolatile)
        {
            modVolatile = false;
            modExtern = false;
            modStatic = false;
            modConst = false;
            bool readModifier;
            do
            {
                readModifier = false;
                if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "volatile"))
                {
                    if (modVolatile)
                        throw new ParserException("Multiple volatile modifers for type.", i, tokens[i]);
                    modVolatile = true;
                    readModifier = true;
                }
                else if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "extern"))
                {
                    if (modExtern)
                        throw new ParserException("Multiple extern modifers for type.", i, tokens[i]);
                    modExtern = true;
                    readModifier = true;
                }
                else if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "static"))
                {
                    if (modStatic)
                        throw new ParserException("Multiple static modifiers for type.", i, tokens[i]);
                    modStatic = true;
                    readModifier = true;
                }
                else if (Parser.CheckLiteral(tokens, i, TokenType.AlphaNum, "const"))
                {
                    if (modConst)
                        throw new ParserException("Multiple const modifiers for type.", i, tokens[i]);
                    modConst = true;
                    readModifier = true;
                }
                if (readModifier)
                    i++;
            } while (readModifier);
        }

        private static string[] Modifiers = { "extern", "static", "volatile", "const" };
        private static bool IsModifier(Token[] tokens, int i)
        {
            return Modifiers.Contains(tokens[i].Literal);
        }
    }

    public enum TypeClassification
    {
        Standard,
        Struct,
        Pointer
    }
}
