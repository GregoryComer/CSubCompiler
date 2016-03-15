using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class ILScope
    {
        public ILScope ParentScope { get; set; }
        public Dictionary<string, ILType> Structs;
        public Dictionary<string, ILTypeReference> Typedefs;
        public Dictionary<string, ILTypeReference> Variables;
        public List<string> EnumNames;
        public Dictionary<string, int> EnumValues;

        public ILScope()
        {
            Structs = new Dictionary<string, ILType>();
            Typedefs = new Dictionary<string, ILTypeReference>();
            Variables = new Dictionary<string, ILTypeReference>();
            EnumNames = new List<string>();
            EnumValues = new Dictionary<string, int>();
        }

        public void RegisterStruct(ILGenerationContext context, ILStructType type)
        {
            if (Structs.ContainsKey(type.Name))
                throw new ParserException(string.Format("Duplicate definition for type struct \"{0}\".", type.Name), context.CurrentNode.TokenIndex, context.CurrentNode.Token);
            Structs.Add(type.Name, type);
        }

        public void RegisterTypedef(ILGenerationContext context, string name, ILTypeReference type)
        {
            if (Typedefs.ContainsKey(name))
                throw new ParserException(string.Format("Duplicate definition for type \"{0}\".", name), context.CurrentNode.TokenIndex, context.CurrentNode.Token);
            Typedefs.Add(name, type);
        }

        public void RegisterVariable(ILGenerationContext context, string name, ILTypeReference type)
        {
            if (Variables.ContainsKey(name))
                throw new ParserException(string.Format("Duplicate definition for variable \"{0}\".", name), context.CurrentNode.TokenIndex, context.CurrentNode.Token);
        }
    }
}
