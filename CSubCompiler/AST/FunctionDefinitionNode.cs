using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class FunctionDefinitionNode : TopLevelNode
    {
        BlockNode Body
        {
            get;
            set;
        }
        string Name
        {
            get;
            set;
        }
        FunctionParameterNode[] Parameters
        {
            get;
            set;
        }
        TypeReferenceNode ReturnType
        {
            get;
            set;
        }

        public FunctionDefinitionNode(TypeReferenceNode returnType, string name, FunctionParameterNode[] parameters, BlockNode body, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Body = body;
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }

        public static new FunctionDefinitionNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            TypeReferenceNode returnType = TypeReferenceNode.Parse(tokens, ref i);
            string name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            List<FunctionParameterNode> parameters = new List<FunctionParameterNode>();
            while (Parser.CheckBounds(tokens, i) && tokens[i].Type != TokenType.RightParen)
            {
                FunctionParameterNode param = FunctionParameterNode.Parse(tokens, ref i);
            }
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            BlockNode body = BlockNode.Parse(tokens, ref i);
            return new FunctionDefinitionNode(returnType, name, parameters.ToArray(), body, startToken, startIndex);
        }

        public static TopLevelNode ParseFunctionDefinitionOrDeclaration(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            TypeReferenceNode returnType = TypeReferenceNode.Parse(tokens, ref i);
            string name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            List<FunctionParameterNode> parameters = new List<FunctionParameterNode>();
            while (Parser.CheckBounds(tokens, i) && tokens[i].Type != TokenType.RightParen)
            {
                FunctionParameterNode param = FunctionParameterNode.Parse(tokens, ref i, false);
                parameters.Add(param);
                if (Parser.Check(tokens, i, TokenType.Comma)) //Todo: "void test(float x,)" should error.
                {
                    i++; //Consume Comma
                }
            }
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            if (Parser.Check(tokens, i, TokenType.Semicolon)) //Function Declaration
            {
                i++; //Consume Semicolon
                return new FunctionDeclarationNode(returnType, name, parameters.ToArray(), startToken, startIndex);
            }
            else //Function Definition
            {
                if (parameters.Any(n => (n.Name == null || n.Name == "")))
                    throw new ParserException("Missing parameter name.", i, tokens[i]); //Todo: Better error reporting
                BlockNode body = BlockNode.Parse(tokens, ref i);
                return new FunctionDefinitionNode(returnType, name, parameters.ToArray(), body, startToken, startIndex);
            }
        }

        public static bool IsFunctionDefinition(Token[] tokens, int i)
        {
            if (!Parser.CheckBoundsNoThrow(tokens, i) || !Parser.Check(tokens, i, TokenType.AlphaNum))
                return false;
            i++;
            if (!Parser.CheckBoundsNoThrow(tokens, i) || !Parser.Check(tokens, i, TokenType.AlphaNum))
                return false;
            i++;
            if (!Parser.CheckBoundsNoThrow(tokens, i) || tokens[i].WhitespaceBefore || !Parser.Check(tokens, i, TokenType.LeftParen))
                return false;
            return true;
        }
        public static bool IsFunctionDefinitionOrDeclaration(Token[] tokens, int i)
        {
            if (!Parser.CheckBoundsNoThrow(tokens, i) || !Parser.Check(tokens, i, TokenType.AlphaNum))
                return false;
            i++;
            if (!Parser.CheckBoundsNoThrow(tokens, i) || !Parser.Check(tokens, i, TokenType.AlphaNum))
                return false;
            i++;
            if (!Parser.CheckBoundsNoThrow(tokens, i) || tokens[i].WhitespaceBefore || !Parser.Check(tokens, i, TokenType.LeftParen))
                return false;
            return true;
        }
    }
}
