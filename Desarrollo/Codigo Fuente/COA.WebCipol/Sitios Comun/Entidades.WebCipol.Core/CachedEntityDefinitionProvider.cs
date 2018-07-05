using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Implements a simple cached entity definition provider
    /// </summary>
    public class CachedEntityDefinitionProvider : IEntityDefinitionProvider
    {
        /// <summary>
        /// The internal entity type cache
        /// </summary>
        static readonly List<Type> EntityTypes = new List<Type>();

        /// <summary>
        /// Determines if the specified type is an entity type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsEntity(Type t)
        {
            // If there are no types to check, we do not know
            // if the type is an entity or not
            if (EntityTypes.Count == 0) return false;

            // If the type is present in our type cache
            // its an entity type
            return EntityTypes.Contains(t);
        }

        /// <summary>
        /// Adds an entity type to the entity cache
        /// </summary>
        /// <param name="entityType"></param>
        public void AddEntityType(Type entityType)
        {
            if (!EntityTypes.Contains(entityType))
                EntityTypes.Add(entityType);
        }
    }
}
