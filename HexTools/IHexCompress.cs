using System;
using System.IO;

namespace HexTools
{
    public interface IHexCompress
    {
        /// <summary>
        /// Known name, such as: LZ77, RLE, etc.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Compression algorithm.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="information"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Stream Compress(byte[] data, out IHexCompressionInformation information, IHexCompressionOptions options = null);
        /// <summary>
        /// Decompression algorithm.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="information"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        byte[] Decompress(Stream stream, out IHexDecompressionInformation information, IHexDecompressionOptions options = null);
    }

    public interface IHexCompressOptions
    {
        int? Position { get; }
        int? Length { get; }
        bool? Single { get; }
    }

    public interface IHexCompressInformation
    {
        int[] Offsets { get; }
    }

    public interface IHexDecompressInformation : IHexCompressInformation { }

    public interface IHexCompressionOptions : IHexCompressOptions { }
    public interface IHexDecompressionOptions : IHexCompressOptions { }

    public abstract class HexCompressOptions : IHexCompressOptions
    {
        public int? Position { get; set; }
        public int? Length { get; set; }
        public bool? Single { get; set; }
    }

    public class HexCompressionOptions : HexCompressOptions, IHexCompressionOptions { }
    public class HexDecompressionOptions : HexCompressOptions, IHexDecompressionOptions { }

    public interface IHexCompressionInformation : IHexCompressInformation { }
    public interface IHexDecompressionInformation : IHexDecompressInformation { }

    public abstract class HexCompressInformation : IHexCompressInformation
    {
        public int[] Offsets { get; set; }
    }

    public class HexCompressionInformation : HexCompressInformation, IHexCompressionInformation { }
    public class HexDecompressionInformation : HexCompressInformation, IHexDecompressionInformation { }
}
