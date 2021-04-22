﻿using akaBizAuto.Service.Constants;
using akaBizAuto.Service.Interfaces;
using akaBizAuto.Service.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace akaBizAuto.Service.Services
{
    public class AccountFacebookService : IAccountFacebookService
    {

        public bool Login(AccountFacebookView acc, IWebDriver driver = null, bool isShowChrome = false)
        {
            try
            {
                if (driver is null)
                {
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument($@"{UrlConstant.ProfileChromePath}\{acc.Username}");
                    if (!isShowChrome)
                        options.AddArgument("--headless");

                    driver = new ChromeDriver(options);

                    driver.Url = UrlConstant.FbLogin;
                    driver.Navigate();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                }
                driver.FindElement(By.CssSelector(SelectorConstant.inputUsername)).SendKeys(acc.Username);
                driver.FindElement(By.CssSelector(SelectorConstant.inputPass)).SendKeys(acc.Password);
                driver.FindElement(By.CssSelector(SelectorConstant.buttonLogin)).Click();
                if (driver.FindElements(By.CssSelector(SelectorConstant.inputUsername)).Count > 0)
                {
                    driver.Quit();
                    return false;
                }
            }
            catch
            {
                driver.Quit();
                return false;
            }
            driver.Quit();
            return true;
        }

        public bool IsLoggedIn(AccountFacebookView acc, bool isShowChrome = false)
        {
            try
            {

                ChromeOptions options = new ChromeOptions();
                options.AddArgument($@"{UrlConstant.ProfileChromePath}\{acc.Username}");
                if (!isShowChrome)
                    options.AddArgument("--headless");

                IWebDriver driver = new ChromeDriver(options);

                driver.Url = UrlConstant.FbLogin;
                driver.Navigate();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                if (driver.FindElements(By.CssSelector(SelectorConstant.inputUsername)).Count > 0)
                {
                    if (!Login(acc, driver))
                    {
                        return false;
                    }
                }

                driver.Quit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool OpenFacebook(AccountFacebookView acc)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument($@"{UrlConstant.ProfileChromePath}\{acc.Username}");

                IWebDriver driver = new ChromeDriver(options);
                driver.Url = UrlConstant.FbLogin;
                driver.Navigate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Logout(AccountFacebookView acc)
        {
            throw new NotImplementedException();
        }

        public int AddFriend(AccountFacebookView acc, string uid, bool isShowChrome = false)
        {
            int result = 0;
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument($@"{UrlConstant.ProfileChromePath}\{acc.Username}");
                
                IWebDriver driver = new ChromeDriver(options);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                driver.Url = $"{UrlConstant.FbLogin}/profile.php?id={uid}";
                if (!isShowChrome)
                    options.AddArgument("--headless");
                driver.Navigate();

                var eCancelRequestBtn = driver.FindElements(By.CssSelector(SelectorConstant.CancelRequestFriendBtn));

                if (eCancelRequestBtn.Count == 0)
                {
                    var eAddFriendBtn = driver.FindElements(By.CssSelector(SelectorConstant.AddFriendBtn));
                    if (eAddFriendBtn.Count > 0)
                    {
                        eAddFriendBtn[0].Click();
                        result = 2;
                    }
                    else
                        result = 10;
                        
                }
                else
                    result = 2;
                driver.Quit();
            }
            catch
            {
                 result = 3;
            }
            return result;

        }
    }
}