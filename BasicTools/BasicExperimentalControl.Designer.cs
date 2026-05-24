using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BasicExperimentalControl : System.Windows.Forms.UserControl
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
            components = new Container();
            LayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            CheckBox = new System.Windows.Forms.CheckBox();
            CheckBox.Click += new EventHandler(CheckBox_CheckedChanged);
            ToolTipWarning = new System.Windows.Forms.ToolTip(components);
            LayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // LayoutPanel
            // 
            LayoutPanel.AutoSize = true;
            LayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            LayoutPanel.Controls.Add(CheckBox);
            LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            LayoutPanel.Location = new System.Drawing.Point(0, 0);
            LayoutPanel.Name = "LayoutPanel";
            LayoutPanel.Size = new System.Drawing.Size(155, 33);
            LayoutPanel.TabIndex = 0;
            // 
            // CheckBox
            // 
            CheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            CheckBox.AutoSize = true;
            CheckBox.Location = new System.Drawing.Point(3, 3);
            CheckBox.Name = "CheckBox";
            CheckBox.Size = new System.Drawing.Size(147, 27);
            CheckBox.TabIndex = 0;
            CheckBox.Text = "Enable Experimental";
            CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            ToolTipWarning.SetToolTip(CheckBox, "The associated control is considered experimental and could cause unintend conseq" + "uenes, proceed with caution.");
            CheckBox.UseVisualStyleBackColor = true;
            // 
            // BasicExperimentalControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(LayoutPanel);
            Name = "BasicExperimentalControl";
            Size = new System.Drawing.Size(155, 33);
            LayoutPanel.ResumeLayout(false);
            LayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        internal System.Windows.Forms.FlowLayoutPanel LayoutPanel;
        internal System.Windows.Forms.CheckBox CheckBox;
        internal System.Windows.Forms.ToolTip ToolTipWarning;
    }
}