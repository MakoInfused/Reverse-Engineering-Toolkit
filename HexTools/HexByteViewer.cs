using System.ComponentModel;
using System.ComponentModel.Design;

namespace HexTools
{

    #region  HexByteViewer 

    [ToolboxItem(false)]
    public partial class HexByteViewer
    {
        private ByteViewer ByteViewer;

        #region  Constructor 

        public HexByteViewer()
        {

            // This call is required by the designer.
            InitializeComponent();

            ByteViewer = new ByteViewer();
            Controls.Add(ByteViewer);
        }

        #endregion

        #region  Public 

        public new void Load(byte[] Bytes)
        {
            ByteViewer.SetBytes(Bytes);
        }

        #endregion

    }
}

#endregion
