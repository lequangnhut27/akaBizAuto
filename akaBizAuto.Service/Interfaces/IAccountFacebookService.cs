using akaBizAuto.Service.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Interfaces
{
    public interface IAccountFacebookService
    {
        bool IsLoggedIn(AccountFacebookView acc, bool isShowChrome = false);
        int Logout(AccountFacebookView acc);
        bool OpenFacebook(AccountFacebookView acc);
        bool Login(AccountFacebookView acc, IWebDriver driver = null, bool isShowChrome = false);
        int AddFriend(AccountFacebookView acc, string uid, bool isShowChrome = false);
        int SendMessage(AccountFacebookView acc, string uid, string content, string image, bool isShowChrome = false);
    }
}