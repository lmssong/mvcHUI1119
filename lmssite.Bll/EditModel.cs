using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lmssite.Model;

namespace lmssite.Bll
{
    [Serializable]
    public class EditModel
    {
        public bool IsNew { get; set; }

        public string Crt_Code { get; set; }

        public string Crt_Name { get; set; }

        public System.DateTime Crt_Time { get; set; }

    }
}
