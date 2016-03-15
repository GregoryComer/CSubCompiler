using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public struct ILSignEx
    {
        public GeneralOperandSize DestinationSize;
        public GeneralOperandSize SourceSize;
    }
    public struct ILZeroEx
    {
        public GeneralOperandSize DestinationSize;
        public GeneralOperandSize SourceSize;
    }
    /// <summary>
    /// Pops the top value off of the general stack. Pushes 1 if A != 0, otherwise pushes 0
    /// </summary>
    public struct ILBool
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops the top value off of the general stack. Pushes 0 if A != 0, otherwise pushes 1
    /// </summary>
    public struct ILNBool
    {
        public GeneralOperandSize OperandSize;
    }

    public enum AddressingScaleFactor
    {
        One = 1,
        Two = 2,
        Four = 4,
        Eight = 8
    }
    public enum FloatOperandSize
    {
        Size32 = 32,
        Size64 = 64
    }
    public enum GeneralOperandSize
    {
        Size8 = 8,
        Size16 = 16,
        Size32 = 32,
        Size64 = 64
    }
}
