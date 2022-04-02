using DoMain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ConnectSqlDapper;
using Scrypt;
using DoMainModel.Helper;
using DoMainModel;

namespace School_Manager
{
    public partial class fCreating : Form
    {
        public fCreating()
        {
            InitializeComponent();
        }

        private void bntcreat_Click(object sender, EventArgs e)
        {
            if (txbcusername.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập");
                txbcusername.Focus();
            }
            else if (txbpass.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu");
                txbpass.Focus();
            }
            else if (txbpassagian.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập lại mật khẩu");
                txbpassagian.Focus();
            }
            else
          if (txbpass.Text != txbpassagian.Text)
            {
                MessageBox.Show(" Mật khẩu bạn nhập lại chưa đúng");
                txbpassagian.Focus();
                txbpassagian.SelectAll();
            }
            else
            if (txbEmail.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Email");
                txbEmail.Focus();
            }
            else
            if (txbPhone.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                txbPhone.Focus();
            }
            else
            {
                ScryptEncoder encoder = new ScryptEncoder();
                string PasswordHash = encoder.Encode(txbpassagian.Text.Trim());
                bool isCreateUser = UserConnectSQL.CreateUserName(txbcusername.Text.Trim(),PasswordHash, txbEmail.Text.Trim(), txbPhone.Text.Trim());
                if (isCreateUser)
                {
                    MessageBox.Show("Đăng ký tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Mật khẩu sai định dạng");
                    txbpass.Focus();
                    txbpass.SelectAll();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
               
                this.Close();
            }
        }

        private void bntcancel_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn có muốn hủy không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScryptEncoder encoder1 = new ScryptEncoder();
            if (StaticSettings.User == null || StaticSettings.User.PK_ID == null)
            {
                MessageBox.Show("Có sự cố, vui lòng kiểm tra lại");
                return;
            }
            if (tbpass.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu cũ");
                tbpass.Focus();
            }
            else if(!encoder1.Compare(tbpass.Text, StaticSettings.User.PasswordHash))
            {
                MessageBox.Show("Mật khẩu của bạn không đúng");
                tbpass.Focus();
            }    
            else if (tbNewpass.Text == "")
            {
                MessageBox.Show("bạn chưa nhập mật khẩu mới");
                tbNewpass.Focus();
            }
            else if (tbNewpassA.Text != tbNewpass.Text)
            {
                MessageBox.Show(" Mật khẩu bạn nhập lại chưa đúng");
                tbNewpassA.Focus();
                tbNewpassA.SelectAll();
            }
            else
            {
                string PasswordHash = encoder1.Encode(tbNewpassA.Text.Trim());
                ChangePassWord changePass = new ChangePassWord()
                {
                    PK_ID = StaticSettings.User.PK_ID,
                    userName = StaticSettings.User.userName,
                    OldPasswordhash = StaticSettings.User.PasswordHash,
                    NewPasswordHash = PasswordHash,

                };

                bool isChangeUser = ChangePasswordConnectSQL.ChangePassWord(changePass);
                if (isChangeUser)
                {
                    MessageBox.Show("Thay đổi mật khẩu thành công");
                }
                else
                {
                    MessageBox.Show("Mật khẩu sai định dạng");
                    txbpass.Focus();
                    txbpass.SelectAll();
                }
            }
        }

        private void tbuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn có muốn hủy không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
                fCreating fc = new fCreating();
                this.Hide();
                fc.ShowDialog();
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
