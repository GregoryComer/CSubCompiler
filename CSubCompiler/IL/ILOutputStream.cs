using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class ILOutputStream
    {
        List<IILInstruction> data;

        public ILOutputStream()
        {
            data = new List<IILInstruction>();
        }

        public IILInstruction[] GetBuffer()
        {
            return data.ToArray();
        }
        public void Write(IILInstruction instruction)
        {
            data.Add(instruction);
        }
        public void Write(IEnumerable<IILInstruction> instructions)
        {
            data.AddRange(instructions);
        }

        internal void Write(ILNBool iLNBool)
        {
            throw new NotImplementedException();
        }
    }
}
