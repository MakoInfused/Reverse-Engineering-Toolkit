using System;
using System.Windows.Forms;
using BasicTools;

namespace HexTools
{

    public partial class Editor_GoTo : HexForm
    {
        public Editor_GoTo()
        {
            InitializeComponent();
        }

        private void OK_Button_Click(object Sender, EventArgs e)
        {
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FormGoToResult ShowAsTool()
        {
            HexBox.Select();
            return new FormGoToResult(this);
        }

    }

    public class FormGoToResult
    {
        public int Offset { get; private set; } = -1;

        public FormGoToResult(Editor_GoTo Dialog)
        {
            var Result = Dialog.ShowFormAsTool();
            if (Result == DialogResult.OK)
            {
                Offset = HexConvert.HexToIntRaw(Dialog.HexBox.Text);
            }
        }

    }
}