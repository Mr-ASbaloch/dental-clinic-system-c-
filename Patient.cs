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
using System.Text.RegularExpressions;

namespace Dclinic__system
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

       

           

    




       /* private void button1_Click(object sender, EventArgs e) 
        {
            /*
            String Query = "insert into PatientTbl values  ( '" + PatNameTb.Text + "','" + textBox3.Text + "','" + AddressTb.Text + "','" + DOBDate.Value.Date + "','" + GenderCb.SelectedItem.ToString() + "','" + AllergyTb.Text + "','" + CnicTb.Text + "'";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(Query);
                MessageBox.Show("Patient Suucessfully Added");
                Populate();
                Cleardata();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            

        }*/


        private void Cleardata()
        {
            PatNameTb.Text = "";
            textBox3.Text = "";
            AddressTb.Text = "";
            DOBDate.Text = "";
            AllergyTb.Text = "";
            GenderCb.SelectedItem = "";
            CnicTb.Text = "";
          
        }
        void Populate()
        {

            MyPatient Pat = new MyPatient();
            String Query = " select * from PatientTbl";
            DataSet ds = Pat.ShowPatient(Query);
            PatientDGV.DataSource = ds.Tables[0];
        }


        private void Patient_Load(object sender, EventArgs e)
        {
            Populate();
        }
        int Key = 0;


        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (Key == 0)
            {
                MessageBox.Show("Select The Patient");
            }
            else
            {
                try
                {
                    String Query = " Delete  from PatientTbl Where PatId=" + Key + "";
                    Pat.DeletePatient(Query);
                    MessageBox.Show("Patient Deleted SUCCESSFULLY");
                    Populate();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
          
        }

      

        private void label7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }



        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("error,phone number can,t contain letters");
            }


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

        private void label14_Click(object sender, EventArgs e)
        {
            DashBoard dsh = new DashBoard();
            dsh.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            User usr = new User();
            usr.Show();
            this.Hide();
        }

        private void DOBDate_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Today < DOBDate.Value)
            {
                MessageBox.Show("invalid date ");
                DOBDate.Value = DateTime.Today;

            }
        }
        private void BindGrid()
        {
            SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from PatientTbl ", Con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            PatientDGV.DataSource = dt;
        }

        private void PatientDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(PatientDGV.Rows[e.RowIndex].Cells["PatId"].FormattedValue.ToString());
                SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
                Con.Open();
                SqlCommand cmd = new SqlCommand("Delete  PatientTbl where PatId='" + id + "' ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("deleted Successfully");
                BindGrid();
                Con.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            Con.Open();
            SqlCommand cmd = new SqlCommand("insert into PatientTbl values (@PatName,@PatPhone,@PatAddress,@PatDOB,@PatGender, @PAtAllergies,@Cnic)", Con);
            cmd.Parameters.AddWithValue("@PatName", PatNameTb.Text);
            cmd.Parameters.AddWithValue("@PatPhone", textBox3.Text);
            cmd.Parameters.AddWithValue("@PatAddress", AddressTb.Text);
            cmd.Parameters.AddWithValue("@PatDOB", DOBDate.Value.Date);
            cmd.Parameters.AddWithValue("@PatGender", GenderCb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PatAllergies", AllergyTb.Text);
            cmd.Parameters.AddWithValue("@Cnic", CnicTb.Text);
            cmd.ExecuteNonQuery();

            MessageBox.Show("successfully Added");
            Cleardata();
            Con.Close();
        }

        private void CnicTb_TextChanged(object sender, EventArgs e)
        {
           
            Regex regex = new Regex(@"^[0-9]{5}-[0-9]{7}-[0-9]{1}$");

          
            if (!regex.IsMatch(CnicTb.Text))
            {
                errorProvider1.SetError(CnicTb, "Invalid CNIC format. Please enter a valid CNIC number.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
          /*  SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
            Con.Open();
            SqlCommand cmd = new SqlCommand("update  PatientTbl set PatName=@PatName,PatPhone=@PatPhone,PatAddress=@PatAddress,PatDOB=@PatDOB,PAtGender=@PatGender, PAtAllergies=@PAtAllergies ,Cnic=@Cnic where PatId=" + id + "", Con);

            cmd.ExecuteNonQuery();

            MessageBox.Show("successfully Updated");            Cleardata();
            Con.Close();*/
        }
        /*  int id = 0;
 private void button3_Click(object sender, EventArgs e)
 {

     SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\OneDrive\\Documents\\DentalDb.mdf;Integrated Security=True;Connect Timeout=30");
     Con.Open();
     SqlCommand cmd = new SqlCommand("update  PatientTbl set PatName=@PatName,PatPhone=@PatPhone,PatAddress=@PatAddress,PatDOB=@PatDOB,PAtGender=@PatGender, PAtAllergies=@PAtAllergies ,Cnic=@Cnic where PatId="+ id +"", Con);

     cmd.ExecuteNonQuery();

     MessageBox.Show("successfully Updated");
     Cleardata();
     Con.Close();
 }*/
    }

       
}
