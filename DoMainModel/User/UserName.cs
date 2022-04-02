using System;
using System.Collections.Generic;
using System.Text;

namespace DoMain
{
    public class UserName
    {
        public int? PK_ID { get; set; } 
        public string userName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
    }
}
