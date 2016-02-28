using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public class FloatLiteralNode : LiteralNode
    {
        public float Value
        {
            get;
            set;
        }

        public FloatLiteralNode(float value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static FloatLiteralNode Parse(Token[] tokens, ref int i)
        {
            float value;
            if (!float.TryParse(tokens[i].Literal, out value))
            {
                throw new ParserException("Invalid float literal.", i, tokens[i]); //Should not occur. Any invalid float literals should be caught by lexer. This is a fallback.
            }
            i++; //Consume token
            return new FloatLiteralNode(value);
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
