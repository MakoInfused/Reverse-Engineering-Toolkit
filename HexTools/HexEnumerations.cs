using System.ComponentModel.DataAnnotations;

namespace HexTools.HexEnumerations
{

    public enum OffsetType
    {
        Value,     // Uses only the hexoffset as the value
        Constant,  // Same place for all items
        Relative,  // Based solely on this Controls Offset + Index
        Pointer,   // Uses this Controls Offset to determine the location of the pointer to the real location
        Indexed,   // Uses this Controls Offset + Index to determine the location of the pointer to the real location
        Temporary, // Based on this Controls Offset + Index + ListBoxOffset
        Terminated // Based on the existence of a terminating character FF
    }

    public enum EndianType
    {
        Big_Endian,
        Little_Endian
    }

    public enum DisplayType
    {
        Text,
        Numeric,
        Hex
    }

    public enum InputType
    {
        Normal,
        Hex,
        Numeric
    }

    public enum FindDirectionType
    {
        First,
        Next,
        Previous
    }

    public enum FindLocationType
    {
        Hex,
        Ascii,
        Tbl
    }

    public enum FindQuantityType
    {
        One,
        All
    }

    public enum ViewHexType
    {
        PlainText,
        FontTable
    }

    public enum CompressionType
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "LZ77 & RLE")]
        LZ77_RLE
    }

    public enum ImageEncodingType
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Planar | 2 BPP")]
        FormatPlanar2BPP,
        [Display(Name = "Planar | 4 BPP")]
        FormatPlanar4BPP,
    }

    public enum ColorDepthType
    {
        [Display(Name = "Full Color")]
        FullColor,
        [Display(Name = "RGB555 | 15 Bit", GroupName = "SNES")]
        FormatRGB555,
    }

    public enum HexAddressFormatType
    {
        Raw,
        Index,
        PC,
        [Display(Name = "SNES LoROM")]
        SNES_LoROM
    }

    public enum DataManagementType
    {
        Manual,
        Automatic
    }
}