using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Editor_BasicColorSelector : System.Windows.Forms.Form
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
            ShapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            PreviewBorderBefore = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            PreviwBorderAfter = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            PreviewSwatchAfter = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            PreviewSwatchBefore = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            R = new System.Windows.Forms.NumericUpDown();
            R.ValueChanged += new EventHandler(RGB_ValueChanged);
            R.KeyDown += new System.Windows.Forms.KeyEventHandler(NumericUpDown1_KeyDown);
            Label1 = new System.Windows.Forms.Label();
            Label2 = new System.Windows.Forms.Label();
            G = new System.Windows.Forms.NumericUpDown();
            G.ValueChanged += new EventHandler(RGB_ValueChanged);
            G.KeyDown += new System.Windows.Forms.KeyEventHandler(NumericUpDown1_KeyDown);
            Label3 = new System.Windows.Forms.Label();
            B = new System.Windows.Forms.NumericUpDown();
            B.ValueChanged += new EventHandler(RGB_ValueChanged);
            B.KeyDown += new System.Windows.Forms.KeyEventHandler(NumericUpDown1_KeyDown);
            ButtonOK = new System.Windows.Forms.Button();
            ButtonCancel = new System.Windows.Forms.Button();
            Label4 = new System.Windows.Forms.Label();
            ((ISupportInitialize)R).BeginInit();
            ((ISupportInitialize)G).BeginInit();
            ((ISupportInitialize)B).BeginInit();
            SuspendLayout();
            // 
            // ShapeContainer1
            // 
            ShapeContainer1.Location = new System.Drawing.Point(0, 0);
            ShapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            ShapeContainer1.Name = "ShapeContainer1";
            ShapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { PreviewBorderBefore, PreviwBorderAfter, PreviewSwatchAfter, PreviewSwatchBefore });
            ShapeContainer1.Size = new System.Drawing.Size(323, 183);
            ShapeContainer1.TabIndex = 0;
            ShapeContainer1.TabStop = false;
            // 
            // PreviewBorderBefore
            // 
            PreviewBorderBefore.Location = new System.Drawing.Point(13, 29);
            PreviewBorderBefore.Name = "PreviewBorderBefore";
            PreviewBorderBefore.Size = new System.Drawing.Size(37, 37);
            // 
            // PreviwBorderAfter
            // 
            PreviwBorderAfter.Location = new System.Drawing.Point(70, 29);
            PreviwBorderAfter.Name = "PreviwBorderAfter";
            PreviwBorderAfter.Size = new System.Drawing.Size(37, 37);
            // 
            // PreviewSwatchAfter
            // 
            PreviewSwatchAfter.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            PreviewSwatchAfter.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            PreviewSwatchAfter.Location = new System.Drawing.Point(73, 32);
            PreviewSwatchAfter.Name = "PreviewSwatchAfter";
            PreviewSwatchAfter.Size = new System.Drawing.Size(32, 32);
            // 
            // PreviewSwatchBefore
            // 
            PreviewSwatchBefore.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            PreviewSwatchBefore.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            PreviewSwatchBefore.Location = new System.Drawing.Point(16, 32);
            PreviewSwatchBefore.Name = "PreviewSwatchBefore";
            PreviewSwatchBefore.Size = new System.Drawing.Size(32, 32);
            // 
            // R
            // 
            R.Location = new System.Drawing.Point(228, 16);
            R.Margin = new System.Windows.Forms.Padding(4);
            R.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            R.Name = "R";
            R.Size = new System.Drawing.Size(71, 22);
            R.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new System.Drawing.Point(169, 20);
            Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new System.Drawing.Size(34, 17);
            Label1.TabIndex = 2;
            Label1.Text = "Red";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new System.Drawing.Point(169, 49);
            Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new System.Drawing.Size(48, 17);
            Label2.TabIndex = 4;
            Label2.Text = "Green";
            // 
            // G
            // 
            G.Location = new System.Drawing.Point(228, 46);
            G.Margin = new System.Windows.Forms.Padding(4);
            G.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            G.Name = "G";
            G.Size = new System.Drawing.Size(71, 22);
            G.TabIndex = 3;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new System.Drawing.Point(169, 79);
            Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label3.Name = "Label3";
            Label3.Size = new System.Drawing.Size(36, 17);
            Label3.TabIndex = 6;
            Label3.Text = "Blue";
            // 
            // B
            // 
            B.Location = new System.Drawing.Point(228, 75);
            B.Margin = new System.Windows.Forms.Padding(4);
            B.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            B.Name = "B";
            B.Size = new System.Drawing.Size(71, 22);
            B.TabIndex = 5;
            // 
            // ButtonOK
            // 
            ButtonOK.Location = new System.Drawing.Point(44, 114);
            ButtonOK.Margin = new System.Windows.Forms.Padding(4);
            ButtonOK.Name = "ButtonOK";
            ButtonOK.Size = new System.Drawing.Size(100, 28);
            ButtonOK.TabIndex = 7;
            ButtonOK.Text = "OK";
            ButtonOK.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            ButtonCancel.Location = new System.Drawing.Point(179, 114);
            ButtonCancel.Margin = new System.Windows.Forms.Padding(4);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new System.Drawing.Size(100, 28);
            ButtonCancel.TabIndex = 8;
            ButtonCancel.Text = "Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new System.Drawing.Point(71, 53);
            Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new System.Drawing.Size(21, 17);
            Label4.TabIndex = 9;
            Label4.Text = "->";
            // 
            // Editor_BasicColorSelector
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(323, 183);
            ControlBox = false;
            Controls.Add(Label4);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOK);
            Controls.Add(Label3);
            Controls.Add(B);
            Controls.Add(Label2);
            Controls.Add(G);
            Controls.Add(Label1);
            Controls.Add(R);
            Controls.Add(ShapeContainer1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "Editor_BasicColorSelector";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Change Color";
            ((ISupportInitialize)R).EndInit();
            ((ISupportInitialize)G).EndInit();
            ((ISupportInitialize)B).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        internal Microsoft.VisualBasic.PowerPacks.ShapeContainer ShapeContainer1;
        internal Microsoft.VisualBasic.PowerPacks.RectangleShape PreviewSwatchBefore;
        internal System.Windows.Forms.NumericUpDown R;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.NumericUpDown G;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.NumericUpDown B;
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Button ButtonCancel;
        internal Microsoft.VisualBasic.PowerPacks.RectangleShape PreviewSwatchAfter;
        internal System.Windows.Forms.Label Label4;
        internal Microsoft.VisualBasic.PowerPacks.RectangleShape PreviewBorderBefore;
        internal Microsoft.VisualBasic.PowerPacks.RectangleShape PreviwBorderAfter;
    }
}