using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNaturBioMarktGmbH
{
    public partial class ProductsScreen : Form
    {
        private SqlConnection connectionDatabase = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Nicole\Documents\ProNatur Bimarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");

        public ProductsScreen()
        {
            InitializeComponent();

            connectionDatabase.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, connectionDatabase);

            DataSet dataSet = new DataSet();
            sqlAdapter.Fill(dataSet);

            dataGridViewProduct.DataSource = dataSet.Tables[0];
            dataGridViewProduct.Columns[0].Visible = false;

            connectionDatabase.Close();
        }

        private void ButtonProductSave_Click(object sender, EventArgs e)
        {

        }

        private void ButtonProductEdit_Click(object sender, EventArgs e)
        {

        }

        private void ButtonProductClear_Click(object sender, EventArgs e)
        {

        }

        private void ButtonProductDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
