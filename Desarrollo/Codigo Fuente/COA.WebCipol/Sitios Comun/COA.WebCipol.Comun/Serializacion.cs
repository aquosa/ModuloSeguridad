using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;

namespace COA.WebCipol.Comun
{
    namespace Serializacion
    {
        public class SerializacionBin
        {
            /// <summary>
            /// Permite serializar un objeto en un array de bytes
            /// </summary>
            /// <param name="objInstancia">Objeto que deseo serializar</param>
            /// <history>
            /// [LucianoP]          [viernes, 09 de enero de 2009]       Creado
            /// </history>
            public static byte[] Serializar(object objInstancia)
            {
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //               DESCRIPCION DE VARIABLES LOCALES
                // objFlujoDeMemoria : Flujo de memoria donde se guarda el objeto serializado
                // objFormateador    : Objeto utilizado para serializar el flujo de memoria
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                MemoryStream objFlujoDeMemoria = new MemoryStream();
                BinaryFormatter objFormateador = new BinaryFormatter();

                objFormateador.Serialize(objFlujoDeMemoria, objInstancia);

                return objFlujoDeMemoria.ToArray();
            }

            /// <summary>
            /// Permite deserializar un objeto
            /// </summary>
            /// <param name="objInstancia">Objeto serializado en un array de bytes que deseo
            /// deserializar</param>
            /// <history>
            /// [LucianoP]          [viernes, 09 de enero de 2009]       Creado
            /// </history>
            public static object Deserializar(byte[] objInstancia)
            {
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //               DESCRIPCION DE VARIABLES LOCALES
                // objFlujoDeMemoria : Flujo de memoria donde se guarda el objeto serializado
                // objFormateador    : Objeto utilizado para serializar el flujo de memoria
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                MemoryStream objFlujoDeMemoria = new MemoryStream(objInstancia);
                BinaryFormatter objFormateador = new BinaryFormatter();

                return objFormateador.Deserialize(objFlujoDeMemoria);
            }

            /// <summary>
            /// Serializa un objecto en formato JSON
            /// </summary>
            /// <param name="objInstancia">Objeto a serializar</param>
            /// <returns></returns>
            /// <history>
            /// [LucianoP]          [miércoles, 24 de octubre de 2012]       Creado
            /// </history>
            public static byte[] SerializarJSON2ByteArray(object objInstancia)
            {
                DataContractJsonSerializer objSerializadorJSon = null;
                MemoryStream objMemoria = null;
                byte[] bytRet;

                try
                {
                    objSerializadorJSon = new DataContractJsonSerializer(objInstancia.GetType());
                    objMemoria = new MemoryStream();

                    objSerializadorJSon.WriteObject(objMemoria, objInstancia);
                    bytRet = objMemoria.ToArray();
                    objMemoria.Close();

                    return bytRet;
                }
                catch (Exception)
                {
                    //COA.Log.Grabar.Log(ex, System.Diagnostics.TraceEventType.Error);
                    throw;
                }
            }

        }
    }
}