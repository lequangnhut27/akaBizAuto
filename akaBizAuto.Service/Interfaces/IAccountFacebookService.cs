using akaBizAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Interfaces
{
    public interface IAccountFacebookService
    {
        int Login(AccountFacebook acc);
        int Logout(AccountFacebook acc);
        List<AccountFacebook> GetAll();
    }
}