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
    public partial class LoadingScreen : Form
    {
        private int loadingBarValue;

        public LoadingScreen()
        {
            InitializeComponent();
        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            loadingbarTimer.Start();
        }

        private void LoadingbarTimer_Tick(object sender, EventArgs e)
        {
            loadingBarValue += 1;
            progressBarLoading.Value = loadingBarValue;
            labelProcent.Text = loadingBarValue.ToString() + "%";

            if (loadingBarValue >= progressBarLoading.Maximum)
            {
                loadingbarTimer.Stop();

                MainMenuScreen mainMenu = new MainMenuScreen();
                mainMenu.Show();
                this.Hide();
            }
                         
        }        
    }
}
