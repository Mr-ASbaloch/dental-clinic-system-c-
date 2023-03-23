using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Dclinic__system
{
    public partial class Appointment : Form
    {
        public Appointment()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();
        private void fillPatient()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand Cmd = new SqlCommand("select PatName from PatientTbl", Con);
            SqlDataReader rdr;
            rdr = Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("patName", typeof(string));
            dt.Load(rdr);
            PatientCb.ValueMember = "PatName";
            PatientCb.DataSource = dt;
            Con.Close();
        }
       void clear()
        {
            PatientCb.Text = "";
            TimeCb.Text = "";

        }
        private void fillDoctor()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand Cmd = new SqlCommand("select DocName from DoctorTbl", Con);
            SqlDataReader rdr;
            rdr = Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DocName", typeof(string));
            dt.Load(rdr);
            DocCb.ValueMember = "DocName";
            DocCb.DataSource = dt;
            Con.Close();
        }


        private void Appointment_Load(object sender, EventArgs e)
        {
            fillPatient();
            fillDoctor();
            Populate();
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from AppointmentTbl";
            DataSet ds = Pat.ShowPatient(Query);
            AppointmentDGV.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            string chek_query = "SELECT * FROM AppointmentTbl WHERE Doctor='"+DocCb.SelectedValue.ToString()+"' AND ApTime='"+TimeCb.SelectedItem.ToString()+"' And ApDate='"+Date.Value.Date+"' ";
            DataTable dt = new DataTable(); 
            SqlDataAdapter adat = new SqlDataAdapter(chek_query,con);
            adat.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("An appointment already exist for selected..");
            }
            else
            {

                string Query = "insert into AppointmentTbl values( '" + PatientCb.SelectedValue.ToString() + "','" + Date.Value.Date + "','" + TimeCb.SelectedItem.ToString() + "','" + DocCb.SelectedValue.ToString() + "')";
                MyPatient Pat = new MyPatient();
                try
                {
                    Pat.AddPatient(Query);
                    MessageBox.Show("Appointment Suucessfully Recorded");
                    Populate();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }







        }

       

        

      

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The Appointment to cancel");
            }
            else
            {
                try
                {
                    String Query = " Delete  from AppointmentTbl Where ApId=" + Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("Appointment Deleted SUCCESSFULLY");
                    Populate();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }
        int Key = 0;

        

        private void label2_Click(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }

      

        private void label6_Click(object sender, EventArgs e)
        {
            //Dashboard dash = new Dashboard();
           // dash.show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            prescription prec = new prescription();
            prec.Show();
            this.Hide();
        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from AppointmentTbl ", Con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
           AppointmentDGV.DataSource = dt;
        }

        private void AppointmentDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(AppointmentDGV.Rows[e.RowIndex].Cells["ApId"].FormattedValue.ToString());
                SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
                Con.Open();
                SqlCommand cmd = new SqlCommand("Delete  AppointmentTbl where ApId='" + id + "' ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("deleted Successfully");
                BindGrid();
                Con.Close();
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }
    }
}
