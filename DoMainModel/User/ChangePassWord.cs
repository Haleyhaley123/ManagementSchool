using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoMainModel
{
    public class ChangePassWord
    {
        public int? PK_ID { get; set; }
        public string userName { get; set; }
        public string OldPasswordhash { get; set; }
        public string NewPasswordHash { get; set; }
    }
}
