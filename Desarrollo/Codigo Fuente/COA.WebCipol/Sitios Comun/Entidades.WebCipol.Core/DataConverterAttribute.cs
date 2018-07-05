using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Marks a property with the data converter attribute
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DataConverterAttribute : Attribute
    {
        public IDataConverter Converter { get; private set; }

        public DataConverterAttribute(Type dataConverterType)
        {
            Converter = (IDataConverter)Activator.CreateInstance(dataConverterType);
        }
    }
}
