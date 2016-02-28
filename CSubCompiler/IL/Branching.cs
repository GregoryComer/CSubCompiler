using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class Branching
    {
        #region General Instructions
        /// <summary>
        /// Pops the top two values from the general stack. Branches to label if A < B
        /// </summary>
        public struct ILBlG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the general stack. Branches to label if A <= B
        /// </summary>
        public struct ILBleG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the general stack. Branches to label if A == B
        /// </summary>
        public struct ILBeG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the general stack. Branches to label if A != B
        /// </summary>
        public struct ILBneG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the general stack. Branches to label if A > B
        /// </summary>
        public struct ILBgG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the general stack. Branches to label if A >= B
        /// </summary>
        public struct ILBgeG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top vlaue from the general stack. Branches to label if A == 0
        /// </summary>
        public struct ILBzG : IILGeneralInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top vlaue from the general stack. Branches to label if A != 0
        /// </summary>
        public struct ILBnzG : IILGeneralInstruction
        {
            public int Target;
        }
        #endregion
        #region Floating Point Instructions
        /// <summary>
        /// Pops the top two values from the float stack. Branches to label if A < B
        /// </summary>
        public struct ILBlF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the float stack. Branches to label if A <= B
        /// </summary>
        public struct ILBleF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the float stack. Branches to label if A == B
        /// </summary>
        public struct ILBeF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the float stack. Branches to label if A != B
        /// </summary>
        public struct ILBneF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the float stack. Branches to label if A > B
        /// </summary>
        public struct ILBgF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top two values from the float stack. Branches to label if A >= B
        /// </summary>
        public struct ILBgeF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top vlaue from the float stack. Branches to label if A == 0
        /// </summary>
        public struct ILBzF : IILFloatInstruction
        {
            public int Target;
        }
        /// <summary>
        /// Pops the top vlaue from the float stack. Branches to label if A != 0
        /// </summary>
        public struct ILBnzF : IILFloatInstruction
        {
            public int Target;
        }
        #endregion
    }
}
