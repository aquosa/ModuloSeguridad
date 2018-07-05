using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace COA.Logger
{
    public static class Logueador
    {
        private const string SEPARADOR_LINEAS = "--------------------------------------------------------------------------------------------------------";

        public static void Loggear(Exception Ex, EventLogEntryType TipoError)
        {
            try
            {
                string strAuditarEvent = System.Configuration.ConfigurationManager.AppSettings["AuditarExcepciones"];

                if (String.IsNullOrEmpty(strAuditarEvent))
                {
                    throw new Exception("No se encuentra especificado en el archivo de configuración la clave AuditarExcepciones o se encuentra vacío. Consulte al administrador del sistema");
                }

                if (strAuditarEvent.ToUpper().Equals("NO"))
                {
                    return;
                }

                if (!strAuditarEvent.ToUpper().Equals("SI"))
                {
                    throw new Exception("El valor de la clave AuditarExcepciones se encuentra mal especificado en el archivo de configuración. Los posibles valores son SI y NO. ");
                }

                string strRegistroEvent = System.Configuration.ConfigurationManager.AppSettings["RegistrodeEventos"];
                string strOrigenEvent = System.Configuration.ConfigurationManager.AppSettings["OrigenRegistroEventos"];

                if (String.IsNullOrEmpty(strRegistroEvent))
                {
                    throw new Exception("El Registro de Eventos no se encuentra en el archivo de configuración o se encuentra vacío. Consulte al administrador del sistema");
                }
                if (String.IsNullOrEmpty(strOrigenEvent))
                {
                    throw new Exception("El Origen de Eventos no se encuentra en el archivo de configuración o se encuentra vacío. Consulte al administrador del sistema");
                }

                if (!EventLog.SourceExists(strRegistroEvent))
                {
                    EventLog.CreateEventSource(strOrigenEvent, strRegistroEvent);
                }

                EventLog myLog = new EventLog();
                myLog.Source = strOrigenEvent;
                myLog.WriteEntry(Encabezado() + Environment.NewLine + SerializarExcepcion(Ex), TipoError);
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al tratar de escribir en el Visor de Eventos. Consulte al administrador del sistema.\n Excepción: " + ex.Message);
            }
        }

        public static void Loggear(string Mensaje, EventLogEntryType TipoError)
        {
            try
            {
                string strAuditarEvent = System.Configuration.ConfigurationManager.AppSettings["AuditarExcepciones"];

                if (String.IsNullOrEmpty(strAuditarEvent))
                {
                    throw new Exception("No se encuentra especificado en el archivo de configuración la clave AuditarExcepciones o se encuentra vacío. Consulte al administrador del sistema");
                }

                if (strAuditarEvent.ToUpper().Equals("NO"))
                {
                    return;
                }

                if (!strAuditarEvent.ToUpper().Equals("SI"))
                {
                    throw new Exception("El valor de la clave AuditarExcepciones se encuentra mal especificado en el archivo de configuración. Los posibles valores son SI y NO. ");
                }

                string strRegistroEvent = System.Configuration.ConfigurationManager.AppSettings["RegistrodeEventos"];
                string strOrigenEvent = System.Configuration.ConfigurationManager.AppSettings["OrigenRegistroEventos"];

                if (String.IsNullOrEmpty(strRegistroEvent))
                {
                    throw new Exception("El Registro de Eventos no se encuentra en el archivo de configuración o se encuentra vacío. Consulte al administrador del sistema");
                }
                if (String.IsNullOrEmpty(strOrigenEvent))
                {
                    throw new Exception("El Origen de Eventos no se encuentra en el archivo de configuración o se encuentra vacío. Consulte al administrador del sistema");
                }

                if (!EventLog.SourceExists(strRegistroEvent))
                {
                    EventLog.CreateEventSource(strOrigenEvent, strRegistroEvent);
                }

                EventLog myLog = new EventLog();
                myLog.Source = strOrigenEvent;
                myLog.WriteEntry(Encabezado() + Environment.NewLine + Mensaje, TipoError);
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al tratar de escribir en el Visor de Eventos. Consulte al administrador del sistema.\n Excepción: " + ex.Message);
            }
        }

        private static string Encabezado()
        {
            StringBuilder sblExMensaje = new StringBuilder();
            DateTime dtmFechaLog = DateTime.Now;
            sblExMensaje.AppendFormat("Exception Log Generated On {1} at {2}",
                                        Environment.NewLine, dtmFechaLog.ToLongDateString(),
                                        dtmFechaLog.ToLongTimeString());

            return sblExMensaje.ToString();
        }

        private static string SerializarExcepcion(Exception ex)
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
                sblExMensaje.Append(Environment.NewLine);
                return sblExMensaje.ToString();
            }
            catch (Exception LogEx)
            {
                throw new Exception("Ha ocurrido un error al tratar de setrializar la excepción. " + LogEx.Message);
            }
        }



       
    }
}
