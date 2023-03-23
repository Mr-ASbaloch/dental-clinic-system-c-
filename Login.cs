using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dclinic__system
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        
        ConnectionString MyConnection = new ConnectionString();
        
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection Con = MyConnection.GetCon();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from UserTbl where Uname='" + UnameTb.Text + "'  and Upass='" + UpassTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString()=="1")
            {
                Appointment App = new Appointment();
                App.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong user name or Password");
                UnameTb.Text = "";
                UpassTb.Text = "";

            }
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminLogin log = new AdminLogin();
            log.Show();
            this.Hide();
        }

       
    }
}
