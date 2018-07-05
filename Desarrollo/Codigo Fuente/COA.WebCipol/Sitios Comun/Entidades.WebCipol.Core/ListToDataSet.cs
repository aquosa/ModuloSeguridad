using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace COA.WebCipol.Entidades.Core
{
    public static class ListToDataSet
    {
        public static DataSet ToDataSet<T>(this IList<T> list, string NombreTabla)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable(NombreTabla);
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }

        public static void ToDataSet<T>(this IList<T> list, string NombreTabla, DataSet Datos)
        {
            Type elementType = typeof(T);

            if (!Datos.Tables.Contains(NombreTabla))
            {
                DataTable t = new DataTable(NombreTabla);
                Datos.Tables.Add(t);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = Datos.Tables[NombreTabla].NewRow();
                foreach (var propInfo in elementType.GetProperties())
                {
                    if (!Datos.Tables[NombreTabla].Columns.Contains(propInfo.Name))
                    {
                        Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                        Datos.Tables[NombreTabla].Columns.Add(propInfo.Name, ColType);
                    }
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }
                Datos.Tables[NombreTabla].Rows.Add(row);
            }
        }

    }
}
