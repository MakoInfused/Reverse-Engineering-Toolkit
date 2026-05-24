using System;
using HexTools.HexEnumerations;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;
using BasicTools.BasicControls;
using BasicTools;

namespace HexTools
{
    public partial class HexAddress : UserControl
    {
        public HexAddress()
        {
            InitializeComponent();
        }

        [Category("Function")]
        [Description("The Address to use.")]
        [DefaultValue(0)]
        public int Value
        {
            get => (int)Address.Value;
            set => Address.Value = value;
        }

        [Category("Function")]
        [Description("The Address Maximum to use.")]
        [DefaultValue(0)]
        public int Maximum
        {
            get => (int)Address.Maximum;
            set => Address.Maximum = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int PCAddress
        {
            get => ConvertToType(Value, HexAddressFormatType.PC, Type);
            set => Value = ConvertToType(value, HexAddressFormatType.PC, Type);
        }

        [Category("Function")]
        [Description("The Address Type to use.")]
        [DefaultValue(HexAddressFormatType.PC)]
        public HexAddressFormatType Type { get; set; } = HexAddressFormatType.PC;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public BasicNumericBox NumericBox => Address;

        private HexAddressFormatType LastType;
        private int LastMaximum;

        private void HexAddressEditor_Load(object sender, EventArgs e)
        {
            LastMaximum = Maximum;
            LastType = Type == HexAddressFormatType.SNES_LoROM
                ? HexAddressFormatType.PC
                : HexAddressFormatType.SNES_LoROM;
            DrawOldType(LastType, Type);

            foreach (var format in Enum.GetValues(typeof(HexAddressFormatType)).Cast<HexAddressFormatType>())
            {
                if (format == HexAddressFormatType.Raw || format == HexAddressFormatType.Index) continue;
                if (Type == HexAddressFormatType.Raw) Type = format;

                var radioButton = new RadioButton();
                radioButton.AutoSize = true;
                radioButton.Text = format.DisplayName();
                radioButton.Tag = format;
                
                if (Type == format) radioButton.Checked = true;
                radioButton.CheckedChanged += RadioButton_CheckedChanged;

                TypePanel.Controls.Add(radioButton);
            }

            Address.ValueChanged += Address_ValueChanged;
        }

        private void DrawOldType(HexAddressFormatType newType, HexAddressFormatType? oldType = null)
        {
            OldTypeLabel.Text = newType.DisplayName() + ":";
            Offset.Text = oldType.HasValue
                ? HexConvert.IntToHexRaw(ConvertToType(Value, newType, oldType.Value), 5)
                : HexConvert.IntToHexRaw(Value, 5);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton radioButton && radioButton.Checked
                && radioButton.Tag is HexAddressFormatType formatType)) return;

            LastType = Type;
            DrawOldType(Type);

            Type = formatType;
            NewTypeLabel.Text = Type.DisplayName() + ":";

            var oldValue = Value;
            Maximum = Type == HexAddressFormatType.PC 
                ? LastMaximum
                : 0xFFFFFF;
            Value = ConvertToType(oldValue, Type, LastType);
        }

        private int ConvertToType(int address, HexAddressFormatType newType, HexAddressFormatType currentType)
        {
            if (currentType == newType) return address;

            switch (newType)
            {
                case HexAddressFormatType.PC:
                    return HexConvert.SnesToPC(HexConvert.IntToHex(address, 5), true);
                case HexAddressFormatType.SNES_LoROM:
                    return HexConvert.PCToSnes(HexConvert.IntToHex(address, 5), true);
                default:
                    return address;
            }
        }

        private void Address_ValueChanged(object sender, EventArgs e)
        {
            Offset.Text = HexConvert.IntToHexRaw(ConvertToType(Value, LastType, Type), 5);
        }
    }
}
