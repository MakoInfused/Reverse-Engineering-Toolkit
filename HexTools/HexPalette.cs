using BasicTools;
using HexTools.HexEnumerations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HexTools
{
    public partial class HexPalette : UserControl, IHexControl
    {
        public HexPalette()
        {
            InitializeComponent();

            var supportedTypes = "";
            foreach (var porter in Porters)
            {
                supportedTypes += (supportedTypes.Length == 0 ? "" : "|") +
                    $"{porter.Name}|*{porter.Extension}";
            }
            ImportFileDialog.Filter = supportedTypes;
            ExportFileDialog.Filter = supportedTypes;
        }

        public override string Text
        {
            get => GroupBox.Text;
            set => base.Text = GroupBox.Text = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public HexColorSwatch[] AllColorSwatches => GroupBox.Controls.OfType<HexColorSwatch>()
            .OrderBy(x => HexConvert.HexToInt(x.HexOffset))
            .ToArray();

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public HexColorSwatch[] ColorSwatches => AllColorSwatches
            .Take(Colors)
            .ToArray();

        public readonly IBasicPorterContainer Porters = new BasicPorterContainer(
            new HexPalettePorter(),
            new HexGimpPalettePorter(),
            new HexImagePalettePorter()
        );

        private EndianType _Endian = EndianType.Big_Endian;
        [Category("Function")]
        [Description("Determines whether the data is stored using Big Endian (Forward) or Little Endian (Reversed).")]
        [DefaultValue(EndianType.Big_Endian)]
        public EndianType Endian
        {
            get
            {
                return _Endian;
            }
            set
            {
                if (_Endian != value)
                {
                    _Endian = value;
                    foreach (var colorSwatch in ColorSwatches)
                    {
                        colorSwatch.Endian = value;
                    }
                }
            }
        }

        private string _HexOffset = "&H000000";
        [Category("Function")]
        [Description("Appends a hex value to be associated with this palette.")]
        [DefaultValue("&H000000")]
        public string HexOffset
        {
            get
            {
                return _HexOffset;
            }
            set
            {
                if ((_HexOffset ?? "") != (value ?? ""))
                {
                    _HexOffset = value;
                    for (int i = 0; i < ColorSwatches.Length; i++)
                    {
                        var colorSwatch = ColorSwatches[i];
                        colorSwatch.HexOffset = HexConvert.IntToHex(i * 2, 6);
                    }
                }
            }
        }

        private ColorDepthType _ColorDepth = ColorDepthType.FormatRGB555;
        [Category("Function")]
        [Description("Determines the depth of the colors in this palette.")]
        [DefaultValue(ColorDepthType.FormatRGB555)]
        public ColorDepthType ColorDepth { get; set; }

        private ImageEncodingType _Encoding = ImageEncodingType.FormatPlanar4BPP;
        [Category("Function")]
        [Description("Provides a pixel format algorithim to be used when rendering or storing this image.")]
        [DefaultValue(ImageEncodingType.FormatPlanar4BPP)]
        public ImageEncodingType Encoding
        {
            get
            {
                return _Encoding;
            }
            set
            {
                if (value != _Encoding)
                {
                    _Encoding = value;

                    var colorSwatches = AllColorSwatches;
                    for (int i = 0; i < colorSwatches.Length; i++)
                    {
                        colorSwatches[i].Visible = i < Colors;
                    }
                }
            }
        }

        public int Colors
        {
            get
            {
                switch (Encoding)
                {
                    case ImageEncodingType.FormatPlanar2BPP:
                        return 4;
                    case ImageEncodingType.FormatPlanar4BPP:
                        return 16;
                    default:
                        return 0;
                }
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if(ImportFileDialog.ShowDialog() == DialogResult.OK)
            {
                Color[] colorSwatches = (Color[]) Porters.Import(
                    File.ReadAllBytes(ImportFileDialog.FileName), 
                    BasicPorterArgs.FromExtension(ImportFileDialog.FileName)
                );
                for (int i = 0; i < Colors; i++)
                {
                    ColorSwatches[i].SetValue(colorSwatches[i]);
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if(ExportFileDialog.ShowDialog() == DialogResult.OK)
            {
                var colorData = Porters.Export(
                    ColorSwatches.Select(x => HexColorSwatch.From15BitInt((ushort) x.Value)).ToArray(),
                    BasicPorterArgs.FromExtension(ExportFileDialog.FileName)
                );
                File.WriteAllBytes(ExportFileDialog.FileName, colorData);
            }
        }

        public new void Load()
        {
            foreach (var colorSwatch in ColorSwatches)
            {
                colorSwatch.Load();
            }
        }

        public void Save(int Offset = -1)
        {
            foreach (var colorSwatch in ColorSwatches)
            {
                colorSwatch.Save();
            }
        }
    }
}
