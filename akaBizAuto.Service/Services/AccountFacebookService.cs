using akaBizAuto.Data.Models;
using akaBizAuto.Service.Constants;
using akaBizAuto.Service.Interfaces;
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
        public List<AccountFacebook> GetAll()
        {
            List<AccountFacebook> accs = new List<AccountFacebook>();
            var acc1 = new AccountFacebook()
            {
                Username = "kjuen11@gmail.com",
                Password = "elkjuen@11",
            };
            var acc2 = new AccountFacebook()
            {
                Username = "kjuen12@gmail.com",
                Password = "elkjuen@12",
            };
            var acc3 = new AccountFacebook()
            {
                Username = "kjuenfb@gmail.com",
                Password = "elkjuen@13",
            };
            accs.Add(acc1);
            accs.Add(acc2);
            accs.Add(acc3);
            return accs;
        }

        

        public bool Login(IWebDriver driver, AccountFacebook acc)
        {
            try
            {
                driver.FindElement(By.CssSelector(SelectorLogin.inputUsername)).SendKeys(acc.Username);
                driver.FindElement(By.CssSelector(SelectorLogin.inputPass)).SendKeys(acc.Password);
                driver.FindElement(By.CssSelector(SelectorLogin.buttonLogin)).Click();

                if (driver.FindElements(By.CssSelector(SelectorLogin.inputUsername)).Count > 0)
                    return false;

            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool IsLoggedIn(AccountFacebook acc)
        {
            try
            {

                ChromeOptions options = new ChromeOptions();
                options.AddArgument($@"user-data-dir=C:\Users\Nitrogen\AppData\Local\Google\Chrome\User Data\{acc.Username}");

                IWebDriver driver = new ChromeDriver(options);

                driver.Url = "https://fb.com";
                driver.Navigate();

                if (driver.FindElements(By.CssSelector(SelectorLogin.inputUsername)).Count > 0)
                {
                    if (!Login(driver, acc))
                    {
                        driver.Quit();
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

        public bool OpenFacebook(AccountFacebook acc)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument($@"user-data-dir=C:\Users\Nitrogen\AppData\Local\Google\Chrome\User Data\{acc.Username}");

                IWebDriver driver = new ChromeDriver(options);
                driver.Url = "https://fb.com";
                driver.Navigate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Logout(AccountFacebook acc)
        {
            throw new NotImplementedException();
        }
    }
}