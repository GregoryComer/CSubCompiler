using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    /// <summary>
    /// Pops top two values from the general stack, pushes A & B
    /// </summary>
    public struct ILAnd : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A | B
    /// </summary>
    public struct ILOr : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A ^ B
    /// </summary>
    public struct ILXor : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A << B
    /// </summary>
    public struct ILShl : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top two values from the general stack, pushes A >> B
    /// </summary>
    public struct ILShr : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
    /// <summary>
    /// Pops top value from the general stack. Pushes ~A
    /// </summary>
    public struct ILNot : IILGeneralInstruction
    {
        public GeneralOperandSize OperandSize;
    }
}