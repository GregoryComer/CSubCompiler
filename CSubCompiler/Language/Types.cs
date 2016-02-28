using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.Language
{
    public static class Types
    {
        private static Dictionary<string, BaseType> baseTypeNameMapping = new Dictionary<string, BaseType>
        {
            { "char", BaseType.Char },
            { "short", BaseType.Short },
            { "int", BaseType.Int },
            { "long", BaseType.Long },
            { "float", BaseType.Float }
        };

        private static BaseType[] IntegralTypes = new BaseType[] { BaseType.Char, BaseType.Short, BaseType.Int, BaseType.Long };

        public static BaseType GetBaseTypeByName(string name)
        {
            if (!baseTypeNameMapping.ContainsKey(name))
                throw new ArgumentException("Invalid base type.");
            return baseTypeNameMapping["name"];
        }

        public static bool IsBaseType(string name)
        {
            return baseTypeNameMapping.ContainsKey(name);
        }
        
        public static bool IsIntegralType(BaseType type)
        {
            return IntegralTypes.Contains(type);
        }
    }
    public enum BaseType
    {
        Char,
        UChar,
        Short,
        UShort,
        Int,
        UInt,
        Long,
        ULong,
        Float,
        Double
    }
}
