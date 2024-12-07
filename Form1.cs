using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace CRUDApplication
{
    public partial class Form1 : Form
    {

        private const string PATH = "Data Source=LAPTOP-0NDRRN1S\\SQLEXPRESS;Initial Catalog=crudapp;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
        }

        private void clearFields()
        {
            foreach(Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = "";
                else if (control is NumericUpDown numericUpDown)
                    numericUpDown.Value = 0;                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Delete
            SqlConnection con = new SqlConnection(PATH);
            con.Open();
            string deleteQuery = "DELETE FROM crudapp WHERE id=@id";
            SqlCommand cmd = new SqlCommand(deleteQuery, con);
            // the parameter id we pass to the query from the form
            cmd.Parameters.AddWithValue("@id", numericUpDown1.Value);
            // returns the number of rows deleted in the table
            int IsDeleted = cmd.ExecuteNonQuery();
            con.Close();
            if (IsDeleted > 0)
            {
                MessageBox.Show("Deleted successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
            }
            else
                MessageBox.Show("Error in delete", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Update
            SqlConnection con = new SqlConnection(PATH);
            con.Open();
            string updateQuery = "UPDATE crudapp SET  email=@email, name=@name, username=@username, password=@password WHERE id=@id";

            SqlCommand cmd = new SqlCommand(updateQuery, con);

            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@username", txtUser.Text);
            cmd.Parameters.AddWithValue("@password", txtpass.Text);
            cmd.Parameters.AddWithValue("@id", numericUpDown1.Value);
            int count = cmd.ExecuteNonQuery();
            con.Close();
            if (count > 0)
            {
                MessageBox.Show("Updated successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
            }

            else
                MessageBox.Show("Error in update", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Read
            
            foreach (Control control in this.Controls)
            {
                if (control is TextBox || control is NumericUpDown || control is Label)
                    control.Visible = false;

                else
                    control.Visible = true;
            }
            SqlConnection con = new SqlConnection(PATH);
            string readQuery = "SELECT * FROM crudapp";
            // the data adapter allows us to fill DataTable in our form
            SqlDataAdapter sda = new SqlDataAdapter(readQuery, con);
            //SqlCommandBuilder cmd = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            // FILL opens and closes the connection automaticlly
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Create
            
            bool isEmpty = false;
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    if (control.Text.Length == 0)
                    {
                        isEmpty = true;
                        break;
                    }
                }
            }
            if (isEmpty)
                MessageBox.Show("Please fill the required form",
                    "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                SqlConnection con = new SqlConnection(PATH);
                con.Open();
                string insertQuery = "INSERT INTO crudapp (email, name, username, password) " +
                "VALUES (@email, @name, @username, @password)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@username", txtUser.Text);
                cmd.Parameters.AddWithValue("@password", txtpass.Text);
                int isInserted = cmd.ExecuteNonQuery();
                con.Close();
                if (isInserted == 1)
                {
                    MessageBox.Show("Create successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                else
                    MessageBox.Show("Error in creation", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Home
            
            foreach (Control control in this.Controls)
            {
                if (control is DataGridView)
                    control.Visible = false;
                else
                    control.Visible = true;
            }
        }
    }
}