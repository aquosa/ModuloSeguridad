using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesEmpresariales;
using System.Data;
using Fachada;

namespace COA.WebCipol.Comun
{
    [Serializable]
    public class DatosCIPOL
    {
        public EntidadesEmpresariales.PadreCipolCliente DatosPadreCIPOLCliente { get; set; }

        ///METODO PARA DEVOLVER LOS SISTEMAS EN LISTAS
        /// <summary>
        /// Convierte el data set de sistemas habilitados en una lista de sistemas habilitados
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [LeandroF]          [viernes, 13 de septiembre de 2013]       Creado
        /// </history>
        public List<Se_SistemasHabilitados> ObtenerListaSistemas()
        {
            List<Se_SistemasHabilitados> objListaSistemas = new List<Se_SistemasHabilitados>();
            DataSet dtsSistemas = DatosPadreCIPOLCliente.ObtenerSistemas();
            Se_SistemasHabilitados objSistema;
            for (int i = 0; i < dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows.Count; i++)
            {
                objSistema = new Se_SistemasHabilitados();
                objSistema.IDSistema = Convert.ToInt16(dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows[i]["IDSistema"]);
                objSistema.DescSistema = dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows[i]["DescSistema"].ToString().Trim();
                objSistema.CodSistema = dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows[i]["CodSistema"].ToString().Trim();
                objSistema.Icono = dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows[i]["Icono"].ToString().Trim();
                objSistema.NombreExec = dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows[i]["NOMBREEXEC"].ToString().Trim();
                objSistema.PaginaPorDefecto = dtsSistemas.Tables["SE_SIST_HABILITADOS"].Rows[i]["PAGINAPORDEFECTO"].ToString().Trim();

                objListaSistemas.Add(objSistema);
            }
            return objListaSistemas;
        }

        public string strCipol { get; set; }

    }
}
