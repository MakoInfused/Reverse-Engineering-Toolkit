using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BasicBitFlags : System.Windows.Forms.UserControl
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
            Layout = new System.Windows.Forms.TableLayoutPanel();
            BasicBitFlag5 = new BasicBitFlag();
            BasicBitFlag5.BitChange += new BitChanged(BasicBitFlag5_BitChange);
            BasicBitFlag5.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag4 = new BasicBitFlag();
            BasicBitFlag4.BitChange += new BitChanged(BasicBitFlag4_BitChange);
            BasicBitFlag4.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag3 = new BasicBitFlag();
            BasicBitFlag3.BitChange += new BitChanged(BasicBitFlag3_BitChange);
            BasicBitFlag3.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag2 = new BasicBitFlag();
            BasicBitFlag2.BitChange += new BitChanged(BasicBitFlag2_BitChange);
            BasicBitFlag2.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag1 = new BasicBitFlag();
            BasicBitFlag1.BitChange += new BitChanged(BasicBitFlag1_BitChange);
            BasicBitFlag1.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag6 = new BasicBitFlag();
            BasicBitFlag6.BitChange += new BitChanged(BasicBitFlag6_BitChange);
            BasicBitFlag6.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag7 = new BasicBitFlag();
            BasicBitFlag7.BitChange += new BitChanged(BasicBitFlag7_BitChange);
            BasicBitFlag7.ControlClick += new EventHandler(CheckBox_Click);
            BasicBitFlag8 = new BasicBitFlag();
            BasicBitFlag8.BitChange += new BitChanged(BasicBitFlag8_BitChange);
            BasicBitFlag8.ControlClick += new EventHandler(CheckBox_Click);
            Layout.SuspendLayout();
            SuspendLayout();
            // 
            // Layout
            // 
            Layout.AutoSize = true;
            Layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Layout.ColumnCount = 4;
            Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Layout.Controls.Add(BasicBitFlag5, 0, 1);
            Layout.Controls.Add(BasicBitFlag4, 3, 0);
            Layout.Controls.Add(BasicBitFlag3, 2, 0);
            Layout.Controls.Add(BasicBitFlag2, 1, 0);
            Layout.Controls.Add(BasicBitFlag1, 0, 0);
            Layout.Controls.Add(BasicBitFlag6, 1, 1);
            Layout.Controls.Add(BasicBitFlag7, 2, 1);
            Layout.Controls.Add(BasicBitFlag8, 3, 1);
            Layout.Dock = System.Windows.Forms.DockStyle.Fill;
            Layout.Location = new System.Drawing.Point(0, 0);
            Layout.Name = "Layout";
            Layout.RowCount = 2;
            Layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Layout.Size = new System.Drawing.Size(348, 54);
            Layout.TabIndex = 6;
            // 
            // BasicBitFlag5
            // 
            BasicBitFlag5.AutoSize = true;
            BasicBitFlag5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag5.Label = "BitFlag5";
            BasicBitFlag5.Location = new System.Drawing.Point(0, 27);
            BasicBitFlag5.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag5.Name = "BasicBitFlag5";
            BasicBitFlag5.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag5.TabIndex = 4;
            BasicBitFlag5.Value = false;
            // 
            // BasicBitFlag4
            // 
            BasicBitFlag4.AutoSize = true;
            BasicBitFlag4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag4.Label = "BitFlag4";
            BasicBitFlag4.Location = new System.Drawing.Point(261, 0);
            BasicBitFlag4.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag4.Name = "BasicBitFlag4";
            BasicBitFlag4.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag4.TabIndex = 3;
            BasicBitFlag4.Value = false;
            // 
            // BasicBitFlag3
            // 
            BasicBitFlag3.AutoSize = true;
            BasicBitFlag3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag3.Label = "BitFlag3";
            BasicBitFlag3.Location = new System.Drawing.Point(174, 0);
            BasicBitFlag3.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag3.Name = "BasicBitFlag3";
            BasicBitFlag3.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag3.TabIndex = 2;
            BasicBitFlag3.Value = false;
            // 
            // BasicBitFlag2
            // 
            BasicBitFlag2.AutoSize = true;
            BasicBitFlag2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag2.Label = "BitFlag2";
            BasicBitFlag2.Location = new System.Drawing.Point(87, 0);
            BasicBitFlag2.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag2.Name = "BasicBitFlag2";
            BasicBitFlag2.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag2.TabIndex = 1;
            BasicBitFlag2.Value = false;
            // 
            // BasicBitFlag1
            // 
            BasicBitFlag1.AutoSize = true;
            BasicBitFlag1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag1.Label = "BitFlag1";
            BasicBitFlag1.Location = new System.Drawing.Point(0, 0);
            BasicBitFlag1.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag1.Name = "BasicBitFlag1";
            BasicBitFlag1.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag1.TabIndex = 0;
            BasicBitFlag1.Value = false;
            // 
            // BasicBitFlag6
            // 
            BasicBitFlag6.AutoSize = true;
            BasicBitFlag6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag6.Label = "BitFlag6";
            BasicBitFlag6.Location = new System.Drawing.Point(87, 27);
            BasicBitFlag6.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag6.Name = "BasicBitFlag6";
            BasicBitFlag6.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag6.TabIndex = 5;
            BasicBitFlag6.Value = false;
            // 
            // BasicBitFlag7
            // 
            BasicBitFlag7.AutoSize = true;
            BasicBitFlag7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag7.Label = "BitFlag7";
            BasicBitFlag7.Location = new System.Drawing.Point(174, 27);
            BasicBitFlag7.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag7.Name = "BasicBitFlag7";
            BasicBitFlag7.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag7.TabIndex = 6;
            BasicBitFlag7.Value = false;
            // 
            // BasicBitFlag8
            // 
            BasicBitFlag8.AutoSize = true;
            BasicBitFlag8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BasicBitFlag8.Label = "BitFlag8";
            BasicBitFlag8.Location = new System.Drawing.Point(261, 27);
            BasicBitFlag8.Margin = new System.Windows.Forms.Padding(0);
            BasicBitFlag8.Name = "BasicBitFlag8";
            BasicBitFlag8.Size = new System.Drawing.Size(87, 27);
            BasicBitFlag8.TabIndex = 7;
            BasicBitFlag8.Value = false;
            // 
            // BasicBitFlags
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(Layout);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "BasicBitFlags";
            Size = new System.Drawing.Size(348, 54);
            Layout.ResumeLayout(false);
            Layout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        internal new System.Windows.Forms.TableLayoutPanel Layout;
        internal BasicBitFlag BasicBitFlag5;
        internal BasicBitFlag BasicBitFlag4;
        internal BasicBitFlag BasicBitFlag3;
        internal BasicBitFlag BasicBitFlag2;
        internal BasicBitFlag BasicBitFlag1;
        internal BasicBitFlag BasicBitFlag6;
        internal BasicBitFlag BasicBitFlag7;
        internal BasicBitFlag BasicBitFlag8;
    }
}