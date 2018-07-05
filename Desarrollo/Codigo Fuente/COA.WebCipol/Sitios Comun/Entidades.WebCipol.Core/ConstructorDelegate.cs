using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace COA.WebCipol.Entidades.Core
{
    public delegate object ConstructorDelegate();

    public static class ObjectBuilder
    {
        /// <summary>
        /// Creates a dynamic method based constructor for the specified type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        static ConstructorDelegate Constructor(Type entityType)
        {
            DynamicMethod method = new DynamicMethod(entityType.FullName + "_InstanceFactory" + entityType.Name, entityType, null, entityType, true);
            ILGenerator ilGen = method.GetILGenerator();
            ConstructorInfo constructor = entityType.GetConstructor(Type.EmptyTypes);
            // emit code to instantiate new instace of the type using the 'new' operator
            // (rather than rely on Activator.CreateInstance)
            ilGen.Emit(OpCodes.Newobj, constructor);
            ilGen.Emit(OpCodes.Ret);
            return method.CreateDelegate(typeof(ConstructorDelegate)) as ConstructorDelegate;
        }

        /// <summary>
        /// The static constructor cache for commonly used types
        /// </summary>
        static Dictionary<Type, ConstructorDelegate> ConstructorCache = new Dictionary<Type, ConstructorDelegate>();

        /// <summary>
        /// Creates an instance of the specified type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static object Create(Type entityType)
        {
            if (!ConstructorCache.ContainsKey(entityType))
            {
                ConstructorCache[entityType] = Constructor(entityType);
            }
            return ConstructorCache[entityType]();
        }
    }
}
