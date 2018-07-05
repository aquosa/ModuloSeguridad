namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsSistBloqueados
    {

    }

    public class dtsSistBloqueadosSE_SIST_BLOQUEADOS
    {

        private decimal IDSISTEMAField;

        private decimal IDUSUARIOField;

        private string dESCSISTEMAField;

        private string nOMBRESField;

        /// <comentarios/>
        public decimal IDSISTEMA
        {
            get
            {
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }
    }

    public class dtsSistBloqueadosSE_SIST_HABILITADOS
    {

        private decimal IDSISTEMAField;

        private string dESCSISTEMAField;

        /// <comentarios/>
        public decimal IDSISTEMA
        {
            get
            {
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }
    }

    public class dtsSistBloqueadosSE_USUARIOS
    {

        private short IDUSUARIOField;

        private string nOMBRESField;

        /// <comentarios/>
        public short IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }
    }

    public class dtsSistBloqueadosSIST_BLOQUEADOS
    {

        private decimal IDSISTEMAField;

        private string dESCSISTEMAField;

        /// <comentarios/>
        public decimal IDSISTEMA
        {
            get
            {
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }
    }

    public class dtsSistBloqueadosSIST_DESBLOQUEADOS
    {

        private decimal IDSISTEMAField;

        private string dESCSISTEMAField;

        /// <comentarios/>
        public decimal IDSISTEMA
        {
            get
            {
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }
    }

    public class dtsSistBloqueadosUSU_BLOQUEADOS
    {

        private short IDUSUARIOField;

        private string nOMBRESField;

        /// <comentarios/>
        public short IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }
    }

    public class dtsSistBloqueadosUSU_DESBLOQUEADOS
    {

        private short IDUSUARIOField;

        private string nOMBRESField;

        /// <comentarios/>
        public short IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }
    }
}