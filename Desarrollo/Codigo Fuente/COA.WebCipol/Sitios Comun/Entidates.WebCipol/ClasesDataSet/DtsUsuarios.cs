using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class DtsUsuarios
    {
        public List<dtsUsuariosKAREAS> lstKAreas { get; set; }
        public List<dtsUsuariosKDocumentos> lstKDocumentos { get; set; }
        public List<dtsUsuariosRolesXUsuarios> lstRolesXUsuarios { get; set; }
        public List<dtsUsuariosSE_GRUPO_EXCLUSION> lstSE_GRUPO_EXCLUSION { get; set; }
        public List<dtsUsuariosSE_Horarios_Usuario> lstSE_Horarios_Usuario { get; set; }
        public List<dtsUsuariosSE_TAREAS_USUARIO> lstSE_TAREAS_USUARIO { get; set; }
        public List<dtsUsuariosSE_TERMINALES> lstSE_TERMINALES { get; set; }
        public List<dtsUsuariosSE_Term_Usuario> lstSE_Term_Usuario { get; set; }
        public List<dtsUsuariosSist_Usuarios> lstSist_Usuarios { get; set; }
        public List<dtsUsuariosSist_sesionesactivas> lstSist_sesionesactivas { get; set; }
        public List<dtsUsuariosUsuariosXSistema> lstUsuariosXSistema { get; set; }

    }

    public class dtsUsuariosKAREAS
    {

        private long idAreaField;

        private string nombreAreaField;

        private string bajaField;

        public long IdArea
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
        public string baja
        {
            get
            {
                return this.bajaField;
            }
            set
            {
                this.bajaField = value;
            }
        }
    }
    public class dtsUsuariosKDocumentos
    {

        private long IdTipoDocField;

        private string tipoAbreviadoField;

        /// <comentarios/>
        public long IdTipoDoc
        {
            get
            {
                return this.IdTipoDocField;
            }
            set
            {
                this.IdTipoDocField = value;
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
    }
    public class dtsUsuariosRolesXUsuarios
    {

        private string nOMBRESField;

        private string dESCRIPCIONPERFILField;

        private int cANTTAREASROLField;

        private int cANTAREASUSUARIOROLField;

        private decimal IDUSUARIOField;

        private decimal IDROLField;

        private bool IDROLFieldSpecified;

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
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IDROLSpecified
        {
            get
            {
                return this.IDROLFieldSpecified;
            }
            set
            {
                this.IDROLFieldSpecified = value;
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
    public class dtsUsuariosSE_GRUPO_EXCLUSION
    {

        private decimal IDGRUPOACTUALField;

        private decimal IDGRUPEXCLUYENTEField;

        /// <comentarios/>
        public decimal IDGRUPOACTUAL
        {
            get
            {
                return this.IDGRUPOACTUALField;
            }
            set
            {
                this.IDGRUPOACTUALField = value;
            }
        }

        /// <comentarios/>
        public decimal IDGRUPEXCLUYENTE
        {
            get
            {
                return this.IDGRUPEXCLUYENTEField;
            }
            set
            {
                this.IDGRUPEXCLUYENTEField = value;
            }
        }
    }
    public class dtsUsuariosSE_Horarios_Usuario
    {

        private decimal IdHorarioField;

        private decimal IdDiaField;

        private decimal IDUSUARIOField;

        public dtsUsuariosSE_Horarios_Usuario()
        {
            this.IDUSUARIOField = -2;
        }

        /// <comentarios/>
        public decimal IDHORARIO
        {
            get
            {
                return this.IdHorarioField;
            }
            set
            {
                this.IdHorarioField = value;
            }
        }

        /// <comentarios/>
        public decimal IDDIA
        {
            get
            {
                return this.IdDiaField;
            }
            set
            {
                this.IdDiaField = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
            }
        }
    }
    public class dtsUsuariosSE_TAREAS_USUARIO
    {

        private decimal IDTAREAField;

        private decimal IDROLField;

        private decimal IDUSUARIOField;

        private decimal cANTIDADAUTORIZADAField;

        private string tAREAINHIBIDAField;

        private System.DateTime fECHAULTMODIFField;

        private bool fECHAULTMODIFFieldSpecified;

        private decimal iDUSUARIOULTMODIFField;

        /// <comentarios/>
        public decimal IDTAREA
        {
            get
            {
                return this.IDTAREAField;
            }
            set
            {
                this.IDTAREAField = value;
            }
        }

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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
    public class dtsUsuariosSE_TERMINALES
    {

        private decimal IDTERMINALField;

        private string cODTERMINALField;

        private string nOMBRECOMPUTADORAField;

        private string uSOHABILITADOField;

        private string mODELOPROCESADORField;

        private decimal CANTMEMORIARAMField;

        private short tAMANIODISCOField;

        private string mODELOMONITORField;

        private string mODELOACELVIDEOField;

        private string dESCADICIONALField;

        private int iDAREAField;

        private string nOMBREAREAField;

        private string nombreNetBiosField;

        /// <comentarios/>
        public decimal IDTERMINAL
        {
            get
            {
                return this.IDTERMINALField;
            }
            set
            {
                this.IDTERMINALField = value;
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
        public decimal CANTMEMORIARAM
        {
            get
            {
                return this.CANTMEMORIARAMField;
            }
            set
            {
                this.CANTMEMORIARAMField = value;
            }
        }

        /// <comentarios/>
        public short TAMANIODISCO
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
    public class dtsUsuariosSE_Term_Usuario
    {

        private decimal IDTERMINALField;

        private decimal IDUSUARIOField;

        private string cODTERMINALField;

        private string nOMBRENETBIOSField;

        private int idAreaField;

        private string nOMBREAREAField;

        /// <comentarios/>
        public decimal IDTERMINAL
        {
            get
            {
                return this.IDTERMINALField;
            }
            set
            {
                this.IDTERMINALField = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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
    public class dtsUsuariosSist_Usuarios
    {

        private decimal IDUSUARIOField;

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

        public decimal IdTipoDocField;

        private int idAreaField;

        private string cantIntInvUsoCtaField;

        private string aLIAS_USUARIOField;

        private System.DateTime fechaUltUsoCtaField;

        private string emailField;

        public dtsUsuariosSist_Usuarios()
        {
            this.IDUSUARIOField = -2;
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
            this.IdTipoDocField = -1;
            this.idAreaField = -1;
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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

        public decimal IDTIPODOC
        {
            get
            {
                return this.IdTipoDocField;
            }
            set
            {
                this.IdTipoDocField = value;
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
    public class dtsUsuariosSist_sesionesactivas
    {

        private string usuarioField;

        private System.DateTime iNICIOTAREAField;

        private string terminalField;

        private decimal IDUSUARIOField;

        private string nOMBRESField;

        private int iDAREAField;

        private string nOMBREAREAField;

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
        public System.DateTime INICIOTAREA
        {
            get
            {
                return this.iNICIOTAREAField;
            }
            set
            {
                this.iNICIOTAREAField = value;
            }
        }

     

        /// <comentarios/>
        public string Terminal
        {
            get
            {
                return this.terminalField;
            }
            set
            {
                this.terminalField = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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
    public class dtsUsuariosUsuariosXSistema
    {

        private decimal IDUSUARIOField;

        private string nOMBRESField;

        private string cTABLOQUEADAField;

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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

        /// <comentarios/>
        public string CTABLOQUEADA
        {
            get
            {
                return this.cTABLOQUEADAField;
            }
            set
            {
                this.cTABLOQUEADAField = value;
            }
        }
    }
}
