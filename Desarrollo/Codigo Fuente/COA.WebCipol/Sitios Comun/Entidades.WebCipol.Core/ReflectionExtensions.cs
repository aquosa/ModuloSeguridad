using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Core relection extensions
    /// </summary>
    internal static class ReflectionExtensions
    {
        /// <summary>
        /// Gets a custom attribute for the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this PropertyInfo property)
        {
            object[] attributes = property.GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
                return (T)attributes[0];
            else
                return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this Type entityType)
        {
            object[] attributes = entityType.GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
                return (T)attributes[0];
            else
                return default(T);
        }

        /// <summary>
        /// Determines if the current type is an entity type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static bool IsEntity(this Type entityType)
        {
            return EntityInspector.EntityProvider.IsEntity(entityType);
        }

        /// <summary>
        /// Gets the entity type from a list of entities
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public static Type GetEntityTypeFromEntityList(this object entityList)
        {
            Type entityListType = entityList.GetType();

            //// Check if we have an array
            //if (entityListType.IsArray && entityListType.HasElementType)
            //    return entityListType.GetElementType();

            // Be default we assume a generic list<>
            return entityList.GetType().GetGenericArguments()[0];
        }

        /// <summary>
        /// Gets the entity type from a list of entities
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public static Type GetEntityTypeFromEntityListType(this Type entityListType)
        {
            // Be default we assume a generic list<>
            return entityListType.GetGenericArguments()[0];
        }

        /// <summary>
        /// Gets all properties that are entities in the current parent entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static IEnumerable<EntityPropertyInfo> GetEntityProperties(this object entity)
        {
            List<EntityPropertyInfo> entityProperties = EntityInfoCache.Get(entity.GetType());
            foreach (EntityPropertyInfo property in entityProperties)
            {
                if (property.IsEntity)
                    yield return property;
            }
        }

        /// <summary>
        /// Gets all properties that are lists of entities for the parent entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static IEnumerable<EntityPropertyInfo> GetEntityListProperties(this object entity)
        {
            List<EntityPropertyInfo> entityProperties = EntityInfoCache.Get(entity.GetType());
            foreach (EntityPropertyInfo property in entityProperties)
            {
                if (property.IsEntityList)
                    yield return property;
            }
        }
    }
}
