using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vUtiles
    {
        /// <summary>
        /// Parsea un datatime? a su equivalente en string.
        /// </summary>
        /// <param name="fecha">DateTime</param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 09 de octubre de 2013]       Creado  GCP-Cambios 
        /// </history>
        public static string DateToString(DateTime? fecha)
        {
            return (fecha.HasValue) ? DateToString(fecha.Value.Date) : "";
        }
        /// <summary>
        /// Parsea un datatime a su equivalente en string.
        /// </summary>
        /// <param name="fecha">DateTime</param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 09 de octubre de 2013]       Creado  GCP-Cambios 
        /// </history>
        public static string DateToString(DateTime fecha)
        {
            return (fecha != null) ? fecha.ToString("dd/MM/yyyy") : "";
        }
        
        /// <summary>
        /// Pasa de string a DateTime?
        /// </summary>
        /// <param name="fecha">"dd/MM/yyyy"</param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 09 de octubre de 2013]       Creado  GCP-Cambios 
        /// </history>
        public static DateTime? StringToDateNull(string fecha)
        {
            try
            {
                if (!string.IsNullOrEmpty(fecha))
                    return new DateTime(int.Parse(fecha.Substring(6, 4)), int.Parse(fecha.Substring(3, 2)), int.Parse(fecha.Substring(0, 2)));
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}