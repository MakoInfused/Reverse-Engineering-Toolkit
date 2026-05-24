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
            components = new Container();
            MessageBoxToolTip = new System.Windows.Forms.ToolTip(components);
            PageControlPanel = new System.Windows.Forms.TableLayoutPanel();
            ButtonAdd = new BasicTools.BasicControls.BasicButton();
            ButtonRemove = new BasicTools.BasicControls.BasicButton();
            PagingPanel = new System.Windows.Forms.TableLayoutPanel();
            LabelPages = new System.Windows.Forms.Label();
            ButtonLast = new BasicTools.BasicControls.BasicButton();
            ButtonLast.Click += new EventHandler(ButtonLast_Click);
            ButtonNext = new BasicTools.BasicControls.BasicButton();
            ButtonNext.Click += new EventHandler(ButtonNext_Click);
            HexPanel1 = new HexPanel();
            ToolPanel = new System.Windows.Forms.FlowLayoutPanel();
            ButtonQuote = new BasicTools.BasicControls.BasicCheckBox();
            ButtonClose = new BasicTools.BasicControls.BasicButton();
            PortraitPanel = new System.Windows.Forms.TableLayoutPanel();
            Portrait = new HexComboBox();
            LabelPortrait = new System.Windows.Forms.Label();
            _Text = new HexTextBox();
            _Text.TextChanged += new EventHandler(_Text_TextChanged);
            PageControlPanel.SuspendLayout();
            PagingPanel.SuspendLayout();
            HexPanel1.SuspendLayout();
            ToolPanel.SuspendLayout();
            PortraitPanel.SuspendLayout();
            SuspendLayout();
            // 
            // PageControlPanel
            // 
            PageControlPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            PageControlPanel.ColumnCount = 2;
            PageControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            PageControlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            PageControlPanel.Controls.Add(ButtonAdd, 0, 0);
            PageControlPanel.Controls.Add(ButtonRemove, 1, 0);
            PageControlPanel.Location = new System.Drawing.Point(376, 247);
            PageControlPanel.Margin = new System.Windows.Forms.Padding(0);
            PageControlPanel.Name = "PageControlPanel";
            PageControlPanel.RowCount = 1;
            PageControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            PageControlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0f));
            PageControlPanel.Size = new System.Drawing.Size(60, 28);
            PageControlPanel.TabIndex = 42;
            PageControlPanel.Visible = false;
            // 
            // ButtonAdd
            // 
            ButtonAdd.Location = new System.Drawing.Point(0, 0);
            ButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new System.Drawing.Size(30, 22);
            ButtonAdd.TabIndex = 0;
            ButtonAdd.Text = " +";
            ButtonAdd.UseVisualStyleBackColor = true;
            // 
            // ButtonRemove
            // 
            ButtonRemove.Location = new System.Drawing.Point(30, 0);
            ButtonRemove.Margin = new System.Windows.Forms.Padding(0);
            ButtonRemove.Name = "ButtonRemove";
            ButtonRemove.Size = new System.Drawing.Size(30, 22);
            ButtonRemove.TabIndex = 1;
            ButtonRemove.Text = " -";
            ButtonRemove.UseVisualStyleBackColor = true;
            // 
            // PagingPanel
            // 
            PagingPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            PagingPanel.ColumnCount = 3;
            PagingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            PagingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            PagingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            PagingPanel.Controls.Add(LabelPages, 1, 0);
            PagingPanel.Controls.Add(ButtonLast, 0, 0);
            PagingPanel.Controls.Add(ButtonNext, 2, 0);
            PagingPanel.Location = new System.Drawing.Point(144, 244);
            PagingPanel.Name = "PagingPanel";
            PagingPanel.RowCount = 1;
            PagingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0f));
            PagingPanel.Size = new System.Drawing.Size(150, 28);
            PagingPanel.TabIndex = 41;
            PagingPanel.Visible = false;
            // 
            // LabelPages
            // 
            LabelPages.AutoSize = true;
            LabelPages.Dock = System.Windows.Forms.DockStyle.Fill;
            LabelPages.Location = new System.Drawing.Point(61, 0);
            LabelPages.Name = "LabelPages";
            LabelPages.Size = new System.Drawing.Size(28, 28);
            LabelPages.TabIndex = 43;
            LabelPages.Text = "0/0";
            LabelPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonLast
            // 
            ButtonLast.Location = new System.Drawing.Point(3, 3);
            ButtonLast.Name = "ButtonLast";
            ButtonLast.Size = new System.Drawing.Size(52, 22);
            ButtonLast.TabIndex = 0;
            ButtonLast.Text = "<--";
            ButtonLast.UseVisualStyleBackColor = true;
            // 
            // ButtonNext
            // 
            ButtonNext.Location = new System.Drawing.Point(95, 3);
            ButtonNext.Name = "ButtonNext";
            ButtonNext.Size = new System.Drawing.Size(52, 22);
            ButtonNext.TabIndex = 1;
            ButtonNext.Text = "-->";
            ButtonNext.UseVisualStyleBackColor = true;
            // 
            // HexPanel1
            // 
            HexPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            HexPanel1.Controls.Add(ToolPanel);
            HexPanel1.Controls.Add(PortraitPanel);
            HexPanel1.Controls.Add(_Text);
            HexPanel1.IndexOffset = "&H000002";
            HexPanel1.Location = new System.Drawing.Point(5, 5);
            HexPanel1.Margin = new System.Windows.Forms.Padding(5);
            HexPanel1.Name = "HexPanel1";
            HexPanel1.Padding = new System.Windows.Forms.Padding(5);
            HexPanel1.Pointer = "&H030000";
            HexPanel1.PointerBank = 6;
            HexPanel1.PointerLength = 2;
            HexPanel1.Size = new System.Drawing.Size(439, 240);
            HexPanel1.TabIndex = 38;
            // 
            // ToolPanel
            // 
            ToolPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ToolPanel.Controls.Add(ButtonQuote);
            ToolPanel.Controls.Add(ButtonClose);
            ToolPanel.Location = new System.Drawing.Point(8, 40);
            ToolPanel.Name = "ToolPanel";
            ToolPanel.Size = new System.Drawing.Size(423, 29);
            ToolPanel.TabIndex = 43;
            ToolPanel.Visible = false;
            // 
            // ButtonQuote
            // 
            ButtonQuote.Appearance = System.Windows.Forms.Appearance.Button;
            ButtonQuote.AutoSize = true;
            ButtonQuote.Location = new System.Drawing.Point(3, 3);
            ButtonQuote.Name = "ButtonQuote";
            ButtonQuote.Size = new System.Drawing.Size(57, 27);
            ButtonQuote.TabIndex = 0;
            ButtonQuote.Text = "Quote";
            ButtonQuote.UseVisualStyleBackColor = true;
            // 
            // ButtonClose
            // 
            ButtonClose.AutoSize = true;
            ButtonClose.Location = new System.Drawing.Point(66, 3);
            ButtonClose.Name = "ButtonClose";
            ButtonClose.Size = new System.Drawing.Size(114, 27);
            ButtonClose.TabIndex = 1;
            ButtonClose.Text = "Close Window";
            ButtonClose.UseVisualStyleBackColor = true;
            // 
            // PortraitPanel
            // 
            PortraitPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            PortraitPanel.ColumnCount = 2;
            PortraitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            PortraitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0f));
            PortraitPanel.Controls.Add(Portrait, 1, 0);
            PortraitPanel.Controls.Add(LabelPortrait, 0, 0);
            PortraitPanel.Location = new System.Drawing.Point(8, 8);
            PortraitPanel.Name = "PortraitPanel";
            PortraitPanel.RowCount = 1;
            PortraitPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0f));
            PortraitPanel.Size = new System.Drawing.Size(423, 30);
            PortraitPanel.TabIndex = 42;
            // 
            // Portrait
            // 
            Portrait.Display = HexEnumerations.DisplayType.Hex;
            Portrait.Dock = System.Windows.Forms.DockStyle.Fill;
            Portrait.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            Portrait.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Portrait.FormatString = "X2";
            Portrait.FormattingEnabled = true;
            Portrait.ItemCollection = "Data_Battlers.HexListBox1,Data_Items.HexListBox1";
            Portrait.Location = new System.Drawing.Point(67, 3);
            Portrait.MaxDropDownItems = 15;
            Portrait.MaxLength = 2;
            Portrait.Name = "Portrait";
            Portrait.Size = new System.Drawing.Size(353, 23);
            Portrait.StartIndex = "&H00F400";
            Portrait.TabIndex = 1;
            Portrait.ValueDisplay = true;
            Portrait.Visible = false;
            // 
            // LabelPortrait
            // 
            LabelPortrait.AutoSize = true;
            LabelPortrait.Dock = System.Windows.Forms.DockStyle.Fill;
            LabelPortrait.Location = new System.Drawing.Point(3, 0);
            LabelPortrait.Name = "LabelPortrait";
            LabelPortrait.Size = new System.Drawing.Size(58, 30);
            LabelPortrait.TabIndex = 0;
            LabelPortrait.Text = "Portrait:";
            LabelPortrait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            LabelPortrait.Visible = false;
            // 
            // _Text
            // 
            _Text.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            _Text.ClosingTag = "FFFF";
            _Text.ClosingTagIsEnd = true;
            _Text.Font = new System.Drawing.Font("Consolas", 12.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _Text.FontTableUsesGlobal = true;
            _Text.Location = new System.Drawing.Point(0, 71);
            _Text.Margin = new System.Windows.Forms.Padding(5);
            _Text.MaxLengthLabel = true;
            _Text.Multiline = true;
            _Text.Name = "_Text";
            _Text.Size = new System.Drawing.Size(429, 168);
            _Text.TabIndex = 18;
            // 
            // HexMessageBox
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            Controls.Add(PageControlPanel);
            Controls.Add(PagingPanel);
            Controls.Add(HexPanel1);
            MinimumSize = new System.Drawing.Size(250, 125);
            Name = "HexMessageBox";
            Size = new System.Drawing.Size(445, 275);
            PageControlPanel.ResumeLayout(false);
            PagingPanel.ResumeLayout(false);
            PagingPanel.PerformLayout();
            HexPanel1.ResumeLayout(false);
            HexPanel1.PerformLayout();
            ToolPanel.ResumeLayout(false);
            ToolPanel.PerformLayout();
            PortraitPanel.ResumeLayout(false);
            PortraitPanel.PerformLayout();
            ResumeLayout(false);

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