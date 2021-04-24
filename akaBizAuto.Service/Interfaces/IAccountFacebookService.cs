using akaBizAuto.Service.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Interfaces
{
    public interface IAccountFacebookService
    {

        void UpdateLoginStatus(AccountFacebookView acc);
        void AddFriends(InteractFacebookView interact, string username);
        void SendMessages(InteractFacebookView interact, string username);
        void CommentProfile(InteractFacebookView interact, string username);
        bool OpenFacebook(AccountFacebookView acc);
    }
}