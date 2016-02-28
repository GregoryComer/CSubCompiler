﻿using System;
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

        public StringLiteralNode(string value)
        {
            Value = value;
        }

        public static StringLiteralNode Parse(Token[] tokens, ref int i)
        {
            string value = tokens[i].Literal;
            i++; //Consume token
            return new StringLiteralNode(value);
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
