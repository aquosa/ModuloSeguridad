using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace COA.WebCipol.Inicio.Utiles
{
    public class cPrincipal
    {
        #region Auditoría

        public static string MensajeAuditoria(short CodMensaje, string Usuario = "", string UsuarioAdm = "", string NuevoValor = "", string ValorOriginal = "")
        {
            // '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            // '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //                   DESCRIPCION DE VARIABLES LOCALES
            // strMensaje : Mensaje que se obtiene cuando se reemplazan las valores
            //              din�micos (@)
            //              (#) Para el valor actual antes del cambio
            // rowAudit   : Objeto DataRow que contiene el c�digo de auditor�a
            // '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            string strMensaje = String.Empty;
            bool blnNuevoValorNoUtilizado = true;

            //No vamos a obtener el nombre de la pc, sólo vamos a guardar la ip.
            //string strNombrePC = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            string strNumeroIP = ObtenerTerminal("");  //GetIp();

            //strNumeroIP = (strNumeroIP + (" - " + strNombrePC));

            COA.WebCipol.Entidades.SE_CODAUDITORIA rowAudit = null;
            rowAudit = COA.Cipol.Inicio._UIHelpers.ManejoSesion.gdtsAuditoria.lstCodAuditoria.FirstOrDefault(p => p.CODAUDITORIA == CodMensaje);

            switch (CodMensaje)
            {
                case 100:
                    // Proceso de Login
                    strMensaje = rowAudit.TEXTOAUDITORIA.Replace("@", System.Environment.MachineName);
                    strMensaje = (strMensaje + (" (realizado desde la PC: " + (strNumeroIP + ")")));
                    break;
                case 110:
                case 230:
                case 250:
                    // Proceso de Login
                    strMensaje = Microsoft.VisualBasic.Strings.Replace(rowAudit.TEXTOAUDITORIA, "@", Usuario, Count: 1, Compare: Microsoft.VisualBasic.CompareMethod.Text);
                    strMensaje = strMensaje.Replace("@", System.Environment.MachineName);
                    strMensaje = (strMensaje + (" (realizado desde la PC: " + (strNumeroIP + ")")));
                    break;
                case 120:
                case 130:
                case 140:
                case 150:
                case 160:
                case 170:
                case 180:
                case 190:
                case 200:
                case 210:
                case 220:
                case 240:
                case 205:
                    // Proceso de Login
                    strMensaje = rowAudit.TEXTOAUDITORIA.Replace("@", Usuario);
                    strMensaje = (strMensaje + (" (realizado desde la PC: " + (strNumeroIP + ")")));
                    break;
                case 260:
                case 270:
                    // Supervision
                    strMensaje = Microsoft.VisualBasic.Strings.Replace(rowAudit.TEXTOAUDITORIA, "@", System.Environment.MachineName, Count: 1, Compare: Microsoft.VisualBasic.CompareMethod.Text);
                    strMensaje = strMensaje.Replace("@", Usuario);
                    strMensaje = (strMensaje + (" (realizado desde la PC: " + (strNumeroIP + ")")));
                    break;
                case 400:
                case 410:
                case 420:
                case 430:
                case 440:
                case 450:
                case 460:
                case 470:
                case 480:
                case 490:
                case 500:
                case 510:
                case 520:
                case 530:
                    // Pol�ticas de Seguridad
                    // Usuario
                    strMensaje = Microsoft.VisualBasic.Strings.Replace(rowAudit.TEXTOAUDITORIA, "@", UsuarioAdm, Count: 1, Compare: Microsoft.VisualBasic.CompareMethod.Text);
                    // TODO: Labeled Arguments not supported. Argument: 4 := 'Count'
                    // TODO: Labeled Arguments not supported. Argument: 5 := 'Compare'
                    strMensaje = Microsoft.VisualBasic.Strings.Replace(strMensaje, "@", strNumeroIP, Count: 1, Compare: Microsoft.VisualBasic.CompareMethod.Text);
                    // TODO: Labeled Arguments not supported. Argument: 4 := 'Count'
                    // TODO: Labeled Arguments not supported. Argument: 5 := 'Compare'
                    // Valor Original
                    strMensaje = Microsoft.VisualBasic.Strings.Replace(strMensaje, "@", ValorOriginal, Count: 1, Compare: Microsoft.VisualBasic.CompareMethod.Text);
                    // TODO: Labeled Arguments not supported. Argument: 4 := 'Count'
                    // TODO: Labeled Arguments not supported. Argument: 5 := 'Compare'
                    // Nuevo Valor a asignar
                    strMensaje = strMensaje.Replace("@", NuevoValor);
                    blnNuevoValorNoUtilizado = false;
                    break;
                case 600:
                case 610:
                case 620:
                case 750:
                case 760:
                case 770:
                case 780:
                    // Administracion de Usuarios
                    strMensaje = Microsoft.VisualBasic.Strings.Replace(rowAudit.TEXTOAUDITORIA, "@", UsuarioAdm,Count: 1,Compare: Microsoft.VisualBasic.CompareMethod.Text);
                    strMensaje = strMensaje.Replace("@", Usuario);
                    strMensaje = (strMensaje + (" (realizado desde la PC: " + (strNumeroIP + ")")));
                    break;
                case 700:
                case 710:
                case 720:
                case 730:
                case 740:
                    // ABM de Seguridad�
                    strMensaje = rowAudit.TEXTOAUDITORIA.Replace("@", UsuarioAdm);
                    strMensaje = (strMensaje + (" (realizado desde la PC: " + (strNumeroIP + ")")));
                    break;
            }
            if ((blnNuevoValorNoUtilizado && (NuevoValor.Length > 0)))
            {
                strMensaje += "\r\n";
                strMensaje += NuevoValor;
            }
            return CodMensaje + "Æ" + strMensaje + "Æ" + Usuario + "Æ" + UsuarioAdm + "æ"; //Alt 146 y 145
        }
        
        #endregion

        public static string ObtenerTerminal(string ip)
        {

            if (((System.Configuration.ConfigurationManager.AppSettings["EnviarDireccionIP"] == null) || (System.Configuration.ConfigurationManager.AppSettings["EnviarDireccionIP"] != "S")))
            {
                return ObtenerNETBIOS(); //System.Environment.MachineName; RECUPERABA EL NOMBRE DEL SERVER;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ServicioPublicoIP"] == "S")
                {
                    return ip;
                }
                else 
                {
                    bool strUtilizarMetodoDNS = !((System.Configuration.ConfigurationManager.AppSettings["UtilizarMetodoDNS"] == null) || (System.Configuration.ConfigurationManager.AppSettings["UtilizarMetodoDNS"] != "S"));
                    bool strUtilizarIPV6 = !((System.Configuration.ConfigurationManager.AppSettings["UtilizarIPV6"] == null) || (System.Configuration.ConfigurationManager.AppSettings["UtilizarIPV6"] != "S"));

                    return ObtenerDirecciónIP(strUtilizarMetodoDNS, strUtilizarIPV6);
                }
            }
        }

        private static string ObtenerDirecciónIP(bool blnUtilizarMetodoDNS, bool blnUtilizarIPV6)
        {
            if (blnUtilizarMetodoDNS)
            {
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
                IPAddress ipAddress = null;
                string strListaDeIps = "";
                for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
                {
                    if (blnUtilizarIPV6)
                    {
                        if (ipHostInfo.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            ipAddress = ipHostInfo.AddressList[i];
                            return ipAddress.ToString();
                        }
                    }
                    else //UtilizarIPV4
                    {
                        if (ipHostInfo.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddress = ipHostInfo.AddressList[i];
                            return ipAddress.ToString();
                        }
                    }
                    strListaDeIps += ipHostInfo.AddressList[i].ToString() + " - ";
                }

                throw new Exception("La dirección IP que se está tratando de recuperar no coincide con los soportados por CIPOL. Consulte al administrador del sistema. " + "Nombre del Host: " + strHostName + ". Lista de IPs: " + strListaDeIps);
            }
            else
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string sIPAddress = null;

                sIPAddress = context.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
                if (string.IsNullOrEmpty(sIPAddress))
                {
                    sIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(sIPAddress))
                    {
                        sIPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    else
                    {
                        string[] ipArray = sIPAddress.Split(new Char[] { ',' });
                        sIPAddress = ipArray[0];
                    }
                }
                return sIPAddress;
            }


        }

        private static string ObtenerNETBIOS()
        {
            return System.Environment.MachineName;
        }

    }
}