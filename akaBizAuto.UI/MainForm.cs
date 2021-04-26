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
using System.IO;
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
        private List<InteractFacebookView> _interacts = null;

        public MainForm(IAccountFacebookService accFbService)
        {
            InitializeComponent();
            _accFbService = accFbService;
            _accFbs = AccountFacebookData.GetAccFbs();
            _interacts = AccountFacebookData.GetInteracts();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStatusAccount();
                //_accFbService.OpenFacebook(_accFbs[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadStatusAccount()
        {
            List<Task> t = new List<Task>();
            foreach (var acc in _accFbs)
            {
                t.Add(_accFbService.UpdateLoginStatus(acc));
            }
            Task.WaitAll(t.ToArray());
            AddStatusAccountLlb();
        }

        private void AddStatusAccountLlb()
        {
            accListFlp.Controls.Clear();
            foreach (var acc in _accFbs)
            {
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

                if (acc.LoginStatus == LoginStatusConstant.NOTLOGIN)
                {
                    LoginFbForm loginFbForm = new LoginFbForm(_accFbService, acc);
                    var result = loginFbForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        linkLabel.Text = $"{acc.Username} ({acc.LoginStatus})";
                    }
                }
                else if (acc.LoginStatus == LoginStatusConstant.CHECKPOINT)
                    _accFbService.OpenFacebook(acc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addFriendListFbBtn_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            foreach (var interact in _interacts)
            {
                AccountFacebookView acc = _accFbs.Find(x => x.ShopId == interact.ShopId);
                if (acc.LoginStatus == LoginStatusConstant.LOGGEDIN)
                    tasks.Add(_accFbService.AddFriends(interact, acc.Username));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private void sendMessageBtn_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            foreach (var interact in _interacts)
            {
                AccountFacebookView acc = _accFbs.Find(x => x.ShopId == interact.ShopId);
                if (acc.LoginStatus == LoginStatusConstant.LOGGEDIN)
                    tasks.Add(_accFbService.SendMessages(interact, acc.Username));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private void commentBtn_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            foreach (var interact in _interacts)
            {
                AccountFacebookView acc = _accFbs.Find(x => x.ShopId == interact.ShopId);
                if (acc != null && acc.LoginStatus == LoginStatusConstant.LOGGEDIN)
                    tasks.Add(_accFbService.Comment(interact, acc.Username));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}