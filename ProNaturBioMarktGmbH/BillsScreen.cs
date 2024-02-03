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
    public partial class BillsScreen : Form
    {
        private SqlConnection connectionDatabase = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Nicole\Documents\ProNatur Bimarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");

        public BillsScreen()
        {
            InitializeComponent();

            ShowProducts();
        }

        private void ShowProducts()
        {
            connectionDatabase.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, connectionDatabase);

            DataSet dataSet = new DataSet();
            sqlAdapter.Fill(dataSet);

            dGVProducts.DataSource = dataSet.Tables[0];
            dGVProducts.Columns[0].Visible = false;

            connectionDatabase.Close();
        }

        private void ExecuteQuery(string query)
        {
            connectionDatabase.Open();
            SqlCommand sqlCommand = new SqlCommand(query, connectionDatabase);
            sqlCommand.ExecuteNonQuery();
            connectionDatabase.Close();
        }

        private void DGVProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            // if name in database - load data
            //if doppelname frage nach id
            // else create new table content

            //string name = textBoxCostumer.Text;
            string name = "Peter";

            if(name == "")
            {
                MessageBox.Show("Please insert your name for login.");
                return;
            }

            string query = string.Format("SELECT * FROM Costumer WHERE CostumerName = '{0}'", name);

            connectionDatabase.Open();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, connectionDatabase);

            DataSet dataSet = new DataSet();
            sqlAdapter.Fill(dataSet);

            //if dataset null - create new column

            dGVShoppingList.DataSource = dataSet.Tables[0];
            dGVShoppingList.Columns[0].Visible = false;
            dGVShoppingList.Columns[1].Visible = false;
            dGVShoppingList.Columns[3].Visible = false;

            labelID.Text = "Costumer ID: " + dataSet.Tables[0].Rows[0][0].ToString();
            labelTotalPrice.Text = "Total Price:    " + dataSet.Tables[0].Rows[0][3].ToString();

            connectionDatabase.Close();


            
        }
    }
}
