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

        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A | B
        /// </summary>
        public struct ILOrG : IILGeneralInstruction
        {

        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A ^ B
        /// </summary>
        public struct ILXorG : IILGeneralInstruction
        {

        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A << B
        /// </summary>
        public struct ILShlG : IILGeneralInstruction
        {

        }
        /// <summary>
        /// Pops top two values from the general stack, pushes A >> B
        /// </summary>
        public struct ILShrG : IILGeneralInstruction
        {

        }
    }
}
