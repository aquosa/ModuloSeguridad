using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario
{
    [Serializable]
    public class dcRecuperarReporteUsuario
    {
        /*
         * Sist_Usuarios
         * SE_TERMINALES
         * SE_Term_Usuario
         * SE_Horarios_Usuario
         * RolesXUsuarios
         * SE_TAREAS_USUARIO
         */
        public dcRecuperarReporteUsuario()
        {
            lstRolesXUsuarios = new List<RolesXUsuarios>();
            lstSE_Horarios_Usuario = new List<SE_Horarios_Usuario>();
            lstSE_TAREAS_USUARIO = new List<SE_TAREAS_USUARIO>();
            lstSE_Term_Usuario = new List<SE_Term_Usuario>();
            lstSE_TERMINALES = new List<SE_TERMINALES>();
            lstSist_Usuarios = new List<Sist_Usuarios>();
        }
        public List<Sist_Usuarios> lstSist_Usuarios { get; set; }
        public List<SE_TERMINALES> lstSE_TERMINALES { get; set; }
        public List<SE_Term_Usuario> lstSE_Term_Usuario { get; set; }
        public List<SE_Horarios_Usuario> lstSE_Horarios_Usuario { get; set; }
        public List<RolesXUsuarios> lstRolesXUsuarios { get; set; }
        public List<SE_TAREAS_USUARIO> lstSE_TAREAS_USUARIO { get; set; }
    }

    [Serializable]
    public class Sist_Usuarios
    {

        private int idUsuarioField;

        private string usuarioField;

        private string nombresField;

        private string nombreAreaField;

        private string domicilioField;

        private string tipoAbreviadoField;

        private string nroDocumentoField;

        private string forzarCambioField;

        private string forzarCambioDesField;

        private string ctaBloqueadaField;

        private string ctaBloqueadaDesField;

        private string ctaBloqueadaDesLetraField;

        private string comentarioField;

        private System.DateTime fechaAltaField;

        private System.DateTime fechaBajaField;

        private System.DateTime fechaBloqueoField;

        private string fICTICIAField;

        private int idTipoDocField;

        private int idAreaField;

        private string cantIntInvUsoCtaField;

        private string aLIAS_USUARIOField;

        private System.DateTime fechaUltUsoCtaField;

        private string emailField;

        public Sist_Usuarios()
        {
            this.idUsuarioField = -2;
            this.usuarioField = "_";
            this.nombresField = "_";
            this.nombreAreaField = "_";
            this.domicilioField = "_";
            this.tipoAbreviadoField = "_";
            this.nroDocumentoField = "_";
            this.forzarCambioField = "_";
            this.forzarCambioDesField = "_";
            this.ctaBloqueadaField = "_";
            this.ctaBloqueadaDesField = "_";
            this.ctaBloqueadaDesLetraField = "_";
            this.comentarioField = "_";
            this.fICTICIAField = "_";
            this.idTipoDocField = -1;
            this.idAreaField = -1;
        }

        /// <comentarios/>
        public int IdUsuario
        {
            get
            {
                return this.idUsuarioField;
            }
            set
            {
                this.idUsuarioField = value;
            }
        }

        /// <comentarios/>
        public string Usuario
        {
            get
            {
                return this.usuarioField;
            }
            set
            {
                this.usuarioField = value;
            }
        }

        /// <comentarios/>
        public string Nombres
        {
            get
            {
                return this.nombresField;
            }
            set
            {
                this.nombresField = value;
            }
        }

        /// <comentarios/>
        public string NombreArea
        {
            get
            {
                return this.nombreAreaField;
            }
            set
            {
                this.nombreAreaField = value;
            }
        }

        /// <comentarios/>
        public string Domicilio
        {
            get
            {
                return this.domicilioField;
            }
            set
            {
                this.domicilioField = value;
            }
        }

        /// <comentarios/>
        public string TipoAbreviado
        {
            get
            {
                return this.tipoAbreviadoField;
            }
            set
            {
                this.tipoAbreviadoField = value;
            }
        }

        /// <comentarios/>
        public string NroDocumento
        {
            get
            {
                return this.nroDocumentoField;
            }
            set
            {
                this.nroDocumentoField = value;
            }
        }

        /// <comentarios/>
        public string ForzarCambio
        {
            get
            {
                return this.forzarCambioField;
            }
            set
            {
                this.forzarCambioField = value;
            }
        }

        /// <comentarios/>
        public string ForzarCambioDes
        {
            get
            {
                return this.forzarCambioDesField;
            }
            set
            {
                this.forzarCambioDesField = value;
            }
        }

        /// <comentarios/>
        public string CtaBloqueada
        {
            get
            {
                return this.ctaBloqueadaField;
            }
            set
            {
                this.ctaBloqueadaField = value;
            }
        }

        /// <comentarios/>
        public string CtaBloqueadaDes
        {
            get
            {
                return this.ctaBloqueadaDesField;
            }
            set
            {
                this.ctaBloqueadaDesField = value;
            }
        }

        /// <comentarios/>
        public string CtaBloqueadaDesLetra
        {
            get
            {
                return this.ctaBloqueadaDesLetraField;
            }
            set
            {
                this.ctaBloqueadaDesLetraField = value;
            }
        }

        /// <comentarios/>
        public string Comentario
        {
            get
            {
                return this.comentarioField;
            }
            set
            {
                this.comentarioField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaAlta
        {
            get
            {
                return this.fechaAltaField;
            }
            set
            {
                this.fechaAltaField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaBaja
        {
            get
            {
                return this.fechaBajaField;
            }
            set
            {
                this.fechaBajaField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaBloqueo
        {
            get
            {
                return this.fechaBloqueoField;
            }
            set
            {
                this.fechaBloqueoField = value;
            }
        }

        /// <comentarios/>
        public string FICTICIA
        {
            get
            {
                return this.fICTICIAField;
            }
            set
            {
                this.fICTICIAField = value;
            }
        }

        public int IdTipoDoc
        {
            get
            {
                return this.idTipoDocField;
            }
            set
            {
                this.idTipoDocField = value;
            }
        }

        public int idArea
        {
            get
            {
                return this.idAreaField;
            }
            set
            {
                this.idAreaField = value;
            }
        }

        /// <comentarios/>
        public string CantIntInvUsoCta
        {
            get
            {
                return this.cantIntInvUsoCtaField;
            }
            set
            {
                this.cantIntInvUsoCtaField = value;
            }
        }

        /// <comentarios/>
        public string ALIAS_USUARIO
        {
            get
            {
                return this.aLIAS_USUARIOField;
            }
            set
            {
                this.aLIAS_USUARIOField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaUltUsoCta
        {
            get
            {
                return this.fechaUltUsoCtaField;
            }
            set
            {
                this.fechaUltUsoCtaField = value;
            }
        }

        /// <comentarios/>
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }
    [Serializable]
    public class SE_TERMINALES
    {

        private int iDTERMINALField;

        private string cODTERMINALField;

        private string nOMBRECOMPUTADORAField;

        private string uSOHABILITADOField;

        private string mODELOPROCESADORField;

        private long cANTMEMORIARAMField;

        private long tAMANIODISCOField;

        private string mODELOMONITORField;

        private string mODELOACELVIDEOField;

        private string dESCADICIONALField;

        private int iDAREAField;

        private string nOMBREAREAField;

        private string nombreNetBiosField;

        /// <comentarios/>
        public int IDTERMINAL
        {
            get
            {
                return this.iDTERMINALField;
            }
            set
            {
                this.iDTERMINALField = value;
            }
        }

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

        /// <comentarios/>
        public string NOMBRECOMPUTADORA
        {
            get
            {
                return this.nOMBRECOMPUTADORAField;
            }
            set
            {
                this.nOMBRECOMPUTADORAField = value;
            }
        }

        /// <comentarios/>
        public string USOHABILITADO
        {
            get
            {
                return this.uSOHABILITADOField;
            }
            set
            {
                this.uSOHABILITADOField = value;
            }
        }

        /// <comentarios/>
        public string MODELOPROCESADOR
        {
            get
            {
                return this.mODELOPROCESADORField;
            }
            set
            {
                this.mODELOPROCESADORField = value;
            }
        }

        /// <comentarios/>
        public long CANTMEMORIARAM
        {
            get
            {
                return this.cANTMEMORIARAMField;
            }
            set
            {
                this.cANTMEMORIARAMField = value;
            }
        }

        /// <comentarios/>
        public long TAMANIODISCO
        {
            get
            {
                return this.tAMANIODISCOField;
            }
            set
            {
                this.tAMANIODISCOField = value;
            }
        }


        /// <comentarios/>
        public string MODELOMONITOR
        {
            get
            {
                return this.mODELOMONITORField;
            }
            set
            {
                this.mODELOMONITORField = value;
            }
        }

        /// <comentarios/>
        public string MODELOACELVIDEO
        {
            get
            {
                return this.mODELOACELVIDEOField;
            }
            set
            {
                this.mODELOACELVIDEOField = value;
            }
        }

        /// <comentarios/>
        public string DESCADICIONAL
        {
            get
            {
                return this.dESCADICIONALField;
            }
            set
            {
                this.dESCADICIONALField = value;
            }
        }

        /// <comentarios/>
        public int IDAREA
        {
            get
            {
                return this.iDAREAField;
            }
            set
            {
                this.iDAREAField = value;
            }
        }


        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
            }
        }

        /// <comentarios/>
        public string NombreNetBios
        {
            get
            {
                return this.nombreNetBiosField;
            }
            set
            {
                this.nombreNetBiosField = value;
            }
        }
    }
    [Serializable]
    public class SE_Term_Usuario
    {

        private int idTerminalField;

        private int idUsuarioField;

        private string cODTERMINALField;

        private string nOMBRENETBIOSField;

        private int idAreaField;

        private string nOMBREAREAField;

        /// <comentarios/>
        public int IdTerminal
        {
            get
            {
                return this.idTerminalField;
            }
            set
            {
                this.idTerminalField = value;
            }
        }

        /// <comentarios/>
        public int IdUsuario
        {
            get
            {
                return this.idUsuarioField;
            }
            set
            {
                this.idUsuarioField = value;
            }
        }

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

        /// <comentarios/>
        public string NOMBRENETBIOS
        {
            get
            {
                return this.nOMBRENETBIOSField;
            }
            set
            {
                this.nOMBRENETBIOSField = value;
            }
        }

        /// <comentarios/>
        public int idArea
        {
            get
            {
                return this.idAreaField;
            }
            set
            {
                this.idAreaField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
            }
        }
    }
    [Serializable]
    public class SE_Horarios_Usuario
    {

        private int idHorarioField;

        private int idDiaField;

        private int idUsuarioField;

        public SE_Horarios_Usuario()
        {
            this.idUsuarioField = -2;
        }

        /// <comentarios/>
        public int IdHorario
        {
            get
            {
                return this.idHorarioField;
            }
            set
            {
                this.idHorarioField = value;
            }
        }

        /// <comentarios/>
        public int idDia
        {
            get
            {
                return this.idDiaField;
            }
            set
            {
                this.idDiaField = value;
            }
        }

        /// <comentarios/>
        public int IdUsuario
        {
            get
            {
                return this.idUsuarioField;
            }
            set
            {
                this.idUsuarioField = value;
            }
        }
    }
    [Serializable]
    public class RolesXUsuarios
    {

        private string nOMBRESField;

        private string dESCRIPCIONPERFILField;

        private int cANTTAREASROLField;

        private int cANTAREASUSUARIOROLField;

        private int iDUSUARIOField;

        private int iDROLField;

        private bool iDROLFieldSpecified;

        private string cOMPLETOField;

        private System.DateTime fECHAALTAField;

        private System.DateTime fECHABAJAField;

        private string iDTIPODOCDESCField;

        private string nRODOCUMENTOField;

        private string uSUARIOField;

        private System.DateTime fECHAAMODIFICACIONField;

        private string uSUARIOMODIFField;

        private int iDAREAField;

        private string nOMBREAREAField;

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

        /// <comentarios/>
        public string DESCRIPCIONPERFIL
        {
            get
            {
                return this.dESCRIPCIONPERFILField;
            }
            set
            {
                this.dESCRIPCIONPERFILField = value;
            }
        }

        /// <comentarios/>
        public int CANTTAREASROL
        {
            get
            {
                return this.cANTTAREASROLField;
            }
            set
            {
                this.cANTTAREASROLField = value;
            }
        }

        /// <comentarios/>
        public int CANTAREASUSUARIOROL
        {
            get
            {
                return this.cANTAREASUSUARIOROLField;
            }
            set
            {
                this.cANTAREASUSUARIOROLField = value;
            }
        }

        /// <comentarios/>
        public int IDUSUARIO
        {
            get
            {
                return this.iDUSUARIOField;
            }
            set
            {
                this.iDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public int IDROL
        {
            get
            {
                return this.iDROLField;
            }
            set
            {
                this.iDROLField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IDROLSpecified
        {
            get
            {
                return this.iDROLFieldSpecified;
            }
            set
            {
                this.iDROLFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string COMPLETO
        {
            get
            {
                return this.cOMPLETOField;
            }
            set
            {
                this.cOMPLETOField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAALTA
        {
            get
            {
                return this.fECHAALTAField;
            }
            set
            {
                this.fECHAALTAField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHABAJA
        {
            get
            {
                return this.fECHABAJAField;
            }
            set
            {
                this.fECHABAJAField = value;
            }
        }

        /// <comentarios/>
        public string IDTIPODOCDESC
        {
            get
            {
                return this.iDTIPODOCDESCField;
            }
            set
            {
                this.iDTIPODOCDESCField = value;
            }
        }

        /// <comentarios/>
        public string NRODOCUMENTO
        {
            get
            {
                return this.nRODOCUMENTOField;
            }
            set
            {
                this.nRODOCUMENTOField = value;
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
        public System.DateTime FECHAAMODIFICACION
        {
            get
            {
                return this.fECHAAMODIFICACIONField;
            }
            set
            {
                this.fECHAAMODIFICACIONField = value;
            }
        }

        /// <comentarios/>
        public string USUARIOMODIF
        {
            get
            {
                return this.uSUARIOMODIFField;
            }
            set
            {
                this.uSUARIOMODIFField = value;
            }
        }

        /// <comentarios/>
        public int IDAREA
        {
            get
            {
                return this.iDAREAField;
            }
            set
            {
                this.iDAREAField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
            }
        }
    }
    [Serializable]
    public class SE_TAREAS_USUARIO
    {

        private int iDTAREAField;

        private int iDROLField;

        private int iDUSUARIOField;

        private decimal cANTIDADAUTORIZADAField;

        private string tAREAINHIBIDAField;

        private System.DateTime fECHAULTMODIFField;

        private bool fECHAULTMODIFFieldSpecified;

        private decimal iDUSUARIOULTMODIFField;

        /// <comentarios/>
        public int IDTAREA
        {
            get
            {
                return this.iDTAREAField;
            }
            set
            {
                this.iDTAREAField = value;
            }
        }

        /// <comentarios/>
        public int IDROL
        {
            get
            {
                return this.iDROLField;
            }
            set
            {
                this.iDROLField = value;
            }
        }

        /// <comentarios/>
        public int IDUSUARIO
        {
            get
            {
                return this.iDUSUARIOField;
            }
            set
            {
                this.iDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public decimal CANTIDADAUTORIZADA
        {
            get
            {
                return this.cANTIDADAUTORIZADAField;
            }
            set
            {
                this.cANTIDADAUTORIZADAField = value;
            }
        }

        /// <comentarios/>
        public string TAREAINHIBIDA
        {
            get
            {
                return this.tAREAINHIBIDAField;
            }
            set
            {
                this.tAREAINHIBIDAField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAULTMODIF
        {
            get
            {
                return this.fECHAULTMODIFField;
            }
            set
            {
                this.fECHAULTMODIFField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FECHAULTMODIFSpecified
        {
            get
            {
                return this.fECHAULTMODIFFieldSpecified;
            }
            set
            {
                this.fECHAULTMODIFFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIOULTMODIF
        {
            get
            {
                return this.iDUSUARIOULTMODIFField;
            }
            set
            {
                this.iDUSUARIOULTMODIFField = value;
            }
        }

    }
}

