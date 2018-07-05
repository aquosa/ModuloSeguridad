using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsCipolServices.ConectorWSCipolNET;
using COA.WebCipol.Entidades.ClasesWs;

namespace wsCipolServices.ReglasDeNegocio
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [lunes, 06 de octubre de 2014]       Modificado  GCP-Cambios 15588
    /// </history>
    public partial class RNwsCipolSupervision
    {
        public bool ValidarSupervisor(dcValidarSupervisor datos)
        {
            ConectorwsCipolSupervision objConector;
            try
            {
                objConector = new ConectorwsCipolSupervision();
                return objConector.ValidarSupervisor(datos.usuario, datos.clave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidarSupervisorConAuditoria(dcValidarSupervisor obj)
        {
            ConectorwsCipolSupervision objConector;
            try
            {
                objConector = new ConectorwsCipolSupervision();
                return objConector.ValidarSupervisorConAuditoria(obj.usuario, obj.clave, obj.idusuariosupervisor, obj.idusuario, obj.idtareasupervisor, obj.terminal);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}