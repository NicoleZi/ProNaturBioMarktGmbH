using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNaturBioMarktGmbH
{
    public partial class MainMenuScreen : Form
    {
        public MainMenuScreen()
        {
            InitializeComponent();
        }

        private void ButtonProducts_Click(object sender, EventArgs e)
        {
            ProductsScreen productsScreen = new ProductsScreen();
            productsScreen.Show();

            this.Hide();
        }

        private void ButtonBills_Click(object sender, EventArgs e)
        {
            BillsScreen billsScreen = new BillsScreen();
            billsScreen.Show();

            this.Hide();
        }
    }
}
