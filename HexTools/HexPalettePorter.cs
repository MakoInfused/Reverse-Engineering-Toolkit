using BasicTools;
using System;
using System.Drawing;

namespace HexTools
{
    public class HexPalettePorter : IBasicPorter
    {
        public string Name => "Palette File";

        public string Extension => ".pal";

        public byte[] Export(object source)
        {
            if (!(source is Color[] colors)) throw new ArgumentException("Object must be of type Color[]!");

            var data = new byte[0x300];

            for (int i = 0; i < colors.Length; i++)
            {
                var position = i * 3;
                data[position + 0] = colors[i].R;
                data[position + 1] = colors[i].G;
                data[position + 2] = colors[i].B;
            }

            return data;
        }

        public object Import(byte[] data)
        {
            if (!(data.Length == 0x300)) throw new ArgumentException($"Invalid .pal file: it must have a size of {0x300.ToString()} bytes!");

            var colors = new Color[byte.MaxValue];

            for (int i = 0; i < colors.Length; i++)
            {
                var position = i * 3;
                colors[i] = Color.FromArgb(data[position + 0], data[position + 1], data[position + 2]);
            }

            return colors;
        }

        public bool IsMatch(IBasicPorterArgs args) => true;
    }
}
