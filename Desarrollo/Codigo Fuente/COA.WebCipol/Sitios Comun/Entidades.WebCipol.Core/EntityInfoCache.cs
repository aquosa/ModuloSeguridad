using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// A simple entity cache
    /// </summary>
    internal sealed class EntityInfoCache : Dictionary<Type,List<EntityPropertyInfo>>
    {
        private EntityInfoCache() { }
        private static EntityInfoCache Cache = new EntityInfoCache();

        /// <summary>
        /// Gets the entity property info collection for the specified type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static List<EntityPropertyInfo> Get(Type entityType)
        {
            if (!Cache.ContainsKey(entityType))
            {
                Cache[entityType] = EntityPropertyInfo.GetEntityPropertyInfoList(entityType);
            }
            return Cache[entityType];
        }
    }
}
