using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace COA.WebCipol.Inicio.view
{
    [Serializable]
    [DataContract]
    public class EntidadesBase
    {
        [DataMember()]
        public string MensajeError { get; set; }

        [DataMember()]
        public string MensajeServicio { get; set; }

        [DataMember()]
        public bool ResultadoEjecucion { get; set; }
    }
}