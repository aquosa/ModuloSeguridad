using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsCipolServices.ConectorWSCipolNET
{
    public class ConectorwsSIRActualizaciones
    {
        private Fachada.Seguridad.Downloader.wsSIRActualizaciones obj;

        public bool ExisteActualizacion(DateTime UltimaFechaActualizacion)
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.ExisteActualizacion(UltimaFechaActualizacion);
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return false;
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.ExisteActualizacion(UltimaFechaActualizacion);
        }

        public string[] RecuperarListaArchivos()
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.RecuperarListaArchivos();
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return null;
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.RecuperarListaArchivos();
        }

        public string RecuperarURLActualizador_ServidorLAN()
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.RecuperarURLActualizador_ServidorLAN();
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return "";
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.RecuperarURLActualizador_ServidorLAN();
        }

        public System.Data.DataSet RecuperarArchivosADescagar(System.Data.DataSet dtsCliente)
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.RecuperarArchivosADescagar(dtsCliente);
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return null;
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.RecuperarArchivosADescagar(dtsCliente);
        }

        public byte[] DescargarArchivo(string Nombre)
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.DescargarArchivo(Nombre);
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return null;
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.DescargarArchivo(Nombre);
        }

        public byte[] DescargarArchivoParcial(string Nombre, int intOffset, ref int intLeido)
        {
            //try
            //{
            //obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //return obj.DescargarArchivoParcial(Nombre, intOffset, ref intLeido);
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return null;
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.DescargarArchivoParcial(Nombre, intOffset, ref intLeido);
        }

        public DateTime RecuperarFechaLiberacionVersion()
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.RecuperarFechaLiberacionVersion();
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return DateTime.Now.Date;
            obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            return obj.RecuperarFechaLiberacionVersion();
        }

        public string RecuperarNombreServidor()
        {
            //try
            //{
            //    obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
            //    return obj.RecuperarNombreServidor();
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return "";
           obj = new Fachada.Seguridad.Downloader.wsSIRActualizaciones();
           return obj.RecuperarNombreServidor();
        }
    }
}