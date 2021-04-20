using akaBizAuto.Data.Models;
using akaBizAuto.Service.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace akaBizAuto.Service.Services
{
    public class AccountFacebookService : IAccountFacebookService
    {
        public List<AccountFacebook> GetAll()
        {
            List<AccountFacebook> accs = new List<AccountFacebook>();
            var acc1 = new AccountFacebook()
            {
                Username = "kjuen11@gmail.com",
                Password = "elkjuen@11",
                LoginStatus = 0
            };
            var acc2 = new AccountFacebook()
            {
                Username = "kjuen12@gmail.com",
                Password = "elkjuen@12",
                LoginStatus = 0
            };
            var acc3 = new AccountFacebook()
            {
                Username = "kjuenfb@gmail.com",
                Password = "elkjuen@13",
                LoginStatus = 0
            };
            accs.Add(acc1);
            accs.Add(acc2);
            accs.Add(acc3);
            return accs;
        }

        

        public int Login(AccountFacebook acc)
        {
            try
            {
                IWebDriver driver = new ChromeDriver();

                driver.Url = "https://fb.com";
                driver.Navigate();

                var a = driver.FindElement(By.CssSelector("input#email"));

                driver.FindElement(By.CssSelector("input#email")).SendKeys(acc.Username);
                driver.FindElement(By.CssSelector("input#pass")).SendKeys(acc.Password);
                driver.FindElement(By.CssSelector("button")).Click();

                if (driver.FindElements(By.CssSelector("input#email")).Count == 0)
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }

        public int Logout(AccountFacebook acc)
        {
            throw new NotImplementedException();
        }
    }
}