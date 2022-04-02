using ConnectAPI_1;
using ConnectSqlDapper;
using DoMain;
using DoMainModel.Helper;
using Scrypt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Manager
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            bool isLogin = false;
            //var userName = UserConnectSQL.GetUserName(txbUserName.Text.Trim());
            var userName = UserConnectAPI.GetUserName(txbUserName.Text.Trim());
            if (userName != null && userName.userName != null && userName.PasswordHash != null)
            {
                ScryptEncoder encoder = new ScryptEncoder();
                isLogin = encoder.Compare( txbPassWord.Text.Trim(), userName.PasswordHash);

                // tạm thời thì phải nhập đúng tài khaonr, bỏ qua tạm phần mật khẩu

                // khi  đăng nhập lấy được thông tin rồi thì t lưu uuername vào
                StaticSettings.User = userName;
                isLogin = true;
            }
            if (isLogin)
            {
                if (checkBox1.Checked)
                {
                    MessageBox.Show("Đăng nhập thành công", "Chúc mừng", MessageBoxButtons.OK);
                    string user = txbUserName.Text;
                    string password = txbPassWord.Text;
                    Properties.Settings.Default.username = user;
                    Properties.Settings.Default.password = password;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Bạn đã ghi nhớ", "Thông báo");
                    fManager fmc = new fManager(userName); // cái gì đây
                    this.Hide();
                    fmc.ShowDialog();
                    this.Show();

                }
                else
                {
                    MessageBox.Show("Đăng nhập thành công", "Chúc mừng", MessageBoxButtons.OK);
                   Properties.Settings.Default.Reset();
                   fManager fm = new fManager(userName);
                   this.Hide();
                   fm.ShowDialog();// đoạn này chuyển lên trên k,đoạn đó m làm sau, nó chưa liên quan,nó đang chưa true được kia
                 }
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
             txbUserName.Focus();

            }
        }

        private void btcreat_Click(object sender, EventArgs e)
        {
            fCreating fc = new fCreating();
            this.Hide();
            fc.ShowDialog();
            this.Show();

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
                Application.Exit();
                this.Close();
            }
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fForgotPass fc = new fForgotPass();
            this.Hide();
            fc.ShowDialog();
            this.Show();
        }
    }
}
