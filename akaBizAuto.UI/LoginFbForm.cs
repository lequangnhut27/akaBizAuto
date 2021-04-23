using akaBizAuto.Service.Constants;
using akaBizAuto.Service.Interfaces;
using akaBizAuto.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace akaBizAuto.UI
{
    public partial class LoginFbForm : Form
    {
        private readonly IAccountFacebookService _accFbService;
        public AccountFacebookView _acc = null;
        public LoginFbForm(IAccountFacebookService accFbService, AccountFacebookView acc)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            _accFbService = accFbService;
            _acc = acc;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            _acc.Username = usernameTxt.Text;
            _acc.Password = passTxt.Text;
            if (_accFbService.Login(_acc))
            {
                _acc.LoginStatus = VarConstant.Status.Loggedin;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }

    }
}
