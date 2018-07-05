using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.WebCipol.Fachada;
using COA.WebCipol.Comun;
using COA.Cipol.Presentacion._UIHelpers;
using System.Data;
using Microsoft.VisualBasic;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarPoliticasGenerales;

namespace COA.Cipol.Presentacion
{
    /// <summary>
    /// Permite setear las políticas generales del Sistema.
    /// </summary>
    /// <history>
    /// [MartinV]          [miércoles, 31 de julio de 2013]       Modificado  GCP-Cambios 
    /// </history>
    public partial class frmParametros : PaginaPadre
    {
        /// <summary>
        /// Retorna el id de tarea que permite acceder al formulario.
        /// </summary>
        /// <history>
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// </history>
        public override string IDTarea
        {
            //Definición de Políticas generales
            get { return "1005"; }
        }
        #region ESTADO
        /// <summary>
        /// Indica si los parámetros están o no seteados.
        /// </summary>
        private Boolean mblnRegCreado
        {
            get
            {
                return (Boolean)HttpContext.Current.Session["RegCreado"];
            }
            set
            {
                HttpContext.Current.Session["RegCreado"] = value;
            }
        }


        /// <summary>
        /// Indica si los parámetros están o no seteados.
        /// </summary>
        private short mshtNivelContraseña
        {
            get
            {
                return (short)HttpContext.Current.Session["NivelContrasenia"];
            }
            set
            {
                HttpContext.Current.Session["NivelContrasenia"] = value;
            }
        }

        //public static frmkEntidadesEmpresariales.PadreCipolCliente gobjUsuarioCipol; //TODO[CIPOLWEB] 


        /// <summary>
        /// Array que va a contener los parámetros
        /// </summary>
        private string[] mstrParam
        {
            get
            {
                return (string[])HttpContext.Current.Session["Params"];
            }
            set
            {
                HttpContext.Current.Session["Params"] = value;
            }
        }

        //DataSet que se utiliza para administrar las políticas generales
        //System.Data.DataSet mdtsParam;
        //en ManejoSesión!
        #endregion


