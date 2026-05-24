using System;
using System.Linq;
using System.Windows.Forms;
using BasicTools;
using HexTools.HexEnumerations;

namespace HexTools
{

    public partial class Editor_Find : HexForm
    {
        public Editor_Find()
        {
            InitializeComponent();
        }

        public FindResult ShowAsTool(FindQuantityType Quantity, FindDirectionType Direction)
        {
            TextBox.Select();
            QuantityBox.Controls.OfType<RadioButton>().First((RadioButton) => RadioButton.TabIndex == (int)Quantity).Checked = true;
            DirectionBox.Controls.OfType<RadioButton>().First((RadioButton) => RadioButton.TabIndex == (int)Direction).Checked = true;
            return new FindResult(this);
        }

        private void HexButton_CheckedChanged(object sender, EventArgs e)
        {
            TextBox.Input = InputType.Hex;
            TextBox.Display = DisplayType.Hex;
        }

        private void TextButton_CheckedChanged(object sender, EventArgs e)
        {
            TextBox.Input = InputType.Normal;
            TextBox.Display = DisplayType.Text;
        }

        private void TableButton_CheckedChanged(object sender, EventArgs e)
        {
            TextBox.Input = InputType.Normal;
            TextBox.Display = DisplayType.Text;
        }

        private void OneButton_CheckedChanged(object sender, EventArgs e)
        {
            DirectionBox.Enabled = true;
        }

        private void AllButton_CheckedChanged(object sender, EventArgs e)
        {
            DirectionBox.Enabled = false;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (OK_Button.Enabled == true && TextBox.Text.Length == 0)
            {
                OK_Button.Enabled = false;
            }
            else if (OK_Button.Enabled == false && TextBox.Text.Length > 0)
            {
                OK_Button.Enabled = true;
            }
        }
    }

    public class FindResult
    {
        public string Search { get; private set; } = "";
        public FindQuantityType Quantity { get; private set; } = FindQuantityType.One;
        public FindLocationType Location { get; private set; } = FindLocationType.Hex;
        public FindDirectionType Direction { get; private set; } = FindDirectionType.First;

        public FindResult(Editor_Find Dialog)
        {
            var Result = Dialog.ShowFormAsTool();
            if (Result == DialogResult.OK)
            {
                Search = Dialog.TextBox.Text;
                Quantity = (FindQuantityType)Dialog.QuantityBox.Controls.OfType<RadioButton>().First((RadioButton) => RadioButton.Checked).TabIndex;
                Location = (FindLocationType)Dialog.LocationBox.Controls.OfType<RadioButton>().First((RadioButton) => RadioButton.Checked).TabIndex;
                Direction = (FindDirectionType)Dialog.DirectionBox.Controls.OfType<RadioButton>().First((RadioButton) => RadioButton.Checked).TabIndex;
            }
        }

    }
}