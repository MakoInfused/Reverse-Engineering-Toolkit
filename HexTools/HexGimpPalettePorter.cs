using BasicTools;
using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HexTools
{
    public class HexGimpPalettePorter : IBasicPorter
    {
        public string Name => "Gimp Palette File";

        public string Extension => ".gpl";

        private string ByteToString(byte value)
        {
            return value.ToString().PadRight(3);
        }

        private string ColorToIntString(Color color)
        {
            return $"{ByteToString(color.R)} {ByteToString(color.G)} {ByteToString(color.B)}";
        }

        private string ColorToHexString(Color color)
        {
            return $"#{HexConvert.BytesToHex(new byte[] { color.R, color.G, color.B })}";
        }

        public byte[] Export(object source)
        {
            if (!(source is Color[] colors)) throw new ArgumentException("Object must be of type Color[]!");

            var text = new StringBuilder("GIMP Palette" + Environment.NewLine);

            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];

                text.AppendLine($"{ColorToIntString(color)} {ColorToHexString(color)}");
            }

            return Encoding.ASCII.GetBytes(text.ToString());
        }

        private byte StringToByte(string value)
        {
            return byte.Parse(value.Trim());
        }

        private Color IntStringToColor(string color)
        {
            return Color.FromArgb(
                StringToByte(color.Substring(0, 3)),
                StringToByte(color.Substring(4, 3)),
                StringToByte(color.Substring(8, 3))
            );
        }

        private Color HexStringToColor(string color)
        {
            return Color.FromArgb(
                StringToByte(color.Substring(1, 2)),
                StringToByte(color.Substring(3, 2)),
                StringToByte(color.Substring(5, 2))
            );
        }

        public object Import(byte[] data)
        {
            var text = new string(Encoding.ASCII.GetChars(data));

            if (!text.StartsWith("GIMP Palette")) throw new ArgumentException(@"Invalid gpl file: it must start with the phrase ""GIMP Palette""!");

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            var colors = new Color[0x100];
            var color = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.Length > 0 && (char.IsWhiteSpace(line[0]) || char.IsDigit(line[0])))
                {
                    colors[color] = IntStringToColor(line);
                    color++;
                }
            }

            return colors;
        }

        public bool IsMatch(IBasicPorterArgs args) => true;
    }
}
