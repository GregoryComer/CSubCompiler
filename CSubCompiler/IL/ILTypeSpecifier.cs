using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.Language;

namespace CSubCompiler.IL
{
    public abstract class ILTypeSpecifier
    {
        public ILTypeCategory Category;
    }
    public class ILBaseTypeSpecifier : ILTypeSpecifier
    {
        public BaseType Type;
    }
    public class ILEnumSpecifier : ILTypeSpecifier
    {
        
    }

    public enum ILTypeCategory
    {
        Base,
        Struct,
        Enum,
        Pointer
    }
}
