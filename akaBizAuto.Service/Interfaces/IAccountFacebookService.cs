using akaBizAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Interfaces
{
    public interface IAccountFacebookService
    {
        bool IsLoggedIn(AccountFacebook acc);
        int Logout(AccountFacebook acc);
        bool OpenFacebook(AccountFacebook acc);
        List<AccountFacebook> GetAll();
    }
}