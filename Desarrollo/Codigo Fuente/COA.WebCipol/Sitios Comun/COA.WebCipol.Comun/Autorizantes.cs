using System;


namespace COA.WebCipol.Comun
{

    [Serializable]
	public class Autorizantes
	{
		private short? mshtIDUsuario;
		private string mstrUsuario = "";
		private string mstrNombres = "";
		private int mintIDTareaPrimitiva;
		private string mstrTareaInhibida = "";
		
        public short? IDUsuario
		{
			get
			{
				return mshtIDUsuario;
			}
			set
			{
				mshtIDUsuario = value;
			}
		}
		public string Usuario
		{
			get
			{
				return mstrUsuario;
			}
			set
			{
				mstrUsuario = value;
			}
		}
		public string Nombres
		{
			get
			{
				return mstrNombres;
			}
			set
			{
				mstrNombres = value;
			}
		}
		public int IDTareaPrimitiva
		{
			get
			{
				return mintIDTareaPrimitiva;
			}
			set
			{
				mintIDTareaPrimitiva = value;
			}
		}
		public string TareaInhibida
		{
			get
			{
				return mstrTareaInhibida;
			}
			set
			{
				mstrTareaInhibida = value;
			}
		}
        		

	}

}