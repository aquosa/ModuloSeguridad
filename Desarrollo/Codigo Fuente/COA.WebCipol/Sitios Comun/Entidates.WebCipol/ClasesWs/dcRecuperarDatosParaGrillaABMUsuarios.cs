using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Entidades.ClasesWs.Interfaces;
using COA.WebCipol.Comun;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios
{
    [Serializable]
    public class dcRecuperarDatosParaGrillaABMUsuarios
    {
        /*
         * SE_GRUPO_EXCLUSION
         * Sist_Usuarios
         * SE_PARAMETROS
         * KDocumentos
         * KAREAS
         * ComposicionDeRoles
         * SE_Horarios_Usuario
         */
        public dcRecuperarDatosParaGrillaABMUsuarios()
        {
            lstComposicionDeRoles = new List<ComposicionDeRoles>();
            lstKAREAS = new List<KAREAS>();
            lstKDocumentos = new List<KDocumentos>();
            lstSE_GRUPO_EXCLUSION = new List<SE_GRUPO_EXCLUSION>();
            lstSE_Horarios_Usuario = new List<SE_Horarios_Usuario>();
            lstSE_PARAMETROS = new List<SE_PARAMETROS>();
            lstSist_Usuarios = new List<Sist_Usuarios>();
            lstSE_TERMINALES = new List<SE_TERMINALES>();
        }
        public List<SE_GRUPO_EXCLUSION> lstSE_GRUPO_EXCLUSION { get; set; }
        public List<Sist_Usuarios> lstSist_Usuarios { get; set; }
        public List<KDocumentos> lstKDocumentos { get; set; }
        public List<KAREAS> lstKAREAS { get; set; }
        public List<SE_Horarios_Usuario> lstSE_Horarios_Usuario { get; set; }
        public List<ComposicionDeRoles> lstComposicionDeRoles { get; set; }
        public List<SE_PARAMETROS> lstSE_PARAMETROS { get; set; }
        public List<SE_TERMINALES> lstSE_TERMINALES { get; set; }
    }
    [Serializable]
    public class SE_GRUPO_EXCLUSION
    {

        private int iDGRUPOACTUALField;

        private int iDGRUPEXCLUYENTEField;

        /// <comentarios/>
        public int IDGRUPOACTUAL
        {
            get
            {
                return this.iDGRUPOACTUALField;
            }
            set
            {
                this.iDGRUPOACTUALField = value;
            }
        }

        /// <comentarios/>
        public int IDGRUPEXCLUYENTE
        {
            get
            {
                return this.iDGRUPEXCLUYENTEField;
            }
            set
            {
                this.iDGRUPEXCLUYENTEField = value;
            }
        }
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

        public string fechaBajaStr 
        {
            get { return FormatoFecha.DateToString(FechaBaja); }
        }

        private System.DateTime? fechaBajaField;

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
        public System.DateTime? FechaBaja
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
    public class KDocumentos
    {

        private long idTipoDocField;

        private string tipoAbreviadoField;

        /// <comentarios/>
        public long IdTipoDoc
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
    [Serializable]
    public class KAREAS
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
    [Serializable]
    public class SE_Horarios_Usuario : ISE_Horarios_Usuario
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
    public class ComposicionDeRolesAuxiliar
    {
        public decimal IdRol { get; set; }
        public decimal IdGrupo { get; set; }
        public decimal IdSistema { get; set; }
        public decimal IdTarea { get; set; }
        public string DescripcionPerfil { get; set; }
        public string DescGrupo { get; set; }
        public string DescSistema { get; set; }
        public string DescripcionTarea { get; set; }

    }

    [Serializable]
    public class ComposicionDeRoles
    {
        public int IdRol { get; set; }
        public int IdGrupo { get; set; }
        public int IdSistema { get; set; }
        public int IdTarea { get; set; }
        public string DescripcionPerfil { get; set; }
        public string DescGrupo { get; set; }
        public string DescSistema { get; set; }
        public string DescripcionTarea { get; set; }

    }
    [Serializable]
    public class SE_PARAMETROS
    {
        private string mstrCOLUMNA1 = "";
        public string COLUMNA1
        {
            get { return mstrCOLUMNA1; }
            set { mstrCOLUMNA1 = value; }
        }

        private string mstrCOLUMNA2 = "";
        public string COLUMNA2
        {
            get { return mstrCOLUMNA2; }
            set { mstrCOLUMNA2 = value; }
        }

        private string mstrCOLUMNA3 = "";
        public string COLUMNA3
        {
            get { return mstrCOLUMNA3; }
            set { mstrCOLUMNA3 = value; }
        }

        private string mstrCOLUMNA4 = "";
        public string COLUMNA4
        {
            get { return mstrCOLUMNA4; }
            set { mstrCOLUMNA4 = value; }
        }

        private string mstrCOLUMNA5 = "";
        public string COLUMNA5
        {
            get { return mstrCOLUMNA5; }
            set { mstrCOLUMNA5 = value; }
        }

        private object mobjauxiliar;
        /// <summary>
        /// Propiedad utilizada para fines generales. Ej: al realizar un ABM poder enviar un mensaje del servidor al cliente
        /// </summary>
        public object Auxiliar
        {
            get { return mobjauxiliar; }
            set { mobjauxiliar = value; }
        }
        /// <summary>
        /// Clase que puede ser utilizada por el método List<T>.Sort() de tal manera de enlazar la lista a una grilla en forma ordenada
        /// </summary>
        public class Sort : IComparer<SE_PARAMETROS>
        {
            public int Compare(SE_PARAMETROS x, SE_PARAMETROS y)
            {
                throw new NotImplementedException();
            }
        }
    }
    [Serializable]
    public class SE_TERMINALES
    {

        private int IDTERMINALField;

        private string cODTERMINALField;

        private string nOMBRECOMPUTADORAField;

        private string uSOHABILITADOField;

        private string mODELOPROCESADORField;

        private long CANTMEMORIARAMField;

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
        public long CANTMEMORIARAM
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
}

