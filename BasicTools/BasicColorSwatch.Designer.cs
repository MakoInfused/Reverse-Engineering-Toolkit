using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BasicColorSwatch : System.Windows.Forms.UserControl
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
            Shapes = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            Shapes.Click += new EventHandler(Me_Click);
            Border = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            Swatch = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            Raw = new System.Windows.Forms.Label();
            Raw.Click += new EventHandler(Me_Click);
            SuspendLayout();
            // 
            // Shapes
            // 
            Shapes.Location = new System.Drawing.Point(0, 0);
            Shapes.Margin = new System.Windows.Forms.Padding(0);
            Shapes.Name = "Shapes";
            Shapes.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { Border, Swatch });
            Shapes.Size = new System.Drawing.Size(32, 30);
            Shapes.TabIndex = 0;
            Shapes.TabStop = false;
            // 
            // Border
            // 
            Border.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            Border.CornerRadius = 2;
            Border.Location = new System.Drawing.Point(0, 0);
            Border.Name = "Border";
            Border.Size = new System.Drawing.Size(31, 29);
            // 
            // Swatch
            // 
            Swatch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            Swatch.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            Swatch.CornerRadius = 2;
            Swatch.FillColor = System.Drawing.Color.Black;
            Swatch.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            Swatch.Location = new System.Drawing.Point(2, 2);
            Swatch.Name = "Swatch";
            Swatch.Size = new System.Drawing.Size(28, 26);
            // 
            // Raw
            // 
            Raw.AutoSize = true;
            Raw.Location = new System.Drawing.Point(7, 7);
            Raw.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Raw.Name = "Raw";
            Raw.Size = new System.Drawing.Size(16, 17);
            Raw.TabIndex = 1;
            Raw.Text = "#";
            Raw.Visible = false;
            // 
            // BasicColorSwatch
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(Raw);
            Controls.Add(Shapes);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "BasicColorSwatch";
            Size = new System.Drawing.Size(32, 30);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Microsoft.VisualBasic.PowerPacks.ShapeContainer Shapes;
        internal Microsoft.VisualBasic.PowerPacks.RectangleShape Border;
        internal Microsoft.VisualBasic.PowerPacks.RectangleShape Swatch;
        internal System.Windows.Forms.Label Raw;

    }
}