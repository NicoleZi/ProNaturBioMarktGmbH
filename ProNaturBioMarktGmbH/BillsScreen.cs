using System;
using System.Collections;
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
using System.Xml.Linq;

namespace ProNaturBioMarktGmbH
{
    public partial class BillsScreen : Form
    {
        private SqlConnection connectionDatabase = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Nicole\Documents\ProNatur Bimarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductKey;
        private string CostumerName = "";

        public BillsScreen()
        {
            InitializeComponent();

            ShowProducts();
        }

        private void ShowProducts()
        {
            string query = "SELECT * FROM Products";
            DataSet dataSet = GetDataSet(query);

            dGVProducts.DataSource = dataSet.Tables[0];
            dGVProducts.Columns[0].Visible = false;
        }
       
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            CostumerName = textBoxCostumer.Text;

            if(CostumerName == "")
            {
                MessageBox.Show("Please insert your name for login.");
                return;
            }

            string query = string.Format("SELECT * FROM Costumer WHERE CostumerName = '{0}'", CostumerName);
            DataSet dataSet = GetDataSet(query);

            //New Costumer
            if (dataSet.Tables[0].Rows.Count == 0)
            {              
                NewCostumer(CostumerName);
                return;
            }

            //Else Load Data
            dGVShoppingList.DataSource = dataSet.Tables[0];
            dGVShoppingList.Columns[0].Visible = false;
            dGVShoppingList.Columns[1].Visible = false;
            dGVShoppingList.Columns[3].Visible = false;

            labelID.Text = "Costumer ID: " + dataSet.Tables[0].Rows[0][0].ToString();
            labelTotalPrice.Text = "Total Price:    " + dataSet.Tables[0].Rows[0][3].ToString();

            dGVShoppingList.Rows[0].Cells[2].Value = SplitProductString(dGVShoppingList.Rows[0].Cells[2].Value.ToString());

            buttonPlus.Visible = true;
            buttonMinus.Visible = true;

            labelShoppinglist.Text = "Shoppinglist of " + CostumerName + ":";
        }

        private void NewCostumer(string name)
        {
            string query = string.Format("INSERT INTO Costumer VALUES ('{0}', '{1}', '{2}')", name, null, "0");
            ExecuteQuery(query);

            query = string.Format("SELECT * FROM Costumer WHERE CostumerName = '{0}'", name);
            DataSet dataSet = GetDataSet(query);

            labelID.Text = "Costumer ID: " + dataSet.Tables[0].Rows[0][0].ToString();
        }

        private void ExecuteQuery(string query)
        {
            connectionDatabase.Open();
            SqlCommand sqlCommand = new SqlCommand(query, connectionDatabase);
            sqlCommand.ExecuteNonQuery();
            connectionDatabase.Close();
        }

        private void ButtonPlus_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            string NewProductName = dGVProducts.SelectedRows[0].Cells[1].Value.ToString();
            string priceString = dGVProducts.SelectedRows[0].Cells[4].Value.ToString();

            string query = string.Format("SELECT * FROM Costumer WHERE CostumerName = '{0}'", CostumerName);
            DataSet dataSet = GetDataSet(query);

            //New Products Shopping List
            string productsToBuy = dataSet.Tables[0].Rows[0][2].ToString();
            if (productsToBuy == "")
                productsToBuy = NewProductName;
            else
                productsToBuy = productsToBuy + " " + NewProductName;

            //New Total Price
            string totalPriceString = dataSet.Tables[0].Rows[0][3].ToString();
            float totalPrice = float.Parse(totalPriceString) + float.Parse(priceString);

            query = string.Format("UPDATE Costumer SET Products='{0}', TotalPrice='{1}' WHERE CostumerName='{2}'", productsToBuy, totalPrice.ToString(), CostumerName);
            ExecuteQuery(query);

            ShowShoppinglist();
        }

        private void DGVProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lastSelectedProductKey = (int)dGVProducts.SelectedRows[0].Cells[0].Value;
        }

        private DataSet GetDataSet(string query)
        {
            connectionDatabase.Open();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, connectionDatabase);

            DataSet dataSet = new DataSet();
            sqlAdapter.Fill(dataSet);

            connectionDatabase.Close();

            return dataSet;
        }

        private string SplitProductString(string productsToSplit)
        {
            //Split Productstring and insert new line for each
            string[] split = productsToSplit.Split(' ');

            productsToSplit = "";

            foreach (string item in split)
            {
                productsToSplit = productsToSplit + item + "\n";
            }

            return productsToSplit;
        }

        private void ShowShoppinglist()
        {
            string query = string.Format("SELECT * FROM Costumer WHERE CostumerName = '{0}'", CostumerName);
            DataSet dataSet = GetDataSet(query);

            dGVShoppingList.DataSource = dataSet.Tables[0];

            labelTotalPrice.Text = "Total Price:    " + dataSet.Tables[0].Rows[0][3].ToString();
            dGVShoppingList.Rows[0].Cells[2].Value = SplitProductString(dGVShoppingList.Rows[0].Cells[2].Value.ToString());
        }

        private void ButtonMinus_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            string ProductToRemove = dGVProducts.SelectedRows[0].Cells[1].Value.ToString();

            string query = string.Format("SELECT * FROM Costumer WHERE CostumerName = '{0}'", CostumerName);
            DataSet dataSet = GetDataSet(query);

            string shoppingProducts = dataSet.Tables[0].Rows[0][2].ToString();
            string[] splitProducts = shoppingProducts.Split(' ');

            bool hit = false;
            string remainingProducts = "";

            foreach(string item in splitProducts)
            {
                Console.WriteLine("Before:" + item);
                string replacedItem = item.Replace("\n", "");

                if (item == string.Empty)
                {
                    Console.WriteLine("Skipped:___" + item);
                    continue;
                } else
                    Console.WriteLine("Weitergehts");


                if (item.Contains(ProductToRemove) && !hit)
                {
                    hit = true;
                } else
                {
                    remainingProducts = remainingProducts + " " + item;
                }              
            }

            if (hit)
            {
                //New Total Price
                string priceString = dGVProducts.SelectedRows[0].Cells[4].Value.ToString();
                string totalPriceString = dataSet.Tables[0].Rows[0][3].ToString();
                float totalPrice = float.Parse(totalPriceString) - float.Parse(priceString);

                query = string.Format("UPDATE Costumer SET Products='{0}', TotalPrice='{1}' WHERE CostumerName='{2}'", remainingProducts, totalPrice.ToString(), CostumerName);
                ExecuteQuery(query);
            }

            ShowShoppinglist();
        }

        private void ButtonRemoveAll_Click(object sender, EventArgs e)
        {
            string query = string.Format("UPDATE Costumer SET Products='{0}', TotalPrice='{1}' WHERE CostumerName='{2}'", null, "0", CostumerName);
            ExecuteQuery(query);

            labelTotalPrice.Text = "Total Price:    0";
            dGVShoppingList.Rows[0].Cells[2].Value = "";
        }
    }
}
