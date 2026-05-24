using BasicTools;
using BasicTools.BasicControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexTools
{
    public partial class Editor_AddressPicker : HexForm
    {
        public Editor_AddressPicker()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public MemoryChunk Selected { get; set; }

        public FormAddressSelectorResult ShowAsTool(MemoryChunk[] adddresses)
        {
            Selected = new MemoryChunk(-1, 0);
            Addresses.Items.AddRange(
                adddresses.Select(x => new BasicListBoxItem(
                    x,
                    $"{HexConvert.IntToAddress((int)x.Address)}" + (x.Length > 0 ? $": {x.Length}" : "")
                ))
            );
            Addresses.Select();
            return new FormAddressSelectorResult(this);
        }

        private void Addresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            OK_Button.Enabled = Addresses.SelectedIndex >= 0;
            if (OK_Button.Enabled)
            {
                Selected = Addresses.SelectedItem.Tag as MemoryChunk;
            }
        }
    }

    public class FormAddressSelectorResult
    {
        public MemoryChunk Selected { get; private set; }

        public FormAddressSelectorResult(Editor_AddressPicker Dialog)
        {
            Selected = Dialog.Selected;
            var Result = Dialog.ShowFormAsTool();
            if (Result == DialogResult.OK)
            {
                Selected = Dialog.Selected;
            }
        }

    }
}
