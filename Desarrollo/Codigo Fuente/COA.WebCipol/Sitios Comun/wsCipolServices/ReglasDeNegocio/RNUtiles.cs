using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using COA.WebCipol.Entidades.Core;
using COA.WebCipol.Entidades;

namespace wsCipolServices.ReglasDeNegocio
{
    public class RNUtiles
    {
        public static DataSet RetornaDataSet(string NombreTabla, Object lstDatos)
        {
            switch (NombreTabla.ToUpper())
            {
                case "SE_ATRIBUTOSTAREAS":
                    return ListToDataSet.ToDataSet<SE_ATRIBUTOSTAREAS>((List<SE_ATRIBUTOSTAREAS>)lstDatos, NombreTabla);
                case "SE_AUDITORIA":
                    return ListToDataSet.ToDataSet<SE_ATRIBUTOSTAREAS>((List<SE_ATRIBUTOSTAREAS>)lstDatos, NombreTabla);
                case "SE_AUDITORIASUPERVISION":
                    return ListToDataSet.ToDataSet<SE_AUDITORIASUPERVISION>((List<SE_AUDITORIASUPERVISION>)lstDatos, NombreTabla);
                case "SE_AUDITORIATAREAS":
                    return ListToDataSet.ToDataSet<SE_AUDITORIATAREAS>((List<SE_AUDITORIATAREAS>)lstDatos, NombreTabla);
                case "SE_AUTORIZACION":
                    return ListToDataSet.ToDataSet<SE_AUTORIZACION>((List<SE_AUTORIZACION>)lstDatos, NombreTabla);
                case "SE_CODAUDITORIA":
                    return ListToDataSet.ToDataSet<SE_CODAUDITORIA>((List<SE_CODAUDITORIA>)lstDatos, NombreTabla);
                case "SE_COMP_ROLES":
                    return ListToDataSet.ToDataSet<SE_COMP_ROLES>((List<SE_COMP_ROLES>)lstDatos, NombreTabla);
                case "SE_GRUPO_EXCLUSION":
                    return ListToDataSet.ToDataSet<SE_GRUPO_EXCLUSION>((List<SE_GRUPO_EXCLUSION>)lstDatos, NombreTabla);
                case "SE_GRUPO_TAREA":
                    return ListToDataSet.ToDataSet<SE_GRUPO_TAREA>((List<SE_GRUPO_TAREA>)lstDatos, NombreTabla);
                case "SE_HISTORIAL_USUARIO":
                    return ListToDataSet.ToDataSet<SE_HISTORIAL_USUARIO>((List<SE_HISTORIAL_USUARIO>)lstDatos, NombreTabla);
                case "SE_HORARIOS_USUARIO":
                    return ListToDataSet.ToDataSet<SE_HORARIOS_USUARIO>((List<SE_HORARIOS_USUARIO>)lstDatos, NombreTabla);
                case "SE_INSTANCIAUSUARIO":
                    return ListToDataSet.ToDataSet<SE_INSTANCIAUSUARIO>((List<SE_INSTANCIAUSUARIO>)lstDatos, NombreTabla);
                case "SE_MENSAJES":
                    return ListToDataSet.ToDataSet<SE_MENSAJES>((List<SE_MENSAJES>)lstDatos, NombreTabla);
                case "SE_MENUES":
                    return ListToDataSet.ToDataSet<SE_MENUES>((List<SE_MENUES>)lstDatos, NombreTabla); ;
                case "SE_PARAMETROS":
                    return ListToDataSet.ToDataSet<SE_PARAMETROS>((List<SE_PARAMETROS>)lstDatos, NombreTabla);
                case "SE_REL_AUTORIZ":
                    return ListToDataSet.ToDataSet<SE_REL_AUTORIZ>((List<SE_REL_AUTORIZ>)lstDatos, NombreTabla);
                case "SE_ROLES":
                    return ListToDataSet.ToDataSet<SE_ROLES>((List<SE_ROLES>)lstDatos, NombreTabla);
                case "SE_SESIONESACTIVAS":
                    return ListToDataSet.ToDataSet<SE_SESIONESACTIVAS>((List<SE_SESIONESACTIVAS>)lstDatos, NombreTabla);
                case "SE_SIST_BLOQUEADOS":
                    return ListToDataSet.ToDataSet<SE_SIST_BLOQUEADOS>((List<SE_SIST_BLOQUEADOS>)lstDatos, NombreTabla);
                case "SE_SIST_EVENTOS":
                    return ListToDataSet.ToDataSet<SE_SIST_EVENTOS>((List<SE_SIST_EVENTOS>)lstDatos, NombreTabla);
                case "SE_SIST_HABILITADOS":
                    return ListToDataSet.ToDataSet<SE_SIST_HABILITADOS>((List<SE_SIST_HABILITADOS>)lstDatos, NombreTabla);
                case "SE_TAREAS":
                    return ListToDataSet.ToDataSet<SE_TAREAS>((List<SE_TAREAS>)lstDatos, NombreTabla);
                case "SE_TAREAS_USUARIO":
                    return ListToDataSet.ToDataSet<SE_TAREAS_USUARIO>((List<SE_TAREAS_USUARIO>)lstDatos, NombreTabla);
                case "SE_TERM_USUARIO":
                    return ListToDataSet.ToDataSet<SE_TERM_USUARIO>((List<SE_TERM_USUARIO>)lstDatos, NombreTabla);
                case "SE_TERMINALES":
                    return ListToDataSet.ToDataSet<SE_TERMINALES>((List<SE_TERMINALES>)lstDatos, NombreTabla);
                case "SE_USUARIOS":
                    return ListToDataSet.ToDataSet<SE_USUARIOS>((List<SE_USUARIOS>)lstDatos, NombreTabla);
                case "SIST_KAREAS":
                    return ListToDataSet.ToDataSet<SIST_KAREAS>((List<SIST_KAREAS>)lstDatos, NombreTabla);
                case "SIST_KDOCUMENTOS":
                    return ListToDataSet.ToDataSet<SIST_KDOCUMENTOS>((List<SIST_KDOCUMENTOS>)lstDatos, NombreTabla);
                case "SIST_SESION_USUARIO":
                    return ListToDataSet.ToDataSet<SIST_SESION_USUARIO>((List<SIST_SESION_USUARIO>)lstDatos, NombreTabla);
                case "SE_SIST_HABILITADOS_ok":
                    return ListToDataSet.ToDataSet<SE_SIST_HABILITADOS_ok>((List<SE_SIST_HABILITADOS_ok>)lstDatos, NombreTabla);
                case "SE_SIST_HABILITADOS_okMIO":
                    return ListToDataSet.ToDataSet<SE_SIST_HABILITADOS_okMIO>((List<SE_SIST_HABILITADOS_okMIO>)lstDatos, NombreTabla);
                case "SE_PARAMETROSok":
                    return ListToDataSet.ToDataSet<SE_PARAMETROSok>((List<SE_PARAMETROSok>)lstDatos, NombreTabla);
                default:
                    break;
            }
            return null;
        }
        public static Object RetornaObject(string NombreTabla, DataSet dtsDatos)
        {
            //Entidades.SE_ATRIBUTOSTAREAS obj = new SE_ATRIBUTOSTAREAS();
            //Assembly assembly = Assembly.GetAssembly(obj.GetType());
            //foreach (Type type in assembly.GetTypes())
            //{
            //    if (type.IsClass)
            //    {
            //        strNOmbre += type.Name + "-";
            //    }
            //}
            string strCompara = NombreTabla.ToUpper();
            switch (strCompara)
            {
                case "SE_ATRIBUTOSTAREAS":
                    return EntityLoader.Load<SE_ATRIBUTOSTAREAS>(dtsDatos);
                case "SE_AUDITORIA":
                    return EntityLoader.Load<SE_AUDITORIA>(dtsDatos);
                case "SE_AUDITORIASUPERVISION":
                    return EntityLoader.Load<SE_AUDITORIASUPERVISION>(dtsDatos);
                case "SE_AUDITORIATAREAS":
                    return EntityLoader.Load<SE_AUDITORIATAREAS>(dtsDatos);
                case "SE_AUTORIZACION":
                    return EntityLoader.Load<SE_AUTORIZACION>(dtsDatos);
                case "SE_CODAUDITORIA":
                    return EntityLoader.Load<SE_CODAUDITORIA>(dtsDatos);
                case "SE_COMP_ROLES":
                    return EntityLoader.Load<SE_COMP_ROLES>(dtsDatos);
                case "SE_GRUPO_EXCLUSION":
                    return EntityLoader.Load<SE_GRUPO_EXCLUSION>(dtsDatos);
                case "SE_GRUPO_TAREA":
                    return EntityLoader.Load<SE_GRUPO_TAREA>(dtsDatos);
                case "SE_HISTORIAL_USUARIO":
                    return EntityLoader.Load<SE_HISTORIAL_USUARIO>(dtsDatos);
                case "SE_HORARIOS_USUARIO":
                    return EntityLoader.Load<SE_HORARIOS_USUARIO>(dtsDatos);
                case "SE_INSTANCIAUSUARIO":
                    return EntityLoader.Load<SE_INSTANCIAUSUARIO>(dtsDatos);
                case "SE_MENSAJES":
                    return EntityLoader.Load<SE_MENSAJES>(dtsDatos);
                case "SE_MENUES":
                    return EntityLoader.Load<SE_MENUES>(dtsDatos);
                case "SE_PARAMETROS":
                    return EntityLoader.Load<SE_PARAMETROS>(dtsDatos);
                case "SE_REL_AUTORIZ":
                    return EntityLoader.Load<SE_REL_AUTORIZ>(dtsDatos);
                case "SE_ROLES":
                    return EntityLoader.Load<SE_ROLES>(dtsDatos);
                case "SE_SESIONESACTIVAS":
                    return EntityLoader.Load<SE_SESIONESACTIVAS>(dtsDatos);
                case "SE_SIST_BLOQUEADOS":
                    return EntityLoader.Load<SE_SIST_BLOQUEADOS>(dtsDatos);
                case "SE_SIST_EVENTOS":
                    return EntityLoader.Load<SE_SIST_EVENTOS>(dtsDatos);
                case "SE_SIST_HABILITADOS":
                    return EntityLoader.Load<SE_SIST_HABILITADOS>(dtsDatos);
                case "SE_TAREAS":
                    return EntityLoader.Load<SE_TAREAS>(dtsDatos);
                case "SE_TAREAS_USUARIO":
                    return EntityLoader.Load<SE_TAREAS_USUARIO>(dtsDatos);
                case "SE_TERM_USUARIO":
                    return EntityLoader.Load<SE_TERM_USUARIO>(dtsDatos);
                case "SE_TERMINALES":
                    return EntityLoader.Load<SE_TERMINALES>(dtsDatos);
                case "SE_USUARIOS":
                    return EntityLoader.Load<SE_USUARIOS>(dtsDatos);
                case "SIST_KAREAS":
                    return EntityLoader.Load<SIST_KAREAS>(dtsDatos);
                case "SIST_KDOCUMENTOS":
                    return EntityLoader.Load<SIST_KDOCUMENTOS>(dtsDatos);
                case "SIST_SESION_USUARIO":
                    return EntityLoader.Load<SIST_SESION_USUARIO>(dtsDatos);
                case "SE_SIST_HABILITADOS_OK":
                    return EntityLoader.Load<SE_SIST_HABILITADOS_ok>(dtsDatos);
                case "SE_SIST_HABILITADOS_OKMIO":
                    return EntityLoader.Load<SE_SIST_HABILITADOS_okMIO>(dtsDatos);
                case "SE_PARAMETROSOK":
                    return EntityLoader.Load<SE_PARAMETROSok>(dtsDatos);
                default:
                    break;
            }
            return null;
        }
    }
}