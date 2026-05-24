namespace HexTools
{
    partial class HexAddress
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
            this.components = new System.ComponentModel.Container();
            this.TypePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.NewTypeLabel = new System.Windows.Forms.Label();
            this.AddressPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Address = new BasicTools.BasicControls.BasicNumericBox();
            this.PreviewPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OldTypeLabel = new System.Windows.Forms.Label();
            this.Offset = new BasicTools.BasicControls.BasicTextBox();
            this.TypePanel.SuspendLayout();
            this.AddressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).BeginInit();
            this.PreviewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TypePanel
            // 
            this.TypePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TypePanel.Controls.Add(this.label1);
            this.TypePanel.Location = new System.Drawing.Point(3, 64);
            this.TypePanel.Name = "TypePanel";
            this.TypePanel.Size = new System.Drawing.Size(295, 73);
            this.TypePanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewTypeLabel
            // 
            this.NewTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewTypeLabel.Location = new System.Drawing.Point(3, 0);
            this.NewTypeLabel.Name = "NewTypeLabel";
            this.NewTypeLabel.Size = new System.Drawing.Size(120, 28);
            this.NewTypeLabel.TabIndex = 2;
            this.NewTypeLabel.Text = "Address:";
            this.NewTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AddressPanel
            // 
            this.AddressPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressPanel.Controls.Add(this.NewTypeLabel);
            this.AddressPanel.Controls.Add(this.Address);
            this.AddressPanel.Location = new System.Drawing.Point(3, 2);
            this.AddressPanel.Name = "AddressPanel";
            this.AddressPanel.Size = new System.Drawing.Size(295, 33);
            this.AddressPanel.TabIndex = 3;
            // 
            // Address
            // 
            this.Address.Hexadecimal = true;
            this.Address.Location = new System.Drawing.Point(129, 3);
            this.Address.MaxLength = 1;
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(120, 22);
            this.Address.TabIndex = 0;
            // 
            // PreviewPanel
            // 
            this.PreviewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewPanel.Controls.Add(this.OldTypeLabel);
            this.PreviewPanel.Controls.Add(this.Offset);
            this.PreviewPanel.Location = new System.Drawing.Point(3, 33);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.Size = new System.Drawing.Size(295, 28);
            this.PreviewPanel.TabIndex = 4;
            // 
            // OldTypeLabel
            // 
            this.OldTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OldTypeLabel.Location = new System.Drawing.Point(3, 4);
            this.OldTypeLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.OldTypeLabel.Name = "OldTypeLabel";
            this.OldTypeLabel.Size = new System.Drawing.Size(120, 28);
            this.OldTypeLabel.TabIndex = 2;
            this.OldTypeLabel.Text = "Address:";
            this.OldTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Offset
            // 
            this.Offset.BackColor = System.Drawing.SystemColors.Control;
            this.Offset.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Offset.Location = new System.Drawing.Point(129, 7);
            this.Offset.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.Offset.MaxLength = 8;
            this.Offset.Name = "Offset";
            this.Offset.ReadOnly = true;
            this.Offset.Size = new System.Drawing.Size(100, 15);
            this.Offset.TabIndex = 3;
            this.Offset.TabStop = false;
            // 
            // HexAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TypePanel);
            this.Controls.Add(this.AddressPanel);
            this.Controls.Add(this.PreviewPanel);
            this.Name = "HexAddress";
            this.Size = new System.Drawing.Size(302, 140);
            this.Load += new System.EventHandler(this.HexAddressEditor_Load);
            this.TypePanel.ResumeLayout(false);
            this.TypePanel.PerformLayout();
            this.AddressPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Address)).EndInit();
            this.PreviewPanel.ResumeLayout(false);
            this.PreviewPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BasicTools.BasicControls.BasicNumericBox Address;
        private System.Windows.Forms.FlowLayoutPanel TypePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NewTypeLabel;
        private System.Windows.Forms.FlowLayoutPanel AddressPanel;
        private System.Windows.Forms.FlowLayoutPanel PreviewPanel;
        private System.Windows.Forms.Label OldTypeLabel;
        private BasicTools.BasicControls.BasicTextBox Offset;
    }
}
