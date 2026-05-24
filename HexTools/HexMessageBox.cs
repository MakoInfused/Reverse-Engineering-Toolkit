using System;
using System.ComponentModel;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace HexTools
{
    public partial class HexMessageBox : IHexReader
    {

        public static readonly char OpenQuote = Strings.Chr(147);
        public static readonly char CloseQuote = Strings.Chr(148);

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Page
        {
            get
            {
                return _Text.CurrentPage;
            }
            set
            {
                if (value != _Text.CurrentPage)
                {
                    _Text.CurrentPage = value;
                    int offset = _Text.PageOffsets[Math.Max(_Text.CurrentPage - 1, 0)];
                    Portrait.HexOffset = "&H" + HexConvert.IntToHexRaw(offset, 5);
                    Portrait.Load();
                }
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] Pages
        {
            get
            {
                return _Text.PageOffsets;
            }
        }

        public HexMessageBox()
        {
            InitializeComponent();
        }

        private void RedrawPagePanel()
        {
            ButtonLast.Enabled = Conversions.ToBoolean(_Text.CanUseLastPage);
            ButtonNext.Enabled = Conversions.ToBoolean(_Text.CanUseNextPage);
            LabelPages.Text = $"{Page}/{Pages.Length}";
        }

        private void IHexReader_Load()
        {
            // Portrait.Load()
            _Text.Load();
            // PagingPanel.Visible = Pages.Length > 1
            // If Pages.Length > 1 Then
            // Page = 1
            // RedrawPagePanel()
            // End If
        }

        void IHexReader.Load() => IHexReader_Load();

        private void _Text_TextChanged(object sender, EventArgs e)
        {
            ButtonQuote.Checked = _Text.Text.StartsWith(Conversions.ToString(OpenQuote)) && _Text.Text.EndsWith(Conversions.ToString(CloseQuote));
        }

        private void ButtonLast_Click(object sender, EventArgs e)
        {
            Page -= 1;
            RedrawPagePanel();
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            Page += 1;
            RedrawPagePanel();
        }
    }
}