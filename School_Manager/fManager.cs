using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using System.Data.SqlClient;
using ConnectSqlDapper;
using DoMain;
using ConnectAPI_1;

namespace School_Manager
{
    public partial class fManager : Form
    {
        SqlConnection sqlConection;
        SqlCommand sqlCommand;
        SqlDataAdapter DataAdapter = new SqlDataAdapter();
        DataTable Table = new DataTable();

        protected void loaddata (string TV)
        {   
            sqlCommand = sqlConection.CreateCommand();
            sqlCommand.CommandText = TV ;
            DataAdapter.SelectCommand = sqlCommand;
            Table.Clear();
            DataAdapter.Fill(Table);
            dtgvShow.DataSource = Table;
        }
        private void fManager_Load(object sender, EventArgs e)
        {
           
        }



        private static Timer _timer;

        public fManager(UserName user)
        {
            InitializeComponent();
            
            cbShow.DropDownStyle = ComboBoxStyle.DropDownList;
            timeLogin.Text = DateTime.Now.ToString("dd/MM/yyyy HH/mm/ss");
            userName.Text = user.userName;
         
            SetTimerCountDown();
        }
        
        private void SetTimerCountDown()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimedEventCountDown;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private void OnTimedEventCountDown(Object source, System.Timers.ElapsedEventArgs e)
        {
            timeNow.Invoke(new MethodInvoker(delegate ()
            {
                timeNow.Text = DateTime.Now.ToString("dd/MM/yyyy HH/mm/ss");
            })); 
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
          
            if (cbShow.Text == "Tất cả")
            {
                //var result = PeopleConnectSQL.GetListPeople();
                var result = PeopleConnectAPI.GetPeople();

                if (result != null && result.Count > 0)
                {
                    ShowDataPeopleGridView(result);
                }
             
            }
            else if (cbShow.Text == "Học sinh")
            {
                var result = PeopleConnectAPI.GetPeople().Where(x => x.Department == "Học sinh").ToList();
                //var result = from p in PeopleConnectAPI.GetPeople() where p.Department == "Học sinh" select p;
                //var result =  PeopleConnectAPI.GetPeople().Select((p) => { return p.Department; }).ToList();

                if (result != null && result.Count > 0)
                {
                    
                    ShowDataPeopleGridView(result);
                }
               
            }
            else if (cbShow.Text == "Giáo viên")
            {
                var result = PeopleConnectAPI.GetPeople().Where(x => x.Department == "Giáo viên").ToList();

                if (result != null && result.Count > 0)
                {
                    ShowDataPeopleGridView(result);
                }
               
            }
            else if (cbShow.Text == "Nhân viên")
            {
                var result = PeopleConnectAPI.GetPeople().Where(x => x.Department == "Nhân viên").ToList();

                if (result != null && result.Count > 0)
                {
                    ShowDataPeopleGridView(result);
                }
               
            }
           
        }
        //Show all
        private void ShowDataPeopleGridView(List<People> listData)
        {
            if (listData != null && listData.Count > 0)
            {
                int count = 0;
                dtgvShow.Rows.Clear();
                foreach (var item in listData)
                {
                    var row = new DataGridViewRow();
                    dtgvShow.Rows.Add(row);
                    dtgvShow.Rows[count].Cells[0].Value = item.ID;
                    dtgvShow.Rows[count].Cells[1].Value = item.Name;
                    dtgvShow.Rows[count].Cells[2].Value = item.Gender;
                    dtgvShow.Rows[count].Cells[3].Value = item.Age;
                    dtgvShow.Rows[count].Cells[4].Value = item.Department;
                    dtgvShow.Rows[count].Cells[5].Value = item.Birthday;
                    dtgvShow.Rows[count].Cells[6].Value = item.NumberPhone;
                    dtgvShow.Rows[count].Cells[7].Value = "Xóa";
                    dtgvShow.Rows[count].Cells[7].Style.ForeColor = Color.Red;
                    dtgvShow.Rows[count].Cells[8].Value = "Sửa";
                    dtgvShow.Rows[count].Cells[8].Style.ForeColor = Color.Yellow;
                    count++;
                }

            }
        }
        private void cbShow_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Thêm dữ liệu
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (txbID.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ID");
                txbID.Focus();
            }
            else if (txbName.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên");
                txbName.Focus();
            }
            else if (cbGender.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập giới tính");
                cbGender.Focus();
            }
            else
          if (txbAge.Text == "" & Int32.Parse(txbAge.Text)> 0)
            {
                MessageBox.Show("Bạn chưa nhập tuổi");
                txbAge.Focus();
            }
            else
          if (cbDept.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập nghề nghiệp");
                cbDept.Focus();
            }
           
