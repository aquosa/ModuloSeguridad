using System;
namespace COA.WebCipol.Comun
{
     [Serializable]
	public class Se_Menues
	{
		private short mshtIDSistema;
		private short mshtIDItemMenu;
		private string mstrTextoItem = "";
		private string mstrUrl = "";
		private short mshtIDItemPadre;
		private bool mblnEsPadre = false;
		private string smtrDescripcion = "";
		private int mintIDTarea;
		private short mshtOrdenSubMenu;
		private string mstrAccesoDirecto = "";
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
		public short IDItemMenu
		{
			get
			{
				return mshtIDItemMenu;
			}
			set
			{
				mshtIDItemMenu = value;
			}
		}
		public string TextoItem
		{
			get
			{
				return mstrTextoItem;
			}
			set
			{
				mstrTextoItem = value;
			}
		}
		public string Url
		{
			get
			{
				return mstrUrl;
			}
			set
			{
				mstrUrl = value;
			}
		}
		public short IDItemPadre
		{
			get
			{
				return mshtIDItemPadre;
			}
			set
			{
				mshtIDItemPadre = value;
			}
		}
		public bool EsPadre
		{
			get
			{
				return mblnEsPadre;
			}
			set
			{
				mblnEsPadre = value;
			}
		}
		public string Descripcion
		{
			get
			{
				return smtrDescripcion;
			}
			set
			{
				smtrDescripcion = value;
			}
		}
		public int IDTarea
		{
			get
			{
				return mintIDTarea;
			}
			set
			{
				mintIDTarea = value;
			}
		}
		public short OrdenSubMenu
		{
			get
			{
				return mshtOrdenSubMenu;
			}
			set
			{
				mshtOrdenSubMenu = value;
			}
		}
		public string AccesoDirecto
		{
			get
			{
				return mstrAccesoDirecto;
			}
			set
			{
				mstrAccesoDirecto = value;
			}
		}
		
	}

}