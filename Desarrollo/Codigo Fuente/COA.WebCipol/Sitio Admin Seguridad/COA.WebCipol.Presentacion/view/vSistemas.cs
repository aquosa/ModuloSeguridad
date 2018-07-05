using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vSistemas : EntidadesBase
    {
        public vSistemas()
        {
            SISTEMAHABILITADO = "S";
        }

        public bool update { get; set; }

        //original decimal
        public decimal IDSISTEMA
        {
            get;
            set;
        }

        /// <comentarios/>
        public string CODSISTEMA
        {
            get;
            set;
        }

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get;
            set;
        }

        ///// <comentarios/>
        //public System.DateTime FECHAHABILITACION
        //{
        //    get;
        //    set;
        //}

        //public string strFECHAHABILITACION
        //{
        //    get { return FECHAHABILITACION.Date.ToString("dd/MM/yyyy");}
        //    set {
        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            FECHAHABILITACION = new DateTime(int.Parse(value.Substring(6, 4)), int.Parse(value.Substring(3, 2)), int.Parse(value.Substring(0, 2)));
        //        }
        //    }
        //}

        /// <comentarios/>
        public string NOMBREEXEC
        {
            get;
            set;
        }

        /// <comentarios/>
        public string SISTEMAHABILITADO
        {
            get;
            set;
        }

        public bool blnSISTEMAHABILITADO
        {
            get { return SISTEMAHABILITADO.Equals("S"); }
            set
            {
                if (value)
                    SISTEMAHABILITADO = "S";
                else
                    SISTEMAHABILITADO = "N";

            }
        }

        /// <comentarios/>
        public string ICONO
        {
            get;
            set;
        }

        /// <comentarios/>
        public string PAGINAPORDEFECTO
        {
            get;
            set;
        }

    }
}