using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lmssite.Model;
using lmssite.Dal;

namespace lmssite.Bll
{
    public class MenuBll : BaseBll<M_MENU>, IMenuBll
    {
        private IMenuDal menuDal = DalFactory.CreateDalInstance<MenuDal, IMenuDal>();

        public override void SetDal()
        {
            this.Dal = menuDal;
        }
    }
}
