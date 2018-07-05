using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace COA.Cipol.Presentacion._UIHelpers
{
    public class TreeViewHelper
    {
        public void GuardarTreeView(TreeView treeView, string key)
        {
            List<bool?> list = new List<bool?>();
            GuardarEstadoExpansion(treeView.Nodes, list);
            HttpContext.Current.Session[key + treeView.ID] = list;
        }

        private int indiceTVRecup;

        public void RestaurarTreeView(TreeView treeView, string key)
        {
            indiceTVRecup = 0;
            RestaurarEstadoExpansion(treeView.Nodes,
                (List<bool?>)HttpContext.Current.Session[key + treeView.ID] ?? new List<bool?>());
        }

        private void GuardarEstadoExpansion(TreeNodeCollection nodes, List<bool?> list)
        {
            foreach (TreeNode node in nodes)
            {
                list.Add(node.Expanded);
                
                if (node.ChildNodes.Count > 0)
                {
                    //if (node.Parent != null)
                    //{
                    //    list.Clear();
                    //}
                    GuardarEstadoExpansion(node.ChildNodes, list);
                }
            }
        }

        private void RestaurarEstadoExpansion(TreeNodeCollection nodes, List<bool?> list)
        {
            foreach (TreeNode node in nodes)
            {
                if (indiceTVRecup >= list.Count) break;

                node.Expanded = list[indiceTVRecup++];
                if (node.ChildNodes.Count > 0)
                {
                   RestaurarEstadoExpansion(node.ChildNodes, list);
                }
            }
        }
    }
}