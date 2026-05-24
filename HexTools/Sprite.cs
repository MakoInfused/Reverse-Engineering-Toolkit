using BasicTools;
using System;
using System.ComponentModel;
using System.Drawing;

namespace HexTools
{
    [TypeConverter(typeof(GenericTypeConverter))]
    public class Sprite : Component, ITypeConverter
    {
        //private string _Name;
        //public string Name
        //{
        //    get
        //    {
        //        if (Site != null)
        //            _Name = Site.Name;
        //        return _Name;
        //    }
        //    set
        //    {
        //        if (Site != null)
        //            Site.Name = value;
        //        _Name = value;
        //    }
        //}

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Size TotalSize
        {
            get
            {
                return new Size(Size.Width * Width, Size.Height * Height);
            }
        }

        private Size _Size = new Size(8, 8);
        [Category("Function")]
        [Description("The size of the sprites used by this image.")]
        [DefaultValue(typeof(Size), "8, 8")]
        public Size Size
        {
            get
            {
                return _Size;
            }
            set
            {
                if (value != _Size)
                {
                    _Size = value;
                }
            }
        }

        private int _Width = 0;
        [Category("Function")]
        [Description("The number of horizontal sprites.")]
        [DefaultValue(0)]
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if (value != _Width)
                {
                    _Width = value;
                }
            }
        }

        private int _Height = 0;
        [Category("Function")]
        [Description("The number of vertical sprites.")]
        [DefaultValue(0)]
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                if (value != _Height)
                {
                    _Height = value;
                }
            }
        }

        private SpriteAssembly[] _Assembly;
        [Category("Function")]
        [Description("Determines the way that sprites are put together to form an image.")]
        [DefaultValue(typeof(SpriteAssembly[]), "none")]
        public SpriteAssembly[] Assembly
        {
            get
            {
                return _Assembly;
            }
            set
            {
                if (value != _Assembly)
                {
                    _Assembly = value;
                }
            }
        }

        public Sprite(int width, int height)
        {
            Size = new Size(width, height);
        }

        public string ToTypeConverter(ITypeDescriptorContext context)
        {
            return $"Sprite {TotalSize.Width}, {TotalSize.Height}";
        }

        //public override string ToString()
        //{
        //    return $"{Name}";
        //}
    }

    [TypeConverter(typeof(GenericTypeConverter))]
    public class SpriteAssembly : ITypeConverter
    {
        [Category("Function")]
        [Description("Determines where to source the pixels from.")]
        [DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
        public Rectangle Source { get; set; }

        [Category("Function")]
        [Description("Determines where to move the pixels to.")]
        [DefaultValue(typeof(Point), "0, 0")]
        public Point Destination { get; set; }

        public string ToTypeConverter(ITypeDescriptorContext context)
        {
            return $"Assembly {Destination.X}, {Destination.Y}";
        }
    }
}
