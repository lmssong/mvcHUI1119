using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lmssite.Model;

namespace lmssite.Controllers
{
    public class MenuInfo:M_MENU
    {

        public bool IsRootMenu { get; set; }

        public bool IsNew { get; set; }

        public MenuInfo()
        {
            this.IsRootMenu = true;
            this.KID = Guid.NewGuid();
            this.Menu_No = "3423qmo";
            this.Menu_Name = "这是测试菜单名称";
            this.Is_Delete = false;
        }
    }
}