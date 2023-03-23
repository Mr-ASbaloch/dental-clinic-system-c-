using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dclinic__system
{
    public partial class Treatment : Form
    {
        public Treatment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Query = "insert into TreatmentTbl values  ( '" + TreatNameTb.Text + "','" + TreatCost.Text + "','" + TreatDesc.Text + "')";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(Query);
                MessageBox.Show("Treatment Suucessfully Added");
                Populate();
                clear();
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        void clear()
        {
            TreatCost.Text = "";
            TreatDesc.Text = "";
            TreatNameTb.Text = "";
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from TreatmentTbl";
            DataSet ds = Pat.ShowPatient(Query);
            TreatmentDGV.DataSource = ds.Tables[0];
        }
        private void Treatment_Load(object sender, EventArgs e)
        {
            Populate();
        }
        int Key = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The Treatment");
            }
            else
            {
                try
                {
                    String Query = "Update TreatmentTbl set TreatName='" + TreatNameTb.Text + "', TreatCost='" + TreatCost.Text + "',TreatDesc='" + TreatDesc.Text + "' Where TreatId="+ Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("Treatment  Updated SUCCESSFULLY");
                    Populate();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The Treatment");
            }
            else
            {
                try
                {
                    String Query = " Delete  from TreatmentTbl Where TreatId=" + Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("Treatment Deleted SUCCESSFULLY");
                    Populate();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

       

        private void label8_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Patient pat = new Patient();
            pat.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doctors doc = new Doctors();
            doc.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            prescription prec = new prescription();
            prec.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from TreatmentTbl ", Con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            TreatmentDGV.DataSource = dt;
        }

        private void TreatmentDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(TreatmentDGV.Rows[e.RowIndex].Cells["TreatId"].FormattedValue.ToString());
                SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
                Con.Open();
                SqlCommand cmd = new SqlCommand("Delete  TreatmentTbl where TreatId='" + id + "' ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("deleted Successfully");
                BindGrid();
                Con.Close();
            }
        }
    }
}
