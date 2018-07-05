using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace wsCipolServices.ReglasDeNegocio
{
    public class RNwsCOA_ABMBase
    {
        public Object Recuperar(string NombreTabla)
        {
            ConectorwsCOA_ABMBase objConector;
            DataSet dtsRetorno;
            try
            {

                objConector = new ConectorwsCOA_ABMBase();
                dtsRetorno = objConector.Recuperar(NombreTabla);
                //Retorn la lista correspondiente a la tabla.
                return RNUtiles.RetornaObject(NombreTabla, dtsRetorno);
            }
            catch (Exception)
            {
                //todo: dejar log de errores
                return null;
            }
        }

        public Int32 Grabar(Object lstDatos, string NombreTabla, Fachada.Seguridad.ABM.TipoProceso TipoProceso)
        {
            ConectorwsCOA_ABMBase objConector;
            try
            {
                objConector = new ConectorwsCOA_ABMBase();
                //Transforma la lista en DataSet.
                DataSet DataSet = RNUtiles.RetornaDataSet(NombreTabla, lstDatos);
                if (DataSet != null)
                    return objConector.Grabar(DataSet, NombreTabla, TipoProceso);
                else
                    return 0;
            }
            catch (Exception)
            {
                //todo: dejar log de errores
                return 0;
            }
        }

        
    }
}