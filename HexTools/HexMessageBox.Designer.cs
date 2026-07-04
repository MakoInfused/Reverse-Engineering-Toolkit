using System;
using System.ComponentModel;
using System.Diagnostics;

namespace HexTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class HexMessageBox : HexUserControl
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
            this.components = new System.ComponentModel.Container();
            HexTools.HexStringRow hexStringRow1 = new HexTools.HexStringRow();
            this.MessageBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PageControlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonAdd = new BasicTools.BasicControls.BasicButton();
            this.ButtonRemove = new BasicTools.BasicControls.BasicButton();
            this.PagingPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LabelPages = new System.Windows.Forms.Label();
            this.ButtonLast = new BasicTools.BasicControls.BasicButton();
            this.ButtonNext = new BasicTools.BasicControls.BasicButton();
            this.HexPanel1 = new HexTools.HexPanel();
            this.ToolPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonQuote = new BasicTools.BasicControls.BasicCheckBox();
            this.ButtonClose = new BasicTools.BasicControls.BasicButton();
            this.PortraitPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Portrait = new HexTools.HexComboBox();
            this.LabelPortrait = new System.Windows.Forms.Label();
            this._Text = new HexTools.HexTextBox();
            this.PageControlPanel.SuspendLayout();
            this.PagingPanel.SuspendLayout();
            this.HexPanel1.SuspendLayout();
            this.ToolPanel.SuspendLayout();
            this.PortraitPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PageControlPanel
            // 
            this.PageControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PageControlPanel.ColumnCount = 2;
            this.PageControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PageControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PageControlPanel.Controls.Add(this.ButtonAdd, 0, 0);
            this.PageControlPanel.Controls.Add(this.ButtonRemove, 1, 0);
            this.PageControlPanel.Location = new System.Drawing.Point(376, 247);
            this.PageControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.PageControlPanel.Name = "PageControlPanel";
            this.PageControlPanel.RowCount = 1;
            this.PageControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PageControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PageControlPanel.Size = new System.Drawing.Size(60, 28);
            this.PageControlPanel.TabIndex = 42;
            this.PageControlPanel.Visible = false;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonAdd.Location = new System.Drawing.Point(0, 6);
            this.ButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(30, 22);
            this.ButtonAdd.TabIndex = 0;
            this.ButtonAdd.Text = " +";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            // 
            // ButtonRemove
            // 
            this.ButtonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRemove.Location = new System.Drawing.Point(30, 6);
            this.ButtonRemove.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.Size = new System.Drawing.Size(30, 22);
            this.ButtonRemove.TabIndex = 1;
            this.ButtonRemove.Text = " -";
            this.ButtonRemove.UseVisualStyleBackColor = true;
            // 
            // PagingPanel
            // 
            this.PagingPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PagingPanel.ColumnCount = 3;
            this.PagingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PagingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PagingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PagingPanel.Controls.Add(this.LabelPages, 1, 0);
            this.PagingPanel.Controls.Add(this.ButtonLast, 0, 0);
            this.PagingPanel.Controls.Add(this.ButtonNext, 2, 0);
            this.PagingPanel.Location = new System.Drawing.Point(144, 244);
            this.PagingPanel.Name = "PagingPanel";
            this.PagingPanel.RowCount = 1;
            this.PagingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PagingPanel.Size = new System.Drawing.Size(150, 28);
            this.PagingPanel.TabIndex = 41;
            this.PagingPanel.Visible = false;
            // 
            // LabelPages
            // 
            this.LabelPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPages.AutoSize = true;
            this.LabelPages.Location = new System.Drawing.Point(54, 3);
            this.LabelPages.Name = "LabelPages";
            this.LabelPages.Size = new System.Drawing.Size(42, 25);
            this.LabelPages.TabIndex = 43;
            this.LabelPages.Text = "0/0";
            this.LabelPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonLast
            // 
            this.ButtonLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLast.Location = new System.Drawing.Point(3, 3);
            this.ButtonLast.Name = "ButtonLast";
            this.ButtonLast.Size = new System.Drawing.Size(45, 22);
            this.ButtonLast.TabIndex = 0;
            this.ButtonLast.Text = "<--";
            this.ButtonLast.UseVisualStyleBackColor = true;
            this.ButtonLast.Click += new System.EventHandler(this.ButtonLast_Click);
            // 
            // ButtonNext
            // 
            this.ButtonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonNext.Location = new System.Drawing.Point(102, 3);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(45, 22);
            this.ButtonNext.TabIndex = 1;
            this.ButtonNext.Text = "-->";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // HexPanel1
            // 
            this.HexPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPanel1.Controls.Add(this.ToolPanel);
            this.HexPanel1.Controls.Add(this.PortraitPanel);
            this.HexPanel1.Controls.Add(this._Text);
            this.HexPanel1.Definition = "TextEditor.Dialogues";
            this.HexPanel1.Location = new System.Drawing.Point(5, 5);
            this.HexPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.HexPanel1.Name = "HexPanel1";
            this.HexPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.HexPanel1.Size = new System.Drawing.Size(439, 240);
            this.HexPanel1.TabIndex = 38;
            // 
            // ToolPanel
            // 
            this.ToolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToolPanel.Controls.Add(this.ButtonQuote);
            this.ToolPanel.Controls.Add(this.ButtonClose);
            this.ToolPanel.Location = new System.Drawing.Point(8, 40);
            this.ToolPanel.Name = "ToolPanel";
            this.ToolPanel.Size = new System.Drawing.Size(423, 29);
            this.ToolPanel.TabIndex = 43;
            this.ToolPanel.Visible = false;
            // 
            // ButtonQuote
            // 
            this.ButtonQuote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonQuote.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonQuote.AutoSize = true;
            this.ButtonQuote.Location = new System.Drawing.Point(3, 3);
            this.ButtonQuote.Name = "ButtonQuote";
            this.ButtonQuote.Size = new System.Drawing.Size(80, 35);
            this.ButtonQuote.TabIndex = 0;
            this.ButtonQuote.Text = "Quote";
            this.ButtonQuote.UseVisualStyleBackColor = true;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.AutoSize = true;
            this.ButtonClose.Location = new System.Drawing.Point(89, 3);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(159, 35);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "Close Window";
            this.ButtonClose.UseVisualStyleBackColor = true;
            // 
            // PortraitPanel
            // 
            this.PortraitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PortraitPanel.ColumnCount = 2;
            this.PortraitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PortraitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PortraitPanel.Controls.Add(this.Portrait, 1, 0);
            this.PortraitPanel.Controls.Add(this.LabelPortrait, 0, 0);
            this.PortraitPanel.Location = new System.Drawing.Point(8, 8);
            this.PortraitPanel.Name = "PortraitPanel";
            this.PortraitPanel.RowCount = 1;
            this.PortraitPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PortraitPanel.Size = new System.Drawing.Size(423, 30);
            this.PortraitPanel.TabIndex = 42;
            // 
            // Portrait
            // 
            this.Portrait.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Portrait.Display = HexTools.HexEnumerations.DisplayType.Hex;
            this.Portrait.DisplayMember = "Text";
            this.Portrait.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Portrait.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Portrait.FormatString = "X2";
            this.Portrait.FormattingEnabled = true;
            this.Portrait.Location = new System.Drawing.Point(96, 3);
            this.Portrait.Margin = new System.Windows.Forms.Padding(3, 3, 250, 3);
            this.Portrait.MaxDropDownItems = 15;
            this.Portrait.MaxLength = 2;
            this.Portrait.Name = "Portrait";
            this.Portrait.Size = new System.Drawing.Size(77, 32);
            this.Portrait.StartIndex = "&H00F400";
            this.Portrait.TabIndex = 1;
            this.Portrait.ValueDisplay = true;
            this.Portrait.ValueMember = "Value";
            this.Portrait.Visible = false;
            // 
            // LabelPortrait
            // 
            this.LabelPortrait.AutoSize = true;
            this.LabelPortrait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelPortrait.Location = new System.Drawing.Point(3, 0);
            this.LabelPortrait.Name = "LabelPortrait";
            this.LabelPortrait.Size = new System.Drawing.Size(87, 30);
            this.LabelPortrait.TabIndex = 0;
            this.LabelPortrait.Text = "Portrait:";
            this.LabelPortrait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelPortrait.Visible = false;
            // 
            // _Text
            // 
            this._Text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Text.ClosingTag = "FFFF";
            this._Text.ClosingTagIsEnd = true;
            this._Text.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Text.FontTableUsesGlobal = true;
            this._Text.Location = new System.Drawing.Point(0, 71);
            this._Text.Margin = new System.Windows.Forms.Padding(5);
            this._Text.MaxLengthLabel = true;
            this._Text.Multiline = true;
            this._Text.Name = "_Text";
            this._Text.Size = new System.Drawing.Size(429, 168);
            hexStringRow1.Columns = new string[] {
        "0x00"};
            this._Text.SkipCharacters = hexStringRow1;
            this._Text.TabIndex = 18;
            this._Text.TextChanged += new System.EventHandler(this._Text_TextChanged);
            // 
            // HexMessageBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.PageControlPanel);
            this.Controls.Add(this.PagingPanel);
            this.Controls.Add(this.HexPanel1);
            this.MinimumSize = new System.Drawing.Size(250, 125);
            this.Name = "HexMessageBox";
            this.Size = new System.Drawing.Size(445, 275);
            this.PageControlPanel.ResumeLayout(false);
            this.PagingPanel.ResumeLayout(false);
            this.PagingPanel.PerformLayout();
            this.HexPanel1.ResumeLayout(false);
            this.HexPanel1.PerformLayout();
            this.ToolPanel.ResumeLayout(false);
            this.ToolPanel.PerformLayout();
            this.PortraitPanel.ResumeLayout(false);
            this.PortraitPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        internal HexPanel HexPanel1;
        internal HexTextBox _Text;
        internal BasicTools.BasicControls.BasicButton ButtonLast;
        internal BasicTools.BasicControls.BasicButton ButtonNext;
        internal System.Windows.Forms.Label LabelPortrait;
        internal HexComboBox Portrait;
        internal System.Windows.Forms.TableLayoutPanel PagingPanel;
        internal System.Windows.Forms.TableLayoutPanel PortraitPanel;
        internal System.Windows.Forms.FlowLayoutPanel ToolPanel;
        internal BasicTools.BasicControls.BasicCheckBox ButtonQuote;
        internal BasicTools.BasicControls.BasicButton ButtonClose;
        internal System.Windows.Forms.ToolTip MessageBoxToolTip;
        internal System.Windows.Forms.TableLayoutPanel PageControlPanel;
        internal BasicTools.BasicControls.BasicButton ButtonAdd;
        internal BasicTools.BasicControls.BasicButton ButtonRemove;
        internal System.Windows.Forms.Label LabelPages;
    }
}