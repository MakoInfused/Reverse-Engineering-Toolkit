using BasicTools;
using HexTools.HexEnumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HexTools
{
    public class HexCompressions : BasicSingletonFactory<HexCompressions, CompressionType, IHexCompress>
    {
        public override IReadOnlyCollection<IHexCompress> Available
        {
            get => new List<IHexCompress>()
            {
                new _LZ77RLE()
            };
        }

        public override IReadOnlyDictionary<CompressionType, IHexCompress> Implemented
        {
            get => new Dictionary<CompressionType, IHexCompress>()
            {
                { CompressionType.LZ77_RLE, new _LZ77RLE() }
            };
        }
    }

    public static class HexCompressHelper
    {
        public static bool WriteToBuffer(ref byte[] buffer, ref int offset, byte value)
        {
            buffer[offset] = value;
            offset++;
            return true;
        }

        public static byte[] BufferWriteBlocks(byte[] bytes)
        {
            byte[] buffer = new byte[bytes.Length];
            int size = bytes.Length / 16;
            int write = 0;
            for (int i2 = 0; i2 <= size - 1; i2++)
            {
                WriteToBuffer(ref buffer, ref write, bytes[0 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[8 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[1 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[9 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[2 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[10 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[3 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[11 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[4 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[12 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[5 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[13 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[6 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[14 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[7 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[15 + i2 * 16]);
            }

            return buffer;
        }

        public static byte[] BufferWriteLinear(byte[] bytes)
        {
            byte[] buffer = new byte[bytes.Length];
            int size = bytes.Length / 16;
            int write = 0;
            for (int i2 = 0; i2 <= size - 1; i2++)
            {
                WriteToBuffer(ref buffer, ref write, bytes[0 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[2 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[4 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[6 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[8 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[10 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[12 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[14 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[1 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[3 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[5 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[7 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[9 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[11 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[13 + i2 * 16]);
                WriteToBuffer(ref buffer, ref write, bytes[15 + i2 * 16]);
            }

            return buffer;
        }
    }

    /*
    The compression and decompression algorithm used in this LUA script is a custom algorithm designed for 
    compressing and decompressing data in the context of the Dragon Ball Z - Super Saiya Densetsu game. 
    The algorithm is not explicitly named in the provided script, but it seems to be a variant of LZ77 
    (Lempel-Ziv 77) compression, which is a widely used lossless data compression algorithm.

    In the script, there are functions named DBZDecompData and DBZCompData that handle decompression and 
    compression, respectively. These functions implement the compression and decompression process using 
    specific rules and strategies. The algorithm uses techniques like run-length encoding (RLE) and LZ77
    to achieve compression.

    Here's a general breakdown of the algorithm's steps based on the provided script:

    Decompression (DBZDecompData function):

        -Read the size of the decompressed data.
        -Read the compressed data bit by bit.
        -Depending on the current bit:
            If the bit is 0, perform LZ77 decompression.
            If the bit is 1, copy a literal byte.
        -The LZ77 decompression involves reading an offset and length from the compressed data to determine 
            a sequence of bytes to copy from a previously decompressed section of the data.
        -Repeat the process until the entire decompressed data is reconstructed.

    Compression (DBZCompData function):

        -Read the input data.
        -While reading the data, search for repeating patterns using a sliding window approach similar to LZ77.
        -If a repeating pattern is found, encode it using an LZ77-like approach, which involves storing an offset 
            and length for the pattern.
        -If no repeating pattern is found, encode a literal byte.
        -Write the compressed data along with the encoding information.

    The algorithm involves maintaining a dictionary of previously seen patterns to achieve compression. 
    The specific details of the algorithm's parameters and strategies are defined in the provided script.

    Keep in mind that the script may contain additional details and nuances that are not covered in the 
    provided breakdown. If you need to understand the algorithm in greater detail or modify its behavior,
    you may need to refer to the script's documentation or the author BahaBulle/Hiei's intentions.
    */
    /// <summary>
    /// 
    /// </summary>
    internal class _LZ77RLE : IHexCompress
    {
        private const string NAME = "Lempel-Ziv 1977 + Run-length encoding";
        public string Name => NAME;

        public Stream Compress(byte[] data, out IHexCompressionInformation information, IHexCompressionOptions options = null)
        {
            byte[] uncompressedData;
            if(options != null && options.Position.HasValue && options.Length.HasValue)
            {
                uncompressedData = new byte[options.Length.Value - options.Position.Value];
                Array.Copy(data, options.Position.Value, uncompressedData, 0, options.Length.Value);
            }
            else
            {
                uncompressedData = data;
            }

            var stream = new MemoryStream(HexCompressHelper.BufferWriteLinear(uncompressedData));
            var compressedData = CompressData(stream);

            information = new HexCompressionInformation();

            return compressedData.Item1;
        }

        private static Tuple<MemoryStream, int> CompressData(MemoryStream buf_in)
        {
            short filesize = (short)buf_in.Length;
            BasicLoggers.Instance.Logger.WriteLine($"0x{filesize:X} octets a compresser\n");

            MemoryStream buf_out = new MemoryStream();

            var sizeInBytes = BitConverter.GetBytes(filesize);
            for (int i = 0; i < sizeInBytes.Length; i++)
            {
                buf_out.WriteByte(sizeInBytes[i]);
            }

            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            List<dynamic> window = new List<dynamic>();
            int nb = 0;

            int pos_in = 0;
            int header = 0;
            while (pos_in < filesize)
            {
                BasicLoggers.Instance.Logger.WriteLine($"Read {pos_in:X8} : {buf_in.ToArray()[pos_in]:X2}");

                int nbSuite = 0;
                int pos_lz = 0;
                Tuple<int, int> findResult = FindSuite(buf_in.ToArray(), pos_in, dic, filesize);
                nbSuite = findResult.Item1;
                pos_lz = findResult.Item2;
                BasicLoggers.Instance.Logger.WriteLine($"  Find {nbSuite} in 0x{pos_lz:X}");

                if (nbSuite < 3)
                {
                    header = header + (1 << window.Count);
                    window.Add(new { val = buf_in.ToArray()[pos_in], has = true });
                    BasicLoggers.Instance.Logger.WriteLine($"  New byte {buf_in.ToArray()[pos_in]:X2} (->{header:X2})");
                    pos_in++;
                }
                else
                {
                    window.Add(new { from = pos_lz, to = pos_in, nb = nbSuite, has = false });
                    pos_in += nbSuite;
                }

                var lastBytes = pos_in + nbSuite >= filesize;

                if (window.Count == 8 || lastBytes)
                {
                    BasicLoggers.Instance.Logger.WriteLine($"  Write : {header:X2} ");
                    buf_out.WriteByte((byte)header);

                    foreach (var entry in window)
                    {
                        if (entry.has)
                        {
                            BasicLoggers.Instance.Logger.WriteLine($"{entry.val:X2} ");
                            buf_out.WriteByte((byte)entry.val);
                        }
                        else
                        {
                            int bytes = (entry.nb - 3) & 0xF | ((entry.to - entry.from) << 4) & 0xFFF0;
                            BasicLoggers.Instance.Logger.WriteLine($"{bytes:X4} ");
                            buf_out.WriteByte((byte)(bytes & 0xFF));
                            buf_out.WriteByte((byte)((bytes >> 8) & 0xFF));
                        }
                    }
                    window.Clear();
                    header = 0;

                    BasicLoggers.Instance.Logger.WriteLine("\n");
                }
            }

            return new Tuple<MemoryStream, int>(buf_out, (int)buf_out.Length);
        }

        private static Tuple<int, int> FindSuite(byte[] buf, int pos_in, Dictionary<string, dynamic> dic, int filesize)
        {
            int nb = 0;
            int pos = pos_in;
            int pos_lz = 0;
            int limit = pos_in >= 0x1000 ? pos_in - 0xFFF : 0;

            byte byteValue = buf[pos];
            string sbyteValue = ((char)byteValue).ToString();

            while (dic.ContainsKey(sbyteValue) && dic[sbyteValue].pos >= limit && nb < 18 && pos + nb < filesize)
            {
                pos_lz = dic[sbyteValue].pos;
                ListEntry(buf, pos + nb, dic);
                nb++;
                if (pos + nb >= filesize)
                {
                    continue;
                }

                sbyteValue += (char)buf[pos + nb];
            }
            if (nb == 0)
                ListEntry(buf, pos, dic);

            return new Tuple<int, int>(nb, pos_lz);
        }

        private static void ListEntry(byte[] buf, int pos_in, Dictionary<string, dynamic> dic)
        {
            int pos = pos_in - 17;
            if (pos < 0)
                pos = 0;

            while (pos <= pos_in)
            {
                string suite = "";
                int nb = 1;
                for (int i = pos; i <= pos_in; i++)
                {
                    suite += (char)buf[i];
                    dic[suite] = new { pos = pos };
                }
                pos++;
            }
        }

        public byte[] Decompress(Stream stream, out IHexDecompressionInformation information, IHexDecompressionOptions options = null)
        {
            byte[] compressedData;
            if (options != null && options.Position.HasValue && options.Length.HasValue)
            {
                compressedData = new byte[options.Length.Value - options.Position.Value];
                Array.Copy(stream.ToArray(), options.Position.Value, compressedData, 0, options.Length.Value);
            }
            else
            {
                compressedData = stream.ToArray();
            }

            var compressedStream = new MemoryStream(compressedData) as Stream;
            var decompressedData = DecompressData(compressedStream, options?.Single ?? false, out var offsets);

            information = new HexDecompressionInformation() { Offsets = offsets };

            return HexCompressHelper.BufferWriteBlocks(decompressedData);
        }

        private static byte[] DecompressData(Stream stream, bool single, out int[] offsets)
        {
            var outputBuffer = new List<byte[]>();
            var offsetLocations = new List<int>();
            try
            {
                stream.Seek(0, SeekOrigin.Begin);

                while (stream.Position != stream.Length)
                {
                    if (single && outputBuffer.Count > 0) break;

                    byte[] lengthBuffer = new byte[2];
                    stream.Read(lengthBuffer, 0, 2);
                    var outputSize = BitConverter.ToUInt16(lengthBuffer, 0);
                    if (outputSize == 0x0000 || outputSize == 0xFFFF) continue;

                    int offset = 0;
                    byte[] bytes = new byte[outputSize];
                    offsetLocations.Add((int)stream.Position - 2);

                    BasicLoggers.Instance.Logger.WriteLine($"Decompression {stream.Position - 1:X6}: {outputSize:X2}\n");

                    while (outputSize > offset)
                    {
                        byte[] buffer1 = new byte[1];
                        byte[] buffer2 = new byte[2];
                        stream.Read(buffer1, 0, 1);

                        byte header = buffer1[0];
                        BasicLoggers.Instance.Logger.WriteLine($" Header {stream.Position - 1:X6}: {header:X2} - {Convert.ToString(header, 2):X8}\n");

                        for (int i = 1; i <= 8; i++)
                        {
                            if ((byte)(header & 0x01) == 0)
                            {
                                stream.Read(buffer2, 0, 2);
                                int type = BitConverter.ToUInt16(buffer2, 0);
                                int length = (type & 0x0F) + 3;
                                int position = type >> 4;
                                int place = offset - position;

                                BasicLoggers.Instance.Logger.WriteLine($"Compression: 0x{type:X4}\n");
                                BasicLoggers.Instance.Logger.WriteLine($"     Length: {length} (0x{length:X2})\n");
                                BasicLoggers.Instance.Logger.WriteLine($"     Offset: 0x{offset:X4}\n");
                                BasicLoggers.Instance.Logger.WriteLine($"   Position: 0x{position:X4}\n");
                                BasicLoggers.Instance.Logger.WriteLine($"      Place: {place} (0x{place:X2})\n");

                                // Compression
                                if (place < 0)
                                {
                                    // Padding
                                    int value = (~place & 0xFF);

                                    BasicLoggers.Instance.Logger.WriteLine("      Write: ");
                                    while (value >= 0 && length > 0)
                                    {
                                        bytes[offset] = 0;
                                        BasicLoggers.Instance.Logger.WriteLine($"{0:X2} ");
                                        offset++;

                                        value--;
                                        length--;
                                    }

                                    // Previous
                                    for (int i2 = 1; i2 <= length; i2++)
                                    {
                                        byte previous = bytes[i2 - 1];
                                        BasicLoggers.Instance.Logger.WriteLine($"{previous:X2} ");
                                        bytes[offset] = previous;
                                        offset++;
                                    }
                                    BasicLoggers.Instance.Logger.WriteLine("\n");
                                }
                                else
                                {
                                    // Repeat
                                    BasicLoggers.Instance.Logger.WriteLine("      Write: ");
                                    for (int i2 = 0; i2 <= length - 1; i2++)
                                    {
                                        byte repeat = bytes[place + i2];
                                        BasicLoggers.Instance.Logger.WriteLine($"{repeat:X2} ");
                                        bytes[offset] = repeat;
                                        offset++;
                                    }
                                    BasicLoggers.Instance.Logger.WriteLine("\n");
                                }
                            }
                            else
                            {
                                // Raw
                                stream.Read(buffer1, 0, 1);

                                byte raw = buffer1[0];
                                bytes[offset] = raw;
                                BasicLoggers.Instance.Logger.WriteLine($"        Raw: {raw:X2}");
                                offset++;
                            }

                            header = (byte)(header >> 1);
                            if (offset >= outputSize) { break; }
                        }

                        BasicLoggers.Instance.Logger.WriteLine("\n");
                    }

                    outputBuffer.Add(bytes);
                }
            }
            catch (Exception e) when (e is IndexOutOfRangeException)
            {
                offsets = offsetLocations.ToArray();
                if (outputBuffer.Count == 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }

            offsets = offsetLocations.ToArray();
            var finalOutput = outputBuffer.SelectMany(x => x).ToArray();
            Array.Resize(ref finalOutput, (int)stream.Length);

            return finalOutput;
        }
    }
}
