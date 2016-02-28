using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public struct ILSignEx
    {
        GeneralOperandSize DestinationSize;
        GeneralOperandSize SourceSize;
    }
    public struct ILZeroEx
    {
        GeneralOperandSize DestinationSize;
        GeneralOperandSize SourceSize;
    }

    public enum AddressingScaleFactor
    {
        One,
        Two,
        Four,
        Eight
    }
    public enum FloatOperandSize
    {
        Size32
    }
    public enum GeneralOperandSize
    {
        Size8,
        Size16,
        Size32,
        Size64
    }
}
