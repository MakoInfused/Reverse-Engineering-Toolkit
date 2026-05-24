namespace HexTools
{
    partial class HexAddressBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Address = new BasicTools.BasicControls.BasicNumericBox();
            this.EditButton = new BasicTools.BasicControls.BasicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Address
            // 
            this.Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Address.Hexadecimal = true;
            this.Address.Location = new System.Drawing.Point(3, 5);
            this.Address.MaxLength = 1;
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(120, 22);
            this.Address.TabIndex = 0;
            this.Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Address_KeyDown);
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditButton.Image = global::HexTools.My.Resources.Resources.Edit;
            this.EditButton.Location = new System.Drawing.Point(125, 3);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(30, 24);
            this.EditButton.TabIndex = 1;
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Address);
            this.panel1.Controls.Add(this.EditButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 30);
            this.panel1.TabIndex = 2;
            // 
            // HexAddressBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "HexAddressBox";
            this.Size = new System.Drawing.Size(158, 30);
            this.Load += new System.EventHandler(this.HexAddressBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Address)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BasicTools.BasicControls.BasicNumericBox Address;
        private BasicTools.BasicControls.BasicButton EditButton;
        private System.Windows.Forms.Panel panel1;
    }
}
