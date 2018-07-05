using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DataColumnAttribute : Attribute
    {
        /// <summary>
        /// The name of the data column for this property
        /// </summary>
        public string ColumnName { get; set; }

        public DataColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
