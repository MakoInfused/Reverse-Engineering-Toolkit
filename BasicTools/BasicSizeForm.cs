using System;
using System.Windows.Forms;

namespace BasicTools
{
    public partial class BasicSizeForm : Form
    {
        public BasicSizeForm()
        {
            InitializeComponent();
        }

        public int Value
        {
            get => (int) MaxSize.Value;
            set => MaxSize.Value = value;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public static FormBasicSizeFormResult ShowAsTool(int initialSize)
        {
            var dialog = new BasicSizeForm();
            dialog.Value = initialSize;
            dialog.MaxSize.Select();
            return new FormBasicSizeFormResult(dialog);
        }
    }

    public class FormBasicSizeFormResult
    {
        public BasicSizeForm Dialog { get; set; }
        public int Size { get; private set; } = -1;

        public FormBasicSizeFormResult(BasicSizeForm dialog)
        {
            Dialog = dialog;

            var Result = Dialog.ShowFormAsTool();
            if (Result == DialogResult.OK)
            {
                Size = Dialog.Value;
            }
        }

    }
}
