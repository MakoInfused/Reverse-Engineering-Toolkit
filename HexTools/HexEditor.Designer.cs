using System.ComponentModel;
using System.Diagnostics;

namespace HexTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class HexEditor : System.Windows.Forms.UserControl
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
            BasicTableLayoutPanel1 = new BasicTools.BasicControls.BasicTableLayoutPanel();
            VScrollBar1 = new System.Windows.Forms.VScrollBar();
            VScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(VScrollBar1_Scroll);
            Editor = new HexTextBox();
            BasicTableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // BasicTableLayoutPanel1
            // 
            BasicTableLayoutPanel1.AutoSize = true;
            BasicTableLayoutPanel1.Buffer = BasicTools.BasicControls.ControlBuffer.DoubleBuffered;
            BasicTableLayoutPanel1.ColumnCount = 2;
            BasicTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0f));
            BasicTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0f));
            BasicTableLayoutPanel1.Controls.Add(Editor, 0, 0);
            BasicTableLayoutPanel1.Controls.Add(VScrollBar1, 1, 0);
            BasicTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            BasicTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            BasicTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            BasicTableLayoutPanel1.Name = "BasicTableLayoutPanel1";
            BasicTableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            BasicTableLayoutPanel1.RowCount = 1;
            BasicTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0f));
            BasicTableLayoutPanel1.Size = new System.Drawing.Size(560, 456);
            BasicTableLayoutPanel1.TabIndex = 2;
            // 
            // VScrollBar1
            // 
            VScrollBar1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            VScrollBar1.LargeChange = 300;
            VScrollBar1.Location = new System.Drawing.Point(535, 5);
            VScrollBar1.Maximum = 300;
            VScrollBar1.MinimumSize = new System.Drawing.Size(20, 334);
            VScrollBar1.Name = "VScrollBar1";
            VScrollBar1.Padding = new System.Windows.Forms.Padding(20);
            VScrollBar1.Size = new System.Drawing.Size(20, 446);
            VScrollBar1.SmallChange = 15;
            VScrollBar1.TabIndex = 2;
            // 
            // Editor
            // 
            Editor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            Editor.BackColor = System.Drawing.SystemColors.Control;
            Editor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Editor.Display = HexEnumerations.DisplayType.Hex;
            Editor.HexOffset = "&H000000";
            Editor.Input = HexEnumerations.InputType.Hex;
            Editor.Location = new System.Drawing.Point(8, 8);
            Editor.MaxLength = 1;
            Editor.Multiline = true;
            Editor.Name = "Editor";
            Editor.OvertypeMode = true;
            Editor.Size = new System.Drawing.Size(524, 440);
            Editor.Spacing = new System.Drawing.SizeF(10.0f, 10.0f);
            Editor.SpacingGroups = new System.Drawing.Size(2, 1);
            Editor.TabIndex = 32;
            Editor.Text = "0000";
            Editor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HexEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            BackColor = System.Drawing.SystemColors.Window;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(BasicTableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(20);
            Name = "HexEditor";
            Size = new System.Drawing.Size(560, 456);
            BasicTableLayoutPanel1.ResumeLayout(false);
            BasicTableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        internal BasicTools.BasicControls.BasicTableLayoutPanel BasicTableLayoutPanel1;
        internal System.Windows.Forms.VScrollBar VScrollBar1;
        internal HexTextBox Editor;
    }
}