using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class StructDefinitionNode : TopLevelNode, ITypeDefinitionNode
    {
        public string Name
        {
            get;
            set;
        }
        public FunctionDefinitionNode[] Functions
        {
            get;
            set;
        }
        public VariableDeclarationNode[] Variables
        {
            get;
            set;
        }

        public StructDefinitionNode(string name, FunctionDefinitionNode[] functions, VariableDeclarationNode[] variables, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Name = name;
            Functions = functions;
            Variables = variables;
        }

        public static new StructDefinitionNode Parse(Token[] tokens, ref int i)
        {
            return Parse(tokens, ref i);
        }
        public static StructDefinitionNode Parse(Token[] tokens, ref int i, bool allowMethods)
        {
            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "struct");
            string name = null;
            if (!Parser.Check(tokens, i, TokenType.LeftCurlyBrace))
                name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            return ParseBodyOnly(tokens, ref i, name, allowMethods);
        }
        public static StructDefinitionNode ParseBodyOnly(Token[] tokens, ref int i, string name, bool allowMethods)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            List<FunctionDefinitionNode> functions = new List<FunctionDefinitionNode>();
            List<VariableDeclarationNode> variables = new List<VariableDeclarationNode>();

            Parser.Expect(tokens, ref i, TokenType.LeftCurlyBrace);
            while (!Parser.Check(tokens, i, TokenType.RightCurlyBrace))
            {
                if (allowMethods && FunctionDefinitionNode.IsFunctionDefinition(tokens, i))
                {
                    FunctionDefinitionNode func = FunctionDefinitionNode.Parse(tokens, ref i);
                    functions.Add(func);
                }
                else if (VariableDeclarationNode.IsVariableDeclaration(tokens, i))
                {
                    VariableDeclarationNode var = VariableDeclarationNode.Parse(tokens, ref i, false);
                    variables.Add(var);
                    Parser.Expect(tokens, ref i, TokenType.Semicolon);
                }
            }
            Parser.Expect(tokens, ref i, TokenType.RightCurlyBrace);
            return new StructDefinitionNode(name, functions.ToArray(), variables.ToArray(), startToken, startIndex);
        }

        public static bool IsStructDefinition(Token[] tokens, int i)
        {
            if (!Parser.CheckLiteral(tokens, i++, TokenType.AlphaNum, "struct"))
                return false;
            if (!Parser.Check(tokens, i++, TokenType.AlphaNum))
                return false;
            return Parser.Check(tokens, i, TokenType.LeftCurlyBrace);
        }
    }
}