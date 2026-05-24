using System;
using System.ComponentModel;
using System.Diagnostics;

namespace HexTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Editor_GoTo : HexForm
    {

        // Form overrides dispose to clean up the component list.
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
            this.components = new System.ComponentModel.Container();
            this.Label1 = new System.Windows.Forms.Label();
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.HexBox = new HexTools.HexTextBox();
            this.FlowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.FlowLayoutPanel1.SuspendLayout();
            this.FlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 17);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Offset:";
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.Controls.Add(this.Label1);
            this.FlowLayoutPanel1.Controls.Add(this.HexBox);
            this.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(292, 45);
            this.FlowLayoutPanel1.TabIndex = 1;
            // 
            // HexBox
            // 
            this.HexBox.Display = HexTools.HexEnumerations.DisplayType.Hex;
            this.HexBox.Input = HexTools.HexEnumerations.InputType.Hex;
            this.HexBox.Location = new System.Drawing.Point(3, 20);
            this.HexBox.Name = "HexBox";
            this.HexBox.OvertypeMode = true;
            this.HexBox.Size = new System.Drawing.Size(285, 22);
            this.HexBox.TabIndex = 1;
            this.HexBox.Text = "000000";
            this.HexBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FlowLayoutPanel2
            // 
            this.FlowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanel2.Controls.Add(this.Cancel_Button);
            this.FlowLayoutPanel2.Controls.Add(this.OK_Button);
            this.FlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FlowLayoutPanel2.Location = new System.Drawing.Point(18, 77);
            this.FlowLayoutPanel2.Name = "FlowLayoutPanel2";
            this.FlowLayoutPanel2.Size = new System.Drawing.Size(286, 39);
            this.FlowLayoutPanel2.TabIndex = 2;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(185, 5);
            this.Cancel_Button.Margin = new System.Windows.Forms.Padding(5);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(96, 35);
            this.Cancel_Button.TabIndex = 2;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK_Button.Location = new System.Drawing.Point(79, 5);
            this.OK_Button.Margin = new System.Windows.Forms.Padding(5);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(96, 35);
            this.OK_Button.TabIndex = 1;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Editor_GoTo
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(316, 128);
            this.Controls.Add(this.FlowLayoutPanel2);
            this.Controls.Add(this.FlowLayoutPanel1);
            this.Name = "Editor_GoTo";
            this.Normal = new System.Drawing.Rectangle(19, 19, 334, 175);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go To";
            this.FlowLayoutPanel1.ResumeLayout(false);
            this.FlowLayoutPanel1.PerformLayout();
            this.FlowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
        internal HexTextBox HexBox;
        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel2;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
    }
}