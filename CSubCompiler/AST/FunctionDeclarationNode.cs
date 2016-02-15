using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class FunctionDeclarationNode : TopLevelNode
    {
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

        public FunctionDeclarationNode(TypeReferenceNode returnType, string name, FunctionParameterNode[] parameters)
        {
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }

        public static new FunctionDeclarationNode Parse(Token[] tokens, ref int i)
        {
            TypeReferenceNode returnType = TypeReferenceNode.Parse(tokens, ref i);
            string name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            List<FunctionParameterNode> parameters = new List<FunctionParameterNode>();
            while (Parser.CheckBounds(tokens, i) && tokens[i].Type != TokenType.RightParen)
            {
                FunctionParameterNode param = FunctionParameterNode.Parse(tokens, ref i, false);
            }
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            Parser.Expect(tokens, ref i, TokenType.Semicolon);
            return new FunctionDeclarationNode(returnType, name, parameters.ToArray());
        }

        public static bool IsFunctionDefinitionNode(Token[] tokens, int i)
        {
            throw new NotImplementedException();
        }
    }
}
