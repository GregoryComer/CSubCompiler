using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSubCompiler.AST;
using CSubCompiler.Language;

namespace CSubCompiler.IL
{
    public class ILGenerationContext
    {
        public ILScope CurrentScope { get { return ScopeStack.Peek(); } }
        public Node CurrentNode { get; set; }
        public ILOutputStream Output { get; set; }
        public ScopeStack ScopeStack { get; set; }

        public ILGenerationContext()
        {
            Output = new ILOutputStream();
            ScopeStack = new ScopeStack();
            ILScope globalScope = new ILScope();
            ScopeStack.Push(globalScope);
        }

        public ILTypeReference ResolveTypeReference(TypeReferenceNode typeReferenceNode)
        {
            ILScope currentScope = CurrentScope;
            do
            {
                ILTypeReference? typeReference = TryResolveTypeReference(typeReferenceNode, currentScope);
                if (typeReference != null)
                    return typeReference.Value;
            } while ((currentScope = currentScope.ParentScope) != null);
            throw new ParserException(string.Format("Unable to resolve type \"{0}\".", typeReferenceNode.Name), typeReferenceNode.TokenIndex, typeReferenceNode.Token);
        }

        private ILTypeReference? TryResolveTypeReference(TypeReferenceNode typeReferenceNode, ILScope scope)
        {
            //Todo: Process embedded types
            switch (typeReferenceNode.Classification)
            {
                case TypeClassification.Standard:
                    if (Types.IsBaseType(typeReferenceNode.Name))
                    {
                        BaseType baseType = Types.GetBaseTypeByName(typeReferenceNode.Name);
                        return new ILTypeReference(new ILBaseType(baseType), typeReferenceNode.Modifiers);
                    }
                    else if (scope.Typedefs.ContainsKey(typeReferenceNode.Name))
                    {
                        ILTypeReference typedef = scope.Typedefs[typeReferenceNode.Name];
                        typedef.Modifiers |= typeReferenceNode.Modifiers;
                        return typedef;
                    }
                    else if (scope.EnumNames.Contains(typeReferenceNode.Name))
                    {
                        return new ILTypeReference(new ILBaseType(Types.GetEnumBaseType()), typeReferenceNode.Modifiers);
                    }
                    else
                    {
                        return null;
                    }
                case TypeClassification.Struct:
                    if (scope.Structs.ContainsKey(typeReferenceNode.Name))
                        return new ILTypeReference(scope.Structs[typeReferenceNode.Name], TypeModifiers.None);
                    else
                        return null;
                case TypeClassification.Pointer:
                    ILTypeReference? innerType = TryResolveTypeReference(typeReferenceNode.InnerType, scope);
                    if (innerType == null)
                        return null;
                    ILTypeReference returnType = new ILTypeReference(new ILPointerType(innerType.Value), typeReferenceNode.Modifiers);
                    return returnType;
                default:
                    throw new InternalCompilerException("Unexpected type classification.");
            }
        }
    }
}
