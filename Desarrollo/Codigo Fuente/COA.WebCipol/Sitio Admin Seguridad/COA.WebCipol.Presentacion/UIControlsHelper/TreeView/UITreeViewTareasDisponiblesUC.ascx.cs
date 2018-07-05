using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.WebCipol.Presentacion.Utiles;

namespace COA.WebCipol.Presentacion.UIControlsHelper.TreeView
{
    public partial class UITreeViewTareasDisponiblesUC : System.Web.UI.UserControl
    {

        public string stringUrlImageGrupos { get; set; }
        public string stringUrlImageSistema { get; set; }
        public string stringUrlImageTareas { get; set; }
        public string stringUrlImageTareaAsignadas { get; set; }
        public string stringUrlImageTareaNoAsignadas { get; set; }


        private dcTreeView _datos;
        public dcTreeView datos
        {
            get { return _datos; }
            set
            {
                _datos = value;
                SetDatos();
            }
        }


        private void SetDatos()
        {
            if (datos == null) return;

            if (!string.IsNullOrEmpty(datos.Id))
            {
                tv.ID = datos.Id;
                dv.ID = "div_" + datos.Id;
            }
            if (datos.Height != null)
                tv.Height = datos.Height;
            if (datos.Width != null)
                tv.Width = datos.Width;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrupo();
                //TreeView1.Nodes.Add(new TreeNode("asdad","sdad"));
            }
            catch (Exception)
            {
                
            }
            
        }

        private void CargarGrupo()
        {

            string strDescSist;
            string[] strSistema;
            TreeNode objNode;
            TreeNode objNode1;
            TreeNode objNode2;

            if (datos.list.Count == 0)
                return;

            foreach (var item in datos.list.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo>(p => p.IDGRUPO)))
            {
                if (item.DESCGRUPO.Trim().Length > 50)
                    objNode = new TreeNode(item.DESCGRUPO.Trim().Substring(0, 50), item.IDGRUPO.ToString());
                else
                    objNode = new TreeNode(item.DESCGRUPO.Trim(), item.IDGRUPO.ToString());
                objNode.ToolTip = item.IDGRUPO.ToString();
                objNode.Expand();
                //objNode.Text = item.DESCGRUPO.Trim();
                tv.Nodes.Add(objNode);

                foreach (var itemAux in datos.list.Where(p => p.IDGRUPO == item.IDGRUPO).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo>(p => p.IDSISTEMA)))
                {
                    if (itemAux.IDGRUPO != item.IDGRUPO)
                        continue;

                    strDescSist = itemAux.DESCSISTEMA.TrimEnd().ToLower();
                    strSistema = strDescSist.Split(' ');
                    strSistema[0] = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strSistema[0]);
                    strDescSist = String.Join(" ", strSistema);

                    objNode1 = new TreeNode(strDescSist.Trim(), itemAux.IDSISTEMA.ToString());
                    objNode1.ToolTip = itemAux.IDGRUPO.ToString() + "/" + itemAux.IDSISTEMA.ToString();
                    objNode.ChildNodes.Add(objNode1);

                    foreach (var itemAux2 in datos.list)
                    {
                        if (itemAux.IDGRUPO != itemAux2.IDGRUPO || itemAux.IDSISTEMA != itemAux2.IDSISTEMA)
                            continue;
                        if (itemAux2.DESCRIPCIONTAREA.Trim().Length > 50)
                            objNode2 = new TreeNode(itemAux2.DESCRIPCIONTAREA.Trim().Substring(0, 50), itemAux2.IDTAREA.ToString());
                        else
                            objNode2 = new TreeNode(itemAux2.DESCRIPCIONTAREA.Trim(), itemAux2.IDTAREA.ToString());
                        objNode2.ToolTip = itemAux2.IDGRUPO.ToString() + "/" + itemAux2.IDSISTEMA.ToString() + "/" + itemAux2.IDTAREA.ToString();

                        objNode1.ChildNodes.Add(objNode2);
                    }
                }
            }
        }
    }
}
