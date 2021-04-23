using akaBizAuto.Service.Constants;
using akaBizAuto.Service.Data;
using akaBizAuto.Service.Interfaces;
using akaBizAuto.Service.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace akaBizAuto.UI
{
    public partial class MainForm : Form
    {
        private readonly IAccountFacebookService _accFbService;
        private List<AccountFacebookView> _accFbs = null;
        private List<InteractFacebookView> _interact = null;
        private bool _isShowChrome;
        public MainForm(IAccountFacebookService accFbService)
        {
            InitializeComponent();
            _accFbService = accFbService;
            _accFbs = AccountFacebookData.GetAccFbs();
            _interact = AccountFacebookData.GetInteracts();
            _isShowChrome = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStatusAccount();
                //_accFbService.OpenFacebook(_accFbs[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadStatusAccount()
        {
            accListFlp.Controls.Clear();
            foreach (var acc in _accFbs)
            {
                if (_accFbService.IsLoggedIn(acc, _isShowChrome))
                {
                    acc.LoginStatus = VarConstant.Status.Loggedin;
                }
                else
                {
                    acc.LoginStatus = VarConstant.Status.NotLogin;
                }
                LinkLabel link = new LinkLabel();
                link.Width = accListFlp.Width;
                link.Links.Add(0, 0, acc);
                link.Click += LinkLogin_Click;
                link.Text = $"{acc.Username} ({acc.LoginStatus})";
                accListFlp.Controls.Add(link);
            }
        }

        private void LinkLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LinkLabel linkLabel = sender as LinkLabel;
                AccountFacebookView acc = linkLabel.Links[0].LinkData as AccountFacebookView;

                if (acc.LoginStatus == VarConstant.Status.NotLogin)
                {
                    LoginFbForm loginFbForm = new LoginFbForm(_accFbService, acc);
                    var result = loginFbForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        linkLabel.Text = $"{acc.Username} ({acc.LoginStatus})";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addFriendListFbBtn_Click(object sender, EventArgs e)
        {
            foreach (var interact in _interact)
            {
                if (interact.Action == VarConstant.Action.AddFriend &&
                    interact.Status == VarConstant.Status.Waiting &&
                    interact.Schedule.Date == DateTime.Now.Date)
                {
                    foreach (var customer in interact.Detail)
                    {
                        var result = _accFbService.AddFriend(_accFbs[2], customer.Uid, _isShowChrome);
                        customer.Status = result;
                        Thread.Sleep(interact.TimeDelay);
                    }
                    interact.Status = VarConstant.Status.Finish;
                }
            }
        }

        private void sendMessageBtn_Click(object sender, EventArgs e)
        {
            foreach (var interact in _interact)
            {
                if (interact.Action == VarConstant.Action.SendMessage &&
                    interact.Status == VarConstant.Status.Waiting &&
                    interact.Schedule.Date == DateTime.Now.Date)
                {
                    foreach (var customer in interact.Detail)
                    {
                        var result = _accFbService.SendMessage(_accFbs[2], customer.Uid, interact.Content, interact.Image, _isShowChrome);
                        customer.Status = result;
                        Thread.Sleep(interact.TimeDelay);
                    }
                    interact.Status = VarConstant.Status.Finish;
                }
            }
        }
    }
}