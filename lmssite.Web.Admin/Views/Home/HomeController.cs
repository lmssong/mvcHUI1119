using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lmssite.Bll;
using lmssite.Model;
using lmssite.Common;

namespace lmssite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单项数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenuList()
        {
            IMenuBll service = ServiceFactory.GetService<MenuBll, IMenuBll>();
            List<M_MENU> list = service.GetModels(w => true).ToList<M_MENU>();
            MenuBuildHelper menuHelper = new MenuBuildHelper(list);
            var trees = menuHelper.TreeBuild();
            return Json(trees);
        }

    }
}