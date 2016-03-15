using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.IL;
using CSubCompiler.Language;

namespace CSubCompiler.AST
{
    public class CastNode : SubExpressionNode
    {
        public SubExpressionNode Expression { get; set; }
        public TypeReferenceNode Type { get; set; }

        public CastNode(SubExpressionNode expression, TypeReferenceNode type, Token token, int tokenIndex) : base(token, tokenIndex)
        {

        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            return context.ResolveTypeReference(Type).Type; //TODO: CHECK WHETHER IGNORING MODIFIERS HERE COULD CAUSE ISSUES
        }

        public static bool IsCast(Token[] tokens, int i)
        {
            return (Parser.Check(tokens, i++, TokenType.LeftParen)
                && Parser.Check(tokens, i, TokenType.AlphaNum)
                && Keywords.IsNonTypeKeyword(tokens[i++].Literal)
                && Parser.Check(tokens, i, TokenType.RightParen)
                && ExpressionNode.IsSubExpression(tokens, i));
        }
    }
}