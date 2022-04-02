using ConnectSqlDapper;
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
    public partial class fForgotPass : Form
    {
        public fForgotPass()
        {
            InitializeComponent();
        }

        private void bntchange_Click(object sender, EventArgs e)
        {
            if (tbusername.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập");
                tbusername.Focus();
            }
            else if (txbNewpass.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu");
                txbNewpass.Focus();
            }
            else if (txbNewpassagian.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập lại mật khẩu");
                txbNewpassagian.Focus();
            }
            else
          if (txbNewpass.Text != txbNewpassagian.Text)
            {
                MessageBox.Show(" Mật khẩu bạn nhập lại chưa đúng");
                txbNewpassagian.Focus();
                txbNewpassagian.SelectAll();
            }
            else
            if (txbEmailPass.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập Email");
                txbEmailPass.Focus();
            }
            else
            if (txbPhonePass.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                txbPhonePass.Focus();
            }
            else
            {
                ScryptEncoder encoder = new ScryptEncoder();
                string PasswordHash = encoder.Encode(txbNewpassagian.Text.Trim());
                bool ResetPass = PasswordResetConnectSQL.PassWordReset(tbusername.Text.Trim(), PasswordHash, txbEmailPass.Text.Trim(), txbPhonePass.Text.Trim());
                if (ResetPass)
                {
                    MessageBox.Show("Đăng ký tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Mật khẩu sai định dạng");
                    txbNewpassagian.Focus();
                    txbNewpassagian.SelectAll();
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
    }
}
