using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AnyClone;
using BasicTools.BasicControls;
using Microsoft.VisualBasic;

namespace BasicTools
{

    public partial class Editor_BasicBaseCollection
    {

        public delegate void InstanceEventHandler(object sender, object instance);
        public event InstanceEventHandler InstanceCreated;
        public event InstanceEventHandler DestroyingInstance;
        public event InstanceEventHandler ItemRemoved;
        public event InstanceEventHandler ItemAdded;

        #region  Properties 

        protected EditableListViewCollection MyCollection;
        protected List<EditableListViewItem> MyList;
        protected Type[] MyTypes;
        protected List<object> MyClipboard;

        protected bool CanInsert
        {
            get
            {
                return MyList.Count < MyCollection.Parent.Columns.Count;
            }
        }

        protected bool CanRemove
        {
            get
            {
                return MyList.Count > 0;
            }
        }

        protected bool HasItems
        {
            get
            {
                return ListView.Items.Count > 0;
            }
        }

        protected bool CanMove
        {
            get
            {
                return MyList.Count > 1;
            }
        }

        protected bool HasSelectedItems
        {
            get
            {
                return ListView.SelectedIndices.Count > 0;
            }
        }

        protected bool HasClipboardItems
        {
            get
            {
                return MyClipboard.Count > 0;
            }
        }

        protected int NearestIndex
        {
            get
            {
                return ListView.SelectedIndices.Count > 0 ? ListView.SelectedIndices.OfType<int>().Min() : -1;
            }
        }

        protected string[] Names
        {
            get
            {
                return ListView.SelectedIndices.OfType<int>().Select(Index =>
    {
        var Item = MyList[Index];
        return Item.Name;
    }).Distinct().ToArray();
            }
        }

        public Editor_BasicBaseCollection()
        {
            InitializeComponent();
        }


        #endregion

        #region  Public 

