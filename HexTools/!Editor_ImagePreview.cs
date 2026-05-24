using System;
using System.Drawing;
using System.Windows.Forms;
using BasicTools;

namespace HexTools
{

    public partial class Editor_ImagePreview : HexForm
    {
        public Editor_ImagePreview()
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

        public FormImagePreviewResult ShowAsTool(Image image)
        {
            Preview.Image = image;
            Preview.Cursor = new Cursor(My.Resources.Resources.MagnifyPlus.GetHicon());
            return new FormImagePreviewResult(this);
        }

        private float ZoomFactor = 0.1f;
        private Size ZoomScale;
        private bool Shift = false;

        private void Preview_Click(object sender, EventArgs e)
        {
            ZoomScale = new Size((int)(Preview.Size.Width * ZoomFactor), (int)(Preview.Size.Height * ZoomFactor));
            if (Shift)
            {
                Preview.Size -= ZoomScale;
            }
            else
            {
                Preview.Size += ZoomScale;
            }
        }

        private void Preview_MouseCursor()
        {
            var shiftHeld = ModifierKeys == Keys.Shift;
            if (Shift != shiftHeld)
            {
                Shift = shiftHeld;
                Preview.Cursor = new Cursor(!Shift
                    ? My.Resources.Resources.MagnifyPlus.GetHicon()
                    : My.Resources.Resources.MagnifyMinus.GetHicon());
            }
        }

        Timer Timer;

        private void Preview_MouseEnter(object sender, EventArgs e)
        {
            Timer = new Timer();
            Timer.Interval = 1;
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Preview_MouseCursor();
        }

        private void Preview_MouseLeave(object sender, EventArgs e)
        {
            Timer.Stop();
            Timer.Dispose();
            Timer = null;
        }
    }

    public class FormImagePreviewResult
    {
        public Image Preview { get; private set; }

        public FormImagePreviewResult(Editor_ImagePreview Dialog)
        {
            var Result = Dialog.ShowFormAsTool(true);
            if (Result == DialogResult.OK)
            {
                Preview = Dialog.Preview.Image;
            }
        }

    }
}