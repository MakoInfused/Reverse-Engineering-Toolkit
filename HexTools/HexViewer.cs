using System;
using System.ComponentModel;
using HexTools.HexEnumerations;

namespace HexTools
{

    #region  HexViewer 

    public partial class HexViewer : IHexControl
    {

        private HexUtility<HexViewer> HexUtility;

        #region  Constructor 

        public HexViewer()
        {

            // This call is required by the designer.
            InitializeComponent();

            HexUtility = new HexUtility<HexViewer>(this);
        }

        #endregion

        #region  Properties 

        private string _HexOffset = "&H000000";
        [Category("Function")]
        [Description("Appends a hex value to be associated with this box")]
        [DefaultValue("&H0000000")]
        public string HexOffset
        {
            get
            {
                return _HexOffset;
            }
            set
            {
                if ((value ?? "") != (_HexOffset ?? ""))
                {
                    _HexOffset = value;
                }
            }
        }

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
                }
            }
        }

        #endregion

        #region  Private 



        #endregion

        #region  Public 

        public new void Load()
        {
            int offset = HexUtility.GetHexOffset();
            byte[] buffer = new byte[HexStorage.Memory.Length - offset + 1];
            Array.Copy(HexStorage.Memory, offset, buffer, 0, buffer.Length - 1);
            HexByteViewer.Load(buffer);
        }

        public void Save(int Offset = -1)
        {

        }

        #endregion

    }
}

#endregion
