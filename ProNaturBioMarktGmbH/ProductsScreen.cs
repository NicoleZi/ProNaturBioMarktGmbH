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

            ShowProducts();
        }

        private void ButtonProductSave_Click(object sender, EventArgs e)
        {
            if(textBoxProductName.Text == "" || textBoxProductBrand.Text == "" || comboBoxProductCategory.Text == "" || textBoxProductPrice.Text == "")
            {
                MessageBox.Show("Please fill in all Fields.");

                return;
            }

            string name = textBoxProductName.Text;
            string brand = textBoxProductBrand.Text;
            string category = comboBoxProductCategory.Text;
            string price = textBoxProductPrice.Text;

            connectionDatabase.Open();
            string query = string.Format("insert into Products values ('{0}', '{1}','{2}','{3}')", name, brand, category, price);
            SqlCommand sqlCommand = new SqlCommand(query, connectionDatabase);
            sqlCommand.ExecuteNonQuery();
            connectionDatabase.Close();

            ShowProducts();
            ClearAllFields();
        }

        private void ButtonProductEdit_Click(object sender, EventArgs e)
        {

        }

        private void ButtonProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void ButtonProductDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void ShowProducts()
        {
            connectionDatabase.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, connectionDatabase);

            DataSet dataSet = new DataSet();
            sqlAdapter.Fill(dataSet);

            dataGridViewProduct.DataSource = dataSet.Tables[0];
            dataGridViewProduct.Columns[0].Visible = false;

            connectionDatabase.Close();
        }

        private void ClearAllFields()
        {
            textBoxProductName.Text = string.Empty;
            textBoxProductBrand.Text = string.Empty;
            textBoxProductPrice.Text = string.Empty;           
            comboBoxProductCategory.Text = string.Empty;
            comboBoxProductCategory.SelectedItem = null;
        }
    }
}
