using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.Language
{
    public static class Operators
    {
        #region Precedence Tables
        //Lower precedence values indicate higher precedence (Precendence level 1 is highest)
        public static Dictionary<BinaryOperatorType, int> BinaryOperatorPrecedenceTable = new Dictionary<BinaryOperatorType, int>()
        {
            { BinaryOperatorType.Ampersand, 8 },
            { BinaryOperatorType.AmpersandEqual, 14 },
            { BinaryOperatorType.Caret, 9 },
            { BinaryOperatorType.CaretEqual, 14 },
            { BinaryOperatorType.Divide, 3 },
            { BinaryOperatorType.DivideEqual, 14 },
            { BinaryOperatorType.DoubleAmpersand, 11 },
            { BinaryOperatorType.DoubleEqual, 7 },
            { BinaryOperatorType.DoublePipe, 12 },
            { BinaryOperatorType.Equal, 14 },
            { BinaryOperatorType.Greater, 6 },
            { BinaryOperatorType.GreaterEqual, 6 },
            { BinaryOperatorType.Less, 6},
            { BinaryOperatorType.LessEqual, 6},
            { BinaryOperatorType.Minus, 4 },
            { BinaryOperatorType.MinusEqual, 14 },
            { BinaryOperatorType.Mod, 3 },
            { BinaryOperatorType.ModEqual, 14 },
            { BinaryOperatorType.NotEqual, 7 },
            { BinaryOperatorType.Pipe, 10 },
            { BinaryOperatorType.PipeEqual, 14 },
            { BinaryOperatorType.Plus, 4 },
            { BinaryOperatorType.PlusEqual, 14 },
            { BinaryOperatorType.ShiftLeft, 5 },
            { BinaryOperatorType.ShiftLeftEqual, 14 },
            { BinaryOperatorType.ShiftRight, 5 },
            { BinaryOperatorType.ShiftRightEqual, 14 },
            { BinaryOperatorType.Star, 3 },
            { BinaryOperatorType.StarEqual, 14 }
        };
        public static Dictionary<UnaryPreOperatorType, int> UnaryPreOperatorPrecedenceTable = new Dictionary<UnaryPreOperatorType,int>()
        {
            { UnaryPreOperatorType.Ampersand, 2 },
            { UnaryPreOperatorType.DoublePlus, 1 },
            { UnaryPreOperatorType.DoubleMinus, 1 },
            { UnaryPreOperatorType.Exclamation, 2 },
            { UnaryPreOperatorType.Minus, 2 },
            { UnaryPreOperatorType.Star, 2 },
            { UnaryPreOperatorType.Tilde, 2 }
        };
        public static Dictionary<UnaryPostOperatorType, int> UnaryPostOperatorPrecedenceTable = new Dictionary<UnaryPostOperatorType, int>()
        {
            { UnaryPostOperatorType.DoubleMinus, 1 },
            { UnaryPostOperatorType.DoublePlus, 1 }
        };
        #endregion
        #region Associativity Tables
        public static Dictionary<BinaryOperatorType, OperatorAssociativity> BinaryOperatorAssociativityTable = new Dictionary<BinaryOperatorType, OperatorAssociativity>()
        {
            { BinaryOperatorType.Ampersand, OperatorAssociativity.Left },
            { BinaryOperatorType.AmpersandEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.Caret, OperatorAssociativity.Left },
            { BinaryOperatorType.CaretEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.Divide, OperatorAssociativity.Left },
            { BinaryOperatorType.DivideEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.DoubleAmpersand, OperatorAssociativity.Left },
            { BinaryOperatorType.DoubleEqual, OperatorAssociativity.Left },
            { BinaryOperatorType.DoublePipe, OperatorAssociativity.Left },
            { BinaryOperatorType.Equal, OperatorAssociativity.Right },
            { BinaryOperatorType.Greater, OperatorAssociativity.Left },
            { BinaryOperatorType.GreaterEqual, OperatorAssociativity.Left },
            { BinaryOperatorType.Less, OperatorAssociativity.Left},
            { BinaryOperatorType.LessEqual, OperatorAssociativity.Left},
            { BinaryOperatorType.Minus, OperatorAssociativity.Left },
            { BinaryOperatorType.MinusEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.Mod, OperatorAssociativity.Left },
            { BinaryOperatorType.ModEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.NotEqual, OperatorAssociativity.Left },
            { BinaryOperatorType.Pipe, OperatorAssociativity.Left },
            { BinaryOperatorType.PipeEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.Plus, OperatorAssociativity.Left },
            { BinaryOperatorType.PlusEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.ShiftLeft, OperatorAssociativity.Left },
            { BinaryOperatorType.ShiftLeftEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.ShiftRight, OperatorAssociativity.Left },
            { BinaryOperatorType.ShiftRightEqual, OperatorAssociativity.Right },
            { BinaryOperatorType.Star, OperatorAssociativity.Left },
            { BinaryOperatorType.StarEqual, OperatorAssociativity.Right }
        };
        public static Dictionary<UnaryPreOperatorType, OperatorAssociativity> UnaryPreOperatorAssociativityTable = new Dictionary<UnaryPreOperatorType, OperatorAssociativity>()
        {
            { UnaryPreOperatorType.Ampersand, OperatorAssociativity.Right },
            { UnaryPreOperatorType.DoublePlus, OperatorAssociativity.Left },
            { UnaryPreOperatorType.DoubleMinus, OperatorAssociativity.Left },
            { UnaryPreOperatorType.Exclamation, OperatorAssociativity.Right },
            { UnaryPreOperatorType.Minus, OperatorAssociativity.Right },
            { UnaryPreOperatorType.Star, OperatorAssociativity.Right },
            { UnaryPreOperatorType.Tilde, OperatorAssociativity.Right }
        };
        public static Dictionary<UnaryPostOperatorType, OperatorAssociativity> UnaryPostOperatorAssociativityTable = new Dictionary<UnaryPostOperatorType, OperatorAssociativity>()
        {
            { UnaryPostOperatorType.DoubleMinus, OperatorAssociativity.Left },
            { UnaryPostOperatorType.DoublePlus, OperatorAssociativity.Left }
        };
        #endregion
        #region Operator/Token Tables
        public static Dictionary<TokenType, BinaryOperatorType> BinaryOperatorTokenTable = new Dictionary<TokenType, BinaryOperatorType>()
        {
            { TokenType.Ampersand, BinaryOperatorType.Ampersand },
            { TokenType.AmpersandEqual, BinaryOperatorType.AmpersandEqual },
            { TokenType.Caret, BinaryOperatorType.Caret },
            { TokenType.CaretEqual, BinaryOperatorType.CaretEqual },
            { TokenType.Divide, BinaryOperatorType.Divide },
            { TokenType.DivideEqual, BinaryOperatorType.DivideEqual },
            { TokenType.DoubleAmpersand, BinaryOperatorType.DoubleAmpersand },
            { TokenType.DoubleEqual, BinaryOperatorType.DoubleEqual },
            { TokenType.DoublePipe, BinaryOperatorType.DoublePipe },
            { TokenType.Equal, BinaryOperatorType.Equal },
            { TokenType.GreaterThan, BinaryOperatorType.Greater },
            { TokenType.GreaterThanEqual, BinaryOperatorType.GreaterEqual },
            { TokenType.LessThan, BinaryOperatorType.Less },
            { TokenType.LessThanEqual, BinaryOperatorType.LessEqual },
            { TokenType.Minus, BinaryOperatorType.Minus },
            { TokenType.MinusEquals, BinaryOperatorType.MinusEqual },
            { TokenType.Percent, BinaryOperatorType.Mod },
            { TokenType.PercentEqual, BinaryOperatorType.ModEqual },
            { TokenType.NotEqual, BinaryOperatorType.NotEqual },
            { TokenType.Pipe, BinaryOperatorType.Pipe },
            { TokenType.PipeEquals, BinaryOperatorType.PipeEqual },
            { TokenType.Plus, BinaryOperatorType.Plus },
            { TokenType.PlusEquals, BinaryOperatorType.PlusEqual },
            { TokenType.ShiftLeft, BinaryOperatorType.ShiftLeft },
            { TokenType.ShiftLeftEqual, BinaryOperatorType.ShiftLeftEqual },
            { TokenType.ShiftRight, BinaryOperatorType.ShiftRight },
            { TokenType.ShiftRightEqual, BinaryOperatorType.ShiftRightEqual },
            { TokenType.Star, BinaryOperatorType.Star },
            { TokenType.StarEquals, BinaryOperatorType.StarEqual }
        };
        public static Dictionary<TokenType, UnaryPreOperatorType> UnaryPreOperatorTokenTable = new Dictionary<TokenType, UnaryPreOperatorType>()
        {
            { TokenType.Ampersand, UnaryPreOperatorType.Ampersand },
            { TokenType.DoublePlus, UnaryPreOperatorType.DoublePlus },
            { TokenType.DoubleMinus, UnaryPreOperatorType.DoubleMinus  },
            { TokenType.Exclamation, UnaryPreOperatorType.Exclamation },
            { TokenType.Minus, UnaryPreOperatorType.Minus },
            { TokenType.Star, UnaryPreOperatorType.Star },
            { TokenType.Tilde, UnaryPreOperatorType.Tilde }
        };
        public static Dictionary<TokenType, UnaryPostOperatorType> UnaryPostOperatorTokenTable = new Dictionary<TokenType, UnaryPostOperatorType>()
        {
            { TokenType.DoublePlus, UnaryPostOperatorType.DoublePlus },
            { TokenType.DoubleMinus, UnaryPostOperatorType.DoubleMinus }
        };
        #endregion
        #region Misc
        public const int MinPrecedence = 15;
        public const int CastPrecedence = 2;
        #endregion

        public static bool IsBinaryOperator(Token[] tokens, int i)
        {
            return BinaryOperatorTokenTable.ContainsKey(tokens[i].Type);
        }
        public static bool IsUnaryPreOperator(Token[] tokens, int i)
        {
            return UnaryPreOperatorTokenTable.ContainsKey(tokens[i].Type);
        }
        public static bool IsUnaryPostOperator(Token[] tokens, int i)
        {
            return UnaryPostOperatorTokenTable.ContainsKey(tokens[i].Type);
        }
    }

    public enum OperatorAssociativity
    {
        Left,
        Right
    }
    /*

    */
    public enum BinaryOperatorType
    {
        Ampersand,
        AmpersandEqual,
        Caret,
        CaretEqual,
        Divide,
        DivideEqual,
        DoubleAmpersand,
        DoubleEqual,
        DoublePipe,
        Equal,
        Greater,
        GreaterEqual,
        Less,
        LessEqual,
        Minus,
        MinusEqual,
        Mod,
        ModEqual,
        NotEqual,
        Pipe,
        PipeEqual,
        Plus,
        PlusEqual,
        ShiftLeft,
        ShiftLeftEqual,
        ShiftRight,
        ShiftRightEqual,
        Star,
        StarEqual
    }
    public enum UnaryPreOperatorType
    {
        Ampersand,
        DoublePlus,
        DoubleMinus,
        Exclamation,
        Minus,
        Star,
        Tilde
    }
    public enum UnaryPostOperatorType
    {
        DoublePlus,
        DoubleMinus
    }
}
