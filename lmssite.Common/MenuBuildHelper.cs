using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lmssite.Model;

namespace lmssite.Common
{
   public class MenuBuildHelper
    {

        public List<M_MENU> _treeSource;

        public MenuBuildHelper(List<M_MENU> list)
        {
            _treeSource = list;
        }

        /// <summary>
        /// 构建菜单树
        /// </summary>
        /// <returns></returns>
        public TreeNode[] TreeBuild()
        {
            List<TreeNode> result = new List<TreeNode>();
            if (_treeSource == null)
            {
                return null;
            }
            var orderList = from item in _treeSource
                            orderby item.Menu_No
                            select item;
            //筛选根节点
            var roots = from item in orderList
                        where item.Menu_ParentId is null
                        select item;
            foreach (var root in roots)
            {
                TreeNode nodeItem = new TreeNode(root);
                FetchChildNode(nodeItem, root);
                result.Add(nodeItem);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 循环子节点，构建菜单数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="menuItem"></param>
        private void FetchChildNode(TreeNode node,M_MENU menuItem)
        {
            var childs = from item in _treeSource
                         orderby item.Menu_No
                         where item.Menu_ParentId != null && item.Menu_ParentId == menuItem.KID
                         select item;
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (var child in childs)
            {
                TreeNode nodeItem = new TreeNode(child);
                FetchChildNode(nodeItem, child);
                nodes.Add(nodeItem);
            }
            node.nodes = nodes.ToArray();
        }
    }
}
