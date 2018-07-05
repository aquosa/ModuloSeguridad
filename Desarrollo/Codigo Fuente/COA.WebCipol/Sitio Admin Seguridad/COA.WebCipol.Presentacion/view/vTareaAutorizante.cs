using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.Cipol.Presentacion._UIHelpers.DataSources;

namespace COA.WebCipol.Presentacion.view
{
    [Serializable]
    public class vTareaAutorizante : EntidadesBase
    {
        private CifrarDatos.TresDES objEncriptarNET;
        public vTareaAutorizante()
        {
            objEncriptarNET = new CifrarDatos.TresDES();
            objEncriptarNET.Key = COA.Cipol.Presentacion._UIHelpers.ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
            objEncriptarNET.IV = COA.Cipol.Presentacion._UIHelpers.ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
            blnREQUIEREAUDITORIA = false;
        }
        public bool update { get; set; }
        public int IDTAREA { get; set; }
        public string CODIGOTAREA { get; set; }
        public int IDSISTEMA { get; set; }
        public string DESCSISTEMA { get; set; }
        public string REQUIEREAUDITORIA { get; set; }
        public string DESCRIPCIONTAREA { get; set; }
        public int IDTAREAAUTORIZANTE { get; set; }
        public string DESCIDTAREAAUTORIZANTE { get; set; }
        public bool blnREQUIEREAUDITORIA
        {
            get
            {
                if (!string.IsNullOrEmpty(REQUIEREAUDITORIA))
                    return (objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, REQUIEREAUDITORIA) == "1");
                else
                    return false;

            }
            set
            {
                //Se pone el valor "Unchecked" por encontrarse asi en el CIPOL Original.
                REQUIEREAUDITORIA = objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Encriptacion, ((value) ? "1" : "Unchecked").ToString());
            }
        }
    }
    [Serializable]
    public class Item
    {
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
    }

    

}