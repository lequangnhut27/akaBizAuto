using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Data.Models
{
    public class AccountFacebook
    {
        public string AppId { get; set; }
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginStatus { get; set; }
        public AccountFacebook()
        {
            ShopType = "facebook";
        }

    }
}
