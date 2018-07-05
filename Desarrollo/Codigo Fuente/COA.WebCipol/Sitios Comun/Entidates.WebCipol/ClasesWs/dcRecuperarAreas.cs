using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas
{
    [Serializable]
    public class dcRecuperarAreas
    {
        public dcRecuperarAreas()
        {
            lstKAreas = new List<SIST_KAREAS>();
        }

        public List<SIST_KAREAS> lstKAreas { get; set; }
    }
    [Serializable]
    public class SIST_KAREAS
    {

        private decimal iDAREAField;

        private string nOMBREAREAField;

        private string rESPONSABLEField;

        private string cARGORESPONSABLEField;

        private string cOMENTARIOSField;

        private string bAJAField;

        private string fICTICIAField;

        private bool fICTICIA_VIEWField;

        /// <comentarios/>
        public decimal IDAREA
        {
            get
            {
                return this.iDAREAField;
            }
            set
            {
                this.iDAREAField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
            }
        }

        /// <comentarios/>
        public string RESPONSABLE
        {
            get
            {
                return this.rESPONSABLEField;
            }
            set
            {
                this.rESPONSABLEField = value;
            }
        }

        /// <comentarios/>
        public string CARGORESPONSABLE
        {
            get
            {
                return this.cARGORESPONSABLEField;
            }
            set
            {
                this.cARGORESPONSABLEField = value;
            }
        }

        /// <comentarios/>
        public string COMENTARIOS
        {
            get
            {
                return this.cOMENTARIOSField;
            }
            set
            {
                this.cOMENTARIOSField = value;
            }
        }

        /// <comentarios/>
        public string BAJA
        {
            get
            {
                return this.bAJAField;
            }
            set
            {
                this.bAJAField = value;
            }
        }

        /// <comentarios/>
        public string FICTICIA
        {
            get
            {
                return this.fICTICIAField;
            }
            set
            {
                this.fICTICIAField = value;
            }
        }

        /// <comentarios/>
        public bool FICTICIA_VIEW
        {
            get
            {
                return this.fICTICIA_VIEWField;
            }
            set
            {
                this.fICTICIA_VIEWField = value;
            }
        }
    }
}
