using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public class IntLiteralNode : LiteralNode
    {
        public int Value
        {
            get;
            set;
        }

        public IntLiteralNode(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static IntLiteralNode Parse(Token[] tokens, ref int i)
        {
            int value;
            if (!int.TryParse(tokens[i].Literal, out value))
            {
                throw new ParserException("Invalid integer literal.", i, tokens[i]); //Should not occur. Any invalid int literals should be caught by lexer. This is a fallback.
            }
            i++; //Consume token
            return new IntLiteralNode(value);
        }

        public override void GenerateIL(ILGenerationContext context, List<IILInstruction> output)
        {
            throw new NotImplementedException();
        }

        public override ILTypeSpecifier GetResultType(ILGenerationContext context)
        {
            throw new NotImplementedException();
        }
    }
}