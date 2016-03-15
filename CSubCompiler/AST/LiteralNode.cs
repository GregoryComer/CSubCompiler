using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;

namespace CSubCompiler.AST
{
    public abstract class LiteralNode : SubExpressionNode
    {
        public LiteralNode(Token token, int tokenIndex) : base(token, tokenIndex) { }
    }
}
