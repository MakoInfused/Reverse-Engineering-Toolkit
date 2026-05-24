using System;
using System.ComponentModel;

namespace BasicTools
{

    [DefaultEvent("ControlClick")]
    public partial class BasicBitFlags
    {

        [Category("Function")]
        public event BitsChanged BitsChange;

        public BasicBitFlags()
        {
            // This call is required by the designer.
            InitializeComponent();
        }

        public event EventHandler ControlClick;

        [Category("Appearance")]
        [Description("Gets or sets the Bits Per Row.")]
        public int BitsPerRow
        {
            get
            {
                return Layout.ColumnCount;
            }
            set
            {
                if (Layout.ColumnCount != value)
                {
                    Layout.ColumnCount = value;
                }
            }
        }

        [Category("Function")]
        [Description("The 1st Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag1
        {
            get
            {
                return BasicBitFlag1;
            }
        }

        [Category("Function")]
        [Description("The 2nd Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag2
        {
            get
            {
                return BasicBitFlag2;
            }
        }

        [Category("Function")]
        [Description("The 3rd Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag3
        {
            get
            {
                return BasicBitFlag3;
            }
        }

        [Category("Function")]
        [Description("The 4th Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag4
        {
            get
            {
                return BasicBitFlag4;
            }
        }

        [Category("Function")]
        [Description("The 5th Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag5
        {
            get
            {
                return BasicBitFlag5;
            }
        }

        [Category("Function")]
        [Description("The 6th Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag6
        {
            get
            {
                return BasicBitFlag6;
            }
        }

        [Category("Function")]
        [Description("The 7th Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag7
        {
            get
            {
                return BasicBitFlag7;
            }
        }

        [Category("Function")]
        [Description("The 8th Bit Flag.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BasicBitFlag Flag8
        {
            get
            {
                return BasicBitFlag8;
            }
        }

        [Browsable(false)]
        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BasicBitFlag[] Flags
        {
            get
            {
                return new BasicBitFlag[] { Flag1, Flag2, Flag3, Flag4, Flag5, Flag6, Flag7, Flag8 };
            }
        }

        private void BasicBitFlag1_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(0, Sender, Data));
        }

        private void BasicBitFlag2_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(1, Sender, Data));
        }

        private void BasicBitFlag3_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(2, Sender, Data));
        }

        private void BasicBitFlag4_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(3, Sender, Data));
        }

        private void BasicBitFlag5_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(4, Sender, Data));
        }

        private void BasicBitFlag6_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(5, Sender, Data));
        }

        private void BasicBitFlag7_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(6, Sender, Data));
        }

        private void BasicBitFlag8_BitChange(BasicBitFlag Sender, BitChangeArgs Data)
        {
            BitsChange?.Invoke(this, new BitsChangeArgs(7, Sender, Data));
        }

        private void RaiseClick(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            RaiseClick(this, e);
        }
    }

    public delegate void BitsChanged(BasicBitFlags Sender, BitsChangeArgs Data);

    public class BitsChangeArgs
    {

        public BitsChangeArgs(int Index, BasicBitFlag Bit, BitChangeArgs Value)
        {
            this.Index = Index;
            this.Bit = Bit;
            this.Value = Value;
        }

        public int Index;
        public BasicBitFlag Bit;
        public BitChangeArgs Value;

    }
}