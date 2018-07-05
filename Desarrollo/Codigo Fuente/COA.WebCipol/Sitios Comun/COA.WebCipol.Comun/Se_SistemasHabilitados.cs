using System;
namespace COA.WebCipol.Comun
{
    [Serializable]
	public class Se_SistemasHabilitados
	{
		private short mshtIDSistema;
		private string mstrCodSistema = "";
		private string mstrDescSistema = "";
		private string mstrNombreExec = "";
		private string mstrIcono = "";
        private string mstrPaginaPorDefecto = "";
    
		
		public short IDSistema
		{
			get
			{
				return mshtIDSistema;
			}
			set
			{
				mshtIDSistema = value;
			}
		}
		public string CodSistema
		{
			get
			{
				return mstrCodSistema;
			}
			set
			{
				mstrCodSistema = value;
			}
		}
		public string DescSistema
		{
			get
			{
				return mstrDescSistema;
			}
			set
			{
				mstrDescSistema = value;
			}
		}
		public string NombreExec
		{
			get
			{
				return mstrNombreExec;
			}
			set
			{
				mstrNombreExec = value;
			}
		}
        public string PaginaPorDefecto
        {
            get
            {
                return mstrPaginaPorDefecto;
            }
            set
            {
                mstrPaginaPorDefecto = value;
            }
        }
     	public string Icono
		{
			get
			{
				return mstrIcono;
			}
			set
			{
				mstrIcono = value;
			}
		}
	 

	}

}