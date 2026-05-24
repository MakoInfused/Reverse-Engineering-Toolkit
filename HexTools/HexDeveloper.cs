using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using BasicTools;
using HexTools.HexEnumerations;
using HexTools.HexStructures;
using Microsoft.VisualBasic.CompilerServices;
using WpfHexaEditor.Core.Bytes;

namespace HexTools
{

    public partial class HexDeveloper : IHexControl
    {

        private byte[] Buffer;

        #region  Constructor 

        public HexDeveloper()
        {
            // This call is required by the designer.
            InitializeComponent();

            // Attempted fix for broken designer due to WPF control found here:
            // https://stackoverflow.com/a/39314917
            // but it didn't w
            this.LoadViewFromUri("/WPFHexaEditor;component/hexeditor.xaml");
        }

        #endregion

        #region  Properties 

        private string _HexOffset = "&H000000";
        [Category("Function")]
        [Description("Appends a hex value to be associated with this box")]
        [DefaultValue("&H000000")]
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

        private ViewHexType _ViewMode = ViewHexType.PlainText;
        [Category("Function")]
        [Description("Determines which type of view will be displayed next to the hex values.")]
        [DefaultValue(ViewHexType.PlainText)]
        public ViewHexType ViewMode
        {
            get
            {
                return _ViewMode;
            }
            set
            {
                if (_ViewMode != value)
                {
                    _ViewMode = value;
                    switch (value)
                    {
                        case ViewHexType.FontTable:
                            {
                                HexEditor1.LoadTblFile(FontTable.ActiveFilePath);
                                HexEditor1.TypeOfCharacterTable = WpfHexaEditor.Core.CharacterTableType.TblFile;
                                break;
                            }
                        case ViewHexType.PlainText:
                            {
                                HexEditor1.LoadDefaultTbl();
                                HexEditor1.TypeOfCharacterTable = WpfHexaEditor.Core.CharacterTableType.Ascii;
                                break;
                            }
                    }
                }
            }
        }

        #endregion

        #region  Events 

        private void HexDeveloper_SizeChanged(object Sender, EventArgs Args)
        {
            HexEditor1.ScrollLargeChange = Math.Max(Size.Height / HexEditor1.LineHeight - 4d, 1);
        }

        #endregion

        #region  Public 

        private Dictionary<string, int[]> LoadTable(string FilePath)
        {
            return File.ReadAllLines(FilePath, Encoding.ASCII).Select((Line) =>
    {
        string[] KeyValuePair = Line.Split('=');
        return new { Key = KeyValuePair[1], Value = HexConvert.HexToIntRaw(KeyValuePair[0]) };
    }).GroupBy(KeyValuePair => KeyValuePair.Key).ToDictionary(KeyValuePair => KeyValuePair.Key, KeyValuePair => KeyValuePair.Select(KVP => KVP.Value).ToArray());
        }

        public new void Load()
        {
            Buffer = HexStorage.Memory.Skip(Conversions.ToInteger(HexOffset)).ToArray();
            HexEditor1.Stream = new MemoryStream(Buffer);
            HexEditor1.AllowAutoHighLightSelectionByte = false;
        }

        public void Save(int Offset = -1)
        {
            HexEditor1.SubmitChanges();

            MemoryLiterator.WriteAll(this, Buffer, Offset);
        }

        public void Discard()
        {
            HexEditor1.ClearAllChange();
        }

        public void GoTo(int Offset)
        {
            HexEditor1.SetPosition(Offset);
        }

        public byte[][] ConvertTextToType(string Text, FindLocationType Type)
        {
            byte[][] Bytes;
            switch (Type)
            {
                case FindLocationType.Hex:
                    {
                        Bytes = ByteConverters.HexToByte(Text).Select(Byte => new byte[] { Byte }).ToArray();
                        break;
                    }
                case FindLocationType.Tbl:
                    {
                        var Decoded = new List<byte[]>();
                        var Table = LoadTable(FontTable.ActiveFilePath);
                        foreach (char Character in Text)
                        {
                            foreach (int DecodedCharacter in Table[Conversions.ToString(Character)])
                            {
                                byte[] DecodedBytes = BitConverter.GetBytes(DecodedCharacter).Where(Byte => Byte != 0).ToArray();
                                Decoded.Add(DecodedBytes);
                            }
                        }
                        Bytes = Decoded.ToArray();
                        break;
                    }

                default:
                    {
                        Bytes = ByteConverters.StringToByte(Text).Select(Byte => new byte[] { Byte }).ToArray();
                        break;
                    }
            }
            return Bytes;
        }

        private long FindClosest(byte[] Hex, FindDirectionType Direction)
        {
            switch (Direction)
            {
                case FindDirectionType.First:
                    {
                        return HexEditor1.FindFirst(Hex);
                    }
                case FindDirectionType.Previous:
                    {
                        return HexEditor1.FindLast(Hex);
                    }
                case FindDirectionType.Next:
                    {
                        return HexEditor1.FindNext(Hex);
                    }
            }
            return 0L;
        }

        public void Find(string Text, FindDirectionType Direction, FindLocationType Location)
        {
            byte[][] Hexes = ConvertTextToType(Text, Location);
            long Closest = FindClosest(Hexes.Select(Hex => Hex.First()).ToArray(), Direction);
            GoTo((int)Closest);
            HexEditor1.SelectionStart = Closest;
            HexEditor1.SelectionStop = Closest + Text.Length - 1L;
        }

        public void FindAll(string Text, FindLocationType Location)
        {
            ClearFind();
            byte[] Hexes = ConvertTextToType(Text, Location).Select(Hex => Hex.First()).ToArray();
            HexEditor1.FindAll(Hexes, true);
        }

        public void ClearFind()
        {
            HexEditor1.UnHighLightAll();
        }

        public void Zoom(double Scale, bool Additive = false)
        {
            double NewScale = Additive ? HexEditor1.ZoomScale + Scale : Scale;
            HexEditor1.ZoomScale = Math.Min(Math.Max(NewScale, 0.5d), 2.0d);
        }

        #endregion
    }
}