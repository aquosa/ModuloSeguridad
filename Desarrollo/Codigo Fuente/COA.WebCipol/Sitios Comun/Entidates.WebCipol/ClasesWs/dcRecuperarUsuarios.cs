using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarUsuarios
{
    [Serializable]
    public class dcRecuperarUsuarios
    {
        public dcRecuperarUsuarios()
        {
            lstUsuarios = new List<SE_USUARIOS>();
        }

        public List<SE_USUARIOS> lstUsuarios { get; set; }
    }
    [Serializable]
    public class SE_USUARIOS
    {

        private string uSUARIOField;

        private string nOMBRESField;

        /// <comentarios/>
        public string USUARIO
        {
            get
            {
                return this.uSUARIOField;
            }
            set
            {
                this.uSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }
    }
}
