using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesDataSet
{

    public class dtsAuditoriaEventos
    {
        public List<dtsAuditoriaEventosSE_SIST_HABILITADOS> lstdtsAuditoriaEventosSE_SIST_HABILITADOS { get; set; }
        public List<dtsAuditoriaEventosSE_TERMINALES> lstdtsAuditoriaEventosSE_TERMINALES { get; set; }
        public List<dtsAuditoriaEventosSE_USUARIOS> lstdtsAuditoriaEventosSE_USUARIOS { get; set; }
        public List<dtsAuditoriaEventosSIST_EVENTOS> lstdtsAuditoriaEventosSIST_EVENTOS { get; set; }
        public List<dtsAuditoriaEventosTablasDeSistema> lstdtsAuditoriaEventosTablasDeSistema { get; set; }
        public List<dtsAuditoriaEventosDtFiltros> lstdtsAuditoriaEventosDtFiltros { get; set; }
        public List<dtsAuditoriaEventosDtOperaciones> lstdtsAuditoriaEventosDtOperaciones { get; set; }
        public List<dtsAuditoriaEventosDtSupervisores> lstdtsAuditoriaEventosDtSupervisores { get; set; }
    }

    public class dtsAuditoriaEventosSE_SIST_HABILITADOS
    {

        private string dESCSISTEMAField;

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }
    }

    public class dtsAuditoriaEventosSE_TERMINALES
    {

        private string cODTERMINALField;

        /// <comentarios/>
        public string CODTERMINAL
        {
            get
            {
                return this.cODTERMINALField;
            }
            set
            {
                this.cODTERMINALField = value;
            }
        }
    }

    public class dtsAuditoriaEventosSE_USUARIOS
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

    public class dtsAuditoriaEventosSIST_EVENTOS
    {

        private System.DateTime fECHAHORALOGField;

        private string oPERACIONField;

        private string tABLAField;

        private string sTRINGSQLField;

        private string uSUARIOField;

        private string sUPERVISORField;

        private string nOMBREPCField;

        private string sISTEMAField;

        /// <comentarios/>
        public System.DateTime FECHAHORALOG
        {
            get
            {
                return this.fECHAHORALOGField;
            }
            set
            {
                this.fECHAHORALOGField = value;
            }
        }

        /// <comentarios/>
        public string OPERACION
        {
            get
            {
                return this.oPERACIONField;
            }
            set
            {
                this.oPERACIONField = value;
            }
        }

        /// <comentarios/>
        public string TABLA
        {
            get
            {
                return this.tABLAField;
            }
            set
            {
                this.tABLAField = value;
            }
        }

        /// <comentarios/>
        public string STRINGSQL
        {
            get
            {
                return this.sTRINGSQLField;
            }
            set
            {
                this.sTRINGSQLField = value;
            }
        }

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
        public string SUPERVISOR
        {
            get
            {
                return this.sUPERVISORField;
            }
            set
            {
                this.sUPERVISORField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREPC
        {
            get
            {
                return this.nOMBREPCField;
            }
            set
            {
                this.nOMBREPCField = value;
            }
        }

        /// <comentarios/>
        public string SISTEMA
        {
            get
            {
                return this.sISTEMAField;
            }
            set
            {
                this.sISTEMAField = value;
            }
        }
    }

    public class dtsAuditoriaEventosTablasDeSistema
    {

        private string nombreTablaField;

        /// <comentarios/>
        public string NombreTabla
        {
            get
            {
                return this.nombreTablaField;
            }
            set
            {
                this.nombreTablaField = value;
            }
        }
    }

    public class dtsAuditoriaEventosDtFiltros
    {

        private System.DateTime fECHADESDEField;

        private System.DateTime fECHAHASTAField;

        private string oPERACIONField;

        private string tABLAField;

        private string uSUARIOField;

        private string sUPERVISORField;

        private string nOMBREPCField;

        private string sISTEMAField;

        private string tEXTOBUSCARField;

        /// <comentarios/>
        public System.DateTime FECHADESDE
        {
            get
            {
                return this.fECHADESDEField;
            }
            set
            {
                this.fECHADESDEField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAHASTA
        {
            get
            {
                return this.fECHAHASTAField;
            }
            set
            {
                this.fECHAHASTAField = value;
            }
        }

        /// <comentarios/>
        public string OPERACION
        {
            get
            {
                return this.oPERACIONField;
            }
            set
            {
                this.oPERACIONField = value;
            }
        }

        /// <comentarios/>
        public string TABLA
        {
            get
            {
                return this.tABLAField;
            }
            set
            {
                this.tABLAField = value;
            }
        }

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
        public string SUPERVISOR
        {
            get
            {
                return this.sUPERVISORField;
            }
            set
            {
                this.sUPERVISORField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREPC
        {
            get
            {
                return this.nOMBREPCField;
            }
            set
            {
                this.nOMBREPCField = value;
            }
        }

        /// <comentarios/>
        public string SISTEMA
        {
            get
            {
                return this.sISTEMAField;
            }
            set
            {
                this.sISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string TEXTOBUSCAR
        {
            get
            {
                return this.tEXTOBUSCARField;
            }
            set
            {
                this.tEXTOBUSCARField = value;
            }
        }
    }

    public class dtsAuditoriaEventosDtOperaciones
    {

        private string codigoField;

        private string operacionField;

        /// <comentarios/>
        public string Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <comentarios/>
        public string Operacion
        {
            get
            {
                return this.operacionField;
            }
            set
            {
                this.operacionField = value;
            }
        }
    }

    public class dtsAuditoriaEventosDtSupervisores
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