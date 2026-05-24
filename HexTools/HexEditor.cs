using System;
using System.ComponentModel;
using System.Diagnostics;
using HexTools.HexEnumerations;

namespace HexTools
{

    public partial class HexEditor
    {

        public HexEditor()
        {

            // This call is required by the designer.
            InitializeComponent();

            Editor.Text = "";

            double TotalSpace = 15 * 12 / 2d;

            Editor.MaxLength = (int)Math.Round(TotalSpace);
        }

        [DefaultValue("&H000000")]
        public string FirstOffset { get; set; } = "&H000000";

        [DefaultValue("&H000000")]
        public string FinalOffset { get; set; } = "&H000000";

        [DefaultValue(EndianType.Big_Endian)]
        public EndianType Endian { get; set; }

        public new void Load()
        {
            int DataSize = HexStorage.Memory.Length - 512;

            Editor.Load();

            VScrollBar1.Minimum = 0;
            VScrollBar1.Value = 0;
            VScrollBar1.Maximum = DataSize;
            Debug.WriteLine(VScrollBar1.Maximum);
        }

        private void VScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            Debug.WriteLine(VScrollBar1.Value);
            Editor.HexOffset = VScrollBar1.Value.ToString();
            Editor.Load();
        }
    }
}