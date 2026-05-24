using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BasicTools
{

    public partial class Editor_BasicColorCreator
    {

        [DllImport("Gdi32.dll")]
        public static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public Editor_BasicColorCreator()
        {
            // Completes the normal routine
            InitializeComponent();

            AcceptButton = ButtonOK;
            CancelButton = ButtonCancel;
            ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // red+
            // red green+
            // red- green
            // green+
            // green blue+
            // green- blue
            // blue+
            // blue red+
            // blue- red
            // no need to go below 18, main increments by 3, minor increments by 2

            var canvas = e.Graphics;
            var palette = new Bitmap(16 + 2, 251 + 2);
            Color pixel;
            byte r, g, b;
            r = 186;
            g = 18;
            b = 18;
            int increment = 4;
            for (byte y = 0; y <= 252; y++)
            {
                // Select Case y
                // Case Is <= 28
                // r += increment
                // Case Is <= 56
                // g += increment
                // Case Is <= 84
                // r -= increment
                // Case Is <= 112
                // g += increment
                // Case Is <= 140
                // b += increment
                // Case Is <= 168
                // g -= increment
                // Case Is <= 196
                // b += increment
                // Case Is <= 224
                // r += increment
                // Case Is <= 252
                // b -= increment
                // End Select

                // Select Case y
                // Case Is <= 42
                // g += increment
                // Case Is <= 84
                // r -= increment
                // Case Is <= 126
                // b += increment
                // Case Is <= 168
                // g -= increment
                // Case Is <= 210
                // r += increment
                // Case Is <= 252
                // b -= increment
                // End Select

                for (byte x = 0; x <= 16; x++)
                {
                    pixel = Color.FromArgb(255, r, g, b);
                    palette.SetPixel(x, y, pixel);
                }
            }

            canvas.DrawImage(palette, 0, 0);
        }

    }
}