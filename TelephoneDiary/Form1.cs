using System.Data;
using System.Data.SqlClient;

namespace TelephoneDiary
{
    public partial class Phone_Form : Form
    {
        SqlConnection sql = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Phone_Form()
        {
            InitializeComponent();
        }

        private void Phone_Form_Load(object sender, EventArgs e)
        {
            Display_grid();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            NumberTextBox.Clear();
            MailTextBox.Clear();
            CategoryComboBox.Items.Clear();
            FirstNameTextBox.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PhoneDiary 
                    (FirstName , LastName  , Number , Email , Category) 
            VALUES('" + FirstNameTextBox.Text + "','" + LastNameTextBox.Text + "','" + NumberTextBox.Text + "','" + MailTextBox.Text + "','" + CategoryComboBox.Text + "')", sql);
            cmd.ExecuteNonQuery();
            sql.Close();
            MessageBox.Show("Successfully Added");
            Display_grid();
        }

        void Display_grid()
        {
            SqlDataAdapter sdata = new SqlDataAdapter(@"Select * from PhoneDiary ", sql); 
            DataTable dt = new DataTable();
            sdata.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            FirstNameTextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            LastNameTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            NumberTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            MailTextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            CategoryComboBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PhoneDiary
            WHERE (Number = '" + NumberTextBox.Text + "')", sql);
            cmd.ExecuteNonQuery();
            sql.Close();
            MessageBox.Show("Successfully Deleted");
            Display_grid();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE PhoneDiary
            SET FirstName = '" + FirstNameTextBox.Text + "' , LastName = '" + LastNameTextBox.Text + "' , Number = '" + NumberTextBox.Text + "' , Email= '" + MailTextBox.Text + "' , Category = '" + CategoryComboBox.Text + "' WHERE(Number = '" + NumberTextBox.Text + "')", sql);
            cmd.ExecuteNonQuery ();
            sql.Close();
            MessageBox.Show("Successfully Updated");
            Display_grid();
        }

        
    }
}