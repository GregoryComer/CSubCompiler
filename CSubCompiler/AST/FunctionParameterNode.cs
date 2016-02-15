 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class FunctionParameterNode : Node
    {
        public string Name
        {
            get;
            set;
        }
        public TypeReferenceNode Type
        {
            get;
            set;
        }

        public FunctionParameterNode(TypeReferenceNode type, string name)
        {
            Name = name;
            Type = type;
        }

        public static FunctionParameterNode Parse(Token[] tokens, ref int i)
        {
            return Parse(tokens, ref i, true);
        } 

        public static FunctionParameterNode Parse(Token[] tokens, ref int i, bool requireName)
        {
            TypeReferenceNode type = TypeReferenceNode.Parse(tokens, ref i);
            string name = null;
            if (tokens[i].Type == TokenType.AlphaNum)
            {
                name = Parser.Expect(tokens, ref i, TokenType.AlphaNum).Literal;
            }
            return new FunctionParameterNode(type, name);
        }
    }
}
