using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dclinic__system
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

       

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(AdminPass.Text=="")
            {
                MessageBox.Show("enter Admin password");
            }
            else if (AdminPass.Text=="password")
            {
                Patient pat = new Patient();
                pat.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("wrong password.Contact Admin");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
