using akaBizAuto.Service.Constants;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace akaBizAuto.Service.Common
{
    public class ChromeSelenium
    {
        private IWebDriver _driver = null;
        public ChromeSelenium(string userProfilePath, bool isShowBrowser)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument(userProfilePath);
            if (!isShowBrowser)
                options.AddArgument("--headless");
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public void Quit()
        {
            if (_driver is null)
                return;
            _driver.Quit();
        }

        public void Navigate(string url)
        {
            if (_driver is null)
                return;
            _driver.Url = url;
            _driver.Navigate();
        }

        public string LoginFb(string username, string pass)
        {
            try
            {
                _driver.FindElement(By.CssSelector(SelectorConstant.UsernameInp)).SendKeys(username);
                _driver.FindElement(By.CssSelector(SelectorConstant.PassInp)).SendKeys(pass);
                _driver.FindElement(By.CssSelector(SelectorConstant.LoginBtn)).Click();
                Thread.Sleep(3000);
                return CheckLoginStatusFb();
            }
            catch
            {
                return LoginStatusConstant.NOTLOGIN;
            }
        }

        public string CheckLoginStatusFb()
        {
            try
            {
                if (_driver.FindElements(By.CssSelector(SelectorConstant.UsernameInp)).Count > 0)
                    return LoginStatusConstant.NOTLOGIN;
                if (_driver.Url.Contains(UrlConstant.FbCheckpoint))
                    return LoginStatusConstant.CHECKPOINT;
                return LoginStatusConstant.LOGGEDIN;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int AddFriendFb(string uid)
        {
            try
            {
                Navigate($"{UrlConstant.FbLogin}/profile.php?id={uid}");

                var eAddFriendBtn = _driver.FindElements(By.XPath(SelectorConstant.AddFriendBtn));
                if (eAddFriendBtn.Count > 0)
                {
                    eAddFriendBtn[0].Click();
                    return (int)ActionConstant.Status.Success;
                }
                else
                    return (int)ActionConstant.Status.NotPermission;
            }
            catch
            {
                return (int)ActionConstant.Status.Fail;
            }
        }

        public int SendMessageFb(string uid, string content, string imgPath)
        {
            try
            {
                Navigate($"{UrlConstant.FbLogin}/messages/t/{uid}");

                var eMessageInp = _driver.FindElements(By.CssSelector(SelectorConstant.MessageInp));
                if (eMessageInp.Count > 0)
                {
                    eMessageInp[0].SendKeys(content);
                    _driver.FindElement(By.XPath(SelectorConstant.ImageInp)).SendKeys(imgPath);
                    _driver.FindElement(By.XPath(SelectorConstant.SendMessageBtn)).Click();
                    return (int)ActionConstant.Status.Success;
                }
                else
                    return (int)ActionConstant.Status.NotPermission;
            }
            catch
            {
                return (int)ActionConstant.Status.Fail;
            }
        }

        public void ExecuteScript(string script)
        {
            if (_driver is null)
                return;
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(script);
        }

        public int CommentFb(string uid, string content, string imgPath, int numPost)
        {
            try
            {
                Navigate($"{UrlConstant.FbLogin}/profile.php?id={uid}");
                ReadOnlyCollection<IWebElement> eCommentInp = null;
                int i;
                for (i = 0 ; i < numPost; i++)
                {
                    eCommentInp = _driver.FindElements(By.CssSelector(SelectorConstant.MessageInp));
                    if (eCommentInp.Count > i)
                    {
                        int posScroll = 1500 * (i + 1);
                        ExecuteScript($"window.scrollBy(0,{posScroll})");
                        eCommentInp[i].SendKeys(content);
                        if (!String.IsNullOrEmpty(imgPath))
                        {
                            var eCommentImageInp = _driver.FindElements(By.CssSelector(SelectorConstant.CommentImageInp));
                            if (eCommentImageInp.Count > 0)
                            {
                                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                                eCommentImageInp[i].SendKeys(imgPath);
                                _driver.FindElement(By.CssSelector(SelectorConstant.UploadedImage));
                                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                            }
                        }
                        eCommentInp[i].SendKeys(Keys.Enter);
                    }
                    else if (i == 0)
                        return (int)ActionConstant.Status.NotPermission;
                    else
                        break;
                }
                return (int)ActionConstant.Status.Success;

            }
            catch (Exception ex)
            {
                return (int)ActionConstant.Status.Fail;
            }
        }      
    }
}