using HexTools.HexEnumerations;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;

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

        [Description("Determines the field path that will be used to bind combobox items data from a definition (user settings) file. You can use commas ',' to indicate multiple sources.")]
        [DefaultValue("")]
        public string PortraitDefinition
        {
            get
            {
                return Portrait.Definition;
            }
            set
            {
                if (Portrait.Definition != value)
                {
                    Portrait.Definition = value;
                }
            }
        }

        [Description("Determines the format to convert offsets from the definition (user settings) file.")]
        [DefaultValue(HexAddressFormatType.Raw)]
        public HexAddressFormatType PortraitDefinitionFormat
        {
            get
            {
                return Portrait.DefinitionOffsetFormat;
            }
            set
            {
                if (Portrait.DefinitionOffsetFormat != value)
                {
                    Portrait.DefinitionOffsetFormat = value;
                }
            }
        }

        [Description("Determines the field path that will be used to bind panel offset data from a definition (user settings) file.")]
        [DefaultValue("")]
        public string MessageDefinition
        {
            get
            {
                return HexPanel1.Definition;
            }
            set
            {
                if (HexPanel1.Definition != value)
                {
                    HexPanel1.Definition = value;
                }
            }
        }

        [Description("Determines the format to convert offsets from the definition (user settings) file.")]
        [DefaultValue(HexAddressFormatType.Raw)]
        public HexAddressFormatType MessageDefinitionFormat
        {
            get
            {
                return HexPanel1.DefinitionOffsetFormat;
            }
            set
            {
                if (HexPanel1.DefinitionOffsetFormat != value)
                {
                    HexPanel1.DefinitionOffsetFormat = value;
                }
            }
        }

        private HexMessageBoxDisplayMode _DisplayMode;
        [Description("Determines the type of display to use for the message box.")]
        [DefaultValue(HexMessageBoxDisplayMode.Simple)]
        public HexMessageBoxDisplayMode DisplayMode
        {
            get
            {
                return _DisplayMode;
            }
            set
            {
                if (_DisplayMode != value)
                {
                    _DisplayMode = value;

                    LabelPortrait.Visible = Portrait.Visible = ButtonQuote.Visible = ButtonClose.Visible =
                    ButtonLast.Visible = ButtonNext.Visible = LabelPages.Visible = ButtonAdd.Visible =
                    ButtonRemove.Visible = _DisplayMode == HexMessageBoxDisplayMode.Paged;
                }
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
            _Text.Load();
            if(DisplayMode == HexMessageBoxDisplayMode.Paged)
            {
                Portrait.Load();
                PagingPanel.Visible = Pages.Length > 1;
                if(Pages.Length > 1)
                {
                    Page = 1;
                    RedrawPagePanel();
                }
            }
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

    public enum HexMessageBoxDisplayMode
    {
        Simple,
        Paged
    }
}