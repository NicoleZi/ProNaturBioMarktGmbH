using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProNaturBioMarktGmbH
{
    public partial class ProductsScreen : Form
    {
        private SqlConnection connectionDatabase = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Nicole\Documents\ProNatur Bimarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductKey;

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

            string query = string.Format("insert into Products values ('{0}', '{1}','{2}','{3}')", name, brand, category, price);
            ExecuteQuery(query);

            ShowProducts();
            ClearAllFields();
        }

        private void ButtonProductEdit_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            string name = textBoxProductName.Text;
            string brand = textBoxProductBrand.Text;
            string category = comboBoxProductCategory.Text;
            string price = textBoxProductPrice.Text;

            string query = string.Format("update Products set Name='{0}', Brand='{1}', Category='{2}', Price='{3}' where Id={4}", name, brand, category, price, lastSelectedProductKey);
            ExecuteQuery(query);

            ShowProducts();
        }

        private void ButtonProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void ButtonProductDelete_Click(object sender, EventArgs e)
        {
            if(lastSelectedProductKey == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }               

            string query = string.Format("delete from Products where Id={0};", lastSelectedProductKey);
            ExecuteQuery(query);

            ClearAllFields();
            ShowProducts();
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

        private void ExecuteQuery(string query)
        {
            connectionDatabase.Open();
            SqlCommand sqlCommand = new SqlCommand(query, connectionDatabase);
            sqlCommand.ExecuteNonQuery();
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

        private void DataGridViewProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxProductName.Text = dataGridViewProduct.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductBrand.Text = dataGridViewProduct.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxProductCategory.Text = dataGridViewProduct.SelectedRows[0].Cells[3].Value.ToString();
            textBoxProductPrice.Text = dataGridViewProduct.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectedProductKey = (int)dataGridViewProduct.SelectedRows[0].Cells[0].Value;
        }
    }
}
