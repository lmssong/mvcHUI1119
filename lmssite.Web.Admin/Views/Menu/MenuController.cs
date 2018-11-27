using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lmssite.Bll;
using lmssite.Model;
using Newtonsoft.Json;
using AutoMapper;

namespace lmssite.Controllers
{
    public class MenuController : Controller
    {

        private IMenuBll _menuService = ServiceFactory.GetService<MenuBll, IMenuBll>();

        public MenuInfo MenuInfoModel { get; set; }

        // GET: Menu
        public ActionResult Index()
        {
            
            return View();
        }

        /// <summary>
        /// 编辑/新建
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                MenuInfoModel = new MenuInfo();
            }
            else {
                Guid editId = Guid.Parse(id);
                M_MENU editModel = _menuService.GetModels(l => l.KID.Equals(editId)).FirstOrDefault();
                MenuInfo infoModel = ServiceFactory.Mapping<M_MENU, MenuInfo>(editModel);
                MenuInfoModel = infoModel;
            }
            return View(Json(MenuInfoModel));
        }

        [HttpPost]
        public ActionResult MenuSave(string modelInfo)
        {

            MenuInfo addModel = JsonConvert.DeserializeObject<MenuInfo>(modelInfo);
            if (addModel.IsRootMenu)
            {
                addModel.Menu_ParentId = null;
            }
            addModel.Crt_Code = "sa";
            addModel.Crt_Name = "系统管理员";
            addModel.Crt_Time = DateTime.Now;
            
            M_MENU saveModel = ServiceFactory.Mapping<MenuInfo, M_MENU>(addModel);
            bool result;
            if (addModel.IsNew)
            {
                result = _menuService.Add(saveModel);
            }
            else
            {
                result = _menuService.Update(saveModel);
            }
            var jsResult = new JsonResult
            {
                Data = result
            };
            return jsResult;
        }

        [HttpPost]
        public ActionResult MenuDelete()
        {
            string id = Request.Params["id"];
            Guid deleteId = Guid.Parse(id);
            bool result = false;
            M_MENU deleteModel = _menuService.GetModels(l => l.KID.Equals(deleteId)).FirstOrDefault();
            if (deleteModel != null) {
                result = _menuService.Delete(deleteModel);
            }
            var jsResult = new JsonResult()
            {
                Data = result
            };
            return jsResult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuInfoList(int limit, int offset,string search)
        {
            int total = 0;
            List<M_MENU> list = _menuService.GetModelsByPage(limit, offset, true, l => l.Menu_No, k => !k.Is_Delete,out total).ToList();
            return Json(new { total = total, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMenuSelectItems()
        {
            List<M_MENU> list = _menuService.GetModels(w => true).ToList<M_MENU>();

            var result = from item in list.Distinct()
                         select new
                         {
                             items = new {
                                 id = item.KID.ToString(),
                                 text = item.Menu_Name
                             }
                         };

            return Json(result.ToArray());
        }

    }
}