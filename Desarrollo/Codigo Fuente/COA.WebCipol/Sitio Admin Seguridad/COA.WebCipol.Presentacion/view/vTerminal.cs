using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.Cipol.Presentacion._UIHelpers;

namespace COA.WebCipol.Presentacion.view
{
    public class vterminal : EntidadesBase
    {
        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        private CifrarDatos.TresDES objEncriptarNET;
        public vterminal()
        {
            objFiltro = new vTerminalFiltro(); 
            objEncriptarNET = new CifrarDatos.TresDES();
            objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
            objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
            ORIGENACTUALIZACION = "R";
            blnUSOHABILITADO = true;
        }

        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        public vTerminalFiltro objFiltro { get; set; }

        public bool update { get; set; }

        /// <comentarios/>
        public int IDTERMINAL
        {
            get;
            set;
        }

        /// <comentarios/>
        public string CODTERMINAL
        {
            get;
            set;
        }

        private string _NOMBRECOMPUTADORA;
        /// <comentarios/>
        public string NOMBRECOMPUTADORA
        {
            get { return _NOMBRECOMPUTADORA; }
            set { _NOMBRECOMPUTADORA = (!string.IsNullOrEmpty(value)) ? value.Trim() : value; }
        }

        /// <comentarios/>
        public string USOHABILITADO
        {
            get;
            set;
        }

        public bool blnUSOHABILITADO
        {
            get
            {
                if (!string.IsNullOrEmpty(USOHABILITADO))
                    return (objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, USOHABILITADO) == "1");
                else
                    return false;

            }
            set
            {
                USOHABILITADO = objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Encriptacion, ((value) ? "1" : "0").ToString());
            }
        }

        private string _MODELOPROCESADOR;
        /// <comentarios/>
        public string MODELOPROCESADOR
        {
            get { return _MODELOPROCESADOR; }
            set { _MODELOPROCESADOR = (!string.IsNullOrEmpty(value)) ? value.Trim() : value; }
        }

        /// <comentarios/>
        public long CANTMEMORIARAM
        {
            get;
            set;
        }

        /// <comentarios/>
        public long TAMANIODISCO
        {
            get;
            set;
        }

        private string _MODELOMONITOR;
        /// <comentarios/>
        public string MODELOMONITOR
        {
            get { return _MODELOMONITOR; }
            set { _MODELOMONITOR = (!string.IsNullOrEmpty(value)) ?  value.Trim() : value; }
        }

        private string _MODELOACELVIDEO;
        /// <comentarios/>
        public string MODELOACELVIDEO
        {
            get { return _MODELOACELVIDEO; }
            set { _MODELOACELVIDEO = (!string.IsNullOrEmpty(value)) ? value.Trim() : value; }
        }

        private string _DESCADICIONAL;
        /// <comentarios/>
        public string DESCADICIONAL
        {
            get { return _DESCADICIONAL; }
            set { _DESCADICIONAL = (!string.IsNullOrEmpty(value)) ? value.Trim() : value; }
        }

        /// <comentarios/>
        public int IDAREA
        {
            get;
            set;
        }

        private string _NOMBREAREA;
        /// <comentarios/>
        public string NOMBREAREA
        {
            get { return _NOMBREAREA; }
            set { _NOMBREAREA = (!string.IsNullOrEmpty(value)) ? value.Trim() : value; }
        }

        private string _ORIGENACTUALIZACION;
        /// <comentarios/>
        public string ORIGENACTUALIZACION
        {
            get { return _ORIGENACTUALIZACION; }
            set { _ORIGENACTUALIZACION = (!string.IsNullOrEmpty(value)) ? value.Trim() : value; }
        }

        //public string valueORIGENACTUALIZACION
        //{
        //    get
        //    {
        //        if (ORIGENACTUALIZACION == "R")
        //            return "0";
        //        else
        //            return "1";
        //    }
        //    set
        //    {
        //        if (value == "0")
        //            ORIGENACTUALIZACION = "R";
        //        else
        //            ORIGENACTUALIZACION = "L";
        //    }
        //}

    }

    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    public class vTerminalFiltro : EntidadesBase
    { 
        public string filtro { get; set; }

        public string area { get; set; }
    }
}