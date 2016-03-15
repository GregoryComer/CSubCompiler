using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.Language
{
    public static class Types
    {
        private static Dictionary<BaseType, string> baseTypeNameMapping = new Dictionary<BaseType, string>
        {
            { BaseType.Char, "char" },
            { BaseType.UChar, "unsigned char" },
            { BaseType.Short, "short" },
            { BaseType.UShort, "unsigned short" },
            { BaseType.Int, "int" },
            { BaseType.UInt, "unsigned int" },
            { BaseType.Long, "long" },
            { BaseType.ULong, "unsigned long" },
            { BaseType.Float, "float" },
            { BaseType.Double, "double" }
        };

        public static Dictionary<BaseType, int> BaseTypeSizes = new Dictionary<BaseType, int>
        {
            { BaseType.Char, 1 },
            { BaseType.UChar, 1 },
            { BaseType.Short, 2 },
            { BaseType.UShort, 2 },
            { BaseType.Int, 4 },
            { BaseType.UInt, 4 },
            { BaseType.Long, 8 },
            { BaseType.ULong,8 },
            { BaseType.Float, 4 },
            { BaseType.Double, 8 }
        };

        private static BaseType[] IntegralTypes = new BaseType[] { BaseType.Char, BaseType.UChar, BaseType.Short, BaseType.UShort, BaseType.Int, BaseType.UInt, BaseType.Long, BaseType.ULong };
        private static BaseType[] FloatTypes = new BaseType[] { BaseType.Float, BaseType.Double };

        public static BaseType GetBaseTypeByName(string name)
        {
            if (!baseTypeNameMapping.ContainsValue(name))
                throw new ArgumentException("Invalid base type.");
            return baseTypeNameMapping.First(n => n.Value == name).Key;
        }
        public static string GetBaseTypeName(BaseType baseType)
        {
            return baseTypeNameMapping[baseType];
        }
        public static int GetBaseTypeSize(BaseType type)
        {
            return BaseTypeSizes[type];
        }
        public static BaseType GetEnumBaseType()
        {
            return BaseType.Int;
        }
        public static BaseType GetFloatTypeBySize(int size)
        {
            return FloatTypes.First(n => BaseTypeSizes[n] == size);
        }
        public static BaseType GetIntegralTypeBySize(int size)
        {
            return IntegralTypes.First(n => BaseTypeSizes[n] == size);
        }
        public static int GetPointerSize()
        {
            return 4;
        }
        public static bool IsBaseType(string name)
        {
            return baseTypeNameMapping.ContainsValue(name);
        }
        public static bool IsIntegralType(BaseType type)
        {
            return IntegralTypes.Contains(type);
        }
        public static bool IsFloatType(BaseType type)
        {
            return FloatTypes.Contains(type);
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
        Double,
        Pointer
    }
    [Flags]
    public enum TypeModifiers
    {
        None = 0x0,
        Const = 0x1,
        Extern = 0x2,
        Static = 0x4,
        Volatile = 0x8
    }
}
