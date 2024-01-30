namespace ProNaturBioMarktGmbH
{
    partial class MainMenuScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuScreen));
            this.buttonProducts = new System.Windows.Forms.Button();
            this.buttonBills = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonProducts
            // 
            this.buttonProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonProducts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonProducts.BackgroundImage")));
            this.buttonProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProducts.ForeColor = System.Drawing.Color.White;
            this.buttonProducts.Location = new System.Drawing.Point(12, 29);
            this.buttonProducts.Name = "buttonProducts";
            this.buttonProducts.Size = new System.Drawing.Size(275, 125);
            this.buttonProducts.TabIndex = 0;
            this.buttonProducts.Text = "Manage products";
            this.buttonProducts.UseVisualStyleBackColor = true;
            this.buttonProducts.Click += new System.EventHandler(this.ButtonProducts_Click);
            // 
            // buttonBills
            // 
            this.buttonBills.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBills.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBills.BackgroundImage")));
            this.buttonBills.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBills.ForeColor = System.Drawing.Color.White;
            this.buttonBills.Location = new System.Drawing.Point(300, 29);
            this.buttonBills.Name = "buttonBills";
            this.buttonBills.Size = new System.Drawing.Size(275, 125);
            this.buttonBills.TabIndex = 1;
            this.buttonBills.Text = "Create bill";
            this.buttonBills.UseVisualStyleBackColor = true;
            // 
            // MainMenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(587, 187);
            this.Controls.Add(this.buttonBills);
            this.Controls.Add(this.buttonProducts);
            this.Name = "MainMenuScreen";
            this.Text = "MainMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonProducts;
        private System.Windows.Forms.Button buttonBills;
    }
}