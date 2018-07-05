using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Data;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Implementation of logic to convert a List<T> to a DataSet
    /// </summary>
    public static class DataSetLoader
    {
        /// <summary>
        /// Converts the list to a dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <returns></returns>   
        public static DataSet CreateDataSet<T>(List<T> entityList)
        {
            DataSet dataSet = new DataSet();

            Type entityType = entityList.GetEntityTypeFromEntityList();
            List<EntityPropertyInfo> entityProperties = EntityInfoCache.Get(entityType);

            // Part 1
            // Generate data columns for all simple and single entity properties
            List<DataColumn> dataColumns = GetDataColumns(entityProperties);

            DataTable defaultTable = new DataTable();
            dataSet.Tables.Add(defaultTable);
            dataColumns.ForEach(c => defaultTable.Columns.Add(c));

            // Populate the default table with data
            LoadData(defaultTable, entityList);

            // Part 2
            // Create data tables for all entity lists
            // and populate them with data columns
            T entityInstance = (from e in entityList where null != e select e).FirstOrDefault();
            entityInstance                
                .GetEntityListProperties()
                .Where(p => null != p.DataTable)
                .OrderBy(p => p.DataTable.TableIndex)
                .ToList()
                .ForEach(p =>
                    dataSet.Tables
                        .Add(p.PropertyName)
                        .Columns.AddRange(
                            GetDataColumns(EntityInfoCache.Get(p.PropertyInfo.PropertyType)
                        )
                        .ToArray()
                    )
                );
            // Now popuplate each extra table with the details
            entityInstance                
                .GetEntityListProperties()
                .Where(p => null != p.DataTable)
                .ToList()
                .ForEach(p => 
                    entityList.ForEach(
                        e => LoadData(dataSet.Tables[p.PropertyName], ((IList)p.PropertyAccess.Get(e)))
                    )
                );            
                
            
            return dataSet;
        }

        /// <summary>
        /// Loads data off of a data table to an entity list
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="entityList"></param>
        private static void LoadData(DataTable dataTable, IList entityList)
        {
            if (null == entityList) return;

            foreach (var item in entityList)
            {
                dataTable.Rows.Add(GetEntityData(item.GetType(), item));
            }
        }

        /// <summary>
        /// Gets a list of data columns from the given entity properties
        /// </summary>
        /// <param name="entityProperties"></param>
        /// <returns></returns>
        static List<DataColumn> GetDataColumns(List<EntityPropertyInfo> entityProperties)
        {
            List<DataColumn> dataColumns = new List<DataColumn>();

            // Add all the simple property data columns
            entityProperties
                .Where(p => !p.IsEntity)
                .ToList()
                .ConvertAll(p => CreateDataColumn(p))
                .ForEach(c => dataColumns.Add(c));

            // Now add all the complex data columns
            entityProperties
                .Where(p => p.IsEntity)
                .ToList()
                .ConvertAll(p => EntityInfoCache.Get(p.PropertyInfo.PropertyType))
                .ForEach(pList => dataColumns.AddRange(GetDataColumns(pList)));

            return dataColumns;
        }

        /// <summary>
        /// Creates a data column for the specified entity property
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        static DataColumn CreateDataColumn(EntityPropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyInfo.PropertyType.IsEntity() && !propertyInfo.PropertyInfo.PropertyType.IsEnum)
                throw new ArgumentException("Only basic properties can be used to create a DataColumn", "propertyInfo");

            Type dataColumnType = propertyInfo.PropertyInfo.PropertyType;
            if (null != propertyInfo.DataConverter)
                dataColumnType = propertyInfo.DataConverter.Converter.DataType;

            DataColumn column = new DataColumn(propertyInfo.EffectiveDataColumnName, dataColumnType);
            return column;
        }

        /// <summary>
        /// Returns the entity data present in properties
        /// as an array
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        static object[] GetEntityData(Type entityType, object entity)
        {
            List<object> data = new List<object>();

            List<EntityPropertyInfo> entityProperties = EntityInfoCache.Get(entityType);

            if (null == entity)
            {
                entityProperties
                    .Where(p => !p.IsEntity)
                    .ToList()
                    .ForEach(p => data.Add(null));
            }
            else
            {
                // Take all simple columns and add its data
                entityProperties
                    .Where(p => !p.IsEntity)
                    .ToList()
                    .ForEach(p => data.Add(GetEntityPropertyData(entity, p)));
            }

            // Now add data for each entity property column
            entityProperties
                .Where(p => p.IsEntity)
                .ToList()
                .ForEach(p => data.AddRange(GetEntityData(p.PropertyInfo.PropertyType, p.PropertyAccess.Get(entity))));


            return data.ToArray();
        }

        /// <summary>
        /// Gets the final data to be saved into the datatable for the specified
        /// property
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        static object GetEntityPropertyData(object entity, EntityPropertyInfo property)
        {
            if (null == entity) return null;

            object propertyData = property.PropertyAccess.Get(entity);

            // If a specific converter is mentioned
            // use that to convert the data
            if (!property.IsEntity)
            {
                if (null != property.DataConverter)
                {
                    propertyData = property.DataConverter.Converter.GetEntityData(propertyData);
                }
            }
            return propertyData;
        }
    }
}
