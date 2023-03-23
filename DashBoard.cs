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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        ConnectionString MyConnection = new ConnectionString();

        private void DashBoard_Load(object sender, EventArgs e)
        {
            PendingAppProgress.Value = 100;
            UserPogress.Value = 100;
            Patients.Value = 100;
            SqlConnection con = MyConnection.GetCon();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from appointmentTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Pendinglbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from PatientTbl", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Patientlbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda2= new SqlDataAdapter("select count(*) from UserTbl", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Userlbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }
    }
}
