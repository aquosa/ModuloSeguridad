using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace COA.WebCipol.Presentacion.UIControlsHelper
{
    [DataContract]
    [Serializable]
    public class ElementoLista
    {
        private String mstrLista;
        [DataMember]
        public string Lista
        {
            get { return mstrLista; }
            set { mstrLista = value; }
        }
    }
}