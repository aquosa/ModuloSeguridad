using System;
namespace COA.WebCipol.Comun
{
     [Serializable]
	public class Tareas
	{
		private int mintIDTarea;
		private string mstrDescripcionTarea = "";
		private string mstrTareaInhibida = "";
		private short mshtIDAutorizacion;
		private string mstrRequiereAuditoria = "";
		private short mshtMomento;
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
		public string DescripcionTarea
		{
			get
			{
                return mstrDescripcionTarea;
			}
			set
			{
                mstrDescripcionTarea = value;
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
		public short IDAutorizacion
		{
			get
			{
				return mshtIDAutorizacion;
			}
			set
			{
				mshtIDAutorizacion = value;
			}
		}
		public string RequiereAuditoria
		{
			get
			{
				return mstrRequiereAuditoria;
			}
			set
			{
				mstrRequiereAuditoria = value;
			}
		}
		public short Momento
		{
			get
			{
				return mshtMomento;
			}
			set
			{
				mshtMomento = value;
			}
		}

	}

}