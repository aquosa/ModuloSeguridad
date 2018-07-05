using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using COA.WebCipol.Presentacion.view;

namespace COA.WebCipol.Presentacion.Clases
{
    public class dcTerminales
    {
    }

   

    [Serializable]
    [DataContract()]
    public class Terminal : EntidadesBase
    {
        [DataMember()]
        public bool Habilitada { get; set; }
        [DataMember()]
        public int idterminal { get; set; }
        [DataMember()]
        public string codterminal { get; set; }
        [DataMember()]
        public string nombrecomputadora { get; set; }
        [DataMember()]
        public string usohabilitado { get; set; }
        [DataMember()]
        public string modeloprocesador { get; set; }
        [DataMember()]
        public short cantmemoriaram { get; set; }
        [DataMember()]
        public short tamaniodisco { get; set; }
        [DataMember()]
        public string modelomonitor { get; set; }
        [DataMember()]
        public string modeloacelvideo { get; set; }
        [DataMember()]
        public string descadicional { get; set; }
        [DataMember()]
        public int idarea { get; set; }
        [DataMember()]
        public string nombrearea { get; set; }
        [DataMember()]
        public string nombrenetbios { get; set; }
        [DataMember()]
        public string origenactualizacion { get; set; }
    }



    [Serializable]
    [DataContract()]
    public class Filtro : EntidadesBase
    {
        [DataMember()]
        public string Clave { get; set; }
        [DataMember()]
        public string Valor { get; set; }
    }
    [Serializable]
    [DataContract]
    public class Retorno : EntidadesBase
    {
        [DataMember]
        public string valor1 { get; set; }

        [DataMember]
        public string valor2 { get; set; }
    }
}