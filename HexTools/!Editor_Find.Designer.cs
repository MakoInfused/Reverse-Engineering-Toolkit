using System;
using System.ComponentModel;
using System.Diagnostics;

namespace HexTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Editor_Find : HexForm
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
            LabelText = new System.Windows.Forms.Label();
            FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            TextBox = new HexTextBox();
            TextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            QuantityBox = new System.Windows.Forms.GroupBox();
            AllButton = new System.Windows.Forms.RadioButton();
            AllButton.CheckedChanged += new EventHandler(AllButton_CheckedChanged);
            OneButton = new System.Windows.Forms.RadioButton();
            OneButton.CheckedChanged += new EventHandler(OneButton_CheckedChanged);
            LocationBox = new System.Windows.Forms.GroupBox();
            TableButton = new System.Windows.Forms.RadioButton();
            TableButton.CheckedChanged += new EventHandler(TableButton_CheckedChanged);
            TextButton = new System.Windows.Forms.RadioButton();
            TextButton.CheckedChanged += new EventHandler(TextButton_CheckedChanged);
            HexButton = new System.Windows.Forms.RadioButton();
            HexButton.CheckedChanged += new EventHandler(HexButton_CheckedChanged);
            DirectionBox = new System.Windows.Forms.GroupBox();
            NextButton = new System.Windows.Forms.RadioButton();
            LastButton = new System.Windows.Forms.RadioButton();
            FirstButton = new System.Windows.Forms.RadioButton();
            FlowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            Cancel_Button = new System.Windows.Forms.Button();
            OK_Button = new System.Windows.Forms.Button();
            FlowLayoutPanel1.SuspendLayout();
            QuantityBox.SuspendLayout();
            LocationBox.SuspendLayout();
            DirectionBox.SuspendLayout();
            FlowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // LabelText
            // 
            LabelText.AutoSize = true;
            LabelText.Location = new System.Drawing.Point(3, 0);
            LabelText.Name = "LabelText";
            LabelText.Size = new System.Drawing.Size(39, 17);
            LabelText.TabIndex = 0;
            LabelText.Text = "Text:";
            // 
            // FlowLayoutPanel1
            // 
            FlowLayoutPanel1.Controls.Add(LabelText);
            FlowLayoutPanel1.Controls.Add(TextBox);
            FlowLayoutPanel1.Controls.Add(QuantityBox);
            FlowLayoutPanel1.Controls.Add(LocationBox);
            FlowLayoutPanel1.Controls.Add(DirectionBox);
            FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            FlowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            FlowLayoutPanel1.Size = new System.Drawing.Size(292, 141);
            FlowLayoutPanel1.TabIndex = 1;
            // 
            // TextBox
            // 
            TextBox.AllowEmpty = true;
            TextBox.HexOffset = "&H000000";
            TextBox.Location = new System.Drawing.Point(3, 20);
            TextBox.Multiline = true;
            TextBox.Name = "TextBox";
            TextBox.Size = new System.Drawing.Size(285, 22);
            TextBox.TabIndex = 1;
            TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // QuantityBox
            // 
            QuantityBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            QuantityBox.Controls.Add(AllButton);
            QuantityBox.Controls.Add(OneButton);
            QuantityBox.Location = new System.Drawing.Point(24, 48);
            QuantityBox.Name = "QuantityBox";
            QuantityBox.Size = new System.Drawing.Size(264, 23);
            QuantityBox.TabIndex = 2;
            QuantityBox.TabStop = false;
            QuantityBox.Text = "Type:";
            // 
            // AllButton
            // 
            AllButton.AutoSize = true;
            AllButton.Location = new System.Drawing.Point(121, 0);
            AllButton.Name = "AllButton";
            AllButton.Size = new System.Drawing.Size(44, 21);
            AllButton.TabIndex = 1;
            AllButton.Text = "All";
            AllButton.UseVisualStyleBackColor = true;
            // 
            // OneButton
            // 
            OneButton.AutoSize = true;
            OneButton.Checked = true;
            OneButton.Location = new System.Drawing.Point(61, 0);
            OneButton.Name = "OneButton";
            OneButton.Size = new System.Drawing.Size(56, 21);
            OneButton.TabIndex = 0;
            OneButton.TabStop = true;
            OneButton.Text = "One";
            OneButton.UseVisualStyleBackColor = true;
            // 
            // LocationBox
            // 
            LocationBox.Controls.Add(TableButton);
            LocationBox.Controls.Add(TextButton);
            LocationBox.Controls.Add(HexButton);
            LocationBox.Location = new System.Drawing.Point(3, 77);
            LocationBox.Name = "LocationBox";
            LocationBox.Size = new System.Drawing.Size(284, 24);
            LocationBox.TabIndex = 3;
            LocationBox.TabStop = false;
            LocationBox.Text = "Location:";
            // 
            // TableButton
            // 
            TableButton.AutoSize = true;
            TableButton.Location = new System.Drawing.Point(205, 0);
            TableButton.Name = "TableButton";
            TableButton.Size = new System.Drawing.Size(49, 21);
            TableButton.TabIndex = 2;
            TableButton.Text = "Tbl";
            TableButton.UseVisualStyleBackColor = true;
            // 
            // TextButton
            // 
            TextButton.AutoSize = true;
            TextButton.Location = new System.Drawing.Point(142, 0);
            TextButton.Name = "TextButton";
            TextButton.Size = new System.Drawing.Size(58, 21);
            TextButton.TabIndex = 1;
            TextButton.Text = "Ascii";
            TextButton.UseVisualStyleBackColor = true;
            // 
            // HexButton
            // 
            HexButton.AutoSize = true;
            HexButton.Checked = true;
            HexButton.Location = new System.Drawing.Point(82, 0);
            HexButton.Name = "HexButton";
            HexButton.Size = new System.Drawing.Size(53, 21);
            HexButton.TabIndex = 0;
            HexButton.TabStop = true;
            HexButton.Text = "Hex";
            HexButton.UseVisualStyleBackColor = true;
            // 
            // DirectionBox
            // 
            DirectionBox.Controls.Add(NextButton);
            DirectionBox.Controls.Add(LastButton);
            DirectionBox.Controls.Add(FirstButton);
            DirectionBox.Location = new System.Drawing.Point(3, 107);
            DirectionBox.Name = "DirectionBox";
            DirectionBox.Size = new System.Drawing.Size(284, 23);
            DirectionBox.TabIndex = 4;
            DirectionBox.TabStop = false;
            DirectionBox.Text = "Direction:";
            // 
            // NextButton
            // 
            NextButton.AutoSize = true;
            NextButton.Location = new System.Drawing.Point(142, 0);
            NextButton.Name = "NextButton";
            NextButton.Size = new System.Drawing.Size(57, 21);
            NextButton.TabIndex = 1;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            // 
            // LastButton
            // 
            LastButton.AutoSize = true;
            LastButton.Location = new System.Drawing.Point(205, 0);
            LastButton.Name = "LastButton";
            LastButton.Size = new System.Drawing.Size(56, 21);
            LastButton.TabIndex = 2;
            LastButton.Text = "Last";
            LastButton.UseVisualStyleBackColor = true;
            // 
            // FirstButton
            // 
            FirstButton.AutoSize = true;
            FirstButton.Checked = true;
            FirstButton.Location = new System.Drawing.Point(82, 0);
            FirstButton.Name = "FirstButton";
            FirstButton.Size = new System.Drawing.Size(56, 21);
            FirstButton.TabIndex = 0;
            FirstButton.TabStop = true;
            FirstButton.Text = "First";
            FirstButton.UseVisualStyleBackColor = true;
            // 
            // FlowLayoutPanel2
            // 
            FlowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            FlowLayoutPanel2.Controls.Add(Cancel_Button);
            FlowLayoutPanel2.Controls.Add(OK_Button);
            FlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            FlowLayoutPanel2.Location = new System.Drawing.Point(12, 159);
            FlowLayoutPanel2.Name = "FlowLayoutPanel2";
            FlowLayoutPanel2.Size = new System.Drawing.Size(292, 39);
            FlowLayoutPanel2.TabIndex = 2;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Cancel_Button.Location = new System.Drawing.Point(191, 5);
            Cancel_Button.Margin = new System.Windows.Forms.Padding(5);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new System.Drawing.Size(96, 35);
            Cancel_Button.TabIndex = 2;
            Cancel_Button.Text = "Cancel";
            // 
            // OK_Button
            // 
            OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            OK_Button.Enabled = false;
            OK_Button.Location = new System.Drawing.Point(85, 5);
            OK_Button.Margin = new System.Windows.Forms.Padding(5);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new System.Drawing.Size(96, 35);
            OK_Button.TabIndex = 1;
            OK_Button.Text = "OK";
            // 
            // Editor_Find
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = Cancel_Button;
            ClientSize = new System.Drawing.Size(316, 210);
            Controls.Add(FlowLayoutPanel2);
            Controls.Add(FlowLayoutPanel1);
            Name = "Editor_Find";
            Normal = new System.Drawing.Rectangle(19, 19, 334, 257);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Find";
            FlowLayoutPanel1.ResumeLayout(false);
            FlowLayoutPanel1.PerformLayout();
            QuantityBox.ResumeLayout(false);
            QuantityBox.PerformLayout();
            LocationBox.ResumeLayout(false);
            LocationBox.PerformLayout();
            DirectionBox.ResumeLayout(false);
            DirectionBox.PerformLayout();
            FlowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);

        }

        internal System.Windows.Forms.Label LabelText;
        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
        internal HexTextBox TextBox;
        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel2;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        internal System.Windows.Forms.GroupBox LocationBox;
        internal System.Windows.Forms.RadioButton HexButton;
        internal System.Windows.Forms.RadioButton TextButton;
        internal System.Windows.Forms.GroupBox DirectionBox;
        internal System.Windows.Forms.RadioButton FirstButton;
        internal System.Windows.Forms.RadioButton NextButton;
        internal System.Windows.Forms.RadioButton LastButton;
        internal System.Windows.Forms.RadioButton TableButton;
        internal System.Windows.Forms.GroupBox QuantityBox;
        internal System.Windows.Forms.RadioButton AllButton;
        internal System.Windows.Forms.RadioButton OneButton;
    }
}