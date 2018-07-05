using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Security;
using System.Security.Principal;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace COA.Framework.Log
{
    public class LogEntLib
    {
        private const string SEPARADOR_LINEAS = "--------------------------------------------------------------------------------------------------------";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Log(Exception ex, TraceEventType Nivel)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.Message = Encabezado() + Environment.NewLine + SerializarEx(ex).ToString() + Environment.NewLine + RecuperarDatosHost().ToString();
            logEntry.Title = "Exception";
            logEntry.Severity = Nivel;
            Logger.Write(logEntry);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Log(string Mensaje, TraceEventType Nivel)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.Message = Encabezado() + Environment.NewLine + SEPARADOR_LINEAS + Environment.NewLine + "Mensaje: " + Mensaje.Trim() + Environment.NewLine + RecuperarDatosHost().ToString();
            logEntry.Title = "MensajeNormal";
            logEntry.Severity = Nivel;
            Logger.Write(logEntry);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Log(DbException sqlEx, TraceEventType Nivel)
        {
            Log(sqlEx, Nivel);
        }

        private static void Log(SqlException sqlEx, TraceEventType Nivel)
        {
            if (sqlEx.Errors != null)
            {
                StringBuilder sblErrorSQL = new StringBuilder();
                StringBuilder sblErrorSQLTotal = new StringBuilder();
                int intI = 0;

                foreach (SqlError objErrorSQL in sqlEx.Errors.OfType<SqlError>())
                {
                    sblErrorSQL.Clear();
                    sblErrorSQL.Append("Error SQL ");
                    sblErrorSQL.Append(++intI);
                    sblErrorSQL.Append(": ");
                    foreach (PropertyInfo pInfo in objErrorSQL.GetType().GetProperties())
                    {
                        sblErrorSQL.Append("(");
                        sblErrorSQL.Append(pInfo.Name);
                        sblErrorSQL.Append(", ");
                        sblErrorSQL.Append(pInfo.GetValue(objErrorSQL, null));
                        sblErrorSQL.Append(") ; ");
                    }
                    sblErrorSQLTotal.Append(sblErrorSQL.ToString());
                    sblErrorSQLTotal.Append("|");
                }
                Log("Errores SQL --> " + sblErrorSQLTotal.ToString(), TraceEventType.Critical);
            }

            StringBuilder s = SerializarEx(sqlEx);
            Log("SqlException" + s.ToString(), TraceEventType.Critical);
        }

        #region Funciones Auxiliares

        private static string Encabezado()
        {
            StringBuilder sblExMensaje = new StringBuilder();
            DateTime dtmFechaLog = DateTime.Now;
            sblExMensaje.AppendFormat("Exception Log Generated On {1} at {2}",
                                        Environment.NewLine, dtmFechaLog.ToLongDateString(),
                                        dtmFechaLog.ToLongTimeString());

            return sblExMensaje.ToString();
        }

        private static StringBuilder RecuperarDatosHost()
        {
            Dictionary<string, string> additionalInformation = new Dictionary<string, string>();
            if (additionalInformation == null)
            {
                additionalInformation = new Dictionary<string, string>();
            }
            try
            {
                additionalInformation.Add("ThreadIdentity", Thread.CurrentPrincipal.Identity.Name);
            }
            catch (SecurityException ex)
            {
                additionalInformation.Add("ThreadIdentity", "Insufficient permissions to access information. " + ex.Message);
            }
            catch (Exception ex)
            {
                additionalInformation.Add(".ThreadIdentity", "Error accessing information. " + ex.Message);
            }
            try
            {
                additionalInformation.Add("WindowsIdentity", WindowsIdentity.GetCurrent().Name);
            }
            catch (SecurityException ex)
            {
                additionalInformation.Add("WindowsIdentity", "Insufficient permissions to access information. " + ex.Message);
            }
            catch (Exception ex)
            {
                additionalInformation.Add("WindowsIdentity", "Error accessing information. " + ex.Message);
            }
            try
            {
                additionalInformation.Add("OSVersion", Environment.OSVersion.ToString());
            }
            catch (SecurityException ex)
            {
                additionalInformation.Add("OSVersion", "Insufficient permissions to access information. " + ex.Message);
            }
            catch (Exception ex)
            {
                additionalInformation.Add("OSVersion", "Error accessing information." + ex.Message);
            }
            try
            {
                additionalInformation.Add("CLRVersion", Environment.Version.ToString());
            }
            catch (SecurityException ex)
            {
                additionalInformation.Add("CLRVersion", "Insufficient permissions to access information" + ex.PermissionType.Name);
            }
            catch (Exception ex)
            {
                additionalInformation.Add("CLRVersion", "Error accessing information" + ex.Message);
            }
            try
            {
                additionalInformation.Add("CPU", CPU.NextValue().ToString() + "%");
                additionalInformation.Add("RAM", RAM.NextValue().ToString() + "MB");
            }
            catch (SecurityException ex)
            {
                additionalInformation.Add("CPU - RAM", "Insufficient permissions to access information" + ex.PermissionType.Name);
            }
            catch (Exception ex)
            {
                additionalInformation.Add("CPU - RAM", "Error accessing information" + ex.Message);
            }

            StringBuilder sblExMensaje = new StringBuilder();

            foreach (String strParam in additionalInformation.Keys)
            {
                sblExMensaje.AppendFormat("{0}{1}: {2}", Environment.NewLine, strParam, additionalInformation[strParam]);
            }

            return sblExMensaje;
        }

        private static StringBuilder SerializarEx(Exception ex)
        {
            try
            {
                StringBuilder sblExMensaje = new StringBuilder();

                if (ex == null)
                {
                    sblExMensaje.AppendFormat("{0}No Exception Information Exists{0}", Environment.NewLine);
                }
                else
                {
                    Dictionary<string, string> lstInfAdic;
                    Exception objEx = ex;
                    Int32 intCantEx = 1;
                    while (objEx != null)
                    {
                        sblExMensaje.AppendFormat("{2}{0}Exception Information: Exception #{1}{0}{2}",
                                                   Environment.NewLine, intCantEx.ToString(), SEPARADOR_LINEAS);

                        sblExMensaje.AppendFormat("{0}Exception Type: {1}", Environment.NewLine, objEx.GetType().FullName);


                        foreach (PropertyInfo objPropiedad in objEx.GetType().GetProperties())
                        {
                            if (objPropiedad.Name != "StackTrace" && objPropiedad.Name != "InnerException")
                            {
                                if (objPropiedad.GetValue(objEx, null) == null)
                                {
                                    sblExMensaje.AppendFormat("{0}{1}: NULL", Environment.NewLine, objPropiedad.Name);
                                }
                                else
                                {
                                    if (objPropiedad.Name == "AdditionalInformation")
                                    {
                                        if (objPropiedad.GetValue(objEx, null) != null)
                                        {
                                            lstInfAdic = ((Dictionary<string, string>)(objPropiedad.GetValue(objEx, null)));

                                            if ((lstInfAdic.Count > 0))
                                            {
                                                sblExMensaje.AppendFormat("{0}Additional Exception Information:", Environment.NewLine);

                                                foreach (String lstrParam in lstInfAdic.Keys)
                                                {
                                                    sblExMensaje.AppendFormat("{0}  {1}: {2}", Environment.NewLine, lstrParam, lstInfAdic[lstrParam]);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sblExMensaje.AppendFormat("{0}{1}: {2}", Environment.NewLine, objPropiedad.Name, objPropiedad.GetValue(objEx, null));
                                    }
                                }
                            }
                        }

                        if (objEx.StackTrace != null)
                        {
                            sblExMensaje.AppendFormat("{0}{1}{0}Stack Trace{0}{1}", Environment.NewLine, SEPARADOR_LINEAS);
                            sblExMensaje.AppendFormat("{0}{1}", Environment.NewLine, objEx.StackTrace);
                        }

                        objEx = objEx.InnerException;
                        intCantEx++;
                    }
                }
                return sblExMensaje.Append(Environment.NewLine);
            }
            catch (Exception LogEx)
            {
                throw new Exception("Error serializing  exception to text", LogEx);
            }
        }

        #endregion

        #region Recursos de la PC

        private static PerformanceCounter mobjRAM;
        private static PerformanceCounter RAM
        {
            get
            {
                if (mobjRAM == null)
                    mobjRAM = new PerformanceCounter("Memory", "Available MBytes");
                return mobjRAM;
            }
        }

        private static PerformanceCounter mobjCPU;
        private static PerformanceCounter CPU
        {
            get
            {
                if (mobjCPU == null)
                {
                    mobjCPU = new PerformanceCounter();
                    mobjCPU.CategoryName = "Processor";
                    mobjCPU.CounterName = "% Processor Time";
                    mobjCPU.InstanceName = "_Total";
                }
                return mobjCPU;
            }
        }

        #endregion
    }
}

