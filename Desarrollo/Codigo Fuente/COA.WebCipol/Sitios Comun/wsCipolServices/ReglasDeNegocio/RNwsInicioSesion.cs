using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsCipolServices.ConectorWSCipolNET;
using COA.WebCipol.Entidades.Core;
using COA.WebCipol.Entidades;

namespace wsCipolServices.ReglasDeNegocio
{
    public class RNwsInicioSesion
    {
        public string Recuperar_UsuariosXSistema(Int32 IDSistema, List<SE_USUARIOS> lstUsuarios)
        {
            ConectorwsIniciosesion objConector;
            string strRetorno;
            Fachada.Seguridad.IniciarSesion.dtsUsuarios dtsRetorno = new Fachada.Seguridad.IniciarSesion.dtsUsuarios();
            try
            {
                objConector = new ConectorwsIniciosesion();
                strRetorno = objConector.Recuperar_UsuariosXSistema(IDSistema, ref dtsRetorno);
                //Retorn la lista correspondiente a la tabla.
                lstUsuarios = EntityLoader.Load<SE_USUARIOS>(dtsRetorno);

                return strRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}