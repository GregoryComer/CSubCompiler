using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class AssignmentNode : Node
    {
        IdentifierNode Type;
        IdentifierNode Name;
        ExpressionNode Value;

        public AssignmentNode(IdentifierNode type, IdentifierNode name, ExpressionNode value)
        {
            Type = type;
            Name = name;
            Value = value;
        }

        public static bool IsAssignment(Token[] tokens, int i)
        {
            throw new NotImplementedException();
        }
        public static AssignmentNode Parse(Token[] tokens, ref int i)
        {
            throw new NotImplementedException();
        }
    }
}
