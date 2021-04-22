using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Models
{
    public class AccountFacebookView
    {
        public string AppId { get; set; }
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginStatus { get; set; }
        public AccountFacebookView()
        {
            ShopType = "facebook";
        }

    }
}
