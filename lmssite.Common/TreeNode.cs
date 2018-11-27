using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lmssite.Model;

namespace lmssite.Common
{
   public class TreeNode
    {
        public string menuNo { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string menuUrl { get; set; }
        public TreeNode[] nodes { get; set; }

        public TreeNode(M_MENU menuItem)
        {
            this.menuNo = menuItem.Menu_No + Guid.NewGuid();
            this.text = menuItem.Menu_Name;
            this.icon = menuItem.Menu_Icon;
            this.menuUrl = menuItem.Menu_Url;
        }
    }
}
