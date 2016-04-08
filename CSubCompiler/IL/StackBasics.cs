using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    #region General Instructions
    /// <summary>
    /// Pushes a constant value to the general stack
    /// </summary>
    public struct ILLoad : IILGeneralInstruction
    {
        public ILAddressingReference Address;
        public GeneralOperandSize Size;
    }
    public struct ILStore : IILGeneralInstruction
    {
        public ILAddressingReference Address;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Stores the value on the top of the general stack into [Address] without popping
    /// </summary>
    public struct ILPeek : IILGeneralInstruction
    {
        public ILAddressingReference Address;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Swaps the value at general stack positions A and B
    /// </summary>
    public struct ILExchange : IILGeneralInstruction
    {
        public int Index1;
        public int Index2;
    }
    #endregion

    #region Floating Point Instructions
    /// <summary>
    /// Pushes a constant value to the floating point stack
    /// </summary>
    public struct ILLoadF : IILFloatInstruction
    {
        public ILAddressingReference Address;
        public FloatOperandSize Size;
    }
    public struct ILStoreF : IILFloatInstruction
    {
        public ILAddressingReference Address;
        public FloatOperandSize Size;
    }
    /// <summary>
    /// Stores the value on the top of the floating point stack into [Address] without popping
    /// </summary>
    public struct ILPeekF : IILFloatInstruction
    {
        public ILAddressingReference Address;
        public FloatOperandSize Size;
    }
    /// <summary>
    /// Swaps the value at floating point stack positions A and B
    /// </summary>
    public struct ILExchangeF : IILGeneralInstruction
    {
        public int Reg1;
        public int Reg2;
    } 
    #endregion
}