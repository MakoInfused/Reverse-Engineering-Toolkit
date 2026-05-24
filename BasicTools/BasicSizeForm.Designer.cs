namespace BasicTools
{
    partial class BasicSizeForm
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
            this.MaxSizeLabel = new System.Windows.Forms.Label();
            this.ControlPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MaxSize = new BasicTools.BasicControls.BasicNumericBox();
            this.ControlPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSize)).BeginInit();
            this.SuspendLayout();
            // 
            // MaxSizeLabel
            // 
            this.MaxSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxSizeLabel.Location = new System.Drawing.Point(13, 4);
            this.MaxSizeLabel.Name = "MaxSizeLabel";
            this.MaxSizeLabel.Size = new System.Drawing.Size(92, 28);
            this.MaxSizeLabel.TabIndex = 1;
            this.MaxSizeLabel.Text = "Max Size:";
            this.MaxSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ControlPanel
            // 
            this.ControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlPanel.Controls.Add(this.OK_Button);
            this.ControlPanel.Controls.Add(this.Cancel_Button);
            this.ControlPanel.Location = new System.Drawing.Point(38, 51);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(219, 45);
            this.ControlPanel.TabIndex = 2;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(5, 5);
            this.OK_Button.Margin = new System.Windows.Forms.Padding(5);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(96, 35);
            this.OK_Button.TabIndex = 1;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(111, 5);
            this.Cancel_Button.Margin = new System.Windows.Forms.Padding(5);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(96, 35);
            this.Cancel_Button.TabIndex = 2;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.MaxSize);
            this.panel1.Controls.Add(this.MaxSizeLabel);
            this.panel1.Location = new System.Drawing.Point(30, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 41);
            this.panel1.TabIndex = 2;
            // 
            // MaxSize
            // 
            this.MaxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxSize.Location = new System.Drawing.Point(111, 8);
            this.MaxSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.MaxSize.MaxLength = 1;
            this.MaxSize.Name = "MaxSize";
            this.MaxSize.Size = new System.Drawing.Size(78, 22);
            this.MaxSize.TabIndex = 0;
            this.MaxSize.ThousandsSeparator = true;
            // 
            // BasicSizeForm
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(269, 102);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicSizeForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resize";
            this.ControlPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaxSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BasicControls.BasicNumericBox MaxSize;
        private System.Windows.Forms.Label MaxSizeLabel;
        internal System.Windows.Forms.FlowLayoutPanel ControlPanel;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Panel panel1;
    }
}