﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34014.
// 
#pragma warning disable 1591

namespace wsCipolServices.Fachada.Seguridad.Downloader {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsSIRActualizacionesSoap", Namespace="http://RGP/SIRActualizaciones/")]
    public partial class wsSIRActualizaciones : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ExisteActualizacionOperationCompleted;
        
        private System.Threading.SendOrPostCallback RecuperarListaArchivosOperationCompleted;
        
        private System.Threading.SendOrPostCallback RecuperarURLActualizador_ServidorLANOperationCompleted;
        
        private System.Threading.SendOrPostCallback RecuperarArchivosADescagarOperationCompleted;
        
        private System.Threading.SendOrPostCallback DescargarArchivoOperationCompleted;
        
        private System.Threading.SendOrPostCallback DescargarArchivoParcialOperationCompleted;
        
        private System.Threading.SendOrPostCallback RecuperarFechaLiberacionVersionOperationCompleted;
        
        private System.Threading.SendOrPostCallback RecuperarNombreServidorOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsSIRActualizaciones() {
            this.Url = global::wsCipolServices.Properties.Settings.Default.wsCipolServices_Fachada_Seguridad_Downloader_wsSIRActualizaciones;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ExisteActualizacionCompletedEventHandler ExisteActualizacionCompleted;
        
        /// <remarks/>
        public event RecuperarListaArchivosCompletedEventHandler RecuperarListaArchivosCompleted;
        
        /// <remarks/>
        public event RecuperarURLActualizador_ServidorLANCompletedEventHandler RecuperarURLActualizador_ServidorLANCompleted;
        
        /// <remarks/>
        public event RecuperarArchivosADescagarCompletedEventHandler RecuperarArchivosADescagarCompleted;
        
        /// <remarks/>
        public event DescargarArchivoCompletedEventHandler DescargarArchivoCompleted;
        
        /// <remarks/>
        public event DescargarArchivoParcialCompletedEventHandler DescargarArchivoParcialCompleted;
        
        /// <remarks/>
        public event RecuperarFechaLiberacionVersionCompletedEventHandler RecuperarFechaLiberacionVersionCompleted;
        
        /// <remarks/>
        public event RecuperarNombreServidorCompletedEventHandler RecuperarNombreServidorCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/ExisteActualizacion", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ExisteActualizacion(System.DateTime UltimaFechaActualizacion) {
            object[] results = this.Invoke("ExisteActualizacion", new object[] {
                        UltimaFechaActualizacion});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ExisteActualizacionAsync(System.DateTime UltimaFechaActualizacion) {
            this.ExisteActualizacionAsync(UltimaFechaActualizacion, null);
        }
        
        /// <remarks/>
        public void ExisteActualizacionAsync(System.DateTime UltimaFechaActualizacion, object userState) {
            if ((this.ExisteActualizacionOperationCompleted == null)) {
                this.ExisteActualizacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExisteActualizacionOperationCompleted);
            }
            this.InvokeAsync("ExisteActualizacion", new object[] {
                        UltimaFechaActualizacion}, this.ExisteActualizacionOperationCompleted, userState);
        }
        
