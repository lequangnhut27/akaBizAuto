using akaBizAuto.Data.Constants;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace akaBizAuto.UI
{
    public partial class MainForm : Form
    {
        private readonly IAccountFacebookService _accFbService;
        private List<AccountFacebookView> _accFbs = null;
        private InteractFacebookView _interact = null;
        private bool _isShowChrome;
        public MainForm(IAccountFacebookService accFbService)
        {
            InitializeComponent();
            _accFbService = accFbService;
            _accFbs = AccountFacebookData.GetAccFbs();
            _interact = AccountFacebookData.GetInteract();
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
                    acc.LoginStatus = Constants.LoggedIn;
                }
                else
                {
                    acc.LoginStatus = Constants.NotLogin;
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

                if (acc.LoginStatus == Constants.NotLogin)
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
            if (_interact.Action == "addfriend" && _interact.Status == "waiting")
            {
                foreach (var customer in _interact.Detail)
                {
                    var result = _accFbService.AddFriend(_accFbs[2], customer.Uid, _isShowChrome);
                    customer.Status = result;
                }
            }
        }
    }
}