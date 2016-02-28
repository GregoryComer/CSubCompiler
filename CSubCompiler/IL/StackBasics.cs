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
    public struct ILLoadCG : IILGeneralInstruction
    {
        public long Constant;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Pushes the value of a variable onto the general stack
    /// </summary>
    public struct ILLoadAG : IILGeneralInstruction
    {
        public ILVariable Variable;
    }
    /// <summary>
    /// Pushes the value at [A] onto the general stack
    /// </summary>
    public struct ILLoadRG : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pushes the value at [A + B] onto the general stack
    /// </summary>
    public struct ILLoadROG : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pushes the value at [A + B * Scale] onto the general stack
    /// </summary>
    public struct ILLoadROSG : IILGeneralInstruction
    {
        public AddressingScaleFactor Scale;
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops the top value off the stack into Destination
    /// </summary>
    public struct ILStoreG : IILGeneralInstruction
    {
        public ILDestination Destination;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Stores the value on the top of the general stack into Destination without popping
    /// </summary>
    public struct ILPeekG : IILGeneralInstruction
    {
        public ILDestination Destination;
        public GeneralOperandSize Size;
    }
    /// <summary>
    /// Swaps the value at general stack positions A and B
    /// </summary>
    public struct ILExchangeG : IILGeneralInstruction
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
        public FloatOperandSize Size;
    }
    /// <summary>
    /// Pushes the value of a variable onto the floating point stack
    /// </summary>
    public struct ILLoadAF : IILFloatInstruction
    {
        public ILVariable Variable;
    }
    /// <summary>
    /// Pushes the value at [A] onto the floating point stack
    /// </summary>
    public struct ILLoadRF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pushes the value at [A + B] onto the floating point stack
    /// </summary>
    public struct ILLoadROF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pushes the value at [A + B * Scale] onto the floating point stack
    /// </summary>
    public struct ILLoadROSF : IILFloatInstruction
    {
        public AddressingScaleFactor Scale;
    }
    /// <summary>
    /// Pops the top value off the floating point stack into Destination
    /// </summary>
    public struct ILStoreF : IILFloatInstruction
    {
        public ILDestination Destination;
        public FloatOperandSize Size;
    }
    /// <summary>
    /// Stores the value on the top of the floating point stack into Destination without popping
    /// </summary>
    public struct ILPeekF : IILFloatInstruction
    {

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