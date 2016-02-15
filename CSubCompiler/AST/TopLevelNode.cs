using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class TopLevelNode : Node
    {
        public static TopLevelNode Parse(Token[] tokens, ref int i)
        {
            if (FunctionDefinitionNode.IsFunctionDefinitionOrDeclaration(tokens, i))
            {
                return FunctionDefinitionNode.ParseFunctionDefinitionOrDeclaration(tokens, ref i);
            }
            else if (StructDefinitionNode.IsStructDefinition(tokens, i))
            {
                return StructDefinitionNode.Parse(tokens, ref i);
            }
            else if (TypedefNode.IsTypedef(tokens, i))
            {
                return TypedefNode.Parse(tokens, ref i);
            }
            else
            {
                throw new ParserException("Unexpected token.", i, tokens[i]);
            }
        }
    }
}