            else
          if (txbNP.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                txbNP.Focus();
            }

            else
            {
                //bool isInsert = PeopleConnectSQL.SetListPeople(txbID.Text, txbName.Text, cbGender.Text, Int32.Parse(txbAge.Text), cbDept.Text, DateTime.Parse(dtpBirthday.Text), txbNP.Text);
                var people = new People()
                {
                    ID = txbID.Text,
                    Name = txbName.Text,
                    Gender = cbGender.Text,
                    Age = Int32.Parse(txbAge.Text),
                    Department = cbGender.Text,
                    Birthday = DateTime.Parse(dtpBirthday.Text),
                    NumberPhone = txbNP.Text
                };

                bool isInsert = PeopleConnectAPI.InsertPeople(people);
                if (isInsert)// rồi đó , chạy đi, ê còn 1 cái vấn đề nữa, code truyền cái createbyuser nào nữa mà t làm k đc
                {
                    MessageBox.Show("Thêm dữ liệu thành công");
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu thất bại");
                }
            }

        }

        private void dtgvShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                // Sửa
                if (e.RowIndex >= 0 && e.ColumnIndex == 8)
                {
                    if (dtgvShow[0, e.RowIndex].Value == null)
                    {
                        return;
                    }
                    else

                    {
                        tabControl1.SelectedIndex = 1;
                        int i;
                        i = e.RowIndex;                        
                        txbID.Text = dtgvShow.Rows[i].Cells[0].Value.ToString();
                        txbName.Text = dtgvShow.Rows[i].Cells[1].Value.ToString();
                        cbGender.Text = dtgvShow.Rows[i].Cells[2].Value.ToString();
                        txbAge.Text = dtgvShow.Rows[i].Cells[3].Value.ToString();
                        cbDept.Text = dtgvShow.Rows[i].Cells[4].Value.ToString();
                        dtpBirthday.Text = dtgvShow.Rows[i].Cells[5].Value.ToString();
                        txbNP.Text = dtgvShow.Rows[i].Cells[6].Value.ToString();

                    }
                }
                //Xóa

                if (e.RowIndex >= 0 && e.ColumnIndex == 7)
                {
                    if (dtgvShow[0, e.RowIndex].Value == null)
                    {
                        return;
                    }

                    tabControl1.SelectedIndex = 1;
                    int i;
                    i = e.RowIndex;
                    txbID.Text = dtgvShow.Rows[i].Cells[0].Value.ToString();   
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dtgvEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btchangeP_Click(object sender, EventArgs e)
        {
            fCreating fc = new fCreating();
            tabControl1.SelectedIndex = 1;
            this.Hide();
            fc.ShowDialog();
            this.Show();
            
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            var people = new People()
            {
                ID = txbID.Text,
                Name = txbName.Text,
                Gender = cbGender.Text,
                Age = Int32.Parse(txbAge.Text),
                Department = cbGender.Text,
                Birthday = DateTime.Parse(dtpBirthday.Text),
                NumberPhone = txbNP.Text
            };

            bool isInsert = PeopleConnectAPI.EditPeople(people);
            //var result = PeopleConnectAPI.EditData();
            // bool isInsert = EditDataConnectSQL.EditData(txbID.Text, txbName.Text, cbGender.Text, Int32.Parse(txbAge.Text), cbDept.Text, DateTime.Parse(dtpBirthday.Text), txbNP.Text);
            if (isInsert)
            {
                MessageBox.Show("Sửa dữ liệu thành công");
            }
            else
            {
                MessageBox.Show("Sửa dữ liệu thất bại");
            }
        }

        private void btDele_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
                bool isInsert = DeleteDataConnectSQL.DeleteData(txbID.Text);
                if (isInsert)
                {
                    MessageBox.Show("Xóa dữ liệu thành công");
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu thất bại");
                }

            }
        }
    }
}
