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
    public struct ILLoadC : IILGeneralInstruction
    {
        public long Constant;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Pushes the value at [Address] onto the general stack
    /// </summary>
    public struct ILLoadR : IILGeneralInstruction
    {
        public ulong Address;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Pops the top value off of the general stack
    /// Pushes the value at [Top + Address] onto the general stack
    /// </summary>
    public struct ILLoadRO : IILGeneralInstruction
    {
        public ulong Address;
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops the top value off of the general stack
    /// Pushes the value at [Address + Top * Scale] onto the general stack
    /// </summary>
    public struct ILLoadROS : IILGeneralInstruction
    {
        public ulong Address;
        public AddressingScaleFactor Scale;
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops the top value off the stack into [Address]
    /// </summary>
    public struct ILStore : IILGeneralInstruction
    {
        public ulong Address;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Stores the value on the top of the general stack into [Address] without popping
    /// </summary>
    public struct ILPeek : IILGeneralInstruction
    {
        public ulong Address;
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
    public struct ILLoadCF : IILFloatInstruction
    {
        public double Constant;
    }
    /// <summary>
    /// Pushes the value at [Address] onto the floating point stack
    /// </summary>
    public struct ILLoadRF : IILFloatInstruction
    {
        public ulong Address;
        public FloatOperandSize OperandSize;
    }
    /// <summary>
    /// Pops the top vlaue off of the general stack
    /// Pushes the value at [Address + Top] onto the floating point stack
    /// </summary>
    public struct ILLoadROF : IILFloatInstruction
    {
        public ulong Address;
        public FloatOperandSize OperandSize;
    }
    /// <summary>
    /// Pops the top value off of the stack
    /// Pushes the value at [Adress + Top * Scale] onto the floating point stack
    /// </summary>
    public struct ILLoadROSF : IILFloatInstruction
    {
        public ulong Address;
        public AddressingScaleFactor Scale;
    }
    /// <summary>
    /// Pops the top value off the floating point stack into [Address]
    /// </summary>
    public struct ILStoreF : IILFloatInstruction
    {
        public ulong Address;
        public FloatOperandSize Size;
    }
    /// <summary>
    /// Stores the value on the top of the floating point stack into [Address] without popping
    /// </summary>
    public struct ILPeekF : IILFloatInstruction
    {
        public ulong Address;
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