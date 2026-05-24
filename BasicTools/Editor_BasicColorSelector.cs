using System;
using System.Drawing;
using System.Windows.Forms;

namespace BasicTools
{

    public partial class Editor_BasicColorSelector
    {
        public Editor_BasicColorSelector()
        {
            InitializeComponent();
        }

        public void Setup()
        {
            ButtonOK.DialogResult = DialogResult.OK;
            ButtonCancel.DialogResult = DialogResult.Cancel;

            if (ShortcutKeys == true)
            {
                AcceptButton = ButtonOK;
                CancelButton = ButtonCancel;
            }
            else
            {
                AcceptButton = null;
                CancelButton = null;
            }

            UpdatePrevious();
            UpdatePreview();
        }

        private void UpdatePrevious()
        {
            PreviewSwatchBefore.FillColor = _OldColor;
            R.Value = OldColor.R;
            G.Value = OldColor.G;
            B.Value = OldColor.B;
        }

        private void UpdatePreview()
        {
            NewColor = Color.FromArgb(255, (int)Math.Round(R.Value), (int)Math.Round(G.Value), (int)Math.Round(B.Value));
            PreviewSwatchAfter.FillColor = NewColor;
        }

        private void RGB_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void NumericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                dynamic target = sender;
                target.Focus();
            }
        }

        private Color _OldColor = Color.Black;
        public Color OldColor
        {
            get
            {
                return _OldColor;
            }
            set
            {
                if (_OldColor != value)
                {
                    _OldColor = value;
                }
            }
        }

        private Color _NewColor = Color.Black;
        public Color NewColor
        {
            get
            {
                return _NewColor;
            }
            set
            {
                if (_NewColor != value)
                {
                    _NewColor = value;
                }
            }
        }

        private bool _ShortcutKeys = false;
        public bool ShortcutKeys
        {
            get
            {
                return _ShortcutKeys;
            }
            set
            {
                if (_ShortcutKeys != value)
                {
                    _ShortcutKeys = value;
                }
            }
        }

    }
}