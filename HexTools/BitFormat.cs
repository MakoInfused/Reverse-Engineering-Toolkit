using System.Collections.Generic;
using BasicTools;
using HexTools.HexEnumerations;

namespace HexTools
{
    public class BitFormats : BasicSingletonFactory<BitFormats, ImageEncodingType, IBitFormat>
    {
        public override IReadOnlyCollection<IBitFormat> Available
        {
            get => new List<IBitFormat>()
            {
                new _2BPP(),
                new _4BPP()
            };
        }

        public override IReadOnlyDictionary<ImageEncodingType, IBitFormat> Implemented
        {
            get => new Dictionary<ImageEncodingType, IBitFormat>()
            {
                { ImageEncodingType.FormatPlanar2BPP, new _2BPP() },
                { ImageEncodingType.FormatPlanar4BPP, new _4BPP() }
            };
        }
    }

    public static class BitFormatHelper
    {
        public static byte[] linearToBlocks(byte[] input, int width, int tileWidth)
        {
            int size = input.Length;

            byte[] output = new byte[size];
            int blockSize = tileWidth * tileWidth;
            int a8x8 = size / blockSize;
            int x = 0, y = 0, i = 0;

            int r = 0;

            do
            {
                i = ((i % width) == 0 && (i > 0)) ? i + (width * (tileWidth - 1)) : i;
                y = 0;
                do
                {
                    r = i + (y & (tileWidth - 1)) + (width * (y / tileWidth));
                    if (r >= size) continue;

                    output[x * blockSize + y] = input[r];
                } while (++y < blockSize);
                i += tileWidth;
            } while (++x < a8x8);

            input = null;
            return output;
        }

        public static byte[] blocksToLinear(byte[] input, int width, int tileWidth)
        {
            int size = input.Length;

            byte[] output = new byte[size];
            int blockSize = tileWidth * tileWidth;
            int a8x8 = size / blockSize;
            int x = 0, y = 0, i = 0;

            int r = 0;

            do
            {
                i = ((i % width) == 0 && (i > 0)) ? i + (width * (tileWidth - 1)) : i;
                y = 0;
                do
                {
                    r = i + (y & (tileWidth - 1)) + (width * (y / tileWidth));
                    if (r >= size) continue;

                    output[r] = input[x * blockSize + y];
                } while (++y < blockSize);
                i += tileWidth;
            } while (++x < a8x8);

            input = null;
            return output;
        }
    }

    unsafe class _2BPP : IBitFormat
    {
        public int BitsPerPixel { get { return 2; } }
        public int Colors { get { return 4; } }
        public BitformatType Type { get { return BitformatType.BITFORMAT_PLANAR; } }
        public string Name { get { return "2BPP SNES Planar"; } }
        public bool AlignBy8x8 { get { return true; } }
        public int FixedWidth { get { return 128; } }

        public byte[] Encode(byte[] bitmap)
        {
            return _encode(BitFormatHelper.linearToBlocks(bitmap, 128, 8));
        }

        private byte[] _encode(byte[] input)
        {
            int bits = 0;
            int sizeFix = input.Length / 4; //64/16
            if ((sizeFix % 16) != 0)
            {
                sizeFix += 16 - (sizeFix % 16);
            }
            byte[] data = new byte[sizeFix];

            fixed (byte* output = &data[0], pinput = &input[0])
            {
                byte* ptr1 = output - 1;
                byte* ptr2 = pinput;
                byte* ptr3 = ptr2 + input.Length;

                while (ptr2 < ptr3)
                {
                    bits = 0;
                    for (int y = 0; y < 8; ++y)
                    {
                        bits |= (ptr2[y] & 1) << (7 - y);
                    }
                    *++ptr1 = (byte)bits;

                    bits = 0;
                    for (int y = 0; y < 8; ++y)
                    {
                        bits |= (ptr2[y] & 2) >> 1 << (7 - y);
                    }
                    *++ptr1 = (byte)bits;

                    ptr2 += 8;
                }
            }

            return data;
        }

        private byte decodePixel(byte* ptr2, int shift)
        {
            return (byte)(
                (ptr2[1] >> shift << 1 & 2)
              | (ptr2[0] >> shift & 1));
        }

        public byte[] Decode(byte[] data)
        {
            return BitFormatHelper.blocksToLinear(_decode(data), 128, 8);
        }

