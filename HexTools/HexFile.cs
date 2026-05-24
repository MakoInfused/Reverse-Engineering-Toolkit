using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    public interface IHexFile
    {

    }

    public interface IHexFileSNES : IHexFile
    {
        void Convert(IHexFileSNESConvertArgs args);
    }

    public class HexFileSNES : IHexFileSNES
    {
        public void Convert(IHexFileSNESConvertArgs args)
        {

        }
    }

    public enum HexFileSNESConvertSize
    {
        EightMBit = 8,
        ThirtyTwoMBit = 32
    }

    public interface IHexFileSNESOptions
    {
        HexFileSNESConvertSize Size { get; }
    }

    public class HexFileSNESOptions : IHexFileSNESOptions
    {
        public HexFileSNESConvertSize Size { get; }
    }

    public interface IHexFileSNESConvertArgs
    {
        HexFileSNESOptions Old { get; }
        HexFileSNESOptions New { get; }
    }

    public class HexFileSNESConvertArgs : IHexFileSNESConvertArgs
    {
        public HexFileSNESOptions Old { get; }

        public HexFileSNESOptions New { get; }
    }
}
