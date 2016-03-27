using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class ILAddressingReference
    {
        public object Constant;
        public int Displacement;
        public ILAddressingMode Mode;
        public AddressingScaleFactor Scale;

        public static ILAddressingReference CreateConstant(object constant)
        {
            return new ILAddressingReference { Constant = constant, Mode = ILAddressingMode.Constant };
        }
        public static ILAddressingReference CreateConstAddress(int address)
        {
            return new ILAddressingReference { Displacement = address, Mode = ILAddressingMode.ConstAddress };
        }
        public static ILAddressingReference CreateIndirect()
        {
            return new ILAddressingReference { Mode = ILAddressingMode.Indirect };
        }
        public static ILAddressingReference CreateIndirect(int displacement)
        {
            return new ILAddressingReference { Displacement = displacement, Mode = ILAddressingMode.Indirect };
        }
        public static ILAddressingReference CreateIndirectScaled(AddressingScaleFactor scale)
        {
            return new ILAddressingReference { Scale = scale, Mode = ILAddressingMode.IndirectScaled };
        }
        public static ILAddressingReference CreateIndirectScaled(AddressingScaleFactor scale, int displacement)
        {
            return new ILAddressingReference { Scale = scale, Displacement = displacement, Mode = ILAddressingMode.IndirectScaled };
        }
    }

    public enum ILAddressingMode
    {
        Constant,
        ConstAddress,
        Indirect,
        IndirectScaled
    }
}