        public byte[] _decode(byte[] input)
        {
            int size = input.Length;
            int sizeFix = size * 4;
            if (sizeFix % 64 != 0)
            {
                sizeFix += 64 - (sizeFix % 64);
            }

            byte[] output = new byte[sizeFix]; // 64/16 = 4

            fixed (byte* poutput = &output[0], pinput = &input[0])
            {
                byte* ptr1 = poutput - 1;
                byte* ptr2 = pinput;
                byte* ptr3 = pinput + size;

                while (ptr2 < ptr3)
                {
                    *++ptr1 = decodePixel(ptr2, 7);
                    *++ptr1 = decodePixel(ptr2, 6);
                    *++ptr1 = decodePixel(ptr2, 5);
                    *++ptr1 = decodePixel(ptr2, 4);
                    *++ptr1 = decodePixel(ptr2, 3);
                    *++ptr1 = decodePixel(ptr2, 2);
                    *++ptr1 = decodePixel(ptr2, 1);
                    *++ptr1 = decodePixel(ptr2, 0);

                    ptr2 += 2;
                }
            }

            return output;
        }
    }

    internal unsafe class _4BPP : IBitFormat
    {
        public int BitsPerPixel { get { return 4; } }
        public int Colors { get { return 16; } }
        public BitformatType Type { get { return BitformatType.BITFORMAT_PLANAR; } }
        public string Name { get { return "4BPP SNES Planar"; } }
        public bool AlignBy8x8 { get { return true; } }
        public int FixedWidth { get { return 128; } }

        public byte[] Encode(byte[] bitmapData)
        {
            return _encode(BitFormatHelper.linearToBlocks(bitmapData, 128, 8));
        }

        internal byte[] _encode(byte[] input)
        {
            int sizeFix = input.Length / 2; //64/32 = 2
            if (sizeFix == 0) return input;

            int bits = 0;
            if ((sizeFix % 32) != 0)
            {
                sizeFix += 32 - (sizeFix % 32);
            }
            byte[] data = new byte[sizeFix];

            fixed (byte* output = &data[0], pinput = &input[0])
            {
                byte* ptr1 = output - 1;
                byte* ptr2 = pinput;
                byte* ptr3 = ptr2 + input.Length;

                while (ptr2 < ptr3)
                {
                    for (int i = 0; i < 64; i += 8)
                    {
                        bits = 0;
                        for (int y = 0; y < 8; ++y)
                        {
                            bits |= (ptr2[i + y] & 1) << (7 - y);
                        }
                        *++ptr1 = (byte)bits;

                        bits = 0;
                        for (int y = 0; y < 8; ++y)
                        {
                            bits |= (ptr2[i + y] & 2) >> 1 << (7 - y);
                        }
                        *++ptr1 = (byte)bits;
                    }
                    for (int i = 0; i < 64; i += 8)
                    {
                        bits = 0;
                        for (int y = 0; y < 8; ++y)
                        {
                            bits |= (ptr2[i + y] & 4) >> 2 << (7 - y);
                        }
                        *++ptr1 = (byte)bits;

                        bits = 0;
                        for (int y = 0; y < 8; ++y)
                        {
                            bits |= (ptr2[i + y] & 8) >> 3 << (7 - y);
                        }
                        *++ptr1 = (byte)bits;
                    }

                    ptr2 += 64;
                }
            }

            return data;
        }

        private byte decodePixel(byte* ptr2, int shift)
        {
            return (byte)(
                (ptr2[17] >> shift << 3 & 8)
              | (ptr2[16] >> shift << 2 & 4)
              | (ptr2[1] >> shift << 1 & 2)
              | (ptr2[0] >> shift & 1));
        }

        public byte[] Decode(byte[] binData)
        {
            return BitFormatHelper.blocksToLinear(_decode(binData), 128, 8);
        }

        internal byte[] _decode(byte[] input)
        {
            int size = input.Length;
            if (size == 0) return input;

            int sizeFix = size * 2;
            if (sizeFix % 64 != 0)
            {
                sizeFix += 64 - (sizeFix % 64);
            }

            byte[] output = new byte[sizeFix]; // 64/32 = 2

            fixed (byte* poutput = &output[0], pinput = &input[0])
            {
                byte* ptr1 = poutput - 1;
                byte* ptr2 = pinput;
                byte* ptr3 = pinput + size;

                while (ptr2 < ptr3)
                {
                    for (int i = 0; i < 16; i += 2, ptr2 += 2)
                    {
                        *++ptr1 = decodePixel(ptr2, 7);
                        *++ptr1 = decodePixel(ptr2, 6);
                        *++ptr1 = decodePixel(ptr2, 5);
                        *++ptr1 = decodePixel(ptr2, 4);
                        *++ptr1 = decodePixel(ptr2, 3);
                        *++ptr1 = decodePixel(ptr2, 2);
                        *++ptr1 = decodePixel(ptr2, 1);
                        *++ptr1 = decodePixel(ptr2, 0);
                    }
                    ptr2 += 16; //32-16
                }
            }

            return output;
        }
    }
}
