using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace COA.WebCipol.Inicio.PageBuilders
{
    /// <summary>
    /// Descripción breve de KeepSessionAlive
    /// </summary>
    public class KeepSessionAlive : IHttpHandler , IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Session["KeepSessionAlive"] = DateTime.Now;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}