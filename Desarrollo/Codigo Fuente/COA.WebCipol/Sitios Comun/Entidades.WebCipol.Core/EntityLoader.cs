using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// The implementation of the entity loader class
    /// </summary>
    public static class EntityLoader
    {
        /// <summary>
        /// Loads an entity list as inferred by the
        /// dataset provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public static List<T> Load<T>(DataSet dataSet)
        {
            List<T> entityList = new List<T>();
            FillEntityList(dataSet, 0, entityList);
            return entityList;
        }

        public static List<T> Load<T>(DataSet dataSet, string NombreTabla)
        {
            List<T> entityList = new List<T>();
            int Index = 0;
            //Obtiene el index de la tabla.
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                if (dataSet.Tables[i].TableName.ToUpper() == NombreTabla.ToUpper())
                {
                    Index = i;
                    break;
                }
            }

            FillEntityList(dataSet, Index, entityList);
            return entityList;
        }

        /// <summary>
        /// Fills an entity list provided
        /// </summary>
        /// <param name="dataSet">The dataset with the data</param>
        /// <param name="entityList">The entity list to populate</param>
        /// <returns></returns>
        static object FillEntityList(DataSet dataSet, int currentTable, object entityList)
        {
            // Generate the column map for the current table
            Type entityType = entityList.GetEntityTypeFromEntityList();

            Dictionary<Type, ColumnPropertyMap> entityColumnMaps = ColumnPropertyMap.CreateMapsForEntity(dataSet.Tables[currentTable], entityType);

            // For each row in the table, try to populate an entity
            for (int r = 0; r < dataSet.Tables[currentTable].Rows.Count; r++)
            {
                bool couldFillEntity = false;

                // Create an entity
                object entity = ObjectBuilder.Create(entityType);

                // Fill all non-entity properties in the entity with row columns
                couldFillEntity = FillEntity(dataSet, currentTable, r, entity, entityColumnMaps[entityType]);

                // Fill all the entity properties
                foreach (EntityPropertyInfo entityProperty in entity.GetEntityProperties())
                {
                    object containedEntity = ObjectBuilder.Create(entityProperty.PropertyInfo.PropertyType);
                    if (FillEntity(dataSet, currentTable, r, containedEntity, entityColumnMaps[entityProperty.PropertyInfo.PropertyType]))
                    {
                        if (entityProperty.PropertyAccess.CanWrite)
                        {
                            entityProperty.PropertyAccess.Set(entity, containedEntity);
                            couldFillEntity = true;
                        }
                    }
                }

                // Fill all the entity list properties
                foreach (EntityPropertyInfo entityListProperty in entity.GetEntityListProperties())
                {
                    // Get the specified data table for the curret entity list
                    int entityListTable = entityListProperty.DataTable == null ? currentTable : entityListProperty.DataTable.GetEffectiveDataTable(currentTable);

                    // If a data table index is explicitly specified load the data off of that
                    // In case we have an entity list but there is no table specified
                    // we can't do much.
                    if (entityListTable != currentTable)
                    {
                        // Load the entity list
                        object containedEntityList = ObjectBuilder.Create(entityListProperty.PropertyInfo.PropertyType);

                        // Fill it based on the table specified
                        FillEntityList(dataSet, entityListTable, containedEntityList);

                        // ... and finaly save it back
                        if (entityListProperty.PropertyAccess.CanWrite)
                            entityListProperty.PropertyAccess.Set(entity, containedEntityList);
                    }
                }

                if (couldFillEntity)
                    ((IList)entityList).Add(entity);
            }

            return entityList;
        }

        /// <summary>
        /// Fills an entity's basic properties
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="whichTable"></param>
        /// <param name="whichRow"></param>
        /// <param name="entityType"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        static bool FillEntity(DataSet dataSet, int currentTable, int whichRow, object entity, ColumnPropertyMap map)
        {
            bool couldFillEntity = false;

            DataRow currentRow = dataSet.Tables[currentTable].Rows[whichRow];

            // For every mapped column
            // set the value of the property
            foreach (var column in map.Keys)
            {
                if (!DBNull.Value.Equals(currentRow[column]))
                {
                    object valueToSet = currentRow[column];

                    // If a custom converter is specified
                    // use it to convert the data
                    if (null != map[column].DataConverter)
                        valueToSet = map[column].DataConverter.Converter.GetEntity(valueToSet);

                    if (map[column].PropertyAccess.CanWrite)
                    {
                        map[column].PropertyAccess.Set(entity, valueToSet);
                        couldFillEntity = true;
                    }
                }
            }

            // We return true if at least one basic
            // property was able to be filled
            return couldFillEntity;
        }
    }
}
