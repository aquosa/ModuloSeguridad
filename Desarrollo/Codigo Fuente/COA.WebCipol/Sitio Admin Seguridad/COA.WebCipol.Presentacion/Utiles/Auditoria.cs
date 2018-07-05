using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.Utiles
{
    public class Auditoria
    {
        public string Clave { get; set; }
        public string Mensaje { get; set; }
        public int Cantidad { get; set; }
    }
    public class cAuditoria
    {
        static System.Collections.Generic.List<Auditoria> _colAudit
        {
            get
            {
                if (HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.AUDITORIAS] == null)
                    HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.AUDITORIAS] = new System.Collections.Generic.List<Auditoria>();
                return (System.Collections.Generic.List<Auditoria>)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.AUDITORIAS];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.AUDITORIAS] = value;
            }
        }

        public static void Auditar_Cambios(string clave, string mensaje)
        {
            Auditoria _audit = new Auditoria();
            _audit.Clave = clave;
            _audit.Mensaje = mensaje;
            _colAudit.Add(_audit);
        }


        public static void Limpiar()
        {
            _colAudit = null;
        }

        public static string Recuperar_Auditar_Cambios(string Titulo)
        {
            string strRet = "";
            System.Collections.Generic.List<Auditoria> _lstRet = new System.Collections.Generic.List<Auditoria>();
            int _intJ;
            // Gustavom - 05/06/2012
            // Retorna aquellas auditor�as que se aplicaron sobre items que realmente se eliminaron o agregaron
            // Es decir, este algoritmo trata de resolver el no tener que comparar con lo que estaba persistido anteriormente
            // para poder determinar que cambio. 
            // Ejemplo, utilizando Roles: 
            // es una pantalla que se basa en dos solapas, la izquierda con las tareas a asignar y a la derecha las asignadas
            // Si yo quito o agrego, las que realmente se eliminaron o agregaron van tener como resultado una sumatoria impar
            // Es decir, si yo quito 1 tarea tengo 1 item de auditoria. Si quito y agrego nuevamente tengo 2 items de auditor�a, 
            // con lo cual no hubo cambio. 
            for (int intI = 0; (intI <= (_colAudit.Count - 1)); intI++)
            {
                for (_intJ = 0; (_intJ <= (_lstRet.Count - 1)); _intJ++)
                {
                    if ((_colAudit[intI].Clave == _lstRet[_intJ].Clave))
                        break;
                }

                if (_intJ == _lstRet.Count)
                {
                    Auditoria _retaudit = new Auditoria();
                    _retaudit.Clave = _colAudit[intI].Clave;
                    _retaudit.Mensaje = _colAudit[intI].Mensaje;
                    _retaudit.Cantidad = 1;
                    _lstRet.Add(_retaudit);
                }
                else
                {
                    _lstRet[_intJ].Cantidad++;
                }
            }

            if (_lstRet.Count > 0)
            {
                for (int _intI = 0; (_intI <= (_lstRet.Count - 1)); _intI++)
                {
                    if (((_lstRet[_intI].Cantidad % 2) != 0))
                        strRet += _colAudit[_intI].Mensaje + "\r\n";
                }

                if ((strRet.Length > 0))
                    strRet = ("\r\n" + (Titulo + ("\r\n" + ("\r\n" + strRet))));
            }
            return strRet;
        }
    }
}