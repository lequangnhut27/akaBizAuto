using akaBizAuto.Service.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace akaBizAuto.Service.Interfaces
{
    public interface IAccountFacebookService
    {

        Task UpdateLoginStatus(AccountFacebookView acc);
        Task AddFriends(InteractFacebookView interact, string username);
        Task SendMessages(InteractFacebookView interact, string username);
        Task Comment(InteractFacebookView interact, string username);
        bool OpenFacebook(AccountFacebookView acc);
    }
}