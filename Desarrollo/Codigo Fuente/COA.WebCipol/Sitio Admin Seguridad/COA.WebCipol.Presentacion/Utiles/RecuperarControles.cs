using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using COA.WebCipol.Presentacion.UIControlsHelper;
using System.Text;
using System.IO;
using COA.WebCipol.Presentacion.UIControlsHelper.DropDownList;
using System.Web.UI.HtmlControls;
using COA.WebCipol.Presentacion.UIControlsHelper.ListBox;

namespace COA.WebCipol.Presentacion.Utiles
{
    public class RecuperarControles : System.Web.Services.WebService
    {
        public string GenerarCombos(DatosCboGenerico datos)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            UIcboGenerico objUI = new UIcboGenerico();
            UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");

            ctl.datos = datos;

            objUI.EnableEventValidation = false; objUI.EnableViewState = false;
            HtmlForm _form = new HtmlForm();
            objUI.Controls.Add(_form);
            _form.Controls.Add(ctl);
            StringWriter writer = new StringWriter();
            Server.Execute(objUI, writer, false);
            sb.Append(writer.ToString());
            writer.Close();

            objLista.Lista = Server.HtmlEncode(sb.ToString());
            objSerializador = new DataContractJsonSerializer(objLista.GetType());
            objSerializador.WriteObject(objMemoria, objLista);
            strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

            objMemoria.Close();
            return strRtaJson;
        }

        public string GenerarListBox<T>(List<T> list, string text, string value, string id)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


                ctl.datos = new DatosListBoxGenerico()
                {
                    DataSource = list,
                    DataTextField = text,
                    DataValueField = value,
                    Id = id
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }
    }
}