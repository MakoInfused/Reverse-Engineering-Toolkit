using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Editor_BasicColorCreator : System.Windows.Forms.Form
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
            ButtonCancel = new System.Windows.Forms.Button();
            ButtonOK = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // ButtonCancel
            // 
            ButtonCancel.Location = new System.Drawing.Point(156, 227);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new System.Drawing.Size(75, 23);
            ButtonCancel.TabIndex = 10;
            ButtonCancel.Text = "Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonOK
            // 
            ButtonOK.Location = new System.Drawing.Point(55, 227);
            ButtonOK.Name = "ButtonOK";
            ButtonOK.Size = new System.Drawing.Size(75, 23);
            ButtonOK.TabIndex = 9;
            ButtonOK.Text = "OK";
            ButtonOK.UseVisualStyleBackColor = true;
            // 
            // Editor_BasicColorCreator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6.0f, 13.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(284, 262);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "Editor_BasicColorCreator";
            Text = "Color Creator";
            Paint += new System.Windows.Forms.PaintEventHandler(Form1_Paint);
            ResumeLayout(false);

        }
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.Button ButtonOK;
    }
}