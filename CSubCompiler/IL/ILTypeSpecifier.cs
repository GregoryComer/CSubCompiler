using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public struct ILTypeSpecifier
    {
        public ILTypeSpecifier(string name, ILTypeCategory category, int pointerDepth)
        {
            Category = category;
            Name = name;
            PointerDepth = pointerDepth;
        }

        public ILTypeCategory Category;
        public string Name;
        public int PointerDepth;
    }

    public enum ILTypeCategory
    {
        Base,
        Struct,
        Enum
    }
}
