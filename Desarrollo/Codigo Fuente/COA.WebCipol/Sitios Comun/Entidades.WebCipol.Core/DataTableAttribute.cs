using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// The relative source table for retrieving data
    /// </summary>
    public enum DataTableSource
    {
        None = 0,
        Next = 1
    }

    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DataTableAttribute : Attribute
    {
        /// <summary>
        /// The data table index for the correspondig property
        /// </summary>
        public int TableIndex { get; private set; }

        /// <summary>
        /// The source table for the corresponding data
        /// </summary>
        public DataTableSource TableSource { get; private set; }

        /// <summary>
        /// Gets the effective data table for the entity list
        /// </summary>
        /// <param name="currentTable"></param>
        /// <returns></returns>
        public int GetEffectiveDataTable(int currentTable)
        {
            switch (TableSource)
            {
                case DataTableSource.None:
                    return currentTable + TableIndex;
                case DataTableSource.Next:
                    return currentTable + 1;
            }
            return currentTable;
        }

        public DataTableAttribute(int tableIndex) { TableIndex = tableIndex; }
        public DataTableAttribute(DataTableSource tableSource) { TableSource = tableSource; }
    }
}
