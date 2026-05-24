using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BasicBitFlag : System.Windows.Forms.UserControl
    {

        // UserControl overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            Layout = new System.Windows.Forms.FlowLayoutPanel();
            CheckBox = new System.Windows.Forms.CheckBox();
            CheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            CheckBox.Click += new EventHandler(CheckBox_Click);
            Layout.SuspendLayout();
            SuspendLayout();
            // 
            // Layout
            // 
            Layout.AutoSize = true;
            Layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Layout.Controls.Add(CheckBox);
            Layout.Dock = System.Windows.Forms.DockStyle.Fill;
            Layout.Location = new System.Drawing.Point(0, 0);
            Layout.Name = "Layout";
            Layout.Size = new System.Drawing.Size(87, 27);
            Layout.TabIndex = 0;
            // 
            // CheckBox
            // 
            CheckBox.AutoSize = true;
            CheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            CheckBox.Location = new System.Drawing.Point(3, 3);
            CheckBox.Name = "CheckBox";
            CheckBox.Size = new System.Drawing.Size(81, 21);
            CheckBox.TabIndex = 0;
            CheckBox.Text = "BitFlag1";
            CheckBox.UseVisualStyleBackColor = true;
            // 
            // BasicBitFlag
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(Layout);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "BasicBitFlag";
            Size = new System.Drawing.Size(87, 27);
            Layout.ResumeLayout(false);
            Layout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        internal new System.Windows.Forms.FlowLayoutPanel Layout;
        internal System.Windows.Forms.CheckBox CheckBox;
    }
}