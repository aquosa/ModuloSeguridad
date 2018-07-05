using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace COA.WebCipol.Entidades.Core
{
    internal class EntityPropertyInfo
    {
        /// <summary>
        /// The type of a generic list
        /// </summary>
        public static Type GenericListType = typeof(List<>);

        public string PropertyName { get { return PropertyInfo.Name; } }
        public PropertyInfo PropertyInfo { get; private set; }
        public DataColumnAttribute DataColumn { get; private set; }
        public DataTableAttribute DataTable { get; private set; }
        public DataConverterAttribute DataConverter { get; private set; }
        public bool DataInclude { get; private set; }
        public bool DataExclude { get; private set; }
        public FastProperty PropertyAccess { get; private set; }

        /// <summary>
        /// Gets the effective data column name for the current property
        /// </summary>
        public string EffectiveDataColumnName
        {
            get
            {
                if (null != DataColumn)
                    return DataColumn.ColumnName;
                else
                    return PropertyName;
            }
        }
        /// <summary>
        /// Determines if this is an entity
        /// </summary>
        public bool IsEntity
        {
            get { return PropertyInfo.PropertyType.IsEntity() && !PropertyInfo.PropertyType.IsEnum; }
        }

        /// <summary>
        /// Determines if the property is a entity list
        /// </summary>
        public bool IsEntityList
        {
            get
            {
                // Check for generic List<Entity> property
                if (PropertyInfo.PropertyType.Name.Equals(GenericListType.Name))
                {
                    Type containedEntityType = PropertyInfo.PropertyType.GetEntityTypeFromEntityListType();
                    return containedEntityType.IsEntity() && !containedEntityType.IsEnum;
                }
                return false;
            }
        }

        public EntityPropertyInfo(object o, string propertyName) : this(o.GetType().GetProperty(propertyName)){}

        /// <summary>
        /// Contructs an instance of the Entity property info
        /// </summary>
        /// <param name="propertyInfo"></param>
        public EntityPropertyInfo(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            DataColumn = PropertyInfo.GetCustomAttribute<DataColumnAttribute>();
            DataTable = PropertyInfo.GetCustomAttribute<DataTableAttribute>();
            DataConverter = PropertyInfo.GetCustomAttribute<DataConverterAttribute>();
            DataInclude = null != PropertyInfo.GetCustomAttribute<DataIncludeAttribute>();
            DataExclude = null != PropertyInfo.GetCustomAttribute<DataExcludeAttribute>();
            PropertyAccess = new FastProperty(PropertyInfo);            

            if (DataInclude == true && DataInclude == DataExclude)
                throw new Exception("A property cannot have both DataInclude and DataExclude attributes applied");
        }

        /// <summary>
        /// Gets the entity property info collection for the specfied object
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static List<EntityPropertyInfo> GetEntityPropertyInfoList(Type entityType)
        {
            List<EntityPropertyInfo> properties = new List<EntityPropertyInfo>();
            InspectionPolicy defaultPolicy = InspectionPolicy.OptOut;
            EntityLoaderAttribute inspectionPolicy = entityType.GetCustomAttribute<EntityLoaderAttribute>();
            if (null != inspectionPolicy)
                defaultPolicy = inspectionPolicy.InspectionPolicy;

            foreach (PropertyInfo property in entityType.GetProperties())
            {
                EntityPropertyInfo info = new EntityPropertyInfo(property);
                // If we have no inspection policy, by default include all properties
                if (null == inspectionPolicy)
                {
                    properties.Add(info);
                }
                else
                {
                    switch (defaultPolicy)
                    {
                        case InspectionPolicy.OptIn:
                            if (info.DataInclude) properties.Add(info);
                            break;
                        case InspectionPolicy.OptOut:
                            if (!info.DataExclude) properties.Add(info);
                            break;
                        default:
                            break;
                    }
                }
            }

            return properties;
        }
    }
}
