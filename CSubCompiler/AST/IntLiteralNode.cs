using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class IntLiteralNode : LiteralNode // Todo: Handle int size/sign suffixes
    {
        public int Value
        {
            get;
            set;
        }

        public IntLiteralNode(int value, Token token, int tokenIndex) : base(token, tokenIndex)
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
            return new IntLiteralNode(value, tokens[i], i);
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            context.Output.Write(new ILLoad { Address = ILAddressingReference.CreateConstant(Value), Size = (GeneralOperandSize)Types.GetBaseTypeSize(BaseType.Int) });
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            return new ILBaseType(Language.BaseType.Int);
        }
    }
}