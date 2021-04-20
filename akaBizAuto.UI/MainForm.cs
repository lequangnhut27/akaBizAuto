using akaBizAuto.Data.Models;
using akaBizAuto.Service.Interfaces;
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
        private List<AccountFacebook> accFbs = null;
        public MainForm(IAccountFacebookService accFbService)
        {
            InitializeComponent();
            _accFbService = accFbService;
            accFbs = _accFbService.GetAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStatusAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadStatusAccount()
        {
            accListFlp.Controls.Clear();
            foreach (var acc in accFbs)
            {
                LinkLabel link = new LinkLabel();
                link.Width = accListFlp.Width;
                link.Links.Add(0, 0, acc);
                link.Click += LinkLogin_Click;
                link.Text = acc.Username + " (" + (acc.LoginStatus == 1 ? "Đã đăng nhập" : "Chưa đăng nhập") + ")";
                accListFlp.Controls.Add(link);
            }
        }    

        private void LinkLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LinkLabel linkLabel = sender as LinkLabel;
                AccountFacebook acc = linkLabel.Links[0].LinkData as AccountFacebook;

                if (_accFbService.Login(acc) == 1)
                {
                    acc.LoginStatus = 1;
                    LoadStatusAccount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
