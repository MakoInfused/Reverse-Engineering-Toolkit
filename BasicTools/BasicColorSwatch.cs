using System;
using System.ComponentModel;
using System.Drawing;

namespace BasicTools
{

    [DefaultEvent("Click")]
    public partial class BasicColorSwatch
    {

        public BasicColorSwatch()
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            // Raw.ForeColor = MyBase.ForeColor
            Raw.Font = base.Font;
        }

        private void Me_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                // Raw.ForeColor = MyBase.ForeColor
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                Raw.Font = base.Font;
            }
        }

        // Private _TextAlign As ContentAlignment = ContentAlignment.MiddleCenter
        // <Category("Appearance")>
        // <Description("")>
        // <DefaultValue(ContentAlignment.MiddleCenter)>
        // Public Property TextAlign As ContentAlignment
        // Get
        // Return _TextAlign
        // End Get
        // Set(value As ContentAlignment)
        // _TextAlign = value
        // Select Case _TextAlign
        // Case ContentAlignment.BottomCenter, ContentAlignment.BottomLeft, ContentAlignment.BottomRight
        // Label1.Location = New Point(Label1.Location.X, Border.Height + Border.Location.Y + 1)
        // Case ContentAlignment.MiddleCenter
        // Label1.Location = New Point(Math.Ceiling((Border.Width - Label1.Width) / 2), Math.Ceiling((Border.Height - Label1.Height) / 2))
        // End Select
        // End Set
        // End Property

        [DefaultValue(typeof(Size), "32, 32")]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        [Category("Appearance")]
        [Description("")]
        [DefaultValue(typeof(Color), "Black")]
        public Color BorderColor
        {
            get
            {
                return Border.BorderColor;
            }
            set
            {
                Border.BorderColor = value;
            }
        }

        [Category("Appearance")]
        [Description("")]
        [DefaultValue(typeof(Color), "Black")]
        public Color SwatchColor
        {
            get
            {
                return Swatch.FillColor;
            }
            set
            {
                Swatch.FillColor = value;
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object BackgroundImageLayout { get; set; }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object BackgroundImage { get; set; }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object AutoScroll { get; set; }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object AutoScrollMargin { get; set; }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object AutoScrollMinSize { get; set; }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object AutoSize { get; set; }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public new object AutoSizeMode { get; set; }

    }
}