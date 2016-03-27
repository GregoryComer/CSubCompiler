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
            Expression = expression;
            Type = type;
        }

        public override ILType GetResultType(ILGenerationContext context)
        {
            return context.ResolveTypeReference(Type).Type; //TODO: CHECK WHETHER IGNORING MODIFIERS HERE COULD CAUSE ISSUES
        }

        public static bool IsCast(Token[] tokens, int i) //TODO: CHECK IF PRECEDENCE SHOULD BE CONSIDERED IN ISSUBEXPRESSION CALL
        {
            return (Parser.Check(tokens, i++, TokenType.LeftParen)
                && Parser.Check(tokens, i, TokenType.AlphaNum)
                && !Keywords.IsNonTypeKeyword(tokens[i++].Literal)
                && Parser.Check(tokens, i++, TokenType.RightParen)              
                && ExpressionNode.IsSubExpression(tokens, i));
        }
        
        public static CastNode Parse(Token[] tokens, ref int i)
        {
            Token startToken = tokens[i];
            int startIndex = i;
            Parser.Expect(tokens, ref i, TokenType.LeftParen);
            var type = TypeReferenceNode.Parse(tokens, ref i);
            Parser.Expect(tokens, ref i, TokenType.RightParen);
            var operand = ExpressionNode.ParseQ(tokens, ref i, Operators.CastPrecedence);
            return new CastNode(operand, type, startToken, startIndex);
        }
    }
}