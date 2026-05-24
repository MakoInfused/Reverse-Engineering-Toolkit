using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BasicTools
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Editor_MultiCollection : System.Windows.Forms.Form
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
            var resources = new ComponentResourceManager(typeof(Editor_MultiCollection));
            _PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            _PropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(PropertyGrid1_PropertyValueChanged);
            _Label1 = new System.Windows.Forms.Label();
            _Label2 = new System.Windows.Forms.Label();
            _ButtonOK = new System.Windows.Forms.Button();
            _ButtonCancel = new System.Windows.Forms.Button();
            _ButtonRemove = new System.Windows.Forms.Button();
            _ButtonRemove.Click += new EventHandler(ButtonRemove_Click);
            _ButtonRemove.HelpRequested += new System.Windows.Forms.HelpEventHandler(ButtonRemove_HelpRequested);
            _ComboBox1 = new System.Windows.Forms.ComboBox();
            _ComboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            _TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            _LabelMax = new System.Windows.Forms.Label();
            _LabelMax.HelpRequested += new System.Windows.Forms.HelpEventHandler(LabelMax_HelpRequested);
            _UiListView1 = new BasicControls.BasicListView();
            _UiListView1.SelectedIndexChanged += new EventHandler(ListBox1_SelectedIndexChanged);
            _ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
            _ButtonPaste = new System.Windows.Forms.Button();
            _ButtonPaste.Click += new EventHandler(ButtonPaste_Click);
            _ButtonCopy = new System.Windows.Forms.Button();
            _ButtonCopy.Click += new EventHandler(ButtonCopy_Click);
            _ButtonDown = new System.Windows.Forms.Button();
            _ButtonDown.Click += new EventHandler(ButtonDown_Click);
            _ButtonUp = new System.Windows.Forms.Button();
            _ButtonUp.Click += new EventHandler(ButtonUp_Click);
            _ButtonInsert = new System.Windows.Forms.Button();
            _ButtonInsert.MouseClick += new System.Windows.Forms.MouseEventHandler(ButtonInsert_MouseClick);
            _ButtonInsert.HelpRequested += new System.Windows.Forms.HelpEventHandler(ButtonInsert_HelpRequested);
            _TableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // PropertyGrid1
            // 
            _PropertyGrid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            _PropertyGrid1.CommandsVisibleIfAvailable = false;
            _PropertyGrid1.Location = new System.Drawing.Point(308, 29);
            _PropertyGrid1.Name = "_PropertyGrid1";
            _PropertyGrid1.Size = new System.Drawing.Size(304, 385);
            _PropertyGrid1.TabIndex = 0;
            // 
            // Label1
            // 
            _Label1.AutoSize = true;
            _Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _Label1.Location = new System.Drawing.Point(9, 7);
            _Label1.Name = "_Label1";
            _Label1.Size = new System.Drawing.Size(63, 15);
            _Label1.TabIndex = 2;
            _Label1.Text = "Members:";
            // 
            // Label2
            // 
            _Label2.AutoSize = true;
            _Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _Label2.Location = new System.Drawing.Point(305, 7);
            _Label2.Name = "_Label2";
            _Label2.Size = new System.Drawing.Size(66, 15);
            _Label2.TabIndex = 3;
            _Label2.Text = "Properties:";
            // 
            // ButtonOK
            // 
            _ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            _ButtonOK.Location = new System.Drawing.Point(408, 434);
            _ButtonOK.Name = "_ButtonOK";
            _ButtonOK.Size = new System.Drawing.Size(100, 24);
            _ButtonOK.TabIndex = 5;
            _ButtonOK.Text = "OK";
            _ButtonOK.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            _ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            _ButtonCancel.Location = new System.Drawing.Point(514, 434);
            _ButtonCancel.Name = "_ButtonCancel";
            _ButtonCancel.Size = new System.Drawing.Size(100, 24);
            _ButtonCancel.TabIndex = 6;
            _ButtonCancel.Text = "Cancel";
            _ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonRemove
            // 
            _ButtonRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            _ButtonRemove.Location = new System.Drawing.Point(162, 390);
            _ButtonRemove.Name = "_ButtonRemove";
            _ButtonRemove.Size = new System.Drawing.Size(100, 24);
            _ButtonRemove.TabIndex = 8;
            _ButtonRemove.Text = "Remove";
            _ButtonRemove.UseVisualStyleBackColor = true;
            // 
            // ComboBox1
            // 
            _ComboBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            _ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _ComboBox1.FormattingEnabled = true;
            _ComboBox1.Location = new System.Drawing.Point(16, 392);
            _ComboBox1.Name = "_ComboBox1";
            _ComboBox1.Size = new System.Drawing.Size(128, 21);
            _ComboBox1.TabIndex = 10;
            // 
            // TableLayoutPanel1
            // 
            _TableLayoutPanel1.ColumnCount = 1;
            _TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            _TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            _TableLayoutPanel1.Controls.Add(_LabelMax, 0, 0);
            _TableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _TableLayoutPanel1.Location = new System.Drawing.Point(152, 7);
            _TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            _TableLayoutPanel1.Name = "_TableLayoutPanel1";
            _TableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            _TableLayoutPanel1.RowCount = 1;
            _TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            _TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0f));
            _TableLayoutPanel1.Size = new System.Drawing.Size(110, 18);
            _TableLayoutPanel1.TabIndex = 15;
            // 
            // LabelMax
            // 
            _LabelMax.AutoSize = true;
            _LabelMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            _LabelMax.Location = new System.Drawing.Point(81, 0);
            _LabelMax.Name = "_LabelMax";
            _LabelMax.Size = new System.Drawing.Size(26, 16);
            _LabelMax.TabIndex = 15;
            _LabelMax.Text = "0/0";
            // 
            // UiListView1
            // 
            _UiListView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            _UiListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { _ColumnHeader1 });
            _UiListView1.FullRowSelect = true;
            _UiListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            _UiListView1.HideSelection = false;
            _UiListView1.Location = new System.Drawing.Point(12, 29);
            _UiListView1.MultiSelect = false;
            _UiListView1.Name = "_UiListView1";
            _UiListView1.Size = new System.Drawing.Size(250, 345);
            _UiListView1.TabIndex = 13;
            _UiListView1.UseCompatibleStateImageBehavior = false;
            _UiListView1.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            _ColumnHeader1.Width = 280;
            // 
            // ButtonPaste
            // 
            _ButtonPaste.Image = My.Resources.Resources.PasteHS;
            _ButtonPaste.Location = new System.Drawing.Point(275, 103);
            _ButtonPaste.Name = "_ButtonPaste";
            _ButtonPaste.Size = new System.Drawing.Size(24, 24);
            _ButtonPaste.TabIndex = 17;
            _ButtonPaste.UseVisualStyleBackColor = true;
            // 
            // ButtonCopy
            // 
            _ButtonCopy.Image = My.Resources.Resources.CopyHS;
            _ButtonCopy.Location = new System.Drawing.Point(275, 73);
            _ButtonCopy.Name = "_ButtonCopy";
            _ButtonCopy.Size = new System.Drawing.Size(24, 24);
            _ButtonCopy.TabIndex = 16;
            _ButtonCopy.UseVisualStyleBackColor = true;
            // 
            // ButtonDown
            // 
            _ButtonDown.Image = (System.Drawing.Image)resources.GetObject("ButtonDown.Image");
            _ButtonDown.Location = new System.Drawing.Point(275, 179);
            _ButtonDown.Name = "_ButtonDown";
            _ButtonDown.Size = new System.Drawing.Size(24, 24);
            _ButtonDown.TabIndex = 12;
            _ButtonDown.UseVisualStyleBackColor = true;
            // 
            // ButtonUp
            // 
            _ButtonUp.Image = (System.Drawing.Image)resources.GetObject("ButtonUp.Image");
            _ButtonUp.Location = new System.Drawing.Point(275, 149);
            _ButtonUp.Name = "_ButtonUp";
            _ButtonUp.Size = new System.Drawing.Size(24, 24);
            _ButtonUp.TabIndex = 11;
            _ButtonUp.UseVisualStyleBackColor = true;
            // 
            // ButtonInsert
            // 
            _ButtonInsert.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            _ButtonInsert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            _ButtonInsert.Image = (System.Drawing.Image)resources.GetObject("ButtonInsert.Image");
            _ButtonInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            _ButtonInsert.Location = new System.Drawing.Point(15, 390);
            _ButtonInsert.Name = "_ButtonInsert";
            _ButtonInsert.Size = new System.Drawing.Size(130, 24);
            _ButtonInsert.TabIndex = 7;
            _ButtonInsert.Text = "Insert";
            _ButtonInsert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            _ButtonInsert.UseVisualStyleBackColor = true;
            // 
            // MultiCollectionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6.0f, 13.0f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(624, 470);
            Controls.Add(_ButtonPaste);
            Controls.Add(_ButtonCopy);
            Controls.Add(_TableLayoutPanel1);
            Controls.Add(_UiListView1);
            Controls.Add(_ButtonDown);
            Controls.Add(_ButtonUp);
            Controls.Add(_ButtonRemove);
            Controls.Add(_ButtonInsert);
            Controls.Add(_ButtonCancel);
            Controls.Add(_ButtonOK);
            Controls.Add(_ComboBox1);
            Controls.Add(_Label2);
            Controls.Add(_Label1);
            Controls.Add(_PropertyGrid1);
            HelpButton = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(640, 400);
            Name = "MultiCollectionForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Variable Collection Editor";
            _TableLayoutPanel1.ResumeLayout(false);
            _TableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        private System.Windows.Forms.PropertyGrid _PropertyGrid1;

        internal virtual System.Windows.Forms.PropertyGrid PropertyGrid1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _PropertyGrid1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_PropertyGrid1 != null)
                {
                    _PropertyGrid1.PropertyValueChanged -= PropertyGrid1_PropertyValueChanged;
                }

                _PropertyGrid1 = value;
                if (_PropertyGrid1 != null)
                {
                    _PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
                }
            }
        }
        private System.Windows.Forms.Label _Label1;

        internal virtual System.Windows.Forms.Label Label1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Label1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _Label1 = value;
            }
        }
        private System.Windows.Forms.Label _Label2;

        internal virtual System.Windows.Forms.Label Label2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Label2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _Label2 = value;
            }
        }
        private System.Windows.Forms.Button _ButtonOK;

        internal virtual System.Windows.Forms.Button ButtonOK
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonOK;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _ButtonOK = value;
            }
        }
        private System.Windows.Forms.Button _ButtonCancel;

        internal virtual System.Windows.Forms.Button ButtonCancel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonCancel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _ButtonCancel = value;
            }
        }
        private System.Windows.Forms.Button _ButtonInsert;

        internal virtual System.Windows.Forms.Button ButtonInsert
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonInsert;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonInsert != null)
                {
                    _ButtonInsert.MouseClick -= ButtonInsert_MouseClick;
                    _ButtonInsert.HelpRequested -= ButtonInsert_HelpRequested;
                }

                _ButtonInsert = value;
                if (_ButtonInsert != null)
                {
                    _ButtonInsert.MouseClick += ButtonInsert_MouseClick;
                    _ButtonInsert.HelpRequested += ButtonInsert_HelpRequested;
                }
            }
        }
        private System.Windows.Forms.Button _ButtonRemove;

        internal virtual System.Windows.Forms.Button ButtonRemove
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonRemove;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonRemove != null)
                {
                    _ButtonRemove.Click -= ButtonRemove_Click;
                    _ButtonRemove.HelpRequested -= ButtonRemove_HelpRequested;
                }

                _ButtonRemove = value;
                if (_ButtonRemove != null)
                {
                    _ButtonRemove.Click += ButtonRemove_Click;
                    _ButtonRemove.HelpRequested += ButtonRemove_HelpRequested;
                }
            }
        }
        private System.Windows.Forms.ComboBox _ComboBox1;

        internal virtual System.Windows.Forms.ComboBox ComboBox1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ComboBox1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ComboBox1 != null)
                {
                    _ComboBox1.SelectedIndexChanged -= ComboBox1_SelectedIndexChanged;
                }

                _ComboBox1 = value;
                if (_ComboBox1 != null)
                {
                    _ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
                }
            }
        }
        private System.Windows.Forms.Button _ButtonUp;

        internal virtual System.Windows.Forms.Button ButtonUp
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonUp;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonUp != null)
                {
                    _ButtonUp.Click -= ButtonUp_Click;
                }

                _ButtonUp = value;
                if (_ButtonUp != null)
                {
                    _ButtonUp.Click += ButtonUp_Click;
                }
            }
        }
        private System.Windows.Forms.Button _ButtonDown;

        internal virtual System.Windows.Forms.Button ButtonDown
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonDown;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonDown != null)
                {
                    _ButtonDown.Click -= ButtonDown_Click;
                }

                _ButtonDown = value;
                if (_ButtonDown != null)
                {
                    _ButtonDown.Click += ButtonDown_Click;
                }
            }
        }
        private System.Windows.Forms.ColumnHeader _ColumnHeader1;

        internal virtual System.Windows.Forms.ColumnHeader ColumnHeader1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ColumnHeader1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _ColumnHeader1 = value;
            }
        }
        private BasicControls.BasicListView _UiListView1;

        internal virtual BasicControls.BasicListView UiListView1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _UiListView1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_UiListView1 != null)
                {
                    _UiListView1.SelectedIndexChanged -= ListBox1_SelectedIndexChanged;
                }

                _UiListView1 = value;
                if (_UiListView1 != null)
                {
                    _UiListView1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
                }
            }
        }
        private System.Windows.Forms.TableLayoutPanel _TableLayoutPanel1;

        internal virtual System.Windows.Forms.TableLayoutPanel TableLayoutPanel1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TableLayoutPanel1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _TableLayoutPanel1 = value;
            }
        }
        private System.Windows.Forms.Label _LabelMax;

        internal virtual System.Windows.Forms.Label LabelMax
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _LabelMax;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_LabelMax != null)
                {
                    _LabelMax.HelpRequested -= LabelMax_HelpRequested;
                }

                _LabelMax = value;
                if (_LabelMax != null)
                {
                    _LabelMax.HelpRequested += LabelMax_HelpRequested;
                }
            }
        }
        private System.Windows.Forms.Button _ButtonCopy;

        internal virtual System.Windows.Forms.Button ButtonCopy
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonCopy;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonCopy != null)
                {
                    _ButtonCopy.Click -= ButtonCopy_Click;
                }

                _ButtonCopy = value;
                if (_ButtonCopy != null)
                {
                    _ButtonCopy.Click += ButtonCopy_Click;
                }
            }
        }
        private System.Windows.Forms.Button _ButtonPaste;

        internal virtual System.Windows.Forms.Button ButtonPaste
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ButtonPaste;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ButtonPaste != null)
                {
                    _ButtonPaste.Click -= ButtonPaste_Click;
                }

                _ButtonPaste = value;
                if (_ButtonPaste != null)
                {
                    _ButtonPaste.Click += ButtonPaste_Click;
                }
            }
        }
    }
}