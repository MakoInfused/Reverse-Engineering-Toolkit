using BasicTools;
using System;
using System.Drawing;
using System.IO;

namespace HexTools
{
    public class HexImagePalettePorter : IBasicPorter
    {
        public string Name => "PNG Image File";

        public string Extension => ".png";

        public byte[] Export(object source)
        {
            if (!(source is Color[] colors)) throw new ArgumentException("Object must be of type Color[]!");

            var data = new Bitmap(16, 1);

            for (int i = 0; i < colors.Length; i++)
            {
                var position = i * 3;
                data.SetPixel(i, 0, colors[i]);
            }

            return (byte[])new ImageConverter().ConvertTo(data, typeof(byte[]));
        }

        public object Import(byte[] data)
        {
            try
            {
                var image = new Bitmap(new MemoryStream(data));

                var colors = new Color[byte.MaxValue];

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = image.GetPixel(i, 0);
                }

                return colors;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Invalid .png file!");
            }
        }

        public bool IsMatch(IBasicPorterArgs args) => true;
    }
}
