using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.SIST_USUARIOS
{
    [Serializable]
    public class SIST_USUARIOS
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Usuario { get; set; }
        public string NombreArea { get; set; }
        public string Domicilio { get; set; }
        public string TipoAbreviado { get; set; }
        public int NroDocumento { get; set; }
        public bool ForzarCambio { get; set; }
        public bool ForzarCambioDes { get; set; }
        public bool CtaBloqueada { get; set; }
        public bool CtaBloqueadaDes { get; set; }
        public bool CtaBloqueadaDesLetra { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public DateTime FechaBloqueo { get; set; }
        public string ALIAS_USUARIO { get; set; }
        public string FICTICIA { get; set; }
        public int CANTINTINVUSOCTA { get; set; }
        public DateTime FechaUltUsoCta { get; set; }
        public string Email { get; set; }
    }
}