        public void Initialize(EditableListViewCollection Collection, Type[] Types)
        {
            Text = Collection.Parent.GetType().Name + " Collection Editor";
            MyCollection = Collection;
            MyList = Collection.GetValues().ToList();
            MyTypes = Types;
            MyClipboard = new List<object>();
            Redraw();
            Refresh();
            if (HasItems)
            {
                SelectItem(0, true);
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            if (HasSelectedItems)
            {
                PropertyGrid.SelectedObjects = ListView.SelectedIndices.OfType<int>().Select(Index =>
    {
        var Item = MyList[Index];
        if (Item.Editor != null)
            return ((dynamic)Item).Control;
        return (object)null;
    }).Where(Item => Item != null).ToArray();
            }
            else
            {
                PropertyGrid.SelectedObject = null;
            }
            string[] AllNames = Names;
            TextBoxName.Text = AllNames.Length <= 1 ? AllNames.FirstOrDefault() : "(Mixed)";
            PropertyGrid.Enabled = HasSelectedItems;
            PropertyGrid.ViewBackColor = HasSelectedItems ? System.Drawing.SystemColors.Window : System.Drawing.SystemColors.ControlLight;
            ButtonCopy.Enabled = HasSelectedItems;
            CopyToolStripMenuItem.Enabled = ButtonCopy.Enabled;
            ButtonPaste.Enabled = CanInsert && HasClipboardItems;
            PasteToolStripMenuItem.Enabled = ButtonPaste.Enabled;
            ButtonUp.Enabled = HasSelectedItems && CanMove;
            MoveUpToolStripMenuItem.Enabled = ButtonUp.Enabled;
            ButtonDown.Enabled = HasSelectedItems && CanMove;
            MoveDownToolStripMenuItem.Enabled = ButtonDown.Enabled;
            TextBoxName.Enabled = HasSelectedItems;
        }

        #endregion

        #region  Private 

        protected void Redraw()
        {
            if (MyTypes.Length > 1)
            {
                ComboBoxInsert.Items.Clear();
                ComboBoxInsert.Items.AddRange(MyTypes.Select(Type => Type.Name).ToArray());
                ComboBoxInsert.SelectedIndex = 0;
            }
            else
            {
                ButtonInsert.Image = null;
            }
            ListView.Items.Clear();
            ListView.Items.AddRange(MyList.Select(Item => new ListViewItem(Item.ToString())).ToArray());
            ButtonInsert.Enabled = CanInsert;
            ComboBoxInsert.Enabled = ButtonInsert.Enabled;
            InsertToolStripMenuItem.Enabled = ButtonInsert.Enabled;
            ButtonRemove.Enabled = CanRemove;
            RemoveToolStripMenuItem.Enabled = ButtonRemove.Enabled;
            LabelMax.Text = $"{MyList.Count}/{MyCollection.Parent.Columns.Count}";
        }

        private void SelectListView()
        {
            ListView.Select();
        }

        private void SelectItem(int index, bool focusListView)
        {
            ListView.Items[index].Selected = true;
            if (focusListView == true)
                SelectListView();
        }

        private void AddItem(object item, int itemIndex = -1)
        {
            if (!CanInsert)
                return;
            InstanceCreated?.Invoke(this, item);
            if (item is EditableListViewItem)
            {
                EditableListViewItem editableItem = (EditableListViewItem)item;
                editableItem.Name = $"Item_{(itemIndex < 0 ? MyList.Count + 1 : itemIndex).ToString("D2")}";
            }
            if (itemIndex < 0)
            {
                MyList.Add((EditableListViewItem)item);
            }
            else
            {
                MyList.Insert(itemIndex, (EditableListViewItem)item);
            }
            Redraw();
            Refresh();
            SelectItem(itemIndex < 0 ? ListView.Items.Count - 1 : itemIndex, true);
        }

        private void AddItem(int typeIndex, int itemIndex = -1)
        {
            var type = MyTypes[typeIndex];
            var item = Activator.CreateInstance(type);
            AddItem(item, itemIndex);
        }

        private void RemoveItem(int ItemIndex)
        {
            if (!CanRemove)
                return;
            var item = MyList[ItemIndex];
            DestroyingInstance?.Invoke(this, item);
            MyList.Remove(item);
            Redraw();
            Refresh();
        }

        private int[] StoreIndices()
        {
            return ListView.SelectedIndices.OfType<int>().ToArray();
        }

        private void RestoreIndices(int[] indices)
        {
            foreach (int selectedIndex in indices)
                SelectItem(selectedIndex, false);
            SelectListView();
        }

        private void MoveItem(int offsetIndex)
        {
            int[] indices = StoreIndices();
            int index = 0;
            foreach (int selectedIndex in ListView.SelectedIndices)
            {
                if (selectedIndex == (offsetIndex <= 0 ? 0 : MyList.Count - 1))
                {
                    SelectListView();
                    return;
                }
                var item = MyList[selectedIndex];
                MyList.RemoveAt(selectedIndex);
                MyList.Insert(selectedIndex + offsetIndex, item);
                indices[index] += offsetIndex;
                index += 1;
            }
            Redraw();
            RestoreIndices(indices);
        }

        private void ItemModified()
        {
            ButtonCancel.DialogResult = DialogResult.OK;
        }

        #endregion

        #region  Events 

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonInsert.Text = ComboBoxInsert.SelectedItem.ToString();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void ButtonInsert_MouseClick(object sender, MouseEventArgs e)
        {
            int index = NearestIndex;
            if (ButtonInsert.Image == null)
            {
                AddItem(0, index);
            }
            else if (e.X > ButtonInsert.Width - 24)
            {
                ComboBoxInsert.DroppedDown = true;
            }
            else if (ComboBoxInsert.DroppedDown == true)
            {
                ComboBoxInsert.DroppedDown = false;
            }
            else
            {
                AddItem(ComboBoxInsert.SelectedIndex, index);
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedIndices.Count > 0)
            {
                foreach (int SelectedIndex in ListView.SelectedIndices.OfType<int>().Reverse())
                    RemoveItem(SelectedIndex);
            }
            else
            {
                RemoveItem(ListView.Items.Count - 1);
            }
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            MyClipboard.Clear();
            MyClipboard.AddRange(ListView.SelectedIndices.OfType<int>().Select(selectedIndex => MyList[selectedIndex]).ToArray());
            Refresh();
        }

        private void ButtonPaste_Click(object sender, EventArgs e)
        {
            int index = NearestIndex;
            foreach (dynamic ClipboardItem in MyClipboard)
            {
                if (ClipboardItem.GetType().IsGenericType)
                {
                    try
                    {
                        IHasCloneableControl CloneableClipboardItem = (IHasCloneableControl)ClipboardItem;
                        if (CloneableClipboardItem != null)
                        {
                            IHasCloneableControl Clone = (IHasCloneableControl)ClipboardItem.Clone();
                            Clone.CloneableControl = CloneableClipboardItem.CloneableControl.Clone();
                            AddItem(Clone, index);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Paste Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.AddItem(ClipboardItem.GetClone(), index);
                }
            }
        }

        private void ButtonUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void ButtonDown_Click(object sender, EventArgs e)
        {
            MoveItem(+1);
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                Redraw();
            }
            ItemModified();
        }

        private void ButtonInsert_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Interaction.MsgBox("Inserts a control of a particular type, click on the arrow to change the control type.");
        }

        private void ButtonRemove_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Interaction.MsgBox("Removes the selected control.");
        }

        private void LabelMax_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Interaction.MsgBox("The current and maximum amount of controls that can be added to this collection.");
        }

        private void TextBoxName_Enter(object sender, EventArgs e)
        {
            if (Names.Length > 1)
            {
                TextBoxName.Text = "";
            }
        }

        private void TextBoxName_TextChangeCompleted(object sender, EventArgs e)
        {
            if (Names.Length > 1)
            {
                TextBoxName.Text = "(Mixed)";
            }
            else
            {
                foreach (int SelectedIndex in ListView.SelectedIndices)
                    MyList[SelectedIndex].Name = TextBoxName.Text;
                Redraw();
                ItemModified();
            }
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem(ComboBoxInsert.SelectedIndex, NearestIndex);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonRemove_Click(sender, e);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonCopy_Click(sender, e);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonPaste_Click(sender, e);
        }

        private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonUp_Click(sender, e);
        }

        private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonDown_Click(sender, e);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            foreach (EditableListViewItem item in MyCollection)
            {
                if (MyList.Contains(item))
                    continue;
                MyCollection.Remove(item);
                ItemRemoved?.Invoke(this, item);
            }
            int index = 0;
            foreach (EditableListViewItem item in MyList)
            {
                if (MyCollection.Contains(item))
                {
                    if (MyCollection.IndexOf(item) != index)
                    {
                        MyCollection.Remove(item);
                        MyCollection.Insert(index, item);
                    }
                }
                else
                {
                    MyCollection.Insert(index, item);
                    ItemAdded?.Invoke(this, item);
                }
                index += 1;
            }
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}