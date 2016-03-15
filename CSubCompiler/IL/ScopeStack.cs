using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSubCompiler.IL
{
    public class ScopeStack : Stack<ILScope>
    {
        public new void Push(ILScope scope)
        {
            if (Count > 0)
                scope.ParentScope = Peek();
            base.Push(scope);
        }
    }
}
