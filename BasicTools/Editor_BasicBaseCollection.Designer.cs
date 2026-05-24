using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Editor_BasicBaseCollection : System.Windows.Forms.Form
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
            components = new Container();
            PropertyGrid = new System.Windows.Forms.PropertyGrid();
            PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(PropertyGrid1_PropertyValueChanged);
            Label1 = new System.Windows.Forms.Label();
            Label2 = new System.Windows.Forms.Label();
            ButtonOK = new System.Windows.Forms.Button();
            ButtonOK.Click += new EventHandler(ButtonOK_Click);
            ButtonCancel = new System.Windows.Forms.Button();
            ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
            ButtonRemove = new System.Windows.Forms.Button();
            ButtonRemove.Click += new EventHandler(ButtonRemove_Click);
            ButtonRemove.HelpRequested += new System.Windows.Forms.HelpEventHandler(ButtonRemove_HelpRequested);
            ComboBoxInsert = new System.Windows.Forms.ComboBox();
            ComboBoxInsert.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            LabelMax = new System.Windows.Forms.Label();
            LabelMax.HelpRequested += new System.Windows.Forms.HelpEventHandler(LabelMax_HelpRequested);
            LabelName = new System.Windows.Forms.Label();
            ContextMenuMembers = new System.Windows.Forms.ContextMenuStrip(components);
            TextBoxName = new BasicControls.BasicTextBox();
            TextBoxName.Enter += new EventHandler(TextBoxName_Enter);
            TextBoxName.TextChangeCompleted += new EventHandler(TextBoxName_TextChangeCompleted);
            ListView = new BasicControls.BasicListView();
            ListView.SelectedIndexChanged += new EventHandler(ListBox1_SelectedIndexChanged);
            ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
            ButtonPaste = new System.Windows.Forms.Button();
            ButtonPaste.Click += new EventHandler(ButtonPaste_Click);
            ButtonCopy = new System.Windows.Forms.Button();
            ButtonCopy.Click += new EventHandler(ButtonCopy_Click);
            InsertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            InsertToolStripMenuItem.Click += new EventHandler(InsertToolStripMenuItem_Click);
            RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            RemoveToolStripMenuItem.Click += new EventHandler(DeleteToolStripMenuItem_Click);
            CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            CopyToolStripMenuItem.Click += new EventHandler(CopyToolStripMenuItem_Click);
            PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            PasteToolStripMenuItem.Click += new EventHandler(PasteToolStripMenuItem_Click);
            MoveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MoveUpToolStripMenuItem.Click += new EventHandler(MoveUpToolStripMenuItem_Click);
            MoveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MoveDownToolStripMenuItem.Click += new EventHandler(MoveDownToolStripMenuItem_Click);
            ButtonDown = new System.Windows.Forms.Button();
            ButtonDown.Click += new EventHandler(ButtonDown_Click);
            ButtonUp = new System.Windows.Forms.Button();
            ButtonUp.Click += new EventHandler(ButtonUp_Click);
            ButtonInsert = new System.Windows.Forms.Button();
            ButtonInsert.MouseClick += new System.Windows.Forms.MouseEventHandler(ButtonInsert_MouseClick);
            ButtonInsert.HelpRequested += new System.Windows.Forms.HelpEventHandler(ButtonInsert_HelpRequested);
            TableLayoutPanel1.SuspendLayout();
            ContextMenuMembers.SuspendLayout();
            SuspendLayout();
            // 
            // PropertyGrid
            // 
            PropertyGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            PropertyGrid.CommandsVisibleIfAvailable = false;
            PropertyGrid.Location = new System.Drawing.Point(411, 36);
            PropertyGrid.Margin = new System.Windows.Forms.Padding(4);
            PropertyGrid.Name = "PropertyGrid";
            PropertyGrid.Size = new System.Drawing.Size(405, 474);
            PropertyGrid.TabIndex = 0;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Label1.Location = new System.Drawing.Point(12, 9);
            Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new System.Drawing.Size(75, 18);
            Label1.TabIndex = 2;
            Label1.Text = "Members:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Label2.Location = new System.Drawing.Point(407, 9);
            Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new System.Drawing.Size(80, 18);
            Label2.TabIndex = 3;
            Label2.Text = "Properties:";
            // 
            // ButtonOK
            // 
            ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            ButtonOK.Location = new System.Drawing.Point(544, 534);
            ButtonOK.Margin = new System.Windows.Forms.Padding(4);
            ButtonOK.Name = "ButtonOK";
            ButtonOK.Size = new System.Drawing.Size(133, 30);
            ButtonOK.TabIndex = 5;
            ButtonOK.Text = "OK";
            ButtonOK.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            ButtonCancel.Location = new System.Drawing.Point(685, 534);
            ButtonCancel.Margin = new System.Windows.Forms.Padding(4);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new System.Drawing.Size(133, 30);
            ButtonCancel.TabIndex = 6;
            ButtonCancel.Text = "Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonRemove
            // 
            ButtonRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ButtonRemove.Location = new System.Drawing.Point(216, 480);
            ButtonRemove.Margin = new System.Windows.Forms.Padding(4);
            ButtonRemove.Name = "ButtonRemove";
            ButtonRemove.Size = new System.Drawing.Size(133, 30);
            ButtonRemove.TabIndex = 8;
            ButtonRemove.Text = "Remove";
            ButtonRemove.UseVisualStyleBackColor = true;
            // 
            // ComboBoxInsert
            // 
            ComboBoxInsert.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ComboBoxInsert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ComboBoxInsert.FormattingEnabled = true;
            ComboBoxInsert.Location = new System.Drawing.Point(21, 482);
            ComboBoxInsert.Margin = new System.Windows.Forms.Padding(4);
            ComboBoxInsert.Name = "ComboBoxInsert";
            ComboBoxInsert.Size = new System.Drawing.Size(169, 24);
            ComboBoxInsert.TabIndex = 10;
            // 
            // TableLayoutPanel1
            // 
            TableLayoutPanel1.ColumnCount = 1;
            TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            TableLayoutPanel1.Controls.Add(LabelMax, 0, 0);
            TableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TableLayoutPanel1.Location = new System.Drawing.Point(203, 9);
            TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            TableLayoutPanel1.Name = "TableLayoutPanel1";
            TableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            TableLayoutPanel1.RowCount = 1;
            TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            TableLayoutPanel1.Size = new System.Drawing.Size(147, 22);
            TableLayoutPanel1.TabIndex = 15;
            // 
            // LabelMax
            // 
            LabelMax.AutoSize = true;
            LabelMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            LabelMax.Location = new System.Drawing.Point(111, 0);
            LabelMax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelMax.Name = "LabelMax";
            LabelMax.Size = new System.Drawing.Size(32, 20);
            LabelMax.TabIndex = 15;
            LabelMax.Text = "0/0";
            // 
            // LabelName
            // 
            LabelName.AutoSize = true;
            LabelName.Location = new System.Drawing.Point(561, 41);
            LabelName.Name = "LabelName";
            LabelName.Size = new System.Drawing.Size(49, 17);
            LabelName.TabIndex = 19;
            LabelName.Text = "Name:";
            // 
            // ContextMenuMembers
            // 
            ContextMenuMembers.ImageScalingSize = new System.Drawing.Size(20, 20);
            ContextMenuMembers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { InsertToolStripMenuItem, RemoveToolStripMenuItem, CopyToolStripMenuItem, PasteToolStripMenuItem, MoveUpToolStripMenuItem, MoveDownToolStripMenuItem });
            ContextMenuMembers.Name = "ContextMenuMembers";
            ContextMenuMembers.Size = new System.Drawing.Size(244, 160);
            // 
            // TextBoxName
            // 
            TextBoxName.Location = new System.Drawing.Point(616, 37);
            TextBoxName.Name = "TextBoxName";
            TextBoxName.Size = new System.Drawing.Size(200, 22);
            TextBoxName.TabIndex = 18;
            // 
            // ListView
            // 
            ListView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { ColumnHeader1 });
            ListView.ContextMenuStrip = ContextMenuMembers;
            ListView.FullRowSelect = true;
            ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            ListView.HideSelection = false;
            ListView.Location = new System.Drawing.Point(16, 36);
            ListView.Margin = new System.Windows.Forms.Padding(4);
            ListView.Name = "ListView";
            ListView.Size = new System.Drawing.Size(332, 424);
            ListView.TabIndex = 13;
            ListView.UseCompatibleStateImageBehavior = false;
            ListView.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            ColumnHeader1.Width = 280;
            // 
            // ButtonPaste
            // 
            ButtonPaste.Image = My.Resources.Resources.PasteHS;
            ButtonPaste.Location = new System.Drawing.Point(367, 127);
            ButtonPaste.Margin = new System.Windows.Forms.Padding(4);
            ButtonPaste.Name = "ButtonPaste";
            ButtonPaste.Size = new System.Drawing.Size(32, 30);
            ButtonPaste.TabIndex = 17;
            ButtonPaste.UseVisualStyleBackColor = true;
            // 
            // ButtonCopy
            // 
            ButtonCopy.Image = My.Resources.Resources.CopyHS;
            ButtonCopy.Location = new System.Drawing.Point(367, 90);
            ButtonCopy.Margin = new System.Windows.Forms.Padding(4);
            ButtonCopy.Name = "ButtonCopy";
            ButtonCopy.Size = new System.Drawing.Size(32, 30);
            ButtonCopy.TabIndex = 16;
            ButtonCopy.UseVisualStyleBackColor = true;
            // 
            // InsertToolStripMenuItem
            // 
            InsertToolStripMenuItem.Image = My.Resources.Resources.AddMore;
            InsertToolStripMenuItem.Name = "InsertToolStripMenuItem";
            InsertToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            InsertToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            InsertToolStripMenuItem.Text = "&Insert";
            // 
            // DeleteToolStripMenuItem
            // 
            RemoveToolStripMenuItem.Image = My.Resources.Resources.trash32;
            RemoveToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            RemoveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            RemoveToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            RemoveToolStripMenuItem.Text = "&Delete";
            // 
            // CopyToolStripMenuItem
            // 
            CopyToolStripMenuItem.Image = My.Resources.Resources.CopyHS;
            CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            CopyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C;
            CopyToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            CopyToolStripMenuItem.Text = "&Copy";
            // 
            // PasteToolStripMenuItem
            // 
            PasteToolStripMenuItem.Image = My.Resources.Resources.PasteHS;
            PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            PasteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V;
            PasteToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            PasteToolStripMenuItem.Text = "&Paste";
            // 
            // MoveUpToolStripMenuItem
            // 
            MoveUpToolStripMenuItem.Image = My.Resources.Resources.Up;
            MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem";
            MoveUpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up;
            MoveUpToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            MoveUpToolStripMenuItem.Text = "Move Up";
            // 
            // MoveDownToolStripMenuItem
            // 
            MoveDownToolStripMenuItem.Image = My.Resources.Resources.Down;
            MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem";
            MoveDownToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down;
            MoveDownToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            MoveDownToolStripMenuItem.Text = "Move Down";
            // 
            // ButtonDown
            // 
            ButtonDown.Image = My.Resources.Resources.Down;
            ButtonDown.Location = new System.Drawing.Point(367, 220);
            ButtonDown.Margin = new System.Windows.Forms.Padding(4);
            ButtonDown.Name = "ButtonDown";
            ButtonDown.Size = new System.Drawing.Size(32, 30);
            ButtonDown.TabIndex = 12;
            ButtonDown.UseVisualStyleBackColor = true;
            // 
            // ButtonUp
            // 
            ButtonUp.Image = My.Resources.Resources.Up;
            ButtonUp.Location = new System.Drawing.Point(367, 183);
            ButtonUp.Margin = new System.Windows.Forms.Padding(4);
            ButtonUp.Name = "ButtonUp";
            ButtonUp.Size = new System.Drawing.Size(32, 30);
            ButtonUp.TabIndex = 11;
            ButtonUp.UseVisualStyleBackColor = true;
            // 
            // ButtonInsert
            // 
            ButtonInsert.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ButtonInsert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            ButtonInsert.Image = My.Resources.Resources.AddMore;
            ButtonInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            ButtonInsert.Location = new System.Drawing.Point(20, 480);
            ButtonInsert.Margin = new System.Windows.Forms.Padding(4);
            ButtonInsert.Name = "ButtonInsert";
            ButtonInsert.Size = new System.Drawing.Size(173, 30);
            ButtonInsert.TabIndex = 7;
            ButtonInsert.Text = "Insert";
            ButtonInsert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            ButtonInsert.UseVisualStyleBackColor = true;
            // 
            // Editor_BasicBaseCollection
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8.0f, 16.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(832, 578);
            Controls.Add(LabelName);
            Controls.Add(TextBoxName);
            Controls.Add(ButtonPaste);
            Controls.Add(ButtonCopy);
            Controls.Add(TableLayoutPanel1);
            Controls.Add(ListView);
            Controls.Add(ButtonDown);
            Controls.Add(ButtonUp);
            Controls.Add(ButtonRemove);
            Controls.Add(ButtonInsert);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOK);
            Controls.Add(ComboBoxInsert);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(PropertyGrid);
            HelpButton = true;
            Margin = new System.Windows.Forms.Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(847, 481);
            Name = "Editor_BasicBaseCollection";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Variable Collection Editor";
            TableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanel1.PerformLayout();
            ContextMenuMembers.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }
        internal System.Windows.Forms.PropertyGrid PropertyGrid;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.Button ButtonInsert;
        internal System.Windows.Forms.Button ButtonRemove;
        internal System.Windows.Forms.ComboBox ComboBoxInsert;
        internal System.Windows.Forms.Button ButtonUp;
        internal System.Windows.Forms.Button ButtonDown;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal BasicControls.BasicListView ListView;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Label LabelMax;
        internal System.Windows.Forms.Button ButtonCopy;
        internal System.Windows.Forms.Button ButtonPaste;
        internal BasicControls.BasicTextBox TextBoxName;
        internal System.Windows.Forms.Label LabelName;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuMembers;
        internal System.Windows.Forms.ToolStripMenuItem InsertToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem MoveUpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem MoveDownToolStripMenuItem;
    }
}