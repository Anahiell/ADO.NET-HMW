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

namespace WinFOrms_ADO.NET
{
    public partial class Form1 : Form
    {
        private string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SalesDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTableNames();
        }
        private void LoadTableNames()
        {
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                DataTable tables = sqlCon.GetSchema("Tables");
                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    comboBox1.Items.Add(tableName);
                }
            }
        }
        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = comboBox1.SelectedItem.ToString();
            string query = $"SELECT * FROM {selectedTable}";
            using (SqlConnection sqlCon = new SqlConnection(connString))
            {
                sqlCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlCon);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
