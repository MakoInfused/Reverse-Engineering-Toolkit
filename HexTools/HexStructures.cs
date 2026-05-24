using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using BasicTools;
using Microsoft.VisualBasic.CompilerServices;

namespace HexTools.HexStructures
{

    [Serializable]
    public class FontTable : Encoding, IEnumerable<KeyValuePair<int, string>>
    {

        public const string ActiveName = "FontTable.tbl";
        public static string ActiveFilePath
        {
            get
            {
                return ActiveName.GetResourceFilePath();
            }
        }

        public static byte[] LoadActiveFile
        {
            get
            {
                return File.ReadAllBytes(ActiveFilePath);
            }
        }

        public int Count
        {
            get
            {
                return _Entries.Count;
            }
        }

        public IReadOnlyCollection<int> Entries
        {
            get
            {
                return Array.AsReadOnly(_Entries.Keys.ToArray());
            }
        }

        public IReadOnlyCollection<string> Symbols
        {
            get
            {
                return Array.AsReadOnly(_Entries.Values.ToArray());
            }
        }

        protected readonly SortedDictionary<int, string> _Entries = new SortedDictionary<int, string>();
        protected readonly SortedDictionary<string, int> _Symbols = new SortedDictionary<string, int>();

        public void Clear()
        {
            _Entries.Clear();
            _Symbols.Clear();
        }

        private void AddEntry(int Index, string Value)
        {
            if (Conversions.ToBoolean(IsNew(Index)))
            {
                _Entries.Add(Index, Value);
                if (Conversions.ToBoolean(!IsDuplicate(Value)))
                {
                    _Symbols.Add(Value, Index);
                }
            }
        }

        public bool IsNew(int Index)
        {
            return _Entries.ContainsKey(Index) == false;
        }

        public bool IsDuplicate(string Index)
        {
            return _Symbols.ContainsKey(Index) == true;
        }

        public KeyValuePair<int, string> Insert(int Index)
        {
            int nextIndex = GetNextAvailable(Index);
            AddEntry(nextIndex, "Empty");
            return new KeyValuePair<int, string>(nextIndex, _Entries[nextIndex]);
        }

        public KeyValuePair<int, string> Add()
        {
            int nextIndex = GetNextAvailable();
            AddEntry(nextIndex, "Empty");
            return new KeyValuePair<int, string>(nextIndex, _Entries[nextIndex]);
        }

        public KeyValuePair<int, string> Add(string Value)
        {
            int nextIndex = GetNextAvailable();
            AddEntry(nextIndex, Value);
            return new KeyValuePair<int, string>(nextIndex, _Entries[nextIndex]);
        }

        public KeyValuePair<int, string> Add(int Index, string Value)
        {
            AddEntry(Index, Value);
            return new KeyValuePair<int, string>(Index, _Entries[Index]);
        }

        public void Remove(int entry)
        {
            string symbol = _Entries[entry];
            _Entries.Remove(entry);
            _Symbols.Remove(symbol);
        }

        public int FindIndex(int entry)
        {
            return Array.FindIndex(_Entries.Keys.ToArray(), Key => Key == entry);
        }

        public int FindIndex(string symbol)
        {
            return Array.FindIndex(_Symbols.Keys.ToArray(), Key => (Key ?? "") == (symbol ?? ""));
        }

        private int GetNextAvailable(int Start = 0)
        {
            int Index = Start;
            while (_Entries.ContainsKey(Index))
                Index += 1;
            return Index;
        }

        public int[] AllEntries(string symbol)
        {
            return this.Where(kvp => (kvp.Value ?? "") == (symbol ?? "")).Select(kvp => kvp.Key).ToArray();
        }

        public string[] AllSymbols(int entry)
        {
            return this.Where(kvp => kvp.Key == entry).Select(kvp => kvp.Value).ToArray();
        }

        public override int GetByteCount(char[] chars, int index, int count)
        {
            return byte.MaxValue;
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            int writeIndex = 0;
            var buffer = new StringBuilder();
            var keys = Symbols;
            for (int i = 0, loopTo = charCount - 1; i <= loopTo; i++)
            {
                char @char = chars[charIndex + i];
                buffer.Append(@char);
                string characters = buffer.ToString();
                if (_Symbols.ContainsKey(characters))
                {
                    int encoded = _Symbols[characters];
                    foreach (int @byte in HexConvert.NumericToBytes(encoded.ToString(), false, encoded > 255 ? 2 : 1))
                    {
                        bytes[byteIndex + writeIndex] = (byte)@byte;
                        writeIndex += 1;
                    }
                    buffer.Clear();
                }
                else if (keys.Any(Key => Key.StartsWith(characters)))
                {
                    continue;
                }
                else
                {
                    bytes[byteIndex + writeIndex] = 0x0;
                }
            }
            return charCount;
        }

        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            return short.MaxValue;
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            int writeIndex = 0;
            byte[] buffer;
            for (int i = 0, loopTo = byteCount - 1; i <= loopTo; i++)
            {
                byte @byte = bytes[byteIndex + i];
                if (@byte == 0xF4)
                {
                    buffer = new byte[] { @byte, bytes[byteIndex + i + 1] };
                    i += 1;
                }
                else
                {
                    buffer = new byte[] { @byte };
                }
                int decoded = Conversions.ToInteger(HexConvert.BytesToNumeric(buffer));
                if (_Entries.ContainsKey(decoded))
                {
                    string entry = _Entries[decoded];
                    foreach (char @char in entry)
                    {
                        chars[charIndex + writeIndex] = @char;
                        writeIndex += 1;
                    }
                }
                else
                {
                    chars[charIndex + writeIndex] = '¿';
                }
            }
            return byteCount;
        }

        public override int GetMaxByteCount(int charCount)
        {
            return charCount;
        }

        public override int GetMaxCharCount(int byteCount)
        {
            return byteCount;
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            return _Entries.GetEnumerator();
        }

        private IEnumerator IEnumerable_GetEnumerator()
        {
            return _Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => IEnumerable_GetEnumerator();
    }

}