        #region COMPORTAMIENTO
        /// <summary> Validar auteticación SSO
        /// </summary>
        /// <returns></returns>
        private bool ValidarLoginSSO()
        {
            string strURL_SSO = System.Configuration.ConfigurationManager.AppSettings["ServicioValidacionToken"];
            if (strURL_SSO == null)
            {
                return false;
            }
            if (!String.IsNullOrWhiteSpace(strURL_SSO.Trim()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Al cargar la Página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Evento_load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                InicializarScripts();
                CargarComboNivelesPass();
                CargarPoliticasGenerales();
                divError.Visible = false;
                lblErrores.Text = lblSuccess.Text = string.Empty;
                divSuccess.Visible = false;
            }
        }

        /// <summary>
        /// Setea los scripts necesarios.
        /// </summary>
        private void InicializarScripts()
        {
            txtParametros_1.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtParametros_2.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtParametros_4.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtParametros_5.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtParametros_6.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtParametros_7.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtParametros_8.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
            txtLongMinPass.Attributes.Add("onkeypress", "javascript: return jfSoloNumerosEnteros(this, event);");
        }

        /// <summary>
        /// Carga el combo de niveles de seguridad de contraseña.
        /// </summary>
        private void CargarComboNivelesPass()
        {
            cboNivelPass.Items.Insert(0,
                    new ListItem(
                        COA.WebCipol.Comun.Constantes.RecuperarDescripcionGenuNivelSeguridad(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Sin_requerimiento_específico),
                        Convert.ToInt16(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Sin_requerimiento_específico).ToString()));

            cboNivelPass.Items.Insert(1,
                    new ListItem(
                        COA.WebCipol.Comun.Constantes.RecuperarDescripcionGenuNivelSeguridad(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_solo_por_letras),
                        Convert.ToInt16(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_solo_por_letras).ToString()));

            cboNivelPass.Items.Insert(2,
                    new ListItem(
                        COA.WebCipol.Comun.Constantes.RecuperarDescripcionGenuNivelSeguridad(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_solo_por_numeros),
                        Convert.ToInt16(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_solo_por_numeros).ToString()));

            cboNivelPass.Items.Insert(3,
                    new ListItem(
                        COA.WebCipol.Comun.Constantes.RecuperarDescripcionGenuNivelSeguridad(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_por_letras_y_numeros),
                        Convert.ToInt16(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_por_letras_y_numeros).ToString()));

            cboNivelPass.Items.Insert(4,
                    new ListItem(
                        COA.WebCipol.Comun.Constantes.RecuperarDescripcionGenuNivelSeguridad(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales),
                        Convert.ToInt16(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales).ToString()));
        }

        /// <summary>
        /// Carga los datos actuales de los parámetros.
        /// 
        /// TODO[CIPOLWEB]: hay pendientes.
        /// 
        /// </summary>
        private void CargarPoliticasGenerales()
        {
            //Recupero parámetros de políticas generales.
            COA.WebCipol.Fachada.FSeguridad facSeg = new FSeguridad();
            ManejoSesion.PoliticasGenerales = facSeg.RecuperarPoliticasGenerales(); //TODO[CIPOLWEB]: ver si usamos esta var de sesión o la misma que usa en el login!!!!

            if (ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS.Count > 0)
            {
                //Si existe un registro de parámetros, se debe realizar actualización
                mblnRegCreado = true;

                if (!System.Convert.IsDBNull(ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS[0].COLUMNA4))
                {
                    CifrarDatos.TresDES objEncriptarNET = new CifrarDatos.TresDES();
                    objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
                    objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;

                    string strParamsEncriptados = ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS[0].COLUMNA4;
                    string strParams = objEncriptarNET.Criptografia(CifrarDatos.Accion.Desencriptacion, strParamsEncriptados);
                    mstrParam = Strings.Split(strParams, Constantes.gstrSepParam);

                    //TODO[CIPOLWEB]: ver esto del modo permisivo restrictivo, como sería. Probar.
                    //Angel - Gcp Cambios ID: ???? sistema permisivo o restrictivo.
                    //Parametro 12 - Define si el sistema es PERMISIVO o RESTRICTIVO
                    //con respecto a la asignacion de nuevas tareas y roles.
                    //verificamos si la directiva de seguridad
                    //no estaba en el sistema (esto es un control por
                    //viejas versiones)
                    if (mstrParam.GetUpperBound(0) < 12)
                    {
                        optModoPermisivo.Checked = false;
                        optModoRestrictivo.Checked = true;
                    }
                    else
                    {
                        //TODO[CIPOLWEB]: ver esto del modo permisivo restrictivo, como sería. Probar.
                        //hay casos en que los 2 quedan como false???????????????? si no está ese param...
                        optModoPermisivo.Checked = (System.Convert.ToInt16(mstrParam[12]) == 0);
                        optModoRestrictivo.Checked = (System.Convert.ToInt16(mstrParam[12]) == 1);
                    }

                    //Seguridad mixta
                    if (!ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio && !ValidarLoginSSO())
                    {
                        this.txtParametros_1.Text = mstrParam[1];
                        this.txtParametros_2.Text = mstrParam[2];
                        this.txtLongMinPass.Text = mstrParam[3];
                        this.txtParametros_4.Text = mstrParam[4];
                        this.txtParametros_5.Text = mstrParam[5];
                        this.txtParametros_6.Text = mstrParam[6];
                        this.txtParametros_7.Text = mstrParam[7];
                        this.txtParametros_8.Text = mstrParam[8];

                        //Desencripta el campo que hace referencia al nivel de seguridad.
                        this.mshtNivelContraseña = System.Convert.ToInt16(objEncriptarNET.Criptografia(CifrarDatos.Accion.Desencriptacion, ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS[0].COLUMNA5));
                        this.cboNivelPass.SelectedValue = mshtNivelContraseña.ToString();
                    }
                    else //Seguridad integrada al dominio.
                    {
                        this.txtLongMinPass.Text = mstrParam[3];
                        this.txtLongMinPass.Enabled = false;
                        this.txtParametros_1.Enabled = false;
                        this.txtParametros_2.Enabled = false;
                        this.txtParametros_4.Enabled = false;
                        this.txtParametros_5.Enabled = false;
                        this.txtParametros_6.Enabled = false;
                        this.txtParametros_7.Enabled = false;
                        this.txtParametros_8.Enabled = false;
                        this.txtParametros_8.Enabled = false;

                        //Angel - Gcp Cambios ID:3044
                        //al ser seguridad integrada, movemos al combo
                        //en su primer opcion que es "sin requerimiento especifico"
                        mshtNivelContraseña = Convert.ToInt16(COA.WebCipol.Comun.Constantes.genuNivelSeguridad.Sin_requerimiento_específico);
                        this.cboNivelPass.SelectedValue = mshtNivelContraseña.ToString();
                        this.cboNivelPass.Enabled = false;
                    }
                }
            }
            //lblMensajeContrasenia.Text = lblMensajeContrasenia.Text.Replace("#NS#", gobjGeneral.NombreSistema.Trim())
        }


        /// <summary>
        /// Registra los cambios realizados sobre los parámetros.
        /// 
        /// TODO[CIPOLWEB]: hay pendientes.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //Autor: Gustavo Mazzaglia
            //Fecha de creación: 04/12/2001
            //Modificaciones:
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //                  DESCRIPCION DE VARIABLES LOCALES
            //strParam       : String que contiene los parámetros encriptados
            //bytBloqTer     : Cantidad de tiempo por bloqueo de terminal
            //bytBloqPan     : cantidad de timepo para bloquear el panel de ejecución
            //strAuditoria   : String que va a contener los datos de auditoría a realizar
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            lblSuccess.Text = "";
            divSuccess.Visible = false;
            lblErrores.Text = "";
            divError.Visible = false;

            string strParam = null;
            string strAuditoria = string.Empty;
            CifrarDatos.TresDES objEncriptarNET = new CifrarDatos.TresDES();
            dcAdministrarPoliticasGenerales dcDatos = new dcAdministrarPoliticasGenerales();
            //Valida parámetros ingresados.
            if (!ValidarDatos())
            {
                return;
            }

            ///
            if (mblnRegCreado)
            {
                //Si se debe auditar
                if (mstrParam[1] != this.txtParametros_1.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(410, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_1.Text);
                if (mstrParam[2] != this.txtParametros_2.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(420, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_2.Text);
                if (mstrParam[3] != this.txtLongMinPass.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(430, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtLongMinPass.Text);
                //longitud de contraseña.
                if (mstrParam[4] != this.txtParametros_4.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(440, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_4.Text);
                //duracion en dias de la contraseña
                if (mstrParam[5] != this.txtParametros_5.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(450, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_5.Text);
                if (mstrParam[6] != this.txtParametros_6.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(460, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_6.Text);
                if (mstrParam[7] != this.txtParametros_7.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(470, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_7.Text);
                //cantidad de ocntraseñas almacenadas 
                if (mstrParam[8] != this.txtParametros_8.Text)
                    strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(480, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.txtParametros_8.Text);

                //ditempo de inactividad
                //TODO[CIPOLWEB]: probar si es lo mismo a como estaba antes!
                //if (UBound(mstrParam) > 11) //??
                if (mstrParam.GetUpperBound(0) > 11)
                {
                    if (mstrParam[12] != (optModoPermisivo.Checked ? 0 : 1).ToString().Trim())
                    {
                        strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(520, "", ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, (optModoPermisivo.Checked ? "Permisivo" : "Restrictivo").ToString().Trim());
                    }
                }
                if (mstrParam.GetUpperBound(0) == 12)
                {
                    if (this.mshtNivelContraseña != System.Convert.ToInt16(this.cboNivelPass.SelectedValue))
                    {
                        strAuditoria += COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(530, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, this.cboNivelPass.Text, "");
                    }
                }
            }


            //TODO[CIPOLWEB]
            //[cipolweb] Eliminado -> - cantidad de tiempo, en minutos, para bloquear cada aplicación por inactividad
            strParam = strParam + 0 + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_1.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_2.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtLongMinPass.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_4.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_5.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_6.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_7.Text + Constantes.gstrSepParam;
            strParam = strParam + this.txtParametros_8.Text + Constantes.gstrSepParam;

            //TODO[CIPOLWEB]: se agregan estos 2 ceros porque antes existían 2 parámetos (9 y 10) que no están más. 
            //strParam = strParam + Convert.ToInt16("0" + this.txtParametros_9.Text).ToString() + Constantes.gstrSepParam;
            //strParam = strParam + Convert.ToInt16("0" + this.txtParametros_10.Text).ToString() + Constantes.gstrSepParam;
            strParam = strParam + 0 + Constantes.gstrSepParam;
            strParam = strParam + 0 + Constantes.gstrSepParam;
            //TODO[CIPOLWEB]
            //[cipolweb] Eliminado -> - cantidad de tiempo, en minutos, para bloquear el panel de ejecución por inactividad (0=2 minutos)
            strParam = strParam + 0;
            

            //Angel Lubenov - Gcp Cambios ID:????
            //se agrega un nuevo parametro, la permision sobre el agregado de
            //una nueva tarea.
            strParam = strParam + Constantes.gstrSepParam;
            //Angel Lubenov - Gcp Cambios ID:????
            //dependiendo de la opcion elejida en el option button, seteamos
            //el parametro de permision para decir si la asignacion de roles
            //va a ser PERMISIVA o RESTRICTIVA
            strParam = strParam + (optModoPermisivo.Checked == true ? "0" : "1").ToString();


            //Encripta los parámetros
            objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
            objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
            strParam = objEncriptarNET.Criptografia(CifrarDatos.Accion.Encriptacion, strParam);


            //Ingresa los parámetros
            if (mblnRegCreado)
            {
                //Si hubo cambios
                if (strAuditoria == "")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "",
                    //"page.MostrarMensaje('No se ha realizado ninguna modificación que guardar.');", true);
                    lblErrores.Text = "No se ha realizado ninguna modificación que guardar.";
                    divError.Visible = true;
                    return;
                }
                else
                {
                    COA.WebCipol.Entidades.SE_PARAMETROS fila = ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS[0];

                    //Registra Columna 4 [son los parámetros numéricos]. (ya está encriptada)
                    fila.COLUMNA4 = strParam;

                    //Registra Columna 5 [es el parámetros de nivel de seg de la pass]. (se encripta ahora)
                    objEncriptarNET = new CifrarDatos.TresDES();
                    objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
                    objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
                    fila.COLUMNA5 = objEncriptarNET.Criptografia(CifrarDatos.Accion.Encriptacion, this.cboNivelPass.SelectedValue.ToString());

                    //ManejoSesion.PoliticasGenerales.Tables[0].Rows.Add(new string[] { strAuditoria, "" });
                    ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS.Add(new COA.WebCipol.Entidades.SE_PARAMETROS() { COLUMNA4 = strAuditoria });

                    foreach (COA.WebCipol.Entidades.SE_PARAMETROS item in ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS)
                        dcDatos.lstSE_PARAMETROS.Add(new SE_PARAMETROS() { COLUMNA4 = item.COLUMNA4, COLUMNA5 = item.COLUMNA5 });
                }
            }
            else
            {
                objEncriptarNET = new CifrarDatos.TresDES();
                objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
                objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;

                ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS.Add(new COA.WebCipol.Entidades.SE_PARAMETROS() { COLUMNA4 = strParam, COLUMNA5 = objEncriptarNET.Criptografia(CifrarDatos.Accion.Encriptacion, this.cboNivelPass.SelectedValue.ToString()) });

                foreach (COA.WebCipol.Entidades.SE_PARAMETROS item in ManejoSesion.PoliticasGenerales.lstSE_PARAMETROS)
                    dcDatos.lstSE_PARAMETROS.Add(new SE_PARAMETROS() { COLUMNA4 = item.COLUMNA4, COLUMNA5 = item.COLUMNA5, Added = true });
            }

            //Registra modificaciones en la BD.
            COA.WebCipol.Fachada.FSeguridad facSeg = new FSeguridad();





            if (facSeg.AdministrarPoliticasGenerales(dcDatos))
            {
                ManejoSesion.gudParam.ModoAsignacionTareasYRoles = Convert.ToInt32((optModoPermisivo.Checked ? 0 : 1));
                //ScriptManager.RegisterStartupScript(this, GetType(), "",
                // "page.MostrarMensaje('Los datos fueron guardados correctamente.');", true);

                lblSuccess.Text = "Los datos fueron guardados correctamente.";
                divSuccess.Visible = true;
                CargarPoliticasGenerales();
                return;
            }
            else
            {
                lblErrores.Text = "No se han podido actualizar las Políticas Generales de Seguridad";
                divError.Visible = true;
                //ScriptManager.RegisterStartupScript(this, GetType(), "",

                // "page.MostrarMensaje('No se han podido actualizar las Políticas Generales de Seguridad');", true);
                return;
            }
        }

        /// <summary>
        /// Verifica que los parámetros ingresados sean válidos.
        /// 
        /// TODO[CIPOLWEB]: hay que probarlo bien.
        ///
        /// </summary>
        private bool ValidarDatos()
        {
            lblErrores.Text = "Errores:" + "<br />";

            if (!Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_1.Text)
                || !Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_2.Text)
                || !Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_4.Text)
                || !Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_5.Text)
                || !Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_6.Text)
                || !Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_7.Text)
                || !Validaciones.ValidarCadenaNulaOVacia(this.txtParametros_8.Text)
                )
            {
                lblErrores.Text = "- Debe ingresar todos los parámetros.";
                divError.Visible = true;
                // ScriptManager.RegisterStartupScript(this, GetType(), "",
                //  "page.MostrarMensaje('Debe ingresar todos los parámetros.');", true);
                return false;
            }


            if (System.Convert.ToByte(this.txtParametros_5.Text) > System.Convert.ToByte(this.txtParametros_4.Text))
            {
                lblErrores.Text = "- La duración mínima de la contraseña no puede ser mayor a la duración máxima.";
                // ScriptManager.RegisterStartupScript(this, GetType(), "",
                // "page.MostrarMensaje('La duración mínima de la contraseña no puede ser mayor a la duración máxima.');", true);
                this.txtParametros_5.Focus();
                divError.Visible = true;
                return false;
            }
            //Si la cantidad de días de antelación de aviso de cambio de clave se activa
            if (this.txtParametros_6.Text != "0")
            {
                //Si la diferencia entre la duración mínima de la clave
                //y la duración máxima de la clave es > 1
                if (System.Convert.ToByte(this.txtParametros_4.Text) - System.Convert.ToByte(this.txtParametros_5.Text) > 1) //tobyte??? lo paso a toint??
                {
                    //Si la diferencia entre la duración máxima de la password y la cantidad de días
                    //de antelación es <= a la duración mínima de la password
                    if ((System.Convert.ToInt16(this.txtParametros_4.Text) - System.Convert.ToInt16(this.txtParametros_6.Text))
                            <= System.Convert.ToInt16(this.txtParametros_5.Text))
                    {
                        divError.Visible = true;
                        lblErrores.Text = "- Imposible establecer la antelación selecionada. Ingrese un valor de días intermedio entre la duración mínima y máxima de la password.";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "",
                        //"page.MostrarMensaje('Imposible establecer la antelación selecionada. Ingrese un valor de días intermedio entre la duración mínima y máxima de la password.');", true);
                        this.txtParametros_6.Focus();
                        return false;
                    }
                }
                else
                {
                    divError.Visible = true;
                    lblErrores.Text = "- Imposible establecer la antelación selecionada. Ingrese un valor de días intermedio entre la duración mínima y máxima de la password.";
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "",
                    //"page.MostrarMensaje('Imposible establecer la antelación selecionada. Ingrese un valor de días intermedio entre la duración mínima y máxima de la password.');", true);
                    this.txtParametros_6.Focus();
                    return false;
                }
            }

            //Validaciones OK
            divError.Visible = false;
            lblErrores.Text = "";
            return true;
        }
        #endregion

    }
}