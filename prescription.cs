using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Dclinic__system
{
    public partial class prescription : Form
    {
        public prescription()
        {
            InitializeComponent();
        }

      

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

       

        private void label2_Click(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void prescription_Load(object sender, EventArgs e)
        {
            fillPatient();
            clear();
         

        }
        ConnectionString MyCon = new ConnectionString();

        public object TreatNameTb { get; private set; }

        private void fillPatient()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand Cmd = new SqlCommand("select Patient from AppointmentTbl", Con);
            SqlDataReader rdr;
            rdr = Cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("patient", typeof(string));
            dt.Load(rdr);
            PatId.ValueMember = "Patient";
            PatId.DataSource = dt;
            Con.Close();
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from PrescriptionTbl";
            DataSet ds = Pat.ShowPatient(Query);
            PresDGV.DataSource = ds.Tables[0];
        }
        void clear()
        {
            CostTb.Text = "";
            QuanTb.Text = "";
            TreatTb.Text = "";
            MedTb.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Query = "insert into PrescriptionTbl values( '" + PatId.SelectedValue.ToString() + "','" + TreatTb.Text + "','" + CostTb.Text + "','" + MedTb.Text + "' ,'" + QuanTb.Text + "')";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(Query);
                MessageBox.Show("Suucessfully Recorded");
                Populate();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            clear();
        }
        int key = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (key == 0)
            {
                MessageBox.Show("Select The Prescription");
            }
            else
            {
                try
                {
                    String Query = " Delete  from PrescriptionTbl Where PrescId=" + key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show(" Deleted SUCCESSFULLY");
                    Populate();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
               
            }
        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from PrescriptionTbl ", Con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            PresDGV.DataSource = dt;
        }

        private void PresDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(PresDGV.Rows[e.RowIndex].Cells["PrescId"].FormattedValue.ToString());
                SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
                Con.Open();
                SqlCommand cmd = new SqlCommand("Delete  PrescriptionTbl where PrescId='" + id + "' ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("deleted Successfully");
                BindGrid();
                Con.Close();
            }
        }
    }
}
