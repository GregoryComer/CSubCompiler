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
    public struct ILAddG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A - B
    /// </summary>
    public struct ILSubG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes B - A
    /// </summary>
    public struct ILRSubG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A * B (unsigned)
    /// </summary>
    public struct ILMulG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A * B (signed)
    /// </summary>
    public struct ILIMulG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A / B (unsigned)
    /// </summary>
    public struct ILDivG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes B / A (unsigned)
    /// </summary>
    public struct ILRDivG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A / B (signed)
    /// </summary>
    public struct ILIDivG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes B / A (signed)
    /// </summary>
    public struct ILRIDivG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top value from the general stack, pushes -A
    /// </summary>
    public struct ILNegG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A + B + Carry
    /// </summary>
    public struct ILAdcG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A - B - Carry
    /// </summary>
    public struct ILSbbG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top two value from the general stack, pushes A + 1
    /// </summary>
    public struct ILIncG : IILGeneralInstruction
    {

    }
    /// <summary>
    /// Pops top value from the general stack, pushes A + 1
    /// </summary>
    public struct ILDecG : IILGeneralInstruction
    {

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
