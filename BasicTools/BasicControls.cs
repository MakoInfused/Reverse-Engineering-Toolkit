using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using BasicTools.BasicEnumerations;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BasicTools.BasicControls
{

    #region  BasicComponents 

    [ToolboxItem(false)]
    public class BasicComponents : UserControl
    {

        #region  Designer Generated Code 

        private ContextMenuStrip _BasicTextBoxContextMenu;

        internal virtual ContextMenuStrip BasicTextBoxContextMenu
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _BasicTextBoxContextMenu;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BasicTextBoxContextMenu != null)
                {
                    _BasicTextBoxContextMenu.Opening -= BasicTextBoxContextMenu_Refresh;
                }

                _BasicTextBoxContextMenu = value;
                if (_BasicTextBoxContextMenu != null)
                {
                    _BasicTextBoxContextMenu.Opening += BasicTextBoxContextMenu_Refresh;
                }
            }
        }
        private IContainer components;
        private ToolStripMenuItem _CopyToolStripMenuItem;

        internal virtual ToolStripMenuItem CopyToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CopyToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CopyToolStripMenuItem != null)
                {
                    _CopyToolStripMenuItem.Click -= CopyToolStripMenuItem_Click;
                }

                _CopyToolStripMenuItem = value;
                if (_CopyToolStripMenuItem != null)
                {
                    _CopyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripMenuItem _PasteToolStripMenuItem;

        internal virtual ToolStripMenuItem PasteToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _PasteToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_PasteToolStripMenuItem != null)
                {
                    _PasteToolStripMenuItem.Click -= PasteToolStripMenuItem_Click;
                }

                _PasteToolStripMenuItem = value;
                if (_PasteToolStripMenuItem != null)
                {
                    _PasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripMenuItem _UndoToolStripMenuItem;

        internal virtual ToolStripMenuItem UndoToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _UndoToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_UndoToolStripMenuItem != null)
                {
                    _UndoToolStripMenuItem.Click -= UndoRedoToolStripMenuItem_Click;
                }

                _UndoToolStripMenuItem = value;
                if (_UndoToolStripMenuItem != null)
                {
                    _UndoToolStripMenuItem.Click += UndoRedoToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripSeparator _ToolStripSeparator1;

        internal virtual ToolStripSeparator ToolStripSeparator1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ToolStripSeparator1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _ToolStripSeparator1 = value;
            }
        }
        private ToolStripMenuItem _CopyDataToolStripMenuItem;

        internal virtual ToolStripMenuItem CopyDataToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CopyDataToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CopyDataToolStripMenuItem != null)
                {
                    _CopyDataToolStripMenuItem.Click -= CopyDataToolStripMenuItem_Click;
                }

                _CopyDataToolStripMenuItem = value;
                if (_CopyDataToolStripMenuItem != null)
                {
                    _CopyDataToolStripMenuItem.Click += CopyDataToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripMenuItem _RedoToolStripMenuItem;

        internal virtual ToolStripMenuItem RedoToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RedoToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RedoToolStripMenuItem != null)
                {
                    _RedoToolStripMenuItem.Click -= UndoRedoToolStripMenuItem_Click;
                }

                _RedoToolStripMenuItem = value;
                if (_RedoToolStripMenuItem != null)
                {
                    _RedoToolStripMenuItem.Click += UndoRedoToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripMenuItem _DeleteToolStripMenuItem;

        internal virtual ToolStripMenuItem DeleteToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _DeleteToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_DeleteToolStripMenuItem != null)
                {
                    _DeleteToolStripMenuItem.Click -= DeleteToolStripMenuItem_Click;
                }

                _DeleteToolStripMenuItem = value;
                if (_DeleteToolStripMenuItem != null)
                {
                    _DeleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripSeparator _ToolStripSeparator2;

        internal virtual ToolStripSeparator ToolStripSeparator2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ToolStripSeparator2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _ToolStripSeparator2 = value;
            }
        }
        private ToolStripMenuItem _SelectAllToolStripMenuItem;

        internal virtual ToolStripMenuItem SelectAllToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SelectAllToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SelectAllToolStripMenuItem != null)
                {
                    _SelectAllToolStripMenuItem.Click -= SelectAllToolStripMenuItem_Click;
                }

                _SelectAllToolStripMenuItem = value;
                if (_SelectAllToolStripMenuItem != null)
                {
                    _SelectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripSeparator _ToolStripSeparator3;

        internal virtual ToolStripSeparator ToolStripSeparator3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ToolStripSeparator3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _ToolStripSeparator3 = value;
            }
        }
        private ToolStripMenuItem _RightToLeftReadingOrderToolStripMenuItem;

        internal virtual ToolStripMenuItem RightToLeftReadingOrderToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RightToLeftReadingOrderToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RightToLeftReadingOrderToolStripMenuItem != null)
                {
                    _RightToLeftReadingOrderToolStripMenuItem.Click -= RightToLeftReadingOrderToolStripMenuItem_Click;
                }

                _RightToLeftReadingOrderToolStripMenuItem = value;
                if (_RightToLeftReadingOrderToolStripMenuItem != null)
                {
                    _RightToLeftReadingOrderToolStripMenuItem.Click += RightToLeftReadingOrderToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripMenuItem _DeselectAllToolStripMenuItem;

        internal virtual ToolStripMenuItem DeselectAllToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _DeselectAllToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_DeselectAllToolStripMenuItem != null)
                {
                    _DeselectAllToolStripMenuItem.Click -= DeselectAllToolStripMenuItem_Click;
                }

                _DeselectAllToolStripMenuItem = value;
                if (_DeselectAllToolStripMenuItem != null)
                {
                    _DeselectAllToolStripMenuItem.Click += DeselectAllToolStripMenuItem_Click;
                }
            }
        }
        private ToolStripMenuItem _CutToolStripMenuItem;

        internal virtual ToolStripMenuItem CutToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CutToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CutToolStripMenuItem != null)
                {
                    _CutToolStripMenuItem.Click -= CutToolStripMenuItem_Click;
                }

                _CutToolStripMenuItem = value;
                if (_CutToolStripMenuItem != null)
                {
                    _CutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
                }
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            _BasicTextBoxContextMenu = new ContextMenuStrip(components);
            _BasicTextBoxContextMenu.Opening += new CancelEventHandler(BasicTextBoxContextMenu_Refresh);
            _RedoToolStripMenuItem = new ToolStripMenuItem();
            _RedoToolStripMenuItem.Click += new EventHandler(UndoRedoToolStripMenuItem_Click);
            _UndoToolStripMenuItem = new ToolStripMenuItem();
            _UndoToolStripMenuItem.Click += new EventHandler(UndoRedoToolStripMenuItem_Click);
            _ToolStripSeparator1 = new ToolStripSeparator();
            _CutToolStripMenuItem = new ToolStripMenuItem();
            _CutToolStripMenuItem.Click += new EventHandler(CutToolStripMenuItem_Click);
            _CopyToolStripMenuItem = new ToolStripMenuItem();
            _CopyToolStripMenuItem.Click += new EventHandler(CopyToolStripMenuItem_Click);
            _CopyDataToolStripMenuItem = new ToolStripMenuItem();
            _CopyDataToolStripMenuItem.Click += new EventHandler(CopyDataToolStripMenuItem_Click);
            _PasteToolStripMenuItem = new ToolStripMenuItem();
            _PasteToolStripMenuItem.Click += new EventHandler(PasteToolStripMenuItem_Click);
            _DeleteToolStripMenuItem = new ToolStripMenuItem();
            _DeleteToolStripMenuItem.Click += new EventHandler(DeleteToolStripMenuItem_Click);
            _ToolStripSeparator2 = new ToolStripSeparator();
            _SelectAllToolStripMenuItem = new ToolStripMenuItem();
            _SelectAllToolStripMenuItem.Click += new EventHandler(SelectAllToolStripMenuItem_Click);
            _DeselectAllToolStripMenuItem = new ToolStripMenuItem();
            _DeselectAllToolStripMenuItem.Click += new EventHandler(DeselectAllToolStripMenuItem_Click);
            _ToolStripSeparator3 = new ToolStripSeparator();
            _RightToLeftReadingOrderToolStripMenuItem = new ToolStripMenuItem();
            _RightToLeftReadingOrderToolStripMenuItem.Click += new EventHandler(RightToLeftReadingOrderToolStripMenuItem_Click);
            _BasicTextBoxContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // BasicTextBoxContextMenu
            // 
            _BasicTextBoxContextMenu.ImageScalingSize = new Size(20, 20);
            _BasicTextBoxContextMenu.Items.AddRange(new ToolStripItem[] { _RedoToolStripMenuItem, _UndoToolStripMenuItem, _ToolStripSeparator1, _CutToolStripMenuItem, _CopyToolStripMenuItem, _CopyDataToolStripMenuItem, _PasteToolStripMenuItem, _DeleteToolStripMenuItem, _ToolStripSeparator2, _SelectAllToolStripMenuItem, _DeselectAllToolStripMenuItem, _ToolStripSeparator3, _RightToLeftReadingOrderToolStripMenuItem });
            _BasicTextBoxContextMenu.Name = "_BasicTextBoxContextMenu";
            _BasicTextBoxContextMenu.Size = new Size(263, 282);
            // 
            // RedoToolStripMenuItem
            // 
            _RedoToolStripMenuItem.Image = My.Resources.Resources.redo16;
            _RedoToolStripMenuItem.Name = "_RedoToolStripMenuItem";
            _RedoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            _RedoToolStripMenuItem.Size = new Size(262, 26);
            _RedoToolStripMenuItem.Text = "Redo";
            // 
            // UndoToolStripMenuItem
            // 
            _UndoToolStripMenuItem.Image = My.Resources.Resources.undo16;
            _UndoToolStripMenuItem.Name = "_UndoToolStripMenuItem";
            _UndoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            _UndoToolStripMenuItem.Size = new Size(262, 26);
            _UndoToolStripMenuItem.Text = "Undo";
            // 
            // ToolStripSeparator1
            // 
            _ToolStripSeparator1.Name = "_ToolStripSeparator1";
            _ToolStripSeparator1.Size = new Size(259, 6);
            // 
            // CutToolStripMenuItem
            // 
            _CutToolStripMenuItem.Image = My.Resources.Resources.Cut;
            _CutToolStripMenuItem.Name = "_CutToolStripMenuItem";
            _CutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            _CutToolStripMenuItem.Size = new Size(262, 26);
            _CutToolStripMenuItem.Text = "Cut";
            // 
            // CopyToolStripMenuItem
            // 
            _CopyToolStripMenuItem.Image = My.Resources.Resources.Copy;
            _CopyToolStripMenuItem.Name = "_CopyToolStripMenuItem";
            _CopyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            _CopyToolStripMenuItem.Size = new Size(262, 26);
            _CopyToolStripMenuItem.Text = "&Copy";
            // 
            // CopyDataToolStripMenuItem
            // 
            _CopyDataToolStripMenuItem.Image = My.Resources.Resources.CopyData;
            _CopyDataToolStripMenuItem.Name = "_CopyDataToolStripMenuItem";
            _CopyDataToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.C;
            _CopyDataToolStripMenuItem.Size = new Size(262, 26);
            _CopyDataToolStripMenuItem.Text = "&Copy Data";
            // 
            // PasteToolStripMenuItem
            // 
            _PasteToolStripMenuItem.Image = My.Resources.Resources.Paste;
            _PasteToolStripMenuItem.Name = "_PasteToolStripMenuItem";
            _PasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            _PasteToolStripMenuItem.Size = new Size(262, 26);
            _PasteToolStripMenuItem.Text = "&Paste";
            // 
            // DeleteToolStripMenuItem
            // 
            _DeleteToolStripMenuItem.Image = My.Resources.Resources.trash32;
            _DeleteToolStripMenuItem.Name = "_DeleteToolStripMenuItem";
            _DeleteToolStripMenuItem.ShortcutKeyDisplayString = "Delete";
            _DeleteToolStripMenuItem.ShortcutKeys = Keys.Delete;
            _DeleteToolStripMenuItem.Size = new Size(262, 26);
            _DeleteToolStripMenuItem.Text = "Delete";
            // 
            // ToolStripSeparator2
            // 
            _ToolStripSeparator2.Name = "_ToolStripSeparator2";
            _ToolStripSeparator2.Size = new Size(259, 6);
            // 
            // SelectAllToolStripMenuItem
            // 
            _SelectAllToolStripMenuItem.Image = My.Resources.Resources.select16;
            _SelectAllToolStripMenuItem.Name = "_SelectAllToolStripMenuItem";
            _SelectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            _SelectAllToolStripMenuItem.Size = new Size(262, 26);
            _SelectAllToolStripMenuItem.Text = "Select &All";
            // 
            // DeselectAllToolStripMenuItem
            // 
            _DeselectAllToolStripMenuItem.Image = My.Resources.Resources.select16;
            _DeselectAllToolStripMenuItem.Name = "_DeselectAllToolStripMenuItem";
            _DeselectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            _DeselectAllToolStripMenuItem.Size = new Size(262, 26);
            _DeselectAllToolStripMenuItem.Text = "&Desect All";
            // 
            // ToolStripSeparator3
            // 
            _ToolStripSeparator3.Name = "_ToolStripSeparator3";
            _ToolStripSeparator3.Size = new Size(259, 6);
            // 
            // RightToLeftReadingOrderToolStripMenuItem
            // 
            _RightToLeftReadingOrderToolStripMenuItem.CheckOnClick = true;
            _RightToLeftReadingOrderToolStripMenuItem.Image = My.Resources.Resources.Order;
            _RightToLeftReadingOrderToolStripMenuItem.Name = "_RightToLeftReadingOrderToolStripMenuItem";
            _RightToLeftReadingOrderToolStripMenuItem.Size = new Size(262, 26);
            _RightToLeftReadingOrderToolStripMenuItem.Text = "Right to left Reading Order";
            // 
            // BasicComponents
            // 
            Name = "BasicComponents";
            Size = new Size(500, 400);
            _BasicTextBoxContextMenu.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        #region  Constructor 

        public BasicComponents()
        {
            InitializeComponent();
        }

        #endregion

        #region  Events 

        private void BasicTextBoxContextMenu_Refresh(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                RedoToolStripMenuItem.Visible = textBox.CanRedo;
                UndoToolStripMenuItem.Enabled = textBox.CanUndo;
                UndoToolStripMenuItem.Visible = !textBox.CanRedo;
                CutToolStripMenuItem.Enabled = textBox.SelectionLength > 0;
                CopyToolStripMenuItem.Enabled = CutToolStripMenuItem.Enabled;
                CopyDataToolStripMenuItem.Visible = textBox.CanCopyData;
                PasteToolStripMenuItem.Enabled = My.MyProject.Computer.Clipboard.ContainsText();
                DeleteToolStripMenuItem.Enabled = textBox.TextLength > 0;
                SelectAllToolStripMenuItem.Visible = DeleteToolStripMenuItem.Enabled && textBox.TextLength != textBox.SelectionLength;
                DeselectAllToolStripMenuItem.Visible = CutToolStripMenuItem.Enabled;
                RightToLeftReadingOrderToolStripMenuItem.Checked = Conversions.ToBoolean(textBox.RightToLeft);
                BasicTextBoxContextMenu.RightToLeft = textBox.RightToLeft;
            }
        }

        private void UndoRedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Select();
                textBox.Undo();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Select();
                textBox.Cut();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Copy();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void CopyDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.CopyData();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Select();
                textBox.Paste();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Select();
                SendKeys.Send("{BACKSPACE}");
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Select();
                textBox.SelectAll();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void DeselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.Select();
                textBox.DeselectAll();
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        private void RightToLeftReadingOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTextBox textBox = (BasicTextBox)BasicTextBoxContextMenu.SourceControl;
            if (textBox != null)
            {
                textBox.RightToLeft = RightToLeftReadingOrderToolStripMenuItem.Checked ? RightToLeft.Yes : RightToLeft.No;
            }
            BasicTextBoxContextMenu_Refresh(sender, e);
        }

        #endregion

    }

    #endregion

    #region  BasicModule 

    public static class BasicModule
    {

        public readonly static BasicComponents BasicComponents = new BasicComponents();

    }

    #endregion

    #region EditableListView 

    [ToolboxItem(false)]
    public class EditableListView : ListView
    {

        private void InitializeComponent()
        {
            SuspendLayout();
            ResumeLayout(false);

        }
    }

    [DisplayName("Empty")]
    [DesignTimeVisible(false)]
    public class EditableListViewItem : IComponent
    {

        public event EventHandler Disposed;
        private ISite _curISBNSite;

        private string _Name;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if ((value ?? "") != (_Name ?? ""))
                {
                    // TrySetName(value)
                    _Name = value;
                }
            }
        }

        [Browsable(false)]
        public virtual ISite Site
        {
            get
            {
                return _curISBNSite;
            }
            set
            {
                _curISBNSite = value;
            }
        }

        protected void TrySetName(string newName)
        {
            try
            {
                Site.Name = newName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public virtual void Dispose()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return $"Empty: {Name}";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual object Editor
        {
            get
            {
                return null;
            }
        }

    }

    public interface IHasCloneableControl
    {
        Control CloneableControl { get; set; }
    }

    public class EditableListViewItem<T> : EditableListViewItem where T : class, new()
    {

        public override string ToString()
        {
            return $"{typeof(T).Name}: {Name}";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Behavior")]
        public virtual T Control { get; set; } = new T();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Editor
        {
            get
            {
                return Control;
            }
        }
    }

    public class EditableListViewItemControl<T> : EditableListViewItem<T>, IHasCloneableControl where T : Control, new()
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Name
        {
            get
            {
                return Control.Name;
            }
            set
            {
                Control.Name = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private Control CloneableControl
        {
            get
            {
                return Control;
            }
            set
            {
                if (!ReferenceEquals(value, Control))
                {
                    Control = (T)value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        Control IHasCloneableControl.CloneableControl { get => CloneableControl; set => CloneableControl = value; }
    }

    public class EditableListViewCollection : CollectionBase
    {

        public EditableListView Parent { get; private set; }

        public EditableListViewCollection(EditableListView Parent)
        {
            this.Parent = Parent;
        }

        public EditableListViewItem Add(EditableListViewItem item)
        {
            InnerList.Add(item);
            return item;
        }

        public void AddRange(EditableListViewItem[] item)
        {
            InnerList.AddRange(item);
        }

        public void Insert(int index, EditableListViewItem item)
        {
            InnerList.Insert(index, item);
        }

        public void Remove(EditableListViewItem item)
        {
            InnerList.Remove(item);
        }

        public int IndexOf(EditableListViewItem item)
        {
            return InnerList.IndexOf(item);
        }

        public bool Contains(EditableListViewItem item)
        {
            return InnerList.Contains(item);
        }

        public EditableListViewItem this[int index]
        {
            get
            {
                return (EditableListViewItem)InnerList[index];
            }
            set
            {
                InnerList[index] = value;
            }
        }

        public EditableListViewItem[] GetValues()
        {
            EditableListViewItem[] item = new EditableListViewItem[InnerList.Count];
            InnerList.CopyTo(0, item, 0, InnerList.Count);
            return item;
        }

    }

    #endregion

    #region  EditableListViewCollectionEditor 

    public class DisplayTypeDelegator : TypeDelegator
    {

        public DisplayTypeDelegator(Type delegatingType) : base(delegatingType)
        {
        }

        public override string Name
        {
            get
            {
                var subType = typeImpl.IsGenericType ? typeImpl.GenericTypeArguments[0] : null;
                if (subType != null)
                    return subType.Name;
                DisplayNameAttribute attribute = (DisplayNameAttribute)typeImpl.GetCustomAttribute(typeof(DisplayNameAttribute), false);
                return attribute != null ? attribute.DisplayName : typeImpl.Name;
            }
        }
    }

    public class EditableListViewCollectionEditor : UITypeEditor
    {

        public virtual Type EmptyType
        {
            get
            {
                return new DisplayTypeDelegator(typeof(EditableListViewItem));
            }
        }

        public virtual Type[] Types
        {
            get
            {
                return new Type[] { EmptyType };
            }
        }

        public delegate void CollectionChangedEventHandler(object sender, object instance, object value);
        public event CollectionChangedEventHandler CollectionChanged;
        private ITypeDescriptorContext _context;
        private IWindowsFormsEditorService edSvc = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                var originalValue = value;
                _context = context;
                edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (edSvc != null)
                {
                    var form = CreateForm();
                    form.Initialize((EditableListViewCollection)value, Types);
                    form.ItemAdded += new Editor_BasicBaseCollection.InstanceEventHandler(ItemAdded);
                    form.ItemRemoved += new Editor_BasicBaseCollection.InstanceEventHandler(ItemRemoved);

                    context.OnComponentChanging();
                    var result = edSvc.ShowDialog(form);

                    if (result == DialogResult.OK)
                    {
                        OnCollectionChanged(context.Instance, value);
                        context.OnComponentChanged();
                    }
                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }

            return base.GetEditStyle(context);
        }

        private void ItemAdded(object sender, object item)
        {
            if (_context != null && _context.Container != null)
            {
                IComponent icomp = item as IComponent;

                if (icomp != null)
                {
                    _context.Container.Add(icomp);
                }
            }
        }

        private void ItemRemoved(object sender, object item)
        {
            if (_context != null && _context.Container != null)
            {
                IComponent icomp = item as IComponent;

                if (icomp != null)
                {
                    _context.Container.Remove(icomp);
                }
            }
        }

        protected virtual void OnCollectionChanged(object instance, object value)
        {
            CollectionChanged?.Invoke(this, instance, value);
        }

        protected virtual Editor_BasicBaseCollection CreateForm()
        {
            return new Editor_BasicBaseCollection();
        }
    }

    #endregion

    #region  ITextControl 

    public interface ITextControl
    {

        TextBoxBase TextControl { get; }

    }

    #endregion

    #region  BasicUserControl 

    public class ControlProgressArgs : EventArgs
    {

    }

    public delegate void OnControlProgress();

    public interface IBasicUseControl
    {
        void Active(OnControlProgress Progress);
        void Inactive(OnControlProgress Progress);
    }

    public class BasicUserControl : UserControl, IBasicUseControl
    {

        public virtual void Active(OnControlProgress Progress)
        {
            var self = this as Control;
            ControlExtension.SetControl(ref self, true);
            Progress?.Invoke();
        }

        public virtual void Inactive(OnControlProgress Progress)
        {
            var self = this as Control;
            ControlExtension.SetControl(ref self, false);
            Progress?.Invoke();
        }
    }

    #endregion

    #region  BasicForm 

    public class BasicForm : Form, IBasicUseControl
    {

        private const int CP_NOCLOSE_BUTTON = 0x200;

        public BasicForm()
        {
            Normal = Bounds;
            VisibleChanged += OnClose;
        }

        public virtual void Active(OnControlProgress Progress)
        {
            var self = this as Control;
            ControlExtension.SetControl(ref self, true);
            Progress?.Invoke();
        }

        public virtual void Inactive(OnControlProgress Progress)
        {
            var self = this as Control;
            ControlExtension.SetControl(ref self, false);
            Progress?.Invoke();
        }

        private Rectangle _Normal = new Rectangle();
        public Rectangle Normal
        {
            get
            {
                return _Normal;
            }
            set
            {
                _Normal = value;
            }
        }

        private bool _CloseBox = true;
        [Category("Window Style")]
        [Description("")]
        [DefaultValue(true)]
        public bool CloseBox
        {
            get
            {
                return _CloseBox;
            }
            set
            {
                _CloseBox = value;
            }
        }

        protected event EventHandler<EventArgs> MdiChildClose;

        public void OnMdiChildClose(object sender)
        {
            MdiChildClose?.Invoke(sender, new EventArgs());
        }

        protected void OnClose(object sender, EventArgs e)
        {
            if (!Visible & IsMdiChild)
            {
                BasicForm parent = (BasicForm)MdiParent;
                if (parent != null)
                {
                    parent.OnMdiChildClose(this);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (WindowState == FormWindowState.Normal)
                Normal = Bounds;
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            if (WindowState == FormWindowState.Normal)
                Normal = Bounds;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_RESTORE = 0xF120;
            const int SC_MINIMIZE = 0xF020;
            const int SC_MAXIMIZE = 0xF030;

            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case SC_RESTORE:
                        {
                            break;
                        }
                    case SC_MINIMIZE:
                        {
                            Normal = Bounds;
                            break;
                        }
                    case SC_MAXIMIZE:
                        {
                            break;
                        }

                }
            }

            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var myCp = base.CreateParams;
                if (CloseBox == false)
                    myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

    }

    #endregion

    #region  BasicComponent 

    public abstract class BasicComponent : Component
    {

        protected internal Form DialogBox = new Form();
        public virtual DialogResult ShowDialog(Form ParentOwner = null)
        {
            // Custom code goes here
            return DialogBox.ShowDialog(ParentOwner);
        }


        #region  Component Designer Code 

        public BasicComponent() : base()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private IContainer components;

        private void InitializeComponent()
        {
            components = new Container();
        }

        #endregion

    }

    #endregion

    #region  BasicColorSelector 

    public class BasicColorSelector : BasicComponent
    {

        private bool _ShortcutKeys = false;
        [DefaultValue(false)]
        public bool ShortcutKeys
        {
            get
            {
                return _ShortcutKeys;
            }
            set
            {
                if (_ShortcutKeys != value)
                {
                    _ShortcutKeys = value;
                }
            }
        }

        private Color _OldColor = Color.Black;
        [DefaultValue(typeof(Color), "0, 0, 0")]
        public Color OldColor
        {
            get
            {
                return _OldColor;
            }
            set
            {
                if (_OldColor != value)
                {
                    _OldColor = value;
                }
            }
        }

        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Color NewColor
        {
            get
            {
                return DialogBox.NewColor;
            }
        }

        protected internal new Editor_BasicColorSelector DialogBox = new Editor_BasicColorSelector();
        public override DialogResult ShowDialog(Form ParentOwner = null)
        {
            DialogBox.ShortcutKeys = ShortcutKeys;
            DialogBox.OldColor = OldColor;
            DialogBox.Setup();

            return DialogBox.ShowDialog(ParentOwner);
        }

    }

    #endregion

    #region  BasicCheckbox 

    public class BasicCheckBox : CheckBox
    {


    }

    #endregion

    #region  BasicButton 

    public class BasicButton : Button
    {

        public Timer Timer = new Timer();
        public int Length;

        #region  Constructor 

        public BasicButton()
        {
            Timer.Tick += OnTimer;
            Timer.Enabled = false;
            MouseUp += Me_MouseUp;
            MouseDown += Me_MouseDown;
        }

        #endregion

        #region  Properties 

        private int _Interval = -1;
        [Category("Function")]
        [Description("Changes the length of time (in Milliseconds) before the button is reactivated, -1 will turn off this functionality")]
        [DefaultValue(-1)]
        public int Interval
        {
            get
            {
                return _Interval;
            }
            set
            {
                _Interval = value;
                Timer.Interval = _Interval;
            }
        }

        private bool _Accelerate = false;
        [Category("Function")]
        [Description("Increases the speed at which the button will fire over time")]
        [DefaultValue(false)]
        public bool Accelerate
        {
            get
            {
                return _Accelerate;
            }
            set
            {
                _Accelerate = value;
            }
        }

        #endregion

        #region  Events 

        private void Me_MouseUp(object sender, MouseEventArgs e)
        {
            if (Interval < 0)
                return;
            // turn off the timer
            Timer.Enabled = false;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            if (Interval < 0)
                return;
            // store the length of time the button has repeated
            Length += 1;
            if (Accelerate == true)
                Timer.Interval = Math.Max(Timer.Interval - 1, 1);
            // fire off a click on each timer tick
            OnClick(EventArgs.Empty);
        }

        #endregion

        #region  Private 

        private void Me_MouseDown(object sender, MouseEventArgs e)
        {
            if (Interval < 0)
                return;
            // turn on the timer
            Timer.Enabled = true;
            Timer.Interval = _Interval;
            Length = 0;
        }

        #endregion



    }

    #endregion

    #region  BasicTextBox 

    public class BasicTextBox : TextBox, ITextControl
    {

        private int HighlightedText = 0;

        #region  Constructor 

        public BasicTextBox()
        {
            ContextMenuStrip = BasicModule.BasicComponents.BasicTextBoxContextMenu;
            MouseUp += Me_TextSelected;
            TextChanged += Me_TextChanged;
            Leave += Me_LeaveControl;
            KeyUp += Me_KeyDown;
            SizeChanged += Me_SizeChanged;
        }

        #endregion

        #region  Properties 

        public event EventHandler TextChangeCompleted;
        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit it's contents.")]
        [DefaultValue(false)]
        public bool AutoSizeWidth { get; set; } = false;

        // Broken, unfortunately when it's turned on it breaks word wrapping :(
        [Category("Layout")]
        [Description("Specifies whether a control will automatically display scrollbars.")]
        [DefaultValue(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool AutoScrollbars { get; set; } = false;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBoxBase TextControl
        {
            get
            {
                return this;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual int VisibleWidth
        {
            get
            {
                return Width;
            }
        }

        private bool _CanRedo = false;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool CanRedo
        {
            get
            {
                return _CanRedo;
            }
            protected set
            {
                if (_CanRedo != value)
                {
                    _CanRedo = value;
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool CanCopyData { get; private set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Appearance")]
        [Description("Determines whether this textbox will has a styling preference.")]
        [DefaultValue(BasicTextBoxStyle.Standard)]
        public BasicTextBoxStyle Style
        {
            get
            {
                if (ReadOnly && !TabStop && BorderStyle == BorderStyle.None)
                    return BasicTextBoxStyle.Label;

                return BasicTextBoxStyle.Standard;
            }
            set
            {
                switch (value)
                {
                    case BasicTextBoxStyle.Label:
                        ReadOnly = true;
                        TabStop = false;
                        BorderStyle = BorderStyle.None;
                        AutoSize = false;
                        break;
                    default:
                        ReadOnly = false;
                        TabStop = true;
                        BorderStyle = BorderStyle.Fixed3D;
                        AutoSize = true;
                        break;
                }
            }
        }

        #endregion

        #region  Events 

        private void Me_TextSelected(object sender, MouseEventArgs e)
        {
            if (HighlightedText != SelectedText.Count())
            {
                if (HighlightedText < SelectedText.Count())
                {
                    if (SelectedText.Count() > 0)
                        BasicProgram.RaiseSelectedText(this);
                }
                else if (SelectedText.Count() == 0)
                    BasicProgram.RaiseDeselectedText(this);
                HighlightedText = SelectedText.Count();
            }
        }

        private void Me_TextChanged(object sender, EventArgs e)
        {
            if (AutoSizeWidth)
            {
                var g = CreateGraphics();
                Width = (int)Math.Round(g.MeasureString(Text, Font).Width + 10f);
                g.Dispose();
            }
            Me_SizeChanged(sender, e);
        }

        private void Me_LeaveControl(object sender, EventArgs e)
        {
            PerformTextChangeCompleted();
        }

        private void Me_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformTextChangeCompleted();
            }
        }

        private void Me_SizeChanged(object sender, EventArgs e)
        {
            if (AutoScrollbars)
            {
                var textBoxRect = TextRenderer.MeasureText(Text, Font, new Size(VisibleWidth, int.MaxValue), TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
                ScrollBarVisibility oldScrollbars = (ScrollBarVisibility)ScrollBars;
                ScrollBars = textBoxRect.Height > Height ? ScrollBars.Vertical : ScrollBars.None;
                if ((int)oldScrollbars != (int)ScrollBars)
                {
                    Invalidate();
                }
            }
        }

        #endregion

        #region  Public 

        public void PerformTextChangeCompleted()
        {
            TextChangeCompleted?.Invoke(this, new EventArgs());
        }

        public new void Undo()
        {
            CanRedo = !CanRedo;
            base.Undo();
        }

        public virtual void CopyData()
        {
            // Base implementation does nothing, up to derived class to implement.
        }

        #endregion

    }

    public enum BasicTextBoxStyle
    {
        Standard,
        Label
    }

    #endregion

    #region IBasicItemCollector

    public interface IBasicItemCollector
    {
        event EventHandler<EventArgs> OnItemsChanged;
    }

    #endregion

    #region  BasicComboBox 

    public class BasicComboBox : ComboBox
    {
        private bool Restocked = false;

        #region  Constructor 

        protected override void InitLayout()
        {
            // InitLayout is run after we are added to a control via InitializeComponent()
            base.InitLayout();

            if (InitialSelection != SelectedIndex)
                SelectedIndex = Math.Max(Math.Min(InitialSelection, Items.Count - 1), -1);

            DisplayMember = "Text";
            ValueMember = "Value";

            if (DesignMode == false & !string.IsNullOrEmpty(Parent.Name))
            {
                RestockItems();
            }
        }

        public BasicComboBox() : base()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            _Items = new BasicComboBoxItemCollection(this);
        }

        #endregion

        #region  Properties 

        private int _InitialSelection = -1;
        /// <summary>
        /// Which item should initially be selected?
        /// </summary>
        [Description("Determines what item should be initially selected.")]
        [Category("Data")]
        [DefaultValue(-1)]
        public int InitialSelection
        {
            get
            {
                return _InitialSelection;
            }
            set
            {
                if (value != _InitialSelection)
                {
                    if (DesignMode == true & Items.Count > 0)
                    {
                        _InitialSelection = Math.Max(Math.Min(value, Items.Count - 1), -1);
                    }
                    else
                    {
                        _InitialSelection = value;
                    }
                    if (DesignMode == true & Items.Count > 0)
                        SelectedIndex = InitialSelection;
                }
            }
        }

        private BasicComboBoxItemCollection _Items;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new BasicComboBoxItemCollection Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
            }
        }

        // The original items that the user will never see.
        private ObjectCollection baseItems
        {
            get
            {
                return base.Items;
            }
        }

        private string _ItemCollection = "";
        [DefaultValue("")]
        public string ItemCollection
        {
            get
            {
                return _ItemCollection;
            }
            set
            {
                if ((_ItemCollection ?? "") != (value ?? ""))
                {
                    _ItemCollection = value;
                }
            }
        }

        private ReferenceType _ItemCollectionType = ReferenceType.Classed;
        [DefaultValue(ReferenceType.Classed)]
        public ReferenceType ItemCollectionType
        {
            get
            {
                return _ItemCollectionType;
            }
            set
            {
                if (_ItemCollectionType != value)
                {
                    _ItemCollectionType = value;
                }
            }
        }

        private bool _ValueDisplay = false;
        [Description("Determines if the index number of each item should be displayed.")]
        [DefaultValue(false)]
        public bool ValueDisplay
        {
            get
            {
                return _ValueDisplay;
            }
            set
            {
                if (_ValueDisplay != value)
                {
                    _ValueDisplay = value;
                    Invalidate();
                }
            }
        }

        private string _StartIndex = "&H000000";
        [Description("Determines the starting index for each item displayed.")]
        [DefaultValue("&H000000")]
        public string StartIndex
        {
            get
            {
                return _StartIndex;
            }
            set
            {
                if ((_StartIndex ?? "") != (value ?? ""))
                {
                    _StartIndex = value;
                    Invalidate();
                }
            }
        }

        private bool _ShowImages = false;
        [Description("Determines whether images be shown, by default.")]
        [DefaultValue(false)]
        public bool ShowImages
        {
            get
            {
                return _ShowImages;
            }
            set
            {
                if (_ShowImages != value)
                {
                    _ShowImages = value;
                    Invalidate();
                }
            }
        }

        private string _NullValue = "";
        [Description("Determines whether a null value should be added to the list of items.")]
        [DefaultValue("")]
        public string NullValue
        {
            get
            {
                return _NullValue;
            }
            set
            {
                if ((_NullValue ?? "") != (value ?? ""))
                {
                    _NullValue = value;
                    Invalidate();
                }
            }
        }

        private ContentAlignment _TextAlign = ContentAlignment.MiddleLeft;
        [Description("Determine how the content will be drawn")]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                if (_TextAlign != value)
                {
                    _TextAlign = value;
                    Invalidate();
                }
            }
        }

        private string _ValueField = "";
        [Description("Determine the field which will be used for binding the value.")]
        [DefaultValue("")]
        public string ValueField
        {
            get
            {
                return _ValueField;
            }
            set
            {
                if (_ValueField != value)
                {
                    _ValueField = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region  Draw 

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // MyBase.OnDrawItem(e)

            if (DesignMode && Items.Count == 0)
                return;

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                var item = Items[e.Index];
                if (item != null)
                {
                    if (ShowImages == true & item.Image != null)
                    {
                        // Draw the image
                        e.Graphics.DrawImage(item.Image, e.Bounds.X, e.Bounds.Y, ItemHeight, ItemHeight);
                    }
                }
                // Draw the item text
                if (Enabled == false | item.Enabled == false)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                    if (item != null)
                        DrawItemText(e, SystemBrushes.GrayText);
                }
                else if ((int)e.State == (int)DrawItemState.Selected + (int)DrawItemState.Focus + (int)DrawItemState.NoAccelerator + (int)DrawItemState.NoFocusRect)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                    // e.DrawFocusRectangle()
                    if (item != null)
                        DrawItemText(e, SystemBrushes.HighlightText);
                }
                else
                {
                    e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                    if (item != null)
                        DrawItemText(e);
                }
            }
        }

        protected object GetFormattedText(string text)
        {
            var formattedText = Conversions.ToInteger(text).ToString(FormatString);
            if (int.TryParse(Regex.Match(FormatString, @"\d+").Value, out var length))
            {
                formattedText = formattedText.Substring(Math.Max(formattedText.Length - length, 0), length);
            }
            return formattedText;
        }

        public object GetValueText(BasicComboBoxItem item)
        {
            if (FormattingEnabled == true)
            {
                return Operators.ConcatenateObject(Operators.ConcatenateObject(GetFormattedText(item.Value), ": "), item.Text);
            }
            else
            {
                return item.Value + ": ";
            }
        }

        protected object GetValueText(int index)
        {
            var item = Items[index];

            return GetValueText(item);
        }

        private void DrawItemText(DrawItemEventArgs e, Brush style = null)
        {
            var item = Items[e.Index];
            string displayText = item.Text;

            if (ValueDisplay == true)
            {
                displayText = Conversions.ToString(GetValueText(e.Index));
            }

            float x = default, y = default;
            var textSize = e.Graphics.MeasureString(displayText, Font);
            float w = textSize.Width;
            float h = textSize.Height;

            var bounds = e.Bounds;
            // If we are showing images, make some room for them and adjust the bounds width.
            if (ShowImages == true)
            {
                bounds.X += ItemHeight;
                bounds.Width -= ItemHeight;
            }

            // Depending on which TextAlign is chosen, determine the x and y position of the text.
            switch (TextAlign)
            {
                case ContentAlignment.BottomCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.BottomLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.BottomRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.MiddleCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.MiddleLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.MiddleRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.TopCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y;
                        break;
                    }
                case ContentAlignment.TopLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y;
                        break;
                    }
                case ContentAlignment.TopRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y;
                        break;
                    }
            }

            // Finally draw the text.
            if (style == null)
            {
                e.Graphics.DrawString(displayText, Font, new SolidBrush(item.Color), x, y);
            }
            else
            {
                e.Graphics.DrawString(displayText, Font, style, x, y);
            }
        }

        #endregion

        #region  Nested 

        // A collection of BasicComboBoxItems
        public class BasicComboBoxItemCollection : System.Collections.ObjectModel.Collection<BasicComboBoxItem>
        {

            private int HiddenItems = 0;

            #region  Fields 

            // Keep a reference to the HexListBox so we can update its baseItems list
            private BasicComboBox _comboBox;

            #endregion

            #region  Constructor 

            public BasicComboBoxItemCollection()
            {

            }

            public BasicComboBoxItemCollection(BasicComboBox comboBox)
            {
                _comboBox = comboBox;
            }

            #endregion

            #region  Methods 

            public BasicComboBoxItem Add()
            {
                return Add("Index", "0", Color.Black, null);
            }

            public BasicComboBoxItem Add(string text)
            {
                return Add(text, "0", Color.Black, null);
            }

            public BasicComboBoxItem Add(string text, string value)
            {
                return Add(text, value, Color.Black, null);
            }

            public BasicComboBoxItem Add(string text, string value, Color color)
            {
                return Add(text, value, color, null);
            }

            public BasicComboBoxItem Add(string text, string value, Color color, Image img)
            {
                var item = new BasicComboBoxItem(text, value, color, img);
                InsertItem(Items.Count, item);
                return item;
            }

            protected override void ClearItems()
            {
                base.ClearItems();
                _comboBox.baseItems.Clear();
            }

            protected override void InsertItem(int index, BasicComboBoxItem item)
            {
                item.Index = index + HiddenItems;
                if (_comboBox.DesignMode == false & item.Visible == false)
                {
                    HiddenItems += 1;
                    return;
                }
                if (item.Text == "Index")
                    item.Text = item.Text + string.Format("{0:0#}", Items.Count);
                base.InsertItem(index, item);
                _comboBox.baseItems.Insert(index, item.Value);
                // BUG: AlwaysSelected
                // If Items.Count > 0 And _comboBox.AlwaysSelected = True Then _comboBox.SelectedIndex = 0
            }

            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);
                _comboBox.baseItems.RemoveAt(index);
            }

            public int GetValue(string Value)
            {
                int index = 0;
                int item_value = Conversions.ToInteger(Value);
                foreach (BasicComboBoxItem Item in _comboBox.Items)
                {
                    if (Conversions.ToInteger(Item.Value) == item_value)
                        return index;
                    index += 1;
                }
                return -1;
            }

            protected override void SetItem(int index, BasicComboBoxItem item)
            {
                base.SetItem(index, item);
                _comboBox.baseItems[index] = item;
            }

            public void AddRange(IEnumerable<BasicComboBoxItem> items)
            {
                foreach (BasicComboBoxItem item in items)
                    InsertItem(Items.Count, item);
            }

            #endregion

        }

        #endregion

        #region  Private 



        #endregion

        #region  Public 

        public static Dictionary<string, Tuple<IBasicItemCollector, List<BasicComboBoxItem>>> CachedOptions = 
            new Dictionary<string, Tuple<IBasicItemCollector, List<BasicComboBoxItem>>>();

        public virtual void RestockItems(bool Clear = false)
        {
            if (Clear == false)
            {
                if (Restocked == false)
                    Restocked = true;
                else
                    return;
            }
            else
            {
                Items.Clear();
            }

            if (!string.IsNullOrEmpty(ItemCollection))
            {
                if (!CachedOptions.ContainsKey(ItemCollection))
                {
                    string[] collections = ItemCollection.Split(',');
                    IBasicItemCollector cachedCollector = null;
                    var cachedCollection = new List<BasicComboBoxItem>();
                    foreach (string collection in collections)
                    {
                        dynamic obj = null;
                        if (ItemCollectionType == ReferenceType.Classed)
                        {
                            obj = this.GetControlByName(collection);
                        }
                        else
                        {
                            obj = this.GetControlByFullName(collection);
                        }
                        if (obj == null)
                            throw new Exception("Invalid ComboBox ItemCollection Reference.");
                        if(obj is IBasicItemCollector listBox)
                        {
                            cachedCollector = listBox;
                            listBox.OnItemsChanged += Me_ItemCollectionChanged;
                        }
                        int lastIndex = Items.Count > 0 ? Conversions.ToInteger(Items.Last().Value) + 1 : Conversions.ToInteger(StartIndex);
                        foreach (dynamic Item in (IEnumerable)DynamicUtility.SafeAccess(() => obj.Items, null))
                        {
                            var value = ValueField == "HexOffset" && !string.IsNullOrEmpty(Item.HexOffset) ? Item.HexOffset : Item.Value;
                            int index = Conversions.ToInteger(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(value, Constants.vbNullString, false)) ? Item.Index : value);
                            var cbItem = new BasicComboBoxItem(Conversions.ToString(Item.Text), (lastIndex + index).ToString());
                            cbItem.Enabled = Conversions.ToBoolean(Item.Enabled);
                            cbItem.Visible = Conversions.ToBoolean(Item.Visible);
                            cbItem.Strings = (StringCollection)Item.Strings;
                            Items.Add(cbItem);
                            cachedCollection.Add(cbItem);
                        }
                    }
                    CachedOptions.Add(ItemCollection, 
                        new Tuple<IBasicItemCollector, List<BasicComboBoxItem>>(cachedCollector, cachedCollection));
                }
                else
                {
                    var cachedData = CachedOptions[ItemCollection];
                    if (cachedData.Item1 != null)
                    {
                        cachedData.Item1.OnItemsChanged += Me_ItemCollectionChanged;
                    }
                    Items.AddRange(cachedData.Item2);
                }
            }

            if (!string.IsNullOrEmpty(NullValue))
            {
                var extraItem = new BasicComboBoxItem("Null", Conversions.ToInteger(NullValue).ToString());
                Items.Add(extraItem);
            }
        }

        private void Me_ItemCollectionChanged(object sender, EventArgs args)
        {
            var selectedIndex = SelectedIndex;
            CachedOptions.Remove(ItemCollection);
            RestockItems(true);
            SelectedIndex = selectedIndex;
        }

        #endregion

    }

    #endregion

    #region  BasicComboBoxItem 

    // An item that is added to the BasicComboBox
    public class BasicComboBoxItem
    {

        public int Index = 0;

        #region  Constructors 

        public BasicComboBoxItem() : this("Index", "", Color.Black, null)
        {
        }

        public BasicComboBoxItem(string text) : this(text, "", Color.Black, null)
        {
        }

        public BasicComboBoxItem(string text, string value) : this(text, value, Color.Black, null)
        {
        }

        public BasicComboBoxItem(string text, string value, Color color) : this(text, value, color, null)
        {
        }

        public BasicComboBoxItem(string text, string value, Color color, Image img) : base()
        {
            Text = text;
            Value = value;
            Color = color;
            Image = img;
            Enabled = true;
            Visible = true;
        }

        #endregion

        #region  Properties 

        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private string _Value;
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private StringCollection _Strings = new StringCollection();
        [Editor(BasicConstants.StringCollectionEditor, typeof(UITypeEditor))]
        public StringCollection Strings
        {
            get
            {
                return _Strings;
            }
            set
            {
                _Strings = value;
            }
        }

        private Color _Color;
        public Color Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }

        private Image _Image;
        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }

        private bool _Enabled;
        [Description("Determines whether this item is usable.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }

        private bool _Visible;
        [Description("Determines whether this item is visible.")]
        [DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
            }
        }

        #endregion

    }

    #endregion

    #region  BasicListBoxAssociate 

    public interface IBasicListBoxAssociate
    {

        ListBox ListBox { get; set; }

    }

    public class BasicListBoxAssociate : GroupBox, IBasicListBoxAssociate
    {

        #region  Properties 

        private bool _UserVisible = true;
        [Category("Appearance")]
        [Description("Determines whether this control will be visible to the end-user.")]
        [DefaultValue(true)]
        public bool UserVisible
        {
            get
            {
                return _UserVisible;
            }
            set
            {
                if (_UserVisible != value)
                {
                    _UserVisible = value;
                }
            }
        }

        private ListBox _ListBox = null;
        [Category("Behavior")]
        [Description("Associates all the controls contained in this control with the selected ListBox.")]
        [DefaultValue(default(string))]
        public ListBox ListBox
        {
            get
            {
                return _ListBox;
            }
            set
            {
                if (!ReferenceEquals(_ListBox, value))
                {
                    _ListBox = value;
                    Invalidate();
                    OnListBoxChanged();
                }
            }
        }

        protected virtual void OnListBoxChanged() { }

        #endregion

        #region  Public 

        [Browsable(false)]
        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public override string Text
        {
            get
            {
                return ListBox != null & DesignMode == true ? ListBox.Name : default;
            }
            set
            {
                // Do Nothing
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (UserVisible == false & DesignMode == false)
                return;
            base.OnPaint(e);
        }

        #endregion

    }

#endregion

    #region  BasicListBox 

    public class BasicListBox : ListBox
    {

        #region  Constructor 

        public BasicListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            _Items = new BasicListBoxItemCollection(this);

            _ShowImages = true;
            _TextAlign = ContentAlignment.MiddleLeft;
        }

        #endregion

        #region  Properties 

        private BasicListBoxItemCollection _Items;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new BasicListBoxItemCollection Items
        {
            get
            {
                return _Items;
            }
        }

        // The original items that the user will never see.
        private ObjectCollection baseItems
        {
            get
            {
                return base.Items;
            }
        }

        public new BasicListBoxItem SelectedItem
        {
            get
            {
                return (BasicListBoxItem)base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        public new BasicListBoxSelectedItemCollection SelectedItems
        {
            get
            {
                var items = new BasicListBoxSelectedItemCollection();
                foreach (object item in base.SelectedItems)
                    items.Add((BasicListBoxItem)item);
                return items;
            }
        }

        private bool _ShowImages;
        [DefaultValue(true)]
        public bool ShowImages
        {
            get
            {
                return _ShowImages;
            }
            set
            {
                if (_ShowImages != value)
                {
                    _ShowImages = value;
                    Invalidate();
                }
            }
        }

        private ContentAlignment _TextAlign;
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                if (_TextAlign != value)
                {
                    _TextAlign = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region  Private 

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            // Draw original background and selection.
            // You can remove this and draw your own background if you want.
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                var item = Items[e.Index];
                if (item != null)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    if (ShowImages && item.Image != null)
                    {
                        // Draw the image
                        e.Graphics.DrawImage(item.Image, e.Bounds.X, e.Bounds.Y, ItemHeight, ItemHeight);
                    }
                }

                // Draw the item text
                DrawItemText(e, item);
            }
        }

        private void DrawItemText(DrawItemEventArgs e, BasicListBoxItem item)
        {
            float x = default, y = default;
            var textSize = e.Graphics.MeasureString(item.Text, Font);
            float w = textSize.Width;
            float h = textSize.Height;
            var bounds = e.Bounds;

            // If we are showing images, make some room for them and adjust the bounds width.
            if (ShowImages)
            {
                bounds.X += ItemHeight;
                bounds.Width -= ItemHeight;
            }

            // Depending on which TextAlign is chosen, determine the x and y position of the text.
            switch (TextAlign)
            {
                case ContentAlignment.BottomCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.BottomLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.BottomRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.MiddleCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.MiddleLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.MiddleRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.TopCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y;
                        break;
                    }
                case ContentAlignment.TopLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y;
                        break;
                    }
                case ContentAlignment.TopRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y;
                        break;
                    }
            }

            // Finally draw the text.
            e.Graphics.DrawString(item.Text, Font, new SolidBrush(item.Color), x, y);
        }

        #endregion

        #region  Nested classes 

        // A collection of BasicListBoxItems
        public class BasicListBoxItemCollection : System.Collections.ObjectModel.Collection<BasicListBoxItem>
        {

            #region  Fields 

            // Keep a reference to the BasicListBox so we can update its baseItems list
            private BasicListBox _listBox;

            #endregion

            #region  Constructor 

            public BasicListBoxItemCollection(BasicListBox listBox)
            {
                _listBox = listBox;
            }

            #endregion

            #region  Public 

            public BasicListBoxItem Add(string text)
            {
                return Add(text, null, Color.Black, null);
            }

            public BasicListBoxItem Add(string text, string value)
            {
                return Add(text, value, Color.Black, null);
            }

            public BasicListBoxItem Add(string text, string value, Color color)
            {
                return Add(text, value, color, null);
            }

            public BasicListBoxItem Add(string text, string value, Color color, Image img)
            {
                var item = new BasicListBoxItem(text, value, color, img);
                InsertItem(Items.Count, item);
                return item;
            }

            protected override void ClearItems()
            {
                base.ClearItems();
                _listBox.baseItems.Clear();
            }

            protected override void InsertItem(int index, BasicListBoxItem item)
            {
                base.InsertItem(index, item);
                _listBox.baseItems.Insert(index, item);
            }

            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);
                _listBox.baseItems.RemoveAt(index);
            }

            protected override void SetItem(int index, BasicListBoxItem item)
            {
                base.SetItem(index, item);
                _listBox.baseItems[index] = item;
            }

            public void AddRange(IEnumerable<BasicListBoxItem> items)
            {
                foreach (BasicListBoxItem item in items)
                    InsertItem(Items.Count, item);
            }

            #endregion

        }

        // A collection containing the selected items
        public class BasicListBoxSelectedItemCollection : System.Collections.ObjectModel.Collection<BasicListBoxItem>
        {
        }

        #endregion

    }

    #endregion

    #region  BasicListBoxItem 

    // An item that is added to the BasicListBox
    public class BasicListBoxItem
    {

        #region  Constructors 

        public BasicListBoxItem() : this(null, "New item", null, Color.Black, null) { }

        public BasicListBoxItem(string text) : this(text, null, Color.Black, null) { }

        public BasicListBoxItem(object tag, string text) : this(tag, text, null, Color.Black, null) { }

        public BasicListBoxItem(string text, Color color) : this(null, text, null, color, null) { }

        public BasicListBoxItem(string text, string value) : this(null, text, value, Color.Black, null) { }

        public BasicListBoxItem(string text, string value, Color color, Image img) : this(null, text, value, color, img) { }

        public BasicListBoxItem(object tag, string text, string value, Color color, Image img)
        {
            Tag = tag;
            Text = text;
            Value = value;
            Color = color;
            Image = img;
        }

        #endregion

        #region  Properties 

        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private Color _Color;
        public Color Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }

        private Image _Image;
        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }

        private string _Value = "";
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public object Tag { get; set; }

        private StringCollection _Strings = new StringCollection();
        [Editor(BasicConstants.StringCollectionEditor, typeof(UITypeEditor))]
        public StringCollection Strings
        {
            get
            {
                return _Strings;
            }
            set
            {
                _Strings = value;
            }
        }

        #endregion

    }

    #endregion

    #region  BasicTabControlVertical 

    public class BasicTabControlVertical : TabControl
    {

        public BasicTabControlVertical()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(44, 136);
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            Alignment = TabAlignment.Left;
        }

        public Pen ToPen(Color color)
        {
            return new Pen(color);
        }

        public Brush ToBrush(Color color)
        {
            return new SolidBrush(color);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var B = new Bitmap(Width, Height);
            var G = Graphics.FromImage(B);
            try
            {
                SelectedTab.BackColor = Color.White;
            }
            catch
            {
            }
            G.Clear(Color.White);
            G.FillRectangle(new SolidBrush(Color.FromArgb(246, 248, 252)), new Rectangle(0, 0, ItemSize.Height + 4, Height));
            // G.DrawLine(New Pen(Color.FromArgb(170, 187, 204)), New Point(Width - 1, 0), New Point(Width - 1, Height - 1))    'comment out to get rid of the borders
            // G.DrawLine(New Pen(Color.FromArgb(170, 187, 204)), New Point(ItemSize.Height + 1, 0), New Point(Width - 1, 0))                   'comment out to get rid of the borders
            // G.DrawLine(New Pen(Color.FromArgb(170, 187, 204)), New Point(ItemSize.Height + 3, Height - 1), New Point(Width - 1, Height - 1)) 'comment out to get rid of the borders
            G.DrawLine(new Pen(Color.FromArgb(170, 187, 204)), new Point(ItemSize.Height + 3, 0), new Point(ItemSize.Height + 3, 999));
            for (int i = 0, loopTo = TabCount - 1; i <= loopTo; i++)
            {
                if (i == SelectedIndex)
                {
                    var x2 = new Rectangle(new Point(this.GetTabRect(i).Location.X - 2, this.GetTabRect(i).Location.Y - 2), new Size(this.GetTabRect(i).Width + 3, this.GetTabRect(i).Height - 1));
                    var myBlend = new ColorBlend();
                    myBlend.Colors = new[] { Color.FromArgb(232, 232, 240), Color.FromArgb(232, 232, 240), Color.FromArgb(232, 232, 240) };
                    myBlend.Positions = new[] { 0.0f, 0.5f, 1.0f };
                    var lgBrush = new LinearGradientBrush(x2, Color.Black, Color.Black, 90.0f);
                    lgBrush.InterpolationColors = myBlend;
                    G.FillRectangle(lgBrush, x2);
                    G.DrawRectangle(new Pen(Color.FromArgb(170, 187, 204)), x2);


                    G.SmoothingMode = SmoothingMode.HighQuality;
                    Point[] p = new Point[] { new Point(ItemSize.Height - 3, this.GetTabRect(i).Location.Y + 20), new Point(ItemSize.Height + 4, this.GetTabRect(i).Location.Y + 14), new Point(ItemSize.Height + 4, this.GetTabRect(i).Location.Y + 27) };
                    G.FillPolygon(Brushes.White, p);
                    G.DrawPolygon(new Pen(Color.FromArgb(170, 187, 204)), p);

                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {

                                G.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(x2.Location.X + 8, x2.Location.Y + 6));
                                G.DrawString("      " + TabPages[i].Text, Font, Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                            }
                            else
                            {
                                G.DrawString(TabPages[i].Text, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                            }
                        }
                        catch (Exception ex)
                        {
                            G.DrawString(TabPages[i].Text, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                        }
                    }
                    else
                    {
                        G.DrawString(TabPages[i].Text, new Font(Font.FontFamily, Font.Size, FontStyle.Bold), Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                    }

                    G.DrawLine(new Pen(Color.FromArgb(200, 200, 250)), new Point(x2.Location.X - 1, x2.Location.Y - 1), new Point(x2.Location.X, x2.Location.Y));
                    G.DrawLine(new Pen(Color.FromArgb(200, 200, 250)), new Point(x2.Location.X - 1, x2.Bottom - 1), new Point(x2.Location.X, x2.Bottom));
                }
                else
                {
                    var x2 = new Rectangle(new Point(this.GetTabRect(i).Location.X - 2, this.GetTabRect(i).Location.Y - 2), new Size(this.GetTabRect(i).Width + 3, this.GetTabRect(i).Height + 1));
                    G.FillRectangle(new SolidBrush(Color.FromArgb(246, 248, 252)), x2);
                    G.DrawLine(new Pen(Color.FromArgb(170, 187, 204)), new Point(x2.Right, x2.Top), new Point(x2.Right, x2.Bottom));
                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                G.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(x2.Location.X + 8, x2.Location.Y + 6));
                                G.DrawString("      " + TabPages[i].Text, Font, Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                            }
                            else
                            {
                                G.DrawString(TabPages[i].Text, Font, Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                            }
                        }
                        catch (Exception ex)
                        {
                            G.DrawString(TabPages[i].Text, Font, Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                        }
                    }
                    else
                    {
                        G.DrawString(TabPages[i].Text, Font, Brushes.DimGray, x2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                    }
                }
            }

            e.Graphics.DrawImage(B.Clone() as Image, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion

    #region  BasicTabControl 

    [ToolboxBitmap(typeof(TabControl))]
    [Designer(typeof(Designers.BasicTabControlDesigner))]
    public class BasicTabControl : TabControl
    {

        public event TabControlExEventHandler SelectedIndexChanging;
        public TabPage HotTab = null;

        #region  Constructor 

        public BasicTabControl() : base()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            MouseDown += Me_MouseDown;
            MouseMove += Me_MouseMove;
            DragOver += Me_DragOver;
            DragDrop += Me_DragDrop;

            // Add any initialization after the InitializeComponent() call

        }

        // UserControl1 overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
        }

        #endregion

        #region  Properties 

        [Editor(typeof(BasicTabPageCollection), typeof(UITypeEditor))]
        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
        }

        private bool _Flipped;
        [Category("Appearance")]
        [Description("Determines whether the tab control layout should be flipped.")]
        [DefaultValue(false)]
        public bool Flipped
        {
            get
            {
                return _Flipped;
            }
            set
            {
                if (_Flipped == value)
                    return;
                _Flipped = value;
                UpdateStyles();
            }
        }

        private bool _HideTabs;
        [Category("Appearance")]
        [Description("Determines whether the tab control should allow the user to change tabs.")]
        [DefaultValue(false)]
        public bool HideTabs
        {
            get
            {
                return _HideTabs;
            }
            set
            {
                if (_HideTabs == value)
                    return;
                _HideTabs = value;
                if (value == true)
                    Multiline = true;
                UpdateStyles();
            }
        }

        private ButtonBorderStyle _Border = ButtonBorderStyle.None;
        [Category("Appearance")]
        [Description("Determines what kind of border will surround the content of each page.")]
        [DefaultValue(ButtonBorderStyle.None)]
        public ButtonBorderStyle Border
        {
            get
            {
                return _Border;
            }
            set
            {
                if (_Border == value)
                    return;
                _Border = value;
                UpdateStyles();
            }
        }

        #endregion

        #region  Nested Classes 

        internal class BasicTabPageCollection : CollectionEditor
        {

            public BasicTabPageCollection(Type type) : base(type)
            {
            }

            protected override Type CreateCollectionItemType()
            {
                return typeof(BasicTabPage);
            }

        }

        #endregion

        #region  Private 

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public int HWND;
            public int idFrom;
            public int code;
            public new string ToString()
            {
                return string.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", HWND, idFrom, code);
            }
        }

        private const int TCN_FIRST = default;

        private const int TCN_SELCHANGING = TCN_FIRST - 2;

        private const int WM_USER = 0x400;
        private const int WM_NOTIFY = 0x4E;
        private const int WM_REFLECT = (int) (WM_USER + 0x1C00L);

        private Point DragStartPosition = Point.Empty;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_REFLECT + WM_NOTIFY)
            {
                NMHDR hdr = (NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NMHDR));
                if (hdr.code == TCN_SELCHANGING)
                {
                    if (HotTab != null)
                    {
                        var e = new BasicTabControlEventArgs(HotTab, Controls.IndexOf(HotTab));
                        SelectedIndexChanging?.Invoke(this, e);
                        if (e.Cancel || HotTab.Enabled == false)
                        {
                            m.Result = new IntPtr(1);
                            return;
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                const int WS_EX_LAYOUTRTL = 0x400000;
                const int WS_EX_NOINHERITLAYOUT = 0x100000;
                if (Flipped)
                {
                    cp.ExStyle += WS_EX_LAYOUTRTL | WS_EX_NOINHERITLAYOUT;
                }
                return cp;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (HideTabs)
                {
                    return new Rectangle(0, 0, Width, Height);
                }
                else
                {
                    int tabStripHeight, itemHeight;

                    if (Alignment <= TabAlignment.Bottom)
                    {
                        itemHeight = ItemSize.Height;
                    }
                    else
                    {
                        itemHeight = ItemSize.Width;
                    }

                    if (Appearance == TabAppearance.Normal)
                    {
                        tabStripHeight = 5 + itemHeight * RowCount;
                    }
                    else
                    {
                        tabStripHeight = (3 + itemHeight) * RowCount;
                    }
                    switch (Alignment)
                    {
                        case TabAlignment.Top:
                            {
                                return new Rectangle(4, tabStripHeight, Width - 8, Height - tabStripHeight - 4);
                            }
                        case TabAlignment.Bottom:
                            {
                                return new Rectangle(4, 4, Width - 8, Height - tabStripHeight - 4);
                            }
                        case TabAlignment.Left:
                            {
                                return new Rectangle(tabStripHeight, 4, Width - tabStripHeight - 4, Height - 8);
                            }
                        case TabAlignment.Right:
                            {
                                return new Rectangle(4, 4, Width - tabStripHeight - 4, Height - 8);
                            }
                    }
                }

                return default;
            }
        }

        private void Me_MouseDown(object sender, MouseEventArgs e)
        {
            DragStartPosition = new Point(e.X, e.Y);
        }

        private void Me_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var r = new Rectangle(DragStartPosition, Size.Empty);
            r.Inflate(SystemInformation.DragSize);

            var tp = HoverTab();

            if (tp != null)
            {
                if (!r.Contains(e.X, e.Y))
                {
                    DoDragDrop(tp, DragDropEffects.All);
                }
            }

            DragStartPosition = Point.Empty;
        }

        private void Me_DragOver(object sender, DragEventArgs e)
        {
            var hover_Tab = HoverTab();
            if (hover_Tab == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else if (e.Data.GetDataPresent(typeof(BasicTabPage)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Me_DragDrop(object sender, DragEventArgs e)
        {
            var hover_Tab = HoverTab();
            BasicTabPage drag_tab = (BasicTabPage)e.Data.GetData(typeof(BasicTabPage));

            if (ReferenceEquals(hover_Tab, drag_tab))
                return;

            var TabRect = GetTabRect(TabPages.IndexOf(hover_Tab));
            TabRect.Inflate(-3, -3);
            if (TabRect.Contains(PointToClient(new Point(e.X, e.Y))))
            {
                SwapTabPages(drag_tab, hover_Tab);
                SelectedTab = drag_tab;
            }
        }

        private BasicTabPage HoverTab()
        {
            for (int index = 0, loopTo = TabCount - 1; index <= loopTo; index++)
            {
                if (GetTabRectAbsolute(index).Contains(PointToClient(Cursor.Position)))
                {
                    return (BasicTabPage)TabPages[index];
                }
            }
            return null;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            var imageRect = DisplayRectangle;
            imageRect.Offset(-4, -8);
            imageRect.Width += 8;
            imageRect.Height += 16;
            e.Graphics.FillRectangle(Brushes.White, imageRect);
            ControlPaint.DrawBorder(e.Graphics, imageRect, Color.Gray, ButtonBorderStyle.Solid);

            if(Border != ButtonBorderStyle.None)
            {
                var imageRect2 = DisplayRectangle;
                imageRect2.Offset(-1, -1);
                imageRect2.Width += 2;
                imageRect2.Height += 2;
                ControlPaint.DrawBorder(e.Graphics, imageRect2, Color.Black, Border);
            }

            BasicTabPage drag_tab;
            int index = 0;
            for (int id = 0, loopTo = TabCount - 1; id <= loopTo; id++)
            {
                drag_tab = (BasicTabPage)TabPages[id];
                if (drag_tab.Visible == false & DesignMode == false)
                    continue;
                DrawTabBackground(e.Graphics, index, id);
                index += 1;
            }
        }

        public ArrayList VisibleTabPages()
        {
            var lv_vtb = new ArrayList();
            BasicTabPage drag_tab;
            foreach (TabPage indexed_page in TabPages)
            {
                drag_tab = (BasicTabPage)indexed_page;
                if (drag_tab.Visible == true)
                    lv_vtb.Add(indexed_page);
            }
            return lv_vtb;
        }

        public ArrayList VisibleTabPageIndex()
        {
            var lv_indices = new ArrayList();
            BasicTabPage drag_tab;
            int index = 0;
            foreach (TabPage indexed_page in TabPages)
            {
                drag_tab = (BasicTabPage)indexed_page;
                if (drag_tab.Visible == true)
                    lv_indices.Add(index);
                index += 1;
            }
            return lv_indices;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            BasicTabPage drag_tab;
            int index = 0;
            for (int id = 0, loopTo = TabCount - 1; id <= loopTo; id++)
            {
                drag_tab = (BasicTabPage)TabPages[id];
                if (drag_tab.Visible == false & DesignMode == false)
                    continue;
                DrawTabContent(e.Graphics, index, id);
                index += 1;
            }
        }

        public new Rectangle GetTabRect(int index)
        {
            if (DesignMode == true)
                return GetTabRectAbsolute(index);
            if (index >= this.VisibleTabPages().Count)
                return new Rectangle(new Point(0, 0), new Size(1, 1));
            return GetTabRectAbsolute(index);
        }

        public Rectangle GetTabRectAbsolute(int index)
        {
            return base.GetTabRect(index);
        }

        private int LastSelectedIndex = 0;

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (DesignMode == false & SelectedIndex >= this.VisibleTabPages().Count)
            {
                SelectedIndex = LastSelectedIndex;
            }
            else
            {
                LastSelectedIndex = SelectedIndex;
                base.OnSelectedIndexChanged(e);
            }
        }

        private void DrawTabBackground(Graphics graphics, int index, int id)
        {
            var recttest = GetTabRect(index);
            bool vertical = (int)Alignment >= 2;
            recttest.Height -= 4;
            if (id == SelectedIndex)
            {
                graphics.FillRectangle(Brushes.White, recttest);
            }
            else
            {
                var linGrBrush = new LinearGradientBrush(new Point(0, 0), vertical ? new Point(24, 0) : new Point(0, 24), Color.White, Color.Gray);
                graphics.FillRectangle(linGrBrush, recttest);
            }
            if (Conversions.ToBoolean(FarthestRow(id)))
            {
                ControlPaint.DrawBorder(graphics, recttest, Color.Gray, 1, ButtonBorderStyle.Solid, Color.Gray, 1, ButtonBorderStyle.Solid, Color.Gray, 1, ButtonBorderStyle.Solid, Color.Gray, 0, ButtonBorderStyle.Solid);
            }
            else
            {
                ControlPaint.DrawBorder(graphics, recttest, Color.Gray, 1, ButtonBorderStyle.Solid, Color.Gray, 1, ButtonBorderStyle.Solid, Color.Gray, 0, ButtonBorderStyle.Solid, Color.Gray, 0, ButtonBorderStyle.Solid);
            }
        }

        private object FarthestRow(int id)
        {
            int farthest = 0;
            for (int index = 0, loopTo = TabCount - 1; index <= loopTo; index++)
            {
                if (((BasicTabPage)TabPages[index]).Visible == false)
                    continue;
                if (this.GetTabRect(index).Y != this.GetTabRect(id).Y)
                    continue;
                if (this.GetTabRect(index).X > farthest)
                    farthest = GetTabRect(index).X;
            }
            return this.GetTabRect(id).X >= farthest ? true : false;
        }

        private void DrawTabContent(Graphics graphics, int index, int id)
        {
            bool selectedOrHot = id == SelectedIndex;
            bool vertical = (int)Alignment >= 2;

            Image tabImage = null;

            if (ImageList != null)
            {
                var page = TabPages[index];
                if (page.ImageIndex > -1 && page.ImageIndex < ImageList.Images.Count)
                {
                    tabImage = ImageList.Images[page.ImageIndex];
                }
                if (page.ImageKey.Length > 0 && ImageList.Images.ContainsKey(page.ImageKey))
                {
                    tabImage = ImageList.Images[page.ImageKey];
                }
            }

            var tabRect = GetTabRect(index);
            var contentRect = vertical ? new Rectangle(0 + 5, 0, tabRect.Height, tabRect.Width) : new Rectangle(Point.Empty, tabRect.Size);
            var textrect = contentRect;
            textrect.X += 5;
            if ((int)Alignment == 2)
                textrect.X += 5;
            if ((int)Alignment == 3)
                textrect.X -= 10;
            textrect.Width += 5;
            textrect.Y -= 2;
            textrect.Width -= FontHeight;

            if (tabImage != null)
            {
                textrect.Width -= tabImage.Width;
                textrect.X += tabImage.Width;
            }

            using (var bm = new Bitmap(contentRect.Width, contentRect.Height))
            {
                using (var bmGraphics = Graphics.FromImage(bm))
                {
                    bmGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    TextRenderer.DrawText(bmGraphics, TabPages[id].Text, Font, textrect, SystemColors.ControlText);

                    if (tabImage != null)
                    {
                        var imageRect = new Rectangle(Padding.X, 0, tabImage.Width, tabImage.Height);
                        imageRect.Offset(0, (contentRect.Height - imageRect.Height) / 2);
                        bmGraphics.DrawImage(tabImage, imageRect);
                    }
                }
                if (vertical)
                {
                    if (Alignment == TabAlignment.Left)
                    {
                        bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    else
                    {
                        bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                }
                // If Flipped = True Then
                // bm.RotateFlip(RotateFlipType.RotateNoneFlipX)
                // End If
                graphics.DrawImage(bm, tabRect);
            }

        }

        #endregion

        #region  Public 

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            HotTab = GetHotTab(new Point(e.X, e.Y));
        }


        public void InsertTabPage(TabPage tabpage, int index)
        {

            if (index < 0 | index > TabCount)
            {
                throw new ArgumentException("Index out of Range.");
            }

            TabPages.Add(tabpage);
            if (index < TabCount - 1)
            {
                do
                    SwapTabPages(tabpage, TabPages[TabPages.IndexOf(tabpage) - 1]);
                while (TabPages.IndexOf(tabpage) != index);
            }

            SelectedTab = tabpage;

        }

        public void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            if (TabPages.Contains(tp1) == false | TabPages.Contains(tp2) == false)
            {
                throw new ArgumentException("TabPages must be in the TabCotrols TabPageCollection.");
            }
            int Index1 = TabPages.IndexOf(tp1);
            int Index2 = TabPages.IndexOf(tp2);
            TabPages[Index1] = tp2;
            TabPages[Index2] = tp1;
        }

        private TabPage GetHotTab(Point pt)
        {
            for (int index = 0, loopTo = TabCount - 1; index <= loopTo; index++)
            {
                if (GetTabRectAbsolute(index).Contains(pt.X, pt.Y))
                {
                    return TabPages[index];
                }
            }
            return null;
        }

        #endregion

    }

    #region  SelectedIndexChanging EventArgs 

    public class BasicTabControlEventArgs : EventArgs
    {

        private TabPage m_TabPage = null;
        private int m_TabPageIndex = -1;
        public bool Cancel = false;

        public TabPage TabPage
        {
            get
            {
                return m_TabPage;
            }
        }

        public int TabPageIndex
        {
            get
            {
                return m_TabPageIndex;
            }
        }

        public BasicTabControlEventArgs(TabPage TabPage, int TabPageIndex)
        {
            m_TabPage = TabPage;
            m_TabPageIndex = TabPageIndex;
        }

    }

    public delegate void TabControlExEventHandler(object sender, BasicTabControlEventArgs e);

    #endregion

    #endregion

    #region  BasicTabPage 

    [Designer(typeof(ScrollableControlDesigner))]
    public class BasicTabPage : TabPage
    {

        #region  Constructor 

        public BasicTabPage() : base()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        public BasicTabPage(string Text) : base()
        {
            base.Text = Text;
        }

        // UserControl1 overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
        }

        #endregion

        #region  Properties 

        private bool _Visible = true;
        [Category("Behavior")]
        [Description("Determines whether the tab control should allow the user to view this tab.")]
        [DefaultValue(true)]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public new bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                if (_Visible == value)
                    return;
                _Visible = value;
            }
        }

        #endregion

    }

    #endregion

    #region  BasicTabPageDesigner 

    namespace Designers
    {

        #region BasicTabControl

        internal class BasicTabControlDesigner : ParentControlDesigner
        {

            #region  Private Instance Variables 

            private DesignerVerbCollection m_verbs = new DesignerVerbCollection();
            private IDesignerHost m_DesignerHost;
            private ISelectionService m_SelectionService;

            #endregion

            public BasicTabControlDesigner() : base()
            {

                var verb1 = new DesignerVerb("Add Tab", OnAddPage);
                var verb2 = new DesignerVerb("Insert Tab", OnInsertPage);
                var verb3 = new DesignerVerb("Remove Tab", OnRemovePage);
                m_verbs.AddRange(new DesignerVerb[] { verb1, verb2, verb3 });

            }

            #region  Properties 

            public override DesignerVerbCollection Verbs
            {
                get
                {
                    if (m_verbs.Count == 3)
                    {
                        BasicTabControl MyControl = (BasicTabControl)Control;
                        if (MyControl.TabCount > 0)
                        {
                            m_verbs[1].Enabled = true;
                            m_verbs[2].Enabled = true;
                        }
                        else
                        {
                            m_verbs[1].Enabled = false;
                            m_verbs[2].Enabled = false;
                        }
                    }
                    return m_verbs;
                }
            }

            public IDesignerHost DesignerHost
            {
                get
                {
                    if (m_DesignerHost == null)
                    {
                        m_DesignerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
                    }
                    return m_DesignerHost;
                }
            }

            public ISelectionService SelectionService
            {
                get
                {
                    if (m_SelectionService == null)
                    {
                        m_SelectionService = (ISelectionService)GetService(typeof(ISelectionService));
                    }
                    return m_SelectionService;
                }
            }

            #endregion

            public void OnAddPage(object sender, EventArgs e)
            {

                BasicTabControl ParentControl = (BasicTabControl)Control;
                var oldTabs = ParentControl.Controls;

                RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

                BasicTabPage P = (BasicTabPage)DesignerHost.CreateComponent(typeof(BasicTabPage));
                P.Text = P.Name;
                ParentControl.TabPages.Add(P);

                RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);
                ParentControl.SelectedTab = P;

                SetVerbs();

            }

            public void OnInsertPage(object sender, EventArgs e)
            {

                BasicTabControl ParentControl = (BasicTabControl)Control;
                var oldTabs = ParentControl.Controls;
                int Index = ParentControl.SelectedIndex;

                RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

                BasicTabPage P = (BasicTabPage)DesignerHost.CreateComponent(typeof(BasicTabPage));
                P.Text = P.Name;

                var tpc = new TabPage[ParentControl.TabCount + 1];
                // Starting at our Insert Position, store and remove all the tabpages.
                for (int i = Index, loopTo = ParentControl.TabCount - 1; i <= loopTo; i++)
                {
                    tpc[i] = ParentControl.TabPages[Index];
                    ParentControl.TabPages.Remove(ParentControl.TabPages[Index]);
                }
                // add the tabpage to be inserted.
                ParentControl.TabPages.Add(P);
                // then re-add the original tabpages.
                for (int i = Index, loopTo1 = Information.UBound(tpc) - 1; i <= loopTo1; i++)
                    ParentControl.TabPages.Add(tpc[i]);

                RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);
                ParentControl.SelectedTab = P;

                SetVerbs();

            }

            public void OnRemovePage(object sender, EventArgs e)
            {

                BasicTabControl ParentControl = (BasicTabControl)Control;
                var oldTabs = ParentControl.Controls;

                if (ParentControl.SelectedIndex < 0)
                    return;

                RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

                DesignerHost.DestroyComponent(ParentControl.TabPages[ParentControl.SelectedIndex]);

                RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"], oldTabs, ParentControl.TabPages);

                SelectionService.SetSelectedComponents(new IComponent[] { ParentControl }, SelectionTypes.Auto);

                SetVerbs();

            }

            private void SetVerbs()
            {

                BasicTabControl ParentControl = (BasicTabControl)Control;

                switch (ParentControl.TabPages.Count)
                {
                    case 0:
                        {
                            Verbs[1].Enabled = false;
                            Verbs[2].Enabled = false;
                            break;
                        }
                    case 1:
                        {
                            Verbs[1].Enabled = false;
                            Verbs[2].Enabled = true;
                            break;
                        }

                    default:
                        {
                            Verbs[1].Enabled = true;
                            Verbs[2].Enabled = true;
                            break;
                        }
                }

            }

            private const int WM_NCHITTEST = 0x84;

            private const int HTTRANSPARENT = -1;
            private const int HTCLIENT = 1;

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_NCHITTEST)
                {
                    // select tabcontrol when Tabcontrol clicked outside of TabItem.
                    if (m.Result.ToInt32() == HTTRANSPARENT)
                    {
                        m.Result = (IntPtr)HTCLIENT;
                    }
                }

            }

            private enum TabControlHitTest
            {
                TCHT_NOWHERE = 1,
                TCHT_ONITEMICON = 2,
                TCHT_ONITEMLABEL = 4,
                TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
            }

            private const int TCM_HITTEST = 0x130D;

            private struct TCHITTESTINFO
            {
                public Point pt;
                public TabControlHitTest flags;
            }

            protected override bool GetHitTest(Point point)
            {

                if (ReferenceEquals(SelectionService.PrimarySelection, Control))
                {
                    var hti = new TCHITTESTINFO();

                    hti.pt = Control.PointToClient(point);

                    var m = new Message();
                    m.HWnd = Control.Handle;
                    m.Msg = TCM_HITTEST;

                    var lparam = Marshal.AllocHGlobal(Marshal.SizeOf(hti));
                    Marshal.StructureToPtr(hti, lparam, false);
                    m.LParam = lparam;

                    base.WndProc(ref m);
                    Marshal.FreeHGlobal(lparam);

                    if (m.Result.ToInt32() != -1)
                    {
                        return hti.flags != TabControlHitTest.TCHT_NOWHERE;
                    }

                }

                return false;

            }


            protected override void OnPaintAdornments(PaintEventArgs pe)
            {
                // Don't want DrawGrid dots.
            }

            // Fix the AllSizable selectiorule on DockStyle.Fill
            public override SelectionRules SelectionRules
            {
                get
                {
                    if (Control.Dock == DockStyle.Fill)
                    {
                        return SelectionRules.Visible;
                    }
                    return base.SelectionRules;
                }
            }

        }

        #endregion

    }

    #endregion

    #region  TabControlEx 

    public class TabControlEx : TabControl
    {

        private int _hotTabIndex = -1;

        public TabControlEx() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        #region  Properties 

        private int CloseButtonHeight
        {
            get
            {
                return FontHeight;
            }
        }

        private int HotTabIndex
        {
            get
            {
                return _hotTabIndex;
            }
            set
            {
                if (_hotTabIndex != value)
                {
                    _hotTabIndex = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region  Overridden Methods

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            OnFontChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            var hFont = Font.ToHfont();
            SendMessage(Handle, WM_SETFONT, hFont, new IntPtr(-1));
            SendMessage(Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            UpdateStyles();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var HTI = new TCHITTESTINFO(e.X, e.Y);
            HotTabIndex = SendMessage(Handle, TCM_HITTEST, IntPtr.Zero, ref HTI);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HotTabIndex = -1;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            for (int id = 0, loopTo = TabCount - 1; id <= loopTo; id++)
                DrawTabBackground(pevent.Graphics, id);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int id = 0, loopTo = TabCount - 1; id <= loopTo; id++)
                DrawTabContent(e.Graphics, id);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == TCM_SETPADDING)
            {
                m.LParam = MAKELPARAM(Padding.X + CloseButtonHeight / 2, Padding.Y);
            }
            if (m.Msg == WM_MOUSEDOWN && !DesignMode)
            {
                var pt = PointToClient(Cursor.Position);
                var closeRect = GetCloseButtonRect(HotTabIndex);
                if (closeRect.Contains(pt))
                {
                    TabPages.RemoveAt(HotTabIndex);
                    m.Msg = WM_NULL;
                }
            }
            base.WndProc(ref m);
        }

        #endregion

        #region  Private 

        private IntPtr MAKELPARAM(int lo, int hi)
        {
            return new IntPtr(hi << 16 | lo & 0xFFFF);
        }

        private void DrawTabBackground(Graphics graphics, int id)
        {
            if (id == SelectedIndex)
            {
                graphics.FillRectangle(Brushes.DarkGray, GetTabRect(id));
            }
            else if (id == HotTabIndex)
            {
                var rc = GetTabRect(id);
                rc.Width -= 1;
                rc.Height -= 1;
                graphics.DrawRectangle(Pens.DarkGray, rc);
            }
        }

        private void DrawTabContent(Graphics graphics, int id)
        {
            bool selectedOrHot = id == SelectedIndex || id == HotTabIndex;
            bool vertical = (int)Alignment >= 2;

            Image tabImage = null;

            if (ImageList != null)
            {
                var page = TabPages[id];
                if (page.ImageIndex > -1 && page.ImageIndex < ImageList.Images.Count)
                {
                    tabImage = ImageList.Images[page.ImageIndex];
                }
                if (page.ImageKey.Length > 0 && ImageList.Images.ContainsKey(page.ImageKey))
                {
                    tabImage = ImageList.Images[page.ImageKey];
                }
            }

            var tabRect = GetTabRect(id);
            var contentRect = vertical ? new Rectangle(0, 0, tabRect.Height, tabRect.Width) : new Rectangle(Point.Empty, tabRect.Size);
            var textrect = contentRect;
            textrect.Width -= FontHeight;

            if (tabImage != null)
            {
                textrect.Width -= tabImage.Width;
                textrect.X += tabImage.Width;
            }

            var frColor = id == SelectedIndex ? Color.White : ForeColor;
            var bkColor = id == SelectedIndex ? Color.DarkGray : BackColor;
            using (var bm = new Bitmap(contentRect.Width, contentRect.Height))
            {
                using (var bmGraphics = Graphics.FromImage(bm))
                {
                    TextRenderer.DrawText(bmGraphics, TabPages[id].Text, Font, textrect, frColor, bkColor);
                    if (selectedOrHot)
                    {
                        var closeRect = new Rectangle(contentRect.Right - CloseButtonHeight, 0, CloseButtonHeight, CloseButtonHeight);
                        closeRect.Offset(-2, (contentRect.Height - closeRect.Height) / 2);
                        DrawCloseButton(bmGraphics, closeRect);
                    }
                    if (tabImage != null)
                    {
                        var imageRect = new Rectangle(Padding.X, 0, tabImage.Width, tabImage.Height);
                        imageRect.Offset(0, (contentRect.Height - imageRect.Height) / 2);
                        bmGraphics.DrawImage(tabImage, imageRect);
                    }
                }
                if (vertical)
                {
                    if (Alignment == TabAlignment.Left)
                    {
                        bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    else
                    {
                        bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                }
                graphics.DrawImage(bm, tabRect);
            }

        }

        private void DrawCloseButton(Graphics graphics, Rectangle bounds)
        {
            graphics.FillRectangle(Brushes.Red, bounds);
            using (var closeFont = new Font("Arial", Font.Size, FontStyle.Bold))
            {
                TextRenderer.DrawText(graphics, "X", closeFont, bounds, Color.White, Color.Red, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
            }
        }

        private Rectangle GetCloseButtonRect(int id)
        {

            var tabRect = GetTabRect(id);
            var closeRect = new Rectangle(tabRect.Left, tabRect.Top, CloseButtonHeight, CloseButtonHeight);

            switch (Alignment)
            {
                case TabAlignment.Left:
                    {
                        closeRect.Offset((tabRect.Width - closeRect.Width) / 2, 0);
                        break;
                    }
                case TabAlignment.Right:
                    {
                        closeRect.Offset((tabRect.Width - closeRect.Width) / 2, tabRect.Height - closeRect.Height);
                        break;
                    }

                default:
                    {
                        closeRect.Offset(tabRect.Width - closeRect.Width, (tabRect.Height - closeRect.Height) / 2);
                        break;
                    }
            }

            return closeRect;

        }

        #endregion

        #region  Interop 

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct TCHITTESTINFO
        {
            public Point pt;
            public TCHITTESTFLAGS flags;
            public TCHITTESTINFO(int x, int y)
            {
                pt = new Point(x, y);
                flags = TCHITTESTFLAGS.TCHT_NOWHERE;
            }
        }

        [Flags()]
        private enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int WM_NULL = 0x0;
        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1D;
        private const int WM_MOUSEDOWN = 0x201;

        private const int TCM_FIRST = 0x1300;
        private const int TCM_HITTEST = TCM_FIRST + 13;
        private const int TCM_SETPADDING = TCM_FIRST + 43;

        #endregion

    }

    #endregion

    #region  MultiCollection 

    [ToolboxItem(false)]
    public class MultiCollection : UserControl
    {

        // 
        public MultiCollection() : base()
        {
        }

        [Category("Collections")]
        public virtual object Editor
        {
            get
            {
                switch (Type ?? "")
                {
                    case "TextBox":
                        {
                            return _EditorTextBox;
                        }
                    case "ComboBox":
                        {
                            return _EditorComboBox;
                        }
                }
                return null;
            }
            set
            {
                if (value == null)
                    return;
                switch (value.GetType().Name ?? "")
                {
                    case "TextBox":
                        {
                            _EditorTextBox = (TextBox)value;
                            Type = value.GetType().Name;
                            break;
                        }
                    case "ComboBox":
                        {
                            _EditorComboBox = (ComboBox)value;
                            Type = value.GetType().Name;
                            break;
                        }
                }
            }
        }

        private string _Type = "";
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Category("Collections")]
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        private TextBox _EditorTextBox;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Category("Collections")]
        public object EditorTextBox
        {
            get
            {
                if (_EditorTextBox == null)
                {
                    _EditorTextBox = new TextBox();
                }
                return _EditorTextBox;
            }
            set
            {
                _EditorTextBox = (TextBox)value;
            }
        }

        private bool ShouldSerializeEditorTextBox()
        {
            return Type == "TextBox";
        }

        private ComboBox _EditorComboBox;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [Category("Collections")]
        public object EditorComboBox
        {
            get
            {
                if (_EditorComboBox == null)
                {
                    _EditorComboBox = new ComboBox();
                }
                return _EditorComboBox;
            }
            set
            {
                _EditorComboBox = (ComboBox)value;
            }
        }

        private bool ShouldSerializeEditorComboBox()
        {
            return Type == "ComboBox";
        }
    }

    #endregion

    #region  MultiCollectionEditor 

    public class MultiCollectionEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService;

            if (context == null || context.Instance == null || provider == null)
            {
                return value;
            }

            editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            dynamic CollectionEditor = NewCollectionEditor();
            CollectionEditor.Start(value);
            if (editorService.ShowDialog((Form)CollectionEditor) == DialogResult.OK)
            {
                return CollectionEditor.NewValue;
            }
            else
            {
                return value;
            }
        }

        public virtual object NewCollectionEditor()
        {
            return new Editor_MultiCollection();
        }

    }

    #endregion

    #region  EventDelegation 

    /// <summary>
    /// Event Handler for SubItem events
    /// </summary>
    public delegate void SubItemEventHandler(object sender, SubItemEventArgs e);
    /// <summary>
    /// Event Handler for SubItemEndEditing events
    /// </summary>
    public delegate void SubItemEndEditingEventHandler(object sender, SubItemEndEditingEventArgs e);

    /// <summary>
    /// Event Args for SubItemClicked event
    /// </summary>
    public class SubItemEventArgs : EventArgs
    {
        public SubItemEventArgs(ListViewItem item, int subItem, Control control) : base()
        {
            _subItemIndex = subItem;
            _item = item;
            _control = control;
        }
        private int _subItemIndex = -1;
        private ListViewItem _item = null;
        private Control _control = null;
        public int Column
        {
            get
            {
                return _subItemIndex;
            }
        }
        public ListViewItem Item
        {
            get
            {
                return _item;
            }
        }
        public Control Control
        {
            get
            {
                return _control;
            }
        }
    }


    /// <summary>
    /// Event Args for SubItemEndEditingClicked event
    /// </summary>
    public class SubItemEndEditingEventArgs : SubItemEventArgs
    {
        private string _text = string.Empty;
        private bool _cancel = true;
        private bool _changed = true;

        public SubItemEndEditingEventArgs(ListViewItem item, int subItem, string display, bool cancel, bool changed, Control control) : base(item, subItem, control)
        {
            _text = display;
            _cancel = cancel;
            _changed = changed;
        }
        public string DisplayText
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        public bool Changed
        {
            get
            {
                return _changed;
            }
        }
        public bool Cancel
        {
            get
            {
                return _cancel;
            }
            set
            {
                _cancel = value;
            }
        }
    }

    #endregion

    #region  BasicTableViewEditorsItem 

    public class BasicTableViewEditorsItem : MultiCollection
    {

        public static int MaxSize;

    }

    #endregion

    #region  BasicTableViewEditor 

    public class BasicTableViewEditor : MultiCollectionEditor
    {

        public override object NewCollectionEditor()
        {
            return new BasicTableViewCollectionForm();
        }

    }

    #endregion

    #region  BasicTableViewCollectionForm 

    public class BasicTableViewCollectionForm : Editor_MultiCollection
    {

        protected internal override void Gather(object MyCollection)
        {
            foreach (BasicTableViewEditorsItem item in (IEnumerable)MyCollection)
                MyList.Add(item.Editor);
        }

        protected internal override int MaxSize()
        {
            return BasicTableViewEditorsItem.MaxSize;
        }

        public override object NewValue()
        {
            var Value = new List<BasicTableViewEditorsItem>();
            foreach (object item in MyList)
            {
                var Collection = new BasicTableViewEditorsItem();
                Collection.Editor = item;
                Value.Add(Collection);
            }
            return Value;
        }

        protected internal override void Setup()
        {
            GetComboBox().Items.AddRange(new[] { "TextBox", "ComboBox", "Empty" });
            MyType.AddRange(new[] { "System.Windows.Forms.TextBox", "System.Windows.Forms.ComboBox", "Nothing" });
        }

        protected internal override object ItemName()
        {
            return "BasicTableViewItem";
        }

    }

    #endregion

    #region  BasicListView 

    public class BasicListView : ListView
    {

        private void WmLButtonDown(ref Message m)
        {
            var pt = new Point(m.LParam.ToInt32());
            var ht = HitTest(pt);
            if (ht.Item == null)
            {
                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN:
                    {
                        WmLButtonDown(ref m);
                        break;
                    }

                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }

        private const int WM_LBUTTONDOWN = 0x201;

    }

    #endregion

    #region  BasicTableViewCollectionEditor 

    [DisplayName("Basic CheckBox")]
    public class BasicCheckBoxItemControl : EditableListViewItemControl<BasicCheckBox>
    {
    }

    [DisplayName("Basic TextBox")]
    public class BasicTextBoxItemControl : EditableListViewItemControl<BasicTextBox>
    {
    }

    [DisplayName("Basic NumericBox")]
    public class BasicNumericBoxItemControl : EditableListViewItemControl<BasicNumericBox>
    {
    }

    [DisplayName("Basic ComboBox")]
    public class BasicComboBoxItemControl : EditableListViewItemControl<BasicComboBox>
    {
    }

    public class BasicTableViewCollectionEditor : EditableListViewCollectionEditor
    {

        public override Type[] Types
        {
            get
            {
                return new Type[] { EmptyType, new DisplayTypeDelegator(typeof(BasicCheckBoxItemControl)), new DisplayTypeDelegator(typeof(BasicTextBoxItemControl)), new DisplayTypeDelegator(typeof(BasicNumericBoxItemControl)), new DisplayTypeDelegator(typeof(BasicComboBoxItemControl)) };
            }
        }

    }

    #endregion

    #region  BasicTableView 

    [ToolboxItem(true)]
    public class BasicTableView : EditableListView
    {

        #region  Setup & Constants 
        /// <summary>
        /// MessageHeader for WM_NOTIFY
        /// </summary>
        private struct NMHDR
        {
            public IntPtr hwndFrom;
            public int idFrom;
            public int code;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int len, ref int[] order);

        [DllImport("user32")]
        private static extern long ShowScrollBar(IntPtr handle, ScrollBarVisibility type, bool show);

        // ListView messages
        private const int LVM_FIRST = 0x1000;
        private const int LVM_GETCOLUMNORDERARRAY = LVM_FIRST + 59;

        // Windows Messages that will abort editing
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_SIZE = 0x5;
        private const int WM_NOTIFY = 0x4E;

        private const int HDN_FIRST = -300;
        private const int HDN_BEGINDRAG = HDN_FIRST - 10;
        private const int HDN_ITEMCHANGINGA = HDN_FIRST - 0;
        private const int HDN_ITEMCHANGINGW = HDN_FIRST - 20;

        /// 	<summary>
        /// 	Required designer variable.
        /// 	</summary>
        private Container components = null;

        public event SubItemEventHandler SubItemClicked;
        public event SubItemEventHandler SubItemBeginEditing;
        public event SubItemEndEditingEventHandler SubItemEndEditing;

        /// 	<summary>
        /// 	Required method	for	Designer support - do not modify 
        /// 	the	contents of	this method	with the code editor.
        /// 	</summary>
        private void InitializeComponent()
        {
            components = new Container();
        }

        #endregion

        #region  Constructors 

        public BasicTableView()
        {
            // This	call is	required by	the	Windows.Forms Form Designer.
            InitializeComponent();

            base.View = View.List;
            base.HeaderStyle = ColumnHeaderStyle.Clickable;
            base.MultiSelect = false;
            base.AutoArrange = false;

            ColumnSorter = new TableViewColumnSorter();
            ListViewItemSorter = ColumnSorter;
            ColumnWidthChanging += Me_ColumnWidthChanging;
            ColumnClick += Me_ColumnClick;
            Resize += Me_Resize;
            SubItemClicked += Me_SubItemClicked;
            DrawItem += Me_DrawItem;
            DrawSubItem += Me_DrawSubItem;
            DrawColumnHeader += Me_DrawColumnHeader;

            // Dim attributes As AttributeCollection = TypeDescriptor.GetProperties(Me)("HeaderStyle").Attributes
            // Dim myAttribute As DefaultValueAttribute = CType(attributes(GetType(DefaultValueAttribute)), DefaultValueAttribute)
            // myAttribute.Value
            Editors = new EditableListViewCollection(this);
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            // InitLayout is run after we are added to a control via InitializeComponent()
            // If DesignMode = False Then
            // ReDim SysHdr32Handles(Columns.Count - 1)
            // ReDim TableViewColumnHeaders(Columns.Count - 1)
            // For Index As Integer = 0 To Columns.Count - 1
            // SysHdr32Handles(Index) = GetWindow(Me.Handle, GW_CHILD)
            // TableViewColumnHeaders(Index) = New BasicTableViewHeader(SysHdr32Handles(Index), Me)
            // Next
            // End If
        }

        #endregion

        #region  Properties 

        [Category("Behavior")]
        [Editor(typeof(BasicTableViewCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Contains a collection of editors for each column.")]
        public virtual EditableListViewCollection Editors { get; set; }

        private ScrollBarVisibility _ScrollBars = ScrollBarVisibility.Automatic;
        /// <summary>
        /// How should scroll bars be displayed?
        /// </summary>
        [Description("Determines how the scroll bars will be displayed")]
        [Category("Behavior")]
        [DefaultValue(ScrollBarVisibility.Automatic)]
        public ScrollBarVisibility ScrollBars
        {
            get
            {
                return _ScrollBars;
            }
            set
            {
                if (_ScrollBars != value)
                {
                    _ScrollBars = value;
                    ShowScrollBar(Handle, ScrollBarVisibility.Both, false);
                    ShowScrollBar(Handle, value, true);
                }
            }
        }

        private bool _doubleClickActivation = false;
        /// <summary>
        /// Is a double click required to start editing a cell?
        /// </summary>
        [Description("Determines whether a double, or single, click is required to activate editing.")]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool DoubleClickActivation
        {
            get
            {
                return _doubleClickActivation;
            }
            set
            {
                _doubleClickActivation = value;
            }
        }

        private bool _allowNullValue = false;
        /// <summary>
        /// Is a null value accepted?
        /// </summary>
        [Description("Determines if while editing the user is allowed to clear the value.")]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool AllowNullValue
        {
            get
            {
                return _allowNullValue;
            }
            set
            {
                _allowNullValue = value;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new View View
        {
            get
            {
                return View.List;
            }
        }

        [Category("Appearance")]
        [DefaultValue(TableViewStyles.Standard)]
        public TableViewStyles Style
        {
            get
            {
                return (TableViewStyles)base.View;
            }
            set
            {
                if ((int)base.View != (int)value)
                {
                    base.View = (View)value;
                }
            }
        }

        public enum TableViewStyles
        {
            Columns = 1,
            Standard = 3,
            Tiled = 4
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new ImageList SmallImageList
        {
            get
            {
                return null;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new ImageList LargeImageList
        {
            get
            {
                return null;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new ImageList StateImageList
        {
            get
            {
                return null;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new bool LabelEdit
        {
            get
            {
                return false;
            }
        }

        [DefaultValue(false)]
        public new bool MultiSelect
        {
            get
            {
                return base.MultiSelect;
            }
            set
            {
                base.MultiSelect = value;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DefaultValue(false)]
        public new bool AutoArrange
        {
            get
            {
                return false;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new ColumnHeaderStyle HeaderStyle
        {
            get
            {
                return ColumnHeaderStyle.Clickable;
            }
        }

        [Category("Behavior")]
        [DefaultValue(TableViewColumnActivity.Clickable)]
        public TableViewColumnActivity ColumnActivity
        {
            get
            {
                return (TableViewColumnActivity)base.HeaderStyle;
            }
            set
            {
                if ((int)base.HeaderStyle != (int)value)
                {
                    base.HeaderStyle = (ColumnHeaderStyle)value;
                }
            }
        }

        public enum TableViewColumnActivity
        {
            NonClickable = 1,
            Clickable = 2
        }

        private bool _ColumnResizable = false;
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ColumnResizable
        {
            get
            {
                return _ColumnResizable;
            }
            set
            {
                if (_ColumnResizable != value)
                {
                    _ColumnResizable = value;
                }
            }
        }

        [Browsable(false)]
        public bool IsEditing
        {
            get
            {
                return _editingControl != null;
            }
        }

        public new bool Enabled
        {
            get
            {
                return base.Enabled & !TemporarilyDisabled;
            }
            set
            {
                if (value != base.Enabled)
                {
                    base.Enabled = value;
                    OwnerDraw = !value | TemporarilyDisabled;
                    Invalidate();
                }
            }
        }

        private bool _TemporarilyDisabled { get; set; } = false;
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        protected bool TemporarilyDisabled
        {
            get
            {
                return _TemporarilyDisabled;
            }
            set
            {
                if (value != _TemporarilyDisabled)
                {
                    _TemporarilyDisabled = value;
                    OwnerDraw = !Enabled;
                    Invalidate();
                }
            }
        }

        #endregion

        #region  Events 

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                // Look	for	WM_VSCROLL,WM_HSCROLL or WM_SIZE messages.
                case WM_VSCROLL:
                case WM_HSCROLL:
                case WM_SIZE:
                    {
                        EndEditing(false);
                        break;
                    }
                case WM_NOTIFY:
                    {
                        // Look for WM_NOTIFY of events that might also change the
                        // editor's position/size: Column reordering or resizing
                        NMHDR h = (NMHDR)Marshal.PtrToStructure(msg.LParam, typeof(NMHDR));
                        if (h.code == HDN_BEGINDRAG || h.code == HDN_ITEMCHANGINGA || h.code == HDN_ITEMCHANGINGW)
                        {
                            EndEditing(false);
                        }
                        break;
                    }
            }

            base.WndProc(ref msg);
        }

        private void Me_ColumnWidthChanging(object Sender, ColumnWidthChangingEventArgs E)
        {
            if (DesignMode == false & ColumnResizable == false)
            {
                E.Cancel = true;
                for (int Column = 0, loopTo = Columns.Count - 1; Column <= loopTo; Column++)
                    E.NewWidth = Columns[Column].Width;
            }
        }

        private bool HandleResolved = false;
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (HandleResolved == false)
            {
                SysHdr32Handles = new IntPtr[Columns.Count];
                TableViewColumnHeaders = new BasicTableViewHeader[Columns.Count];
                for (int Index = 0, loopTo = Columns.Count - 1; Index <= loopTo; Index++)
                {
                    SysHdr32Handles[Index] = GetWindow(Handle, GW_CHILD);
                    TableViewColumnHeaders[Index] = new BasicTableViewHeader(SysHdr32Handles[Index], this);
                }
                HandleResolved = true;
            }
        }

        private void Me_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortToggle(e.Column);
            ReassignHandles();
        }

        private void Me_Resize(object sender, EventArgs e)
        {
            ShowScrollBar(Handle, ScrollBars, true);
        }

        protected void OnSubItemBeginEditing(SubItemEventArgs e)
        {
            SubItemBeginEditing?.Invoke(this, e);
        }
        protected void OnSubItemEndEditing(SubItemEndEditingEventArgs e)
        {
            SubItemEndEditing?.Invoke(this, e);
        }
        protected void OnSubItemClicked(SubItemEventArgs e)
        {
            SubItemClicked?.Invoke(this, e);
        }

        protected Control GetTemporaryControl(string Name, bool Nullable = false)
        {
            if (this.Controls != null & Nullable)
                return null;
            Control[] Controls = this.Controls.Find(Name, true);
            return Nullable ? Controls.FirstOrDefault() : Controls.First();
        }

        private string _OriginalName;
        private void Me_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (e.Item.SubItems.Count > e.Column & e.Column < Editors.Count)
            {
                Control ctrl = (Control)Editors[e.Column].Editor;
                if (ctrl == null)
                    return;
                if (string.IsNullOrEmpty(_OriginalName))
                    _OriginalName = ctrl.Name;
                string UniqueName = ControlExtension.GetTemporaryControlName(ref ctrl, _OriginalName);
                string Name = ControlExtension.GetTemporaryControlName(ref ctrl, _OriginalName, e.Item.Index + 1);
                var EditingControl = GetTemporaryControl(UniqueName, true);
                if (EditingControl != null)
                {
                    ctrl = EditingControl;
                    ctrl.Visible = true;
                }
                else
                {
                    ctrl.Name = Name;
                    Controls.Add(ctrl);
                }
                StartEditing(ctrl, e.Item, e.Column);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (DoubleClickActivation == true)
                return;

            EditSubitemAt(new Point(e.X, e.Y));
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (DoubleClickActivation == false)
                return;

            var pt = PointToClient(Cursor.Position);

            EditSubitemAt(pt);
        }

        private void Me_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void Me_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void Me_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);

            using (var sf = e.Header.TextAlign.ToStringFormat())
            {
                e.DrawBackground();

                using (var headerFont = new Font("Microsoft Sans Serif", 8f))
                {
                    var rect = e.Bounds;
                    rect.X += 2;
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Gray, rect, sf);
                }
            }
        }

        #endregion

        #region  Removal 

        // '''	<summary>
        // '''	Clean up any resources being used.
        // '''	</summary>
        // Protected Overrides Sub Dispose(disposing As Boolean)
        // If disposing Then
        // If components IsNot Nothing Then
        // components.Dispose()
        // End If
        // End If
        // MyBase.Dispose(disposing)
        // End Sub

        #endregion

        #region  Public 

        /// <summary>
        /// Retrieve the order in which columns appear
        /// </summary>
        /// <returns>Current display order of column indices</returns>
        public int[] GetColumnOrder()
        {
            var lPar = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * Columns.Count);

            var res = SendMessage(Handle, LVM_GETCOLUMNORDERARRAY, new IntPtr(Columns.Count), lPar);
            if (res.ToInt32() == 0)
            {
                // Something went wrong
                Marshal.FreeHGlobal(lPar);
                return null;
            }

            int[] order = new int[Columns.Count];
            Marshal.Copy(lPar, order, 0, Columns.Count);

            Marshal.FreeHGlobal(lPar);

            return order;
        }


        /// <summary>
        /// Find ListViewItem and SubItem Index at position (x,y)
        /// </summary>
        /// <param name="x">relative to ListView</param>
        /// <param name="y">relative to ListView</param>
        /// <param name="item">Item at position (x,y)</param>
        /// <returns>SubItem index</returns>
        public int GetSubItemAt(int x, int y, ref ListViewItem item)
        {
            item = GetItemAt(x, y);

            if (item != null)
            {
                int[] order = GetColumnOrder();
                Rectangle lviBounds;
                int subItemX;

                lviBounds = item.GetBounds(ItemBoundsPortion.Entire);
                subItemX = lviBounds.Left;
                for (int i = 0, loopTo = order.Length - 1; i <= loopTo; i++)
                {
                    var h = Columns[order[i]];
                    if (x < subItemX + h.Width)
                    {
                        return h.Index;
                    }
                    subItemX += h.Width;
                }
            }

            return -1;
        }


        /// <summary>
        /// Get bounds for a SubItem
        /// </summary>
        /// <param name="Item">Target ListViewItem</param>
        /// <param name="SubItem">Target SubItem index</param>
        /// <returns>Bounds of SubItem (relative to ListView)</returns>
        public Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
        {
            int[] order = GetColumnOrder();

            var subItemRect = Rectangle.Empty;
            if (SubItem >= order.Length)
            {
                throw new IndexOutOfRangeException("SubItem " + SubItem + " out of range");
            }

            if (Item == null)
            {
                throw new ArgumentNullException("Item");
            }

            var lviBounds = Item.GetBounds(ItemBoundsPortion.Entire);
            int subItemX = lviBounds.Left + 2;

            ColumnHeader col;
            int i;
            var loopTo = order.Length - 1;
            for (i = 0; i <= loopTo; i++)
            {
                col = Columns[order[i]];
                if (col.Index == SubItem)
                {
                    break;
                }
                subItemX += col.Width;
            }
            subItemRect = new Rectangle(subItemX, lviBounds.Top, Columns[order[i]].Width, lviBounds.Height);
            return subItemRect;
        }

        /// <summary>
        /// Get bounds for a ColumnHeader
        /// </summary>
        /// <param name="Index">Target ColumnHeader index</param>
        /// <returns>Bounds of ColumnHeader (relative to ListView)</returns>
        public Rectangle GetColumnHeaderBounds(int Index)
        {
            int[] order = GetColumnOrder();

            var columnHeaderRect = Rectangle.Empty;
            if (Index >= order.Length)
            {
                throw new IndexOutOfRangeException("SubItem " + Index + " out of range");
            }

            ColumnHeader col;
            int lix;
            for (int i = 0, loopTo = order.Length - 1; i <= loopTo; i++)
            {
                col = Columns[order[i]];
                lix = i > 0 ? Columns[order[i - 1]].Width : 0;
                if (col.Index == Index)
                {
                    columnHeaderRect = new Rectangle(lix, 0, col.Width, 26);
                    break;
                }
            }

            return columnHeaderRect;
        }

        public void GoToRow(int Row)
        {
            if (Row < 0)
                return;
            Items[Row].Selected = true;
            TopItem = Items[Row];
        }

        public void SortToggle(int ColumnIndex)
        {
            if (ColumnIndex == ColumnSorter.SortColumn)
            {
                // If (ColumnSorter.OrderOfSort = SortOrder.Ascending) Then
                // ColumnSorter.OrderOfSort = SortOrder.Descending
                // Else
                // ColumnSorter.OrderOfSort = SortOrder.Ascending
                // End If
                ColumnSorter.OrderOfSort = SortOrder.Descending;
            }
            else
            {
                // Set the column default sorting.
                ColumnSorter.OrderOfSort = SortOrder.Ascending;
            }
            // Perform the sort with these new sort options.
            Sort();
        }

        private void ReassignHandles()
        {
            // Prevents the: "Handle Already Exists Error"
            for (int Index = 0, loopTo = Columns.Count - 1; Index <= loopTo; Index++)
            {
                if (!(SysHdr32Handles[Index] == GetWindow(Handle, GW_CHILD)))
                {
                    SysHdr32Handles[Index] = GetWindow(Handle, GW_CHILD + Index);
                    TableViewColumnHeaders[Index].AssignHandle(SysHdr32Handles[Index]);
                }
            }
        }

        #endregion

        #region  Sorter 
        private TableViewColumnSorter ColumnSorter;

        public class TableViewColumnSorter : IComparer
        {

            private CaseInsensitiveComparer ObjectCompare;

            public TableViewColumnSorter()
            {
                // Initialize the column to '0'.
                SortColumn = 0;

                // Initialize the sort order to 'none'.
                OrderOfSort = SortOrder.None;

                // Initialize the sort type to 'index'.
                TypeOfSort = SortType.Index;

                // Initialize the CaseInsensitiveComparer object.
                ObjectCompare = new CaseInsensitiveComparer();
            }

            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX;
                ListViewItem listviewY;

                // Cast the objects to be compared to ListViewItem objects.
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                // Compare the two items.
                if (TypeOfSort == SortType.Index & !(OrderOfSort == SortOrder.None))
                {
                    return -CompareIndex(listviewX, listviewY);
                }
                else
                {
                    compareResult = CompareAlpha(listviewX, listviewY);
                }


                // Calculate the correct return value based on the object 
                // comparison.
                if (OrderOfSort == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return typical result of 
                    // compare operation.
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of 
                    // compare operation.
                    return -compareResult;
                }
                else
                {
                    // Return '0' to indicate that they are equal.
                    return 0;
                }
            }

            public int CompareIndex(ListViewItem x, ListViewItem y)
            {
                return ObjectCompare.Compare(x.Index, y.Index);
            }

            public int CompareAlpha(ListViewItem x, ListViewItem y)
            {
                return ObjectCompare.Compare(x.SubItems[SortColumn].Text, y.SubItems[SortColumn].Text);
            }


            private int _SortColumn;
            public int SortColumn
            {
                get
                {
                    return _SortColumn;
                }
                set
                {
                    _SortColumn = value;
                }
            }

            private SortOrder _OrderOfSort;
            public SortOrder OrderOfSort
            {
                get
                {
                    return _OrderOfSort;
                }
                set
                {
                    _OrderOfSort = value;
                }
            }

            private SortType _TypeOfSort;
            public SortType TypeOfSort
            {
                get
                {
                    return _TypeOfSort;
                }
                set
                {
                    _TypeOfSort = value;
                }
            }

            public enum SortType
            {
                Alpha,
                Index
            }
        }
        #endregion

        #region  Header 

        [DllImport("user32", EntryPoint = "GetWindow")]
        private static extern IntPtr GetWindow(IntPtr hwnd, int wCmd);
        private const int GW_CHILD = 5;
        private IntPtr[] SysHdr32Handles;
        private BasicTableViewHeader[] TableViewColumnHeaders;

        private class BasicTableViewHeader : NativeWindow
        {
            private IntPtr ptrHWnd;

            private BasicTableView ParentControl;

            ~BasicTableViewHeader()
            {
                ReleaseHandle();
            }

            public BasicTableViewHeader(IntPtr ControlHandle, BasicTableView Control)
            {
                ptrHWnd = ControlHandle;
                ParentControl = Control;
                AssignHandle(ptrHWnd);
            }

            public void NewHandle(IntPtr ControlHandle)
            {
                if (ptrHWnd == ControlHandle)
                    return;
                ptrHWnd = ControlHandle;
                AssignHandle(ptrHWnd);
            }

            protected override void WndProc(ref Message m)
            {
                if (ParentControl.ColumnResizable == false)
                {
                    switch (m.Msg)
                    {
                        case var @case when @case == 0x20:  // WM_SETCURSOR
                            {
                                m.Msg = 0;
                                break;
                            }
                        case var case1 when case1 == 0x203:  // WM_LBUTTONDBLCLK
                            {
                                m.Msg = 0;
                                break;
                            }
                        case var case2 when case2 == 0x201:  // WM_LBUTTONOWN
                            {
                                m.Msg = 0;
                                break;
                            }
                    }
                }
                // Select Case m.Msg
                // Case Is = &H201  ' WM_LBUTTONDOWN
                // OnHeaderMouseClick()
                // Case Is = &H200 ' MOUSEMOVE
                // OnHeaderMouseHover()
                // End Select
                base.WndProc(ref m);
            }
        }

        #endregion

        #region  Private 

        /// <summary>
        /// Fire SubItemClicked
        /// </summary>
        /// <param name="p">Point of click/doubleclick</param>
        private void EditSubitemAt(Point p)
        {
            ListViewItem item = null;
            int idx = GetSubItemAt(p.X, p.Y, ref item);
            if (idx >= 0)
            {
                OnSubItemClicked(new SubItemEventArgs(item, idx, _editingControl));
            }
        }

        #endregion

        #region  In-place Edit 
        // The control performing the actual editing
        private Control _editingControl;
        // The LVI being edited
        private ListViewItem _editItem;
        // The SubItem being edited
        private int _editSubItem;

        /// <summary>
        /// Begin in-place editing of given cell
        /// </summary>
        /// <param name="c">Control used as cell editor</param>
        /// <param name="Item">ListViewItem to edit</param>
        /// <param name="SubItem">SubItem index to edit</param>
        public void StartEditing(Control c, ListViewItem Item, int SubItem)
        {
            OnSubItemBeginEditing(new SubItemEventArgs(Item, SubItem, c));

            ComboBox cb = null;
            if (c.GetType().Name.Contains("Combo"))
            {
                cb = (ComboBox)c;
            }

            var rcSubItem = GetSubItemBounds(Item, SubItem);

            if (rcSubItem.X < 0)
            {
                // Left edge of SubItem not visible - adjust rectangle position and width
                rcSubItem.Width += rcSubItem.X;
                rcSubItem.X = 0;
            }
            if (rcSubItem.X + rcSubItem.Width > Width)
            {
                // Right edge of SubItem not visible - adjust rectangle width
                rcSubItem.Width = Width - rcSubItem.Left;
            }

            // Subitem bounds are relative to the location of the ListView!
            rcSubItem.Offset(Left, Top);

            // In case the editing control and the listview are on different parents,
            // account for different origins
            var origin = new Point(0, 0);
            var lvOrigin = Parent.PointToScreen(origin);
            var ctlOrigin = c.Parent.PointToScreen(origin);

            rcSubItem.Offset(lvOrigin.X - ctlOrigin.X, lvOrigin.Y - ctlOrigin.Y);

            // Position and show editor
            c.Bounds = rcSubItem;
            if (cb != null)
            {
                cb.Text = Conversions.ToInteger(Operators.ConcatenateObject("&H", Item.SubItems[SubItem].Tag)).ToString();
                cb.Tag = Item.SubItems[SubItem].Text;
            }
            else
            {
                c.Text = Conversions.ToString(Item.SubItems[SubItem].Tag);
                c.Tag = Item.SubItems[SubItem].Text;
            }
            c.Visible = true;
            c.BringToFront();
            c.Focus();

            _editingControl = c;
            EndingThread = false;
            _editingControl.Leave += new EventHandler(_editControl_Leave);
            _editingControl.KeyDown += new KeyEventHandler(_editControl_KeyDown);
            if (cb != null)
                cb.SelectedIndexChanged += new EventHandler(_editControl_Changed);

            _editItem = Item;
            _editSubItem = SubItem;
        }


        private void _editControl_Leave(object sender, EventArgs e)
        {
            // cell editor losing focus
            EndEditing(true);
        }

        private void _editControl_Changed(object sender, EventArgs e)
        {
            dynamic target = sender;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(target.SelectedIndex, 0, false)))
            {
                EndEditing(true);
            }
        }

        private void _editControl_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        EndEditing(false);
                        e.Handled = true;
                        break;
                    }
                case Keys.Enter:
                    {
                        EndEditing(true);
                        e.Handled = true;
                        break;
                    }
            }
        }

        private bool EndingThread;
        /// <summary>
        /// Accept or discard current value of cell editor control
        /// </summary>
        /// <param name="AcceptChanges">Use the _editingControl's Text as new SubItem text or discard changes?</param>
        public void EndEditing(bool AcceptChanges)
        {
            if (EndingThread == true)
                return;
            else
                EndingThread = true;
            if (_editingControl == null)
            {
                return;
            }

            BasicComboBox cb = null;
            if (_editingControl.GetType().Name.Contains("Combo"))
            {
                cb = (BasicComboBox)_editingControl;
            }

            bool changed = false;
            if (AllowNullValue == true)
                changed = true;
            else
            {
                if (!string.IsNullOrEmpty(_editingControl.Text))
                    changed = true;
            }
            if ((_editingControl.Text ?? "") == (_editItem.SubItems[_editSubItem].Text ?? ""))
                changed = false;

            // The item being edited
            // The subitem index being edited
            // Use editControl text if changes are accepted
            // or the original subitem's text, if changes are discarded
            // Cancel?
            // Was there any changes made?
            var e = new SubItemEndEditingEventArgs(_editItem, _editSubItem, Conversions.ToString(AcceptChanges ? _editingControl.Text : _editItem.SubItems[_editSubItem].Tag), !AcceptChanges, changed, _editingControl);

            _editingControl.Leave -= new EventHandler(_editControl_Leave);
            _editingControl.KeyDown -= new KeyEventHandler(_editControl_KeyDown);
            if (cb != null)
                cb.SelectedIndexChanged -= new EventHandler(_editControl_Changed);

            OnSubItemEndEditing(e);

            if (cb != null)
            {
                if (e.Changed == true & e.Cancel == false)
                {
                    var SelectedItem = cb.Items.FirstOrDefault(Item => (Item.Value ?? "") == (e.DisplayText ?? ""));
                    _editItem.SubItems[_editSubItem].Text = Conversions.ToString(cb.GetValueText(SelectedItem));
                    _editItem.SubItems[_editSubItem].Tag = Conversions.ToInteger(e.DisplayText).ToString("X2");
                }
            }
            else if (e.Changed == true & e.Cancel == false)
            {
                _editItem.SubItems[_editSubItem].Text = e.DisplayText;
                _editItem.SubItems[_editSubItem].Tag = e.DisplayText;
            }

            _editingControl.Visible = false;
            _editingControl.ResetText();
            if (cb != null)
                cb.SelectedIndex = -1;
            Focus();

            _editingControl = null;
            _editItem = null;
            _editSubItem = -1;
        }

        #endregion


    }

    #endregion

    #region  BasicNumericBox 

    public class BasicNumericBox : NumericUpDown, ITextControl
    {

        private TextBox TextBox;
        private int HighlightedText = 0;

        #region  Constructor 

        public BasicNumericBox()
        {
            TextBox = (TextBox)Controls[1];
            MouseUp += Me_TextSelected;
            TextChanged += Me_TextChanged;
        }

        #endregion

        #region  Properties 

        private bool _AutoSizeWidth = false;
        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit it's contents.")]
        [DefaultValue(false)]
        public bool AutoSizeWidth
        {
            get
            {
                return _AutoSizeWidth;
            }
            set
            {
                _AutoSizeWidth = value;
            }
        }

        private int _MaxLength = 1;
        [Category("Layout")]
        [Description("Specifies the maximum number of characters this control can work with.")]
        [DefaultValue(false)]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                _MaxLength = value;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                return TextBox.SelectionStart;
            }
            set
            {
                if (TextBox.SelectionStart != value)
                {
                    TextBox.SelectionStart = value;
                }
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get
            {
                return TextBox.SelectionLength;
            }
            set
            {
                if (TextBox.SelectionLength != value)
                {
                    TextBox.SelectionLength = value;
                }
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                return TextBox.SelectedText;
            }
            set
            {
                if ((TextBox.SelectedText ?? "") != (value ?? ""))
                {
                    TextBox.SelectedText = value;
                }
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBoxBase TextControl
        {
            get
            {
                return TextBox;
            }
        }

        #endregion

        #region  Events 

        private void Me_TextSelected(object sender, MouseEventArgs e)
        {
            if (HighlightedText != SelectedText.Count())
            {
                if (HighlightedText < SelectedText.Count())
                {
                    if (SelectedText.Count() > 0)
                        BasicProgram.RaiseSelectedText(this);
                }
                else if (SelectedText.Count() == 0)
                    BasicProgram.RaiseDeselectedText(this);
                HighlightedText = SelectedText.Count();
            }
        }

        private void Me_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox.Text))
            {
                TextBox.Text = "0";
            }

            if (AutoSizeWidth)
            {
                var g = CreateGraphics();
                Width = (int)Math.Round(g.MeasureString(Text, Font).Width + 10f);
                g.Dispose();
            }
        }

        #endregion

    }

    #endregion

    #region  BasicFlowLayoutPanel 

    public class BasicFlowLayoutPanel : FlowLayoutPanel
    {

        private ControlBuffer _Buffer;
        [Category("Behavior")]
        [Description("Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flickering.")]
        [DefaultValue(ControlBuffer.SingleBuffered)]
        public ControlBuffer Buffer
        {
            get
            {
                return _Buffer;
            }
            set
            {
                if (value != _Buffer)
                {
                    _Buffer = value;
                    DoubleBuffered = value == ControlBuffer.DoubleBuffered;
                }
            }
        }
    }

    public enum ControlBuffer
    {
        SingleBuffered,
        DoubleBuffered
    }

    #endregion

    #region  BasicTableLayoutPanel 

    public class BasicTableLayoutPanel : TableLayoutPanel
    {

        private ControlBuffer _Buffer;
        [Category("Behavior")]
        [Description("Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flickering.")]
        [DefaultValue(ControlBuffer.SingleBuffered)]
        public ControlBuffer Buffer
        {
            get
            {
                return _Buffer;
            }
            set
            {
                if (value != _Buffer)
                {
                    _Buffer = value;
                    DoubleBuffered = value == ControlBuffer.DoubleBuffered;
                }
            }
        }
    }

    #endregion

    #region BasicImage

    public class BasicImage : PictureBox
    {
        [Category("Behavior")]
        [Description("Determines the way that the image is drawn when scaling it.")]
        [DefaultValue(InterpolationMode.Default)]
        public InterpolationMode InterpolationMode { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
    }

    #endregion
}