        private void OnExisteActualizacionOperationCompleted(object arg) {
            if ((this.ExisteActualizacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExisteActualizacionCompleted(this, new ExisteActualizacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/RecuperarListaArchivos", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] RecuperarListaArchivos() {
            object[] results = this.Invoke("RecuperarListaArchivos", new object[0]);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void RecuperarListaArchivosAsync() {
            this.RecuperarListaArchivosAsync(null);
        }
        
        /// <remarks/>
        public void RecuperarListaArchivosAsync(object userState) {
            if ((this.RecuperarListaArchivosOperationCompleted == null)) {
                this.RecuperarListaArchivosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecuperarListaArchivosOperationCompleted);
            }
            this.InvokeAsync("RecuperarListaArchivos", new object[0], this.RecuperarListaArchivosOperationCompleted, userState);
        }
        
        private void OnRecuperarListaArchivosOperationCompleted(object arg) {
            if ((this.RecuperarListaArchivosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecuperarListaArchivosCompleted(this, new RecuperarListaArchivosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/RecuperarURLActualizador_ServidorLAN", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RecuperarURLActualizador_ServidorLAN() {
            object[] results = this.Invoke("RecuperarURLActualizador_ServidorLAN", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RecuperarURLActualizador_ServidorLANAsync() {
            this.RecuperarURLActualizador_ServidorLANAsync(null);
        }
        
        /// <remarks/>
        public void RecuperarURLActualizador_ServidorLANAsync(object userState) {
            if ((this.RecuperarURLActualizador_ServidorLANOperationCompleted == null)) {
                this.RecuperarURLActualizador_ServidorLANOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecuperarURLActualizador_ServidorLANOperationCompleted);
            }
            this.InvokeAsync("RecuperarURLActualizador_ServidorLAN", new object[0], this.RecuperarURLActualizador_ServidorLANOperationCompleted, userState);
        }
        
        private void OnRecuperarURLActualizador_ServidorLANOperationCompleted(object arg) {
            if ((this.RecuperarURLActualizador_ServidorLANCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecuperarURLActualizador_ServidorLANCompleted(this, new RecuperarURLActualizador_ServidorLANCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/RecuperarArchivosADescagar", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet RecuperarArchivosADescagar(System.Data.DataSet dtsCliente) {
            object[] results = this.Invoke("RecuperarArchivosADescagar", new object[] {
                        dtsCliente});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void RecuperarArchivosADescagarAsync(System.Data.DataSet dtsCliente) {
            this.RecuperarArchivosADescagarAsync(dtsCliente, null);
        }
        
        /// <remarks/>
        public void RecuperarArchivosADescagarAsync(System.Data.DataSet dtsCliente, object userState) {
            if ((this.RecuperarArchivosADescagarOperationCompleted == null)) {
                this.RecuperarArchivosADescagarOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecuperarArchivosADescagarOperationCompleted);
            }
            this.InvokeAsync("RecuperarArchivosADescagar", new object[] {
                        dtsCliente}, this.RecuperarArchivosADescagarOperationCompleted, userState);
        }
        
        private void OnRecuperarArchivosADescagarOperationCompleted(object arg) {
            if ((this.RecuperarArchivosADescagarCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecuperarArchivosADescagarCompleted(this, new RecuperarArchivosADescagarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/DescargarArchivo", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] DescargarArchivo(string Nombre) {
            object[] results = this.Invoke("DescargarArchivo", new object[] {
                        Nombre});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void DescargarArchivoAsync(string Nombre) {
            this.DescargarArchivoAsync(Nombre, null);
        }
        
        /// <remarks/>
        public void DescargarArchivoAsync(string Nombre, object userState) {
            if ((this.DescargarArchivoOperationCompleted == null)) {
                this.DescargarArchivoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDescargarArchivoOperationCompleted);
            }
            this.InvokeAsync("DescargarArchivo", new object[] {
                        Nombre}, this.DescargarArchivoOperationCompleted, userState);
        }
        
        private void OnDescargarArchivoOperationCompleted(object arg) {
            if ((this.DescargarArchivoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DescargarArchivoCompleted(this, new DescargarArchivoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/DescargarArchivoParcial", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] DescargarArchivoParcial(string Nombre, int intOffset, ref int intLeido) {
            object[] results = this.Invoke("DescargarArchivoParcial", new object[] {
                        Nombre,
                        intOffset,
                        intLeido});
            intLeido = ((int)(results[1]));
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void DescargarArchivoParcialAsync(string Nombre, int intOffset, int intLeido) {
            this.DescargarArchivoParcialAsync(Nombre, intOffset, intLeido, null);
        }
        
        /// <remarks/>
        public void DescargarArchivoParcialAsync(string Nombre, int intOffset, int intLeido, object userState) {
            if ((this.DescargarArchivoParcialOperationCompleted == null)) {
                this.DescargarArchivoParcialOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDescargarArchivoParcialOperationCompleted);
            }
            this.InvokeAsync("DescargarArchivoParcial", new object[] {
                        Nombre,
                        intOffset,
                        intLeido}, this.DescargarArchivoParcialOperationCompleted, userState);
        }
        
        private void OnDescargarArchivoParcialOperationCompleted(object arg) {
            if ((this.DescargarArchivoParcialCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DescargarArchivoParcialCompleted(this, new DescargarArchivoParcialCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/RecuperarFechaLiberacionVersion", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.DateTime RecuperarFechaLiberacionVersion() {
            object[] results = this.Invoke("RecuperarFechaLiberacionVersion", new object[0]);
            return ((System.DateTime)(results[0]));
        }
        
        /// <remarks/>
        public void RecuperarFechaLiberacionVersionAsync() {
            this.RecuperarFechaLiberacionVersionAsync(null);
        }
        
        /// <remarks/>
        public void RecuperarFechaLiberacionVersionAsync(object userState) {
            if ((this.RecuperarFechaLiberacionVersionOperationCompleted == null)) {
                this.RecuperarFechaLiberacionVersionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecuperarFechaLiberacionVersionOperationCompleted);
            }
            this.InvokeAsync("RecuperarFechaLiberacionVersion", new object[0], this.RecuperarFechaLiberacionVersionOperationCompleted, userState);
        }
        
        private void OnRecuperarFechaLiberacionVersionOperationCompleted(object arg) {
            if ((this.RecuperarFechaLiberacionVersionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecuperarFechaLiberacionVersionCompleted(this, new RecuperarFechaLiberacionVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://RGP/SIRActualizaciones/RecuperarNombreServidor", RequestNamespace="http://RGP/SIRActualizaciones/", ResponseNamespace="http://RGP/SIRActualizaciones/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RecuperarNombreServidor() {
            object[] results = this.Invoke("RecuperarNombreServidor", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RecuperarNombreServidorAsync() {
            this.RecuperarNombreServidorAsync(null);
        }
        
        /// <remarks/>
        public void RecuperarNombreServidorAsync(object userState) {
            if ((this.RecuperarNombreServidorOperationCompleted == null)) {
                this.RecuperarNombreServidorOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecuperarNombreServidorOperationCompleted);
            }
            this.InvokeAsync("RecuperarNombreServidor", new object[0], this.RecuperarNombreServidorOperationCompleted, userState);
        }
        
        private void OnRecuperarNombreServidorOperationCompleted(object arg) {
            if ((this.RecuperarNombreServidorCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecuperarNombreServidorCompleted(this, new RecuperarNombreServidorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void ExisteActualizacionCompletedEventHandler(object sender, ExisteActualizacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExisteActualizacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExisteActualizacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void RecuperarListaArchivosCompletedEventHandler(object sender, RecuperarListaArchivosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecuperarListaArchivosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecuperarListaArchivosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void RecuperarURLActualizador_ServidorLANCompletedEventHandler(object sender, RecuperarURLActualizador_ServidorLANCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecuperarURLActualizador_ServidorLANCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecuperarURLActualizador_ServidorLANCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void RecuperarArchivosADescagarCompletedEventHandler(object sender, RecuperarArchivosADescagarCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecuperarArchivosADescagarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecuperarArchivosADescagarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void DescargarArchivoCompletedEventHandler(object sender, DescargarArchivoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DescargarArchivoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DescargarArchivoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void DescargarArchivoParcialCompletedEventHandler(object sender, DescargarArchivoParcialCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DescargarArchivoParcialCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DescargarArchivoParcialCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
        
        /// <remarks/>
        public int intLeido {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void RecuperarFechaLiberacionVersionCompletedEventHandler(object sender, RecuperarFechaLiberacionVersionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecuperarFechaLiberacionVersionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecuperarFechaLiberacionVersionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.DateTime Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.DateTime)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void RecuperarNombreServidorCompletedEventHandler(object sender, RecuperarNombreServidorCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecuperarNombreServidorCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecuperarNombreServidorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591