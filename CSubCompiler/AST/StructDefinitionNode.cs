using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class StructDefinitionNode : TopLevelNode
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

        public StructDefinitionNode(string name, FunctionDefinitionNode[] functions, VariableDeclarationNode[] variables)
        {
            Name = name;
            Functions = functions;
            Variables = variables;
        }

        public static StructDefinitionNode Parse(Token[] tokens, ref int i)
        {
            List<FunctionDefinitionNode> functions = new List<FunctionDefinitionNode>();
            List<VariableDeclarationNode> variables = new List<VariableDeclarationNode>();

            Parser.ExpectLiteral(tokens, ref i, TokenType.AlphaNum, "struct");
            string name = null;
            if (!Parser.Check(tokens, i, TokenType.LeftCurlyBrace))
                name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            Parser.Expect(tokens, ref i, TokenType.LeftCurlyBrace);
            while (!Parser.Check(tokens, i, TokenType.RightCurlyBrace))
            {
                if (FunctionDefinitionNode.IsFunctionDefinition(tokens, i))
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
            return new StructDefinitionNode(name, functions.ToArray(), variables.ToArray());
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
