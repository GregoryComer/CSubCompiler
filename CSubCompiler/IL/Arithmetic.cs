using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    #region General Instructions
    /// <summary>
    /// Pops top two values from the general stack, pushes A + B
    /// </summary>
    public struct ILAdd : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A - B
    /// </summary>
    public struct ILSub : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes B - A
    /// </summary>
    public struct ILRSub : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A * B (unsigned)
    /// </summary>
    public struct ILMul : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A * B (signed)
    /// </summary>
    public struct ILIMul : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A / B (unsigned)
    /// </summary>
    public struct ILDiv : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes B / A (unsigned)
    /// </summary>
    public struct ILRDiv : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A / B (signed)
    /// </summary>
    public struct ILIDiv : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes B / A (signed)
    /// </summary>
    public struct ILRIDiv : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top value from the general stack, pushes -A
    /// </summary>
    public struct ILNeg : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A + B + Carry
    /// </summary>
    public struct ILAdc : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A - B - Carry
    /// </summary>
    public struct ILSbb : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two value from the general stack, pushes A + 1
    /// </summary>
    public struct ILInc : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top value from the general stack, pushes A + 1
    /// </summary>
    public struct ILDec : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    #endregion

    #region Floating Point Instruction
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes A + B
    /// </summary>
    public struct ILAddF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes A - B
    /// </summary>
    public struct ILSubF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes B - A
    /// </summary>
    public struct ILRSubF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes A * B
    /// </summary>
    public struct ILMulF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes A / B
    /// </summary>
    public struct ILDivF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes B / A
    /// </summary>
    public struct ILRDivF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes |A|
    /// </summary>
    public struct ILAbsF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes -A
    /// </summary>
    public struct ILNegF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes round(A) (based on current rounding mode) TODO: Support getting/setting rounding mode
    /// </summary>
    public struct ILRoundF : IILFloatInstruction
    {
        
    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes sqrt(A)
    /// </summary>
    public struct ILSqrtF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes cos(A)
    /// </summary>
    public struct ILCosF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes sin(A)
    /// </summary>
    public struct ILSinF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pops the top value from the floating point stack, pushes tan(A)
    /// </summary>
    public struct ILTanF : IILFloatInstruction
    {

    }
    /// <summary>
    /// Pops the top two values from the floating point stack, pushes atan(A/B)
    /// </summary>
    public struct ILAtanF : IILFloatInstruction
    {

    } 
    #endregion
}
