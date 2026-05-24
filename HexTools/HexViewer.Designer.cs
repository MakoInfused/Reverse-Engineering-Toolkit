using System.ComponentModel;
using System.Diagnostics;

namespace HexTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class HexViewer : System.Windows.Forms.UserControl
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
            HexByteViewer = new HexByteViewer();
            SuspendLayout();
            // 
            // HexByteViewer
            // 
            HexByteViewer.AutoScroll = true;
            HexByteViewer.Location = new System.Drawing.Point(0, 0);
            HexByteViewer.Name = "HexByteViewer";
            HexByteViewer.Size = new System.Drawing.Size(1067, 662);
            HexByteViewer.TabIndex = 0;
            // 
            // HexViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMargin = new System.Drawing.Size(20, 20);
            Controls.Add(HexByteViewer);
            Name = "HexViewer";
            Size = new System.Drawing.Size(755, 588);
            ResumeLayout(false);

        }

        internal HexByteViewer HexByteViewer;
    }
}