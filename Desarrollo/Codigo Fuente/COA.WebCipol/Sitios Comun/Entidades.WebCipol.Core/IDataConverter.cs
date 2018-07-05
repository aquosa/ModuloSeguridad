using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    public interface IDataConverter
    {
        object GetEntity(object entityData);
        object GetEntityData(object entity);
        Type EntityType { get; }
        Type DataType { get; }
    }
}
