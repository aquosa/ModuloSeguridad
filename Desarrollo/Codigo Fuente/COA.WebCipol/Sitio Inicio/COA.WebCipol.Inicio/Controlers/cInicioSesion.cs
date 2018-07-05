using COA.WebCipol.Fachada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Inicio.Controlers
{
    public class cInicioSesion
    {
        /// <summary>
        /// Permite registrar la expiracion de la sesion
        /// </summary>
        /// <history>
        /// [LucianoP]          [jueves, 6 de abril de 2017]    Creado 
        /// </history>
        public void RegistrarExpiroSesion()
        {
            FInicioSesion objFIS = null;
            try
            {
                objFIS = new FInicioSesion();
                objFIS.RegistrarExpiroSesion();
            }
            catch
            {
                throw;
            }
        }
    }
}