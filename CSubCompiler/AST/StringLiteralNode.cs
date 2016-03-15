using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public class StringLiteralNode : LiteralNode
    {
        public string Value
        {
            get;
            set;
        }

        public StringLiteralNode(string value, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Value = value;
        }

        public static StringLiteralNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            string value = tokens[i].Literal;
            i++; //Consume token
            return new StringLiteralNode(value, startToken, startIndex);
        }

        protected override void GenerateILInternal(ILGenerationContext context)
        {
            throw new NotImplementedException();
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            return new ILPointerType(new ILBaseType(Language.BaseType.Char));
        }
    }
}
