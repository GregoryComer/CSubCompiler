using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class Bitwise
    {
        /// <summary>
        /// Pops top two values from the general stack, pushes A & B
        /// </summary>
        public struct ILAndG : IILGeneralInstruction
        {
            public GeneralOperandSize OperandSize;
        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A | B
        /// </summary>
        public struct ILOrG : IILGeneralInstruction
        {
            public GeneralOperandSize OperandSize;
        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A ^ B
        /// </summary>
        public struct ILXorG : IILGeneralInstruction
        {
            public GeneralOperandSize OperandSize;
        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A << B
        /// </summary>
        public struct ILShlG : IILGeneralInstruction
        {
            public GeneralOperandSize OperandSize;
        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A >> B
        /// </summary>
        public struct ILShrG : IILGeneralInstruction
        {
            public GeneralOperandSize OperandSize;
        }
    }
}
