using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// A base converter class to handle data conversion. This is a generic class
    /// that explcitly inmplements IDataConverter. This is because we can't have
    /// generic attributes in C#
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    /// <typeparam name="TDataType"></typeparam>
    public abstract class DataConverterBase<TEntityType, TDataType> : IDataConverter
    {
        #region IDataConverter Members

        public abstract TEntityType GetEntity(TDataType entityData);
        public abstract TDataType GetEntityData(TEntityType entity);
 
        #endregion

        object IDataConverter.GetEntity(object entityData)
        {
            return GetEntity((TDataType)entityData);
        }

        object IDataConverter.GetEntityData(object entity)
        {
            return GetEntityData((TEntityType)entity);
        }

        Type IDataConverter.EntityType
        {
            get { return typeof(TEntityType); }
        }

        Type IDataConverter.DataType
        {
            get { return typeof(TDataType); }
        }

    }
}
