using BasicTools;
using System.ComponentModel;
using System.Windows.Forms;

namespace HexTools
{
    public partial class Editor_HexAddress : Form
    {
        public Editor_HexAddress()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int Offset
        {
            get => HexAddress.PCAddress;
            private set => HexAddress.PCAddress = value;
        }

        public FormHexAddressResult ShowAsTool(int offset, BasicRange range)
        {
            HexAddress.NumericBox.Minimum = range.Min;
            HexAddress.NumericBox.Maximum = range.Max;
            Offset = offset;
            HexAddress.NumericBox.Select();
            return new FormHexAddressResult(this);
        }
    }

    public class FormHexAddressResult
    {
        public int Offset { get; private set; } = -1;

        public FormHexAddressResult(Editor_HexAddress Dialog)
        {
            Offset = Dialog.Offset;
            var Result = Dialog.ShowFormAsTool();
            if (Result == DialogResult.OK)
            {
                Offset = Dialog.Offset;
            }
        }

    }
}
