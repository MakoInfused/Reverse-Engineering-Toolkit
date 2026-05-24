using System;
using System.Windows.Forms;
using System.ComponentModel;
using BasicTools;

namespace HexTools
{
    public partial class HexAddressBox : UserControl
    {
        public HexAddressBox()
        {
            InitializeComponent();
            HexStorage.OnLoad += HexStorage_OnLoad;
        }

        private BasicRange _Range = new BasicRange();
        [Category("Function")]
        [Description("The Address Range to use.")]
        [DefaultValue(typeof(BasicRange), "0, 0")]
        public BasicRange Range
        {
            get => _Range;
            set
            {
                if (_Range != value)
                {
                    _Range = value;
                    UpdateRange();
                }
            }
        }

        [Category("Function")]
        [Description("The Address to use.")]
        [DefaultValue(0)]
        public int Value
        {
            get => (int)Address.Value;
            set
            {
                if (Address.Value != value)
                {
                    Address.Value = value;
                }
            }
        }

        public event EventHandler<EventArgs> OnValueChange;

        private void UpdateRange()
        {
            if (Address.Maximum != Range.Max
                || Address.Minimum != Range.Min)
            {
                Address.Maximum = Range.Max;
                Address.Minimum = Range.Min;
            }
        }

        private void HexStorage_OnLoad()
        {
            if(Range.Max == 0)
            {
                Range = new BasicRange(0, HexStorage.Memory.Length);
            }
        }

        private void HexAddressBox_Load(object sender, EventArgs e)
        {
            Address.Maximum = 0xFFFFFF;
            Address.ValueChanged += Address_ValueChanged;
        }

        private void Address_ValueChanged(object sender, EventArgs e)
        {
            OnValueChange?.Invoke(this, new EventArgs());
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Value = new Editor_HexAddress().ShowAsTool(Value, Range).Offset;
        }

        private void Address_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
