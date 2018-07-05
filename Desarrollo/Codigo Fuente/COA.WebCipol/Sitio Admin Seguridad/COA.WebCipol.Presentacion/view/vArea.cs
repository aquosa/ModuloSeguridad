using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    //[MiguelP]         28/10/2014      GCP - Cambios 15598
    public class vAreaElemntoBase : EntidadesBase   
    {
        public vAreaElemntoBase()
        {
            objBase = new vArea();
            objFiltro = new vAreaFiltro();
        }
        public vArea objBase { get; set; }
        public vAreaFiltro objFiltro { get; set; }
    }

    public class vArea : EntidadesBase
    {
        public vArea()
        {
            update = false;
            FICTICIA = "N";
        }

        public decimal IDAREA { get; set; }
        public string NOMBREAREA { get; set; }
        public string RESPONSABLE { get; set; }
        public string CARGORESPONSABLE { get; set; }
        public string COMENTARIOS { get; set; }

        public string FICTICIA { get; set; }
        public bool blnFICTICIA
        {
            get { return FICTICIA.Equals("S"); }
            set
            {
                if (value)
                    FICTICIA = "S";
                else
                    FICTICIA = "N";
            }
        }

        public bool update { get; set; }

        public string BAJA { get; set; }


    }

    //[MiguelP]         28/10/2014      GCP - Cambios 15598
    public class vAreaFiltro : EntidadesBase
    {
        public string area { get; set; }
        public string baja { get; set; }
    }
}