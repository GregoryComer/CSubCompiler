using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class CharLiteralNode : LiteralNode
    {
        public char Value
        {
            get;
            set;
        }

        public CharLiteralNode(char value, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static CharLiteralNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int tokenIndex = i;

            if (tokens[i].Literal.Length > 1)
            {
                throw new ParserException("Invalid char literal.", i, tokens[i]); //Should not occur. Any invalid char literals should be caught by lexer. This is a fallback.
            }
            char value = tokens[i].Literal[0];
            i++; //Consume token
            return new CharLiteralNode(value, startToken, tokenIndex);
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            context.Output.Write(new ILLoadC { Constant = Value, Size = (GeneralOperandSize)Types.GetBaseTypeSize(BaseType.Char) });
        }
        
        public override ILType GetResultType(ILGenerationContext context)
        {
            return new ILBaseType(BaseType.Char);
        }
    }
}
