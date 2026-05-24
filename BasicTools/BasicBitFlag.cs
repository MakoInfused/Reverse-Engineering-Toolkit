using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BasicTools
{

    [DefaultEvent("ControlClick")]
    [Serializable]
    public partial class BasicBitFlag
    {

        [Category("Function")]
        public event BitChanged BitChange;

        public BasicBitFlag()
        {
            // This call is required by the designer.
            InitializeComponent();
        }

        public event EventHandler ControlClick;

        private int _Order = 0;
        [DefaultValue(0)]
        [Category("Function")]
        public int Order
        {
            get
            {
                return _Order;
            }
            set
            {
                if (_Order != value)
                {
                    _Order = value;
                }
            }
        }

        [Category("Function")]
        public string Label
        {
            get
            {
                return CheckBox.Text;
            }
            set
            {
                if ((CheckBox.Text ?? "") != (value ?? ""))
                {
                    CheckBox.Text = value;
                }
            }
        }

        [Category("Function")]
        [DefaultValue(false)]
        public bool Value
        {
            get
            {
                return CheckBox.Checked;
            }
            set
            {
                if (CheckBox.Checked != value)
                {
                    CheckBox.Checked = value;
                }
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BitChange?.Invoke(this, new BitChangeArgs(CheckBox.Checked));
        }

        private void RaiseClick(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            RaiseClick(this, e);
        }
    }

    public delegate void BitChanged(BasicBitFlag Sender, BitChangeArgs Data);

    public class BitChangeArgs
    {

        public BitChangeArgs(bool Value)
        {
            this.Value = Value;
        }

        public bool Value;

    }
}