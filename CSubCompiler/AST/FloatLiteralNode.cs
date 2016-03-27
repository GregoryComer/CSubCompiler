using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class FloatLiteralNode : LiteralNode //Todo: Handle doubles
    {
        public float Value
        {
            get;
            set;
        }

        public FloatLiteralNode(float value, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static FloatLiteralNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            float value;
            if (!float.TryParse(tokens[i].Literal, out value))
            {
                throw new ParserException("Invalid float literal.", i, tokens[i]); //Should not occur. Any invalid float literals should be caught by lexer. This is a fallback.
            }
            i++; //Consume token
            return new FloatLiteralNode(value, startToken, startIndex);
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            context.Output.Write(new ILLoadF { Address = ILAddressingReference.CreateConstant(1.0), Size = FloatOperandSize.Size32 });
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            return new ILBaseType(Language.BaseType.Float);
        }
    }
}
