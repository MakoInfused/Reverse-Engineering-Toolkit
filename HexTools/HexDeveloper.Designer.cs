using System;
using System.ComponentModel;
using System.Diagnostics;

namespace HexTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class HexDeveloper : System.Windows.Forms.UserControl
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
            ElementHost1 = new System.Windows.Forms.Integration.ElementHost();
            HexEditor1 = new WpfHexaEditor.HexEditor();
            SuspendLayout();
            // 
            // ElementHost1
            // 
            ElementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            ElementHost1.Location = new System.Drawing.Point(0, 0);
            ElementHost1.Name = "ElementHost1";
            ElementHost1.Size = new System.Drawing.Size(986, 595);
            ElementHost1.TabIndex = 0;
            ElementHost1.Text = "ElementHost1";
            ElementHost1.Child = HexEditor1;
            // 
            // HexDeveloper
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(ElementHost1);
            Name = "HexDeveloper";
            Size = new System.Drawing.Size(986, 595);
            SizeChanged += new EventHandler(HexDeveloper_SizeChanged);
            ResumeLayout(false);
        }

        internal System.Windows.Forms.Integration.ElementHost ElementHost1;
        internal WpfHexaEditor.HexEditor HexEditor1;
    }
}