using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public abstract class Node
    {
        public Token Token { get; set; }
        public int TokenIndex { get; set; }

        public Node(Token token, int tokenIndex)
        {
            Token = token;
            TokenIndex = tokenIndex;
        }

        public virtual void GenerateIL(ILGenerationContext context)
        {
            context.CurrentNode = this;
            GenerateILInternal(context);
        }

        protected virtual void GenerateILInternal(ILGenerationContext context) //This should be made internal at some point
        {
            throw new NotImplementedException("Hit Node.cs fallback GenerateILInternal.");
        }
    }
}
