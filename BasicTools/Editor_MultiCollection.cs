using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using BasicTools.BasicControls;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BasicTools
{

    public partial class Editor_MultiCollection
    {

        public List<object> MyList;
        public List<string> MyType;
        private object MyCopy;

        public Editor_MultiCollection()
        {
            InitializeComponent();
            PropertyGrid1 = _PropertyGrid1;
            Label1 = _Label1;
            Label2 = _Label2;
            ButtonOK = _ButtonOK;
            ButtonCancel = _ButtonCancel;
            ButtonRemove = _ButtonRemove;
            ComboBox1 = _ComboBox1;
            TableLayoutPanel1 = _TableLayoutPanel1;
            LabelMax = _LabelMax;
            UiListView1 = _UiListView1;
            ColumnHeader1 = _ColumnHeader1;
            ButtonPaste = _ButtonPaste;
            ButtonCopy = _ButtonCopy;
            ButtonDown = _ButtonDown;
            ButtonUp = _ButtonUp;
            ButtonInsert = _ButtonInsert;
            _PropertyGrid1.Name = "PropertyGrid1";
            _Label1.Name = "Label1";
            _Label2.Name = "Label2";
            _ButtonOK.Name = "ButtonOK";
            _ButtonCancel.Name = "ButtonCancel";
            _ButtonRemove.Name = "ButtonRemove";
            _ComboBox1.Name = "ComboBox1";
            _TableLayoutPanel1.Name = "TableLayoutPanel1";
            _LabelMax.Name = "LabelMax";
            _UiListView1.Name = "UiListView1";
            _ButtonPaste.Name = "ButtonPaste";
            _ButtonCopy.Name = "ButtonCopy";
            _ButtonDown.Name = "ButtonDown";
            _ButtonUp.Name = "ButtonUp";
            _ButtonInsert.Name = "ButtonInsert";
        }

        public void Start(object MyCollection)
        {
            MyList = new List<object>();
            MyType = new List<string>();

            Gather(MyCollection);
            Setup();
            Collect();
            Init();
            Refresh();

            ButtonOK.DialogResult = DialogResult.OK;
            ButtonCancel.DialogResult = DialogResult.Cancel;
        }

        private void DrawMaxItems()
        {
            if (MaxSize() < 0)
            {
                LabelMax.Visible = false;
                return;
            }
            LabelMax.Text = MyList.Count + "/" + MaxSize();
        }

        protected internal virtual void Gather(object MyCollection)
        {
            foreach (MultiCollection item in (IEnumerable)MyCollection)
                MyList.Add(item.Editor);
        }

        private void Collect()
        {
            UiListView1.Items.Clear();
            foreach (dynamic index in MyList)
            {
                if (index != null)
                {
                    UiListView1.Items.Add(Operators.ConcatenateObject(Operators.ConcatenateObject(index.GetType().Name + "{", index.Text), "}"));
                }
                else
                {
                    string name = Conversions.ToString(NothingName());
                    if (string.IsNullOrEmpty(name))
                    {
                        UiListView1.Items.Add("");
                    }
                    else
                    {
                        UiListView1.Items.Add(name);
                    }
                }
            }
        }

        private object NothingName()
        {
            string name = Constants.vbNullString;
            for (int index = 0, loopTo = MyType.Count - 1; index <= loopTo; index++)
            {
                if (MyType[index] == "Nothing")
                {
                    name = ComboBox1.Items[index].ToString();
                    break;
                }
            }
            return name;
        }

        private void Init()
        {
            UiListView1.Select();
            if (UiListView1.SelectedItems.Count == 0 & UiListView1.Items.Count > 0)
                UiListView1.Items[0].Selected = true;
            if (ComboBox1.Items.Count > 0)
            {
                ComboBox1.SelectedIndex = 0;
                ButtonInsert.Text = ComboBox1.SelectedItem.ToString();
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            DrawMaxItems();
            if (UiListView1.SelectedItems.Count == 0)
            {
                PropertyGrid1.SelectedObject = null;
                return;
            }
            if (MyList[UiListView1.SelectedIndices[0]] != null)
            {
                PropertyGrid1.SelectedObject = MyList[UiListView1.SelectedIndices[0]];
                Label2.Text = MyList[UiListView1.SelectedIndices[0]].GetType().Name + " properties:";
            }
            else
            {
                PropertyGrid1.SelectedObject = null;
            }
            ButtonInsert.Enabled = MaxSize() == -1 || UiListView1.Items.Count < MaxSize();
        }

        protected internal virtual void Setup()
        {
            GetComboBox().Items.AddRange(new[] { "TextBox" });
            MyType.AddRange(new[] { "System.Windows.Forms.TextBox" });
        }

        protected internal ComboBox GetComboBox()
        {
            return ComboBox1;
        }

        protected internal virtual int MaxSize()
        {
            return -1;
        }

        protected internal virtual object ItemName()
        {
            return "MultiCollectionItem";
        }

        public virtual object NewValue()
        {
            var Value = new List<MultiCollection>();
            foreach (object item in MyList)
            {
                var Collection = new MultiCollection();
                Collection.Editor = item;
                Value.Add(Collection);
            }
            return Value;
        }

        private void Redraw(int index, int operation = 0)
        {
            Collect();
            if (index + operation > -1)
            {
                UiListView1.Items[index + operation].Selected = true;
            }
            Refresh();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonInsert.Text = ComboBox1.SelectedItem.ToString();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void ButtonInsert_MouseClick(object sender, MouseEventArgs e)
        {
            int index = UiListView1.SelectedIndices.Count > 0 ? UiListView1.SelectedIndices[0] : -1;
            if (e.X > ButtonInsert.Width - 24)
            {
                ComboBox1.DroppedDown = true;
            }
            else if (ComboBox1.DroppedDown == true)
            {
                ComboBox1.DroppedDown = false;
            }
            else if (MaxSize() < 0 | MyList.Count < MaxSize())
            {
                dynamic item = null;
                if (!(MyType[ComboBox1.SelectedIndex] == "Nothing"))
                {
                    item = Activator.CreateInstance(Type.GetType(MyType[ComboBox1.SelectedIndex]));
                    item.Name = Operators.ConcatenateObject(ItemName(), (ComboBox1.SelectedIndex + 1).ToString());
                }
                MyList.Add(item);
                Redraw(index, 1);
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            int index = UiListView1.SelectedIndices.Count > 0 ? UiListView1.SelectedIndices[0] : -1;
            if (UiListView1.SelectedItems.Count > 0)
            {
                MyList.RemoveAt(index);
                if (MyList.Count - 1 > 0)
                {
                    if (index - 1 == MyList.Count - 1)
                        Redraw(index, -1);
                    else
                        Redraw(index);
                }
                else if (MyList.Count > 0)
                {
                    Redraw(0);
                }
                else
                {
                    Redraw(-1);
                }
            }
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            int index = UiListView1.SelectedIndices.Count > 0 ? UiListView1.SelectedIndices[0] : -1;
            if (UiListView1.SelectedItems.Count > 0)
            {
                MyCopy = MyList[index];
            }
        }

        private void ButtonPaste_Click(object sender, EventArgs e)
        {
            int index = UiListView1.SelectedIndices.Count > 0 ? UiListView1.SelectedIndices[0] : -1;
            if (MaxSize() < 0 | MyList.Count < MaxSize())
            {
                if (MyCopy != null)
                {
                    MyList.Insert(index, MyCopy);
                    Redraw(index);
                }
            }
        }

        private void ButtonUp_Click(object sender, EventArgs e)
        {
            int index = UiListView1.SelectedIndices[0];
            if (index - 1 < 0)
                return;

            var item = MyList[index];
            MyList.RemoveAt(index);
            MyList.Insert(index - 1, item);

            Redraw(index - 1);
        }

        private void ButtonDown_Click(object sender, EventArgs e)
        {
            int index = UiListView1.SelectedIndices[0];
            if (index + 1 > UiListView1.Items.Count - 1)
                return;

            var item = MyList[index];
            MyList.RemoveAt(index);
            MyList.Insert(index + 1, item);

            Redraw(index + 1);
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Text")
            {
                Redraw(UiListView1.SelectedIndices[0]);
            }
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
    }
}