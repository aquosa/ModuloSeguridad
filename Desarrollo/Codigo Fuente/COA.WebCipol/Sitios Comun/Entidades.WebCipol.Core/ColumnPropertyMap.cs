using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace COA.WebCipol.Entidades.Core
{
    internal sealed class ColumnPropertyMap : Dictionary<string, EntityPropertyInfo>
    {
        private ColumnPropertyMap() { }

        /// <summary>
        /// Creates a column property mapping for the specified
        /// data table and entity
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static ColumnPropertyMap Create(DataTable sourceTable, Type entityType)
        {
            ColumnPropertyMap map = new ColumnPropertyMap();
            List<EntityPropertyInfo> properties = EntityInfoCache.Get(entityType);
            foreach (DataColumn column in sourceTable.Columns)
            {
                foreach (EntityPropertyInfo property in properties)
                {
                    if (property.IsEntity || property.IsEntityList) continue;

                    if (property.EffectiveDataColumnName.ToUpper().Trim() == column.ColumnName.ToUpper().Trim())
                        map.Add(column.ColumnName, property);
                }
            }
            return map;
        }

        /// <summary>
        /// Creates all column property maps for a given entity
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static Dictionary<Type, ColumnPropertyMap> CreateMapsForEntity(DataTable sourceTable, Type entityType)
        {
            Dictionary<Type, ColumnPropertyMap> entityColumnMaps = new Dictionary<Type, ColumnPropertyMap>();
            List<EntityPropertyInfo> properties = EntityInfoCache.Get(entityType);

            // The first map is for the entity
            entityColumnMaps.Add(entityType, Create(sourceTable, entityType));

            // The next maps are for entity properties in the parent entity object
            foreach (EntityPropertyInfo entityProperty in properties)
            {
                if (entityProperty.IsEntity)
                    entityColumnMaps.Add(entityProperty.PropertyInfo.PropertyType, Create(sourceTable, entityProperty.PropertyInfo.PropertyType));
            }

            return entityColumnMaps;
        }
    }
}
