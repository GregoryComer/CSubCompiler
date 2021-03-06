﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.AST
{
    public class ExpressionStatementNode : StatementNode
    {
        public ExpressionNode Expression
        {
            get;
            set;
        }

        public ExpressionStatementNode(ExpressionNode expression, Token token, int tokenIndex) : base(token, tokenIndex)
        {
            Expression = expression;
        }

        public static new ExpressionStatementNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;

            ExpressionNode expression = ExpressionNode.Parse(tokens, ref i);
            return new ExpressionStatementNode(expression, startToken, startIndex);
        }
    }
}
