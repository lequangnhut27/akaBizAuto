using akaBizAuto.Service.Common;
using akaBizAuto.Service.Constants;
using akaBizAuto.Service.Interfaces;
using akaBizAuto.Service.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace akaBizAuto.Service.Services
{
    public class AccountFacebookService : IAccountFacebookService
    {        

        public bool OpenFacebook(AccountFacebookView acc)
        {
            try
            {
                string userProfilePath = $@"{UrlConstant.ProfileChromePath}\{acc.Username}";
                ChromeSelenium chrome = new ChromeSelenium(userProfilePath, VarConstant.IsShowBrowser);
                // Go to fb
                chrome.Navigate(UrlConstant.FbLogin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task UpdateLoginStatus(AccountFacebookView acc)
        {
            return Task.Run(() =>
            {
                string userProfilePath = $@"{UrlConstant.ProfileChromePath}\{acc.Username}";
                ChromeSelenium chrome = new ChromeSelenium(userProfilePath, VarConstant.IsShowBrowser);
                // Go to fb
                chrome.Navigate(UrlConstant.FbLogin);
                acc.LoginStatus = chrome.CheckLoginStatusFb();
                // chưa đăng nhập thực hiện đăng nhập
                if (acc.LoginStatus == LoginStatusConstant.NOTLOGIN)
                    acc.LoginStatus = chrome.LoginFb(acc.Username, acc.Password);

                chrome.Quit();
            });
        }

        public Task AddFriends(InteractFacebookView interact, string username)
        {
            return Task.Run(() =>
            {
                if (interact == null || interact.Action != ActionConstant.ADDFRIEND ||
                interact.Status == ProcessStatusConstant.FINISH || interact.Status == ProcessStatusConstant.STOP ||
                interact.Schedule.Date != DateTime.Now.Date)
                    return;

                interact.Status = ProcessStatusConstant.PROCESSING;

                string userProfilePath = $@"{UrlConstant.ProfileChromePath}\{username}";
                ChromeSelenium chrome = new ChromeSelenium(userProfilePath, VarConstant.IsShowBrowser);

                bool isFinish = true;

                foreach (var cus in interact.Detail)
                {
                    if (cus.Status == (int)ActionConstant.Status.Waiting || cus.Status == (int)ActionConstant.Status.Fail)
                        cus.Status = chrome.AddFriendFb(cus.Uid);
                    if (cus.Status == (int)ActionConstant.Status.Fail)
                        isFinish = false;
                    Thread.Sleep(interact.TimeDelay);
                }
                if (isFinish)
                    interact.Status = ProcessStatusConstant.FINISH;

                chrome.Quit();
            });
        }

        public Task SendMessages(InteractFacebookView interact, string username)
        {
            return Task.Run(() =>
            {
                if (interact == null || interact.Action != ActionConstant.SENDMESSAGE ||
                interact.Status == ProcessStatusConstant.FINISH || interact.Status == ProcessStatusConstant.STOP ||
                interact.Schedule.Date != DateTime.Now.Date)
                    return;

                interact.Status = ProcessStatusConstant.PROCESSING;

                string userProfilePath = $@"{UrlConstant.ProfileChromePath}\{username}";
                ChromeSelenium chrome = new ChromeSelenium(userProfilePath, VarConstant.IsShowBrowser);

                bool isFinish = true;

                foreach (var cus in interact.Detail)
                {
                    if (cus.Status == (int)ActionConstant.Status.Waiting || cus.Status == (int)ActionConstant.Status.Fail)
                        cus.Status = chrome.SendMessageFb(cus.Uid, interact.Content, interact.Image);
                    if (cus.Status == (int)ActionConstant.Status.Fail)
                        isFinish = false;
                    Thread.Sleep(interact.TimeDelay);
                }
                if (isFinish)
                    interact.Status = ProcessStatusConstant.FINISH;

                chrome.Quit();
            });
        }

        public Task Comment(InteractFacebookView interact, string username)
        {
            return Task.Run(() =>
            {
                if (interact == null || interact.Action != ActionConstant.COMMENT ||
                interact.Status == ProcessStatusConstant.FINISH || interact.Status == ProcessStatusConstant.STOP ||
                interact.Schedule.Date != DateTime.Now.Date)
                    return;

                interact.Status = ProcessStatusConstant.PROCESSING;

                string userProfilePath = $@"{UrlConstant.ProfileChromePath}\{username}";
                ChromeSelenium chrome = new ChromeSelenium(userProfilePath, VarConstant.IsShowBrowser);

                bool isFinish = true;

                foreach (var cus in interact.Detail)
                {
                    if (cus.Status == (int)ActionConstant.Status.Waiting || cus.Status == (int)ActionConstant.Status.Fail)
                        cus.Status = chrome.CommentFb(cus.Uid, interact.Content, interact.Image, interact.CountPost);
                    if (cus.Status == (int)ActionConstant.Status.Fail)
                        isFinish = false;
                    Thread.Sleep(interact.TimeDelay);
                }
                if (isFinish)
                    interact.Status = ProcessStatusConstant.FINISH;

                chrome.Quit();
            });           
        }
    }
}