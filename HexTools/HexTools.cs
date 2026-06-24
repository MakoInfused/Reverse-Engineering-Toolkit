using BasicTools;
using BasicTools.BasicControls;
using HexTools.HexEnumerations;
using HexTools.HexStructures;
using HexTools.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HexTools
{

    #region  Shared 

    #region IMemoryLiterator

    public interface IMemoryLiterator
    {
        void Write(object Sender, MemoryOperation Operation);
        void Resize(object Sender, int NewSize, int Offset = -1);
    }

    #endregion

    #region  MemoryLiterator 

    public class MemoryLiterator
    {
        private static UndoRedoClass<MemoryOperationAction> _ActionModule;

        public static UndoRedoClass<MemoryOperationAction> ActionModule
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ActionModule;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ActionModule != null)
                {
                    _ActionModule.UndoHappened -= Undo;
                    _ActionModule.RedoHappened -= Redo;
                }

                _ActionModule = value;
                if (_ActionModule != null)
                {
                    _ActionModule.UndoHappened += Undo;
                    _ActionModule.RedoHappened += Redo;
                }
            }
        }
        public static object LastWritten { get; private set; }

        public static bool HaveData => HexStorage.IsOpen;

        public static event EventHandler<MemoryOperationAction> OnWrite;

        public static event EventHandler<MemoryOperationResize> OnResize;

        private static readonly PredicateEqualityComparer<byte?> _NullableByteEquality = new PredicateEqualityComparer<byte?>((a, b) => (bool)(!a.HasValue || !b.HasValue ? true : a.HasValue && b.HasValue ? a.Value == b.Value : (bool?)null));

        static MemoryLiterator()
        {
            ActionModule = new UndoRedoClass<MemoryOperationAction>();
        }

        public static MemoryOperation Read(int Offset, long Length)
        {
            byte[] buffer = BasicHelper.TrimByteArray(HexStorage.Memory, Offset, (int)Length);

            return new MemoryOperation(Offset, buffer);
        }

        private static byte?[] ToNullableArray(byte[] bytes)
        {
            return Array.ConvertAll(bytes, value => new byte?(value));
        }

        public static MemoryOperation Scan(int Offset, byte?[] StopBytes)
        {
            long length = 0L;
            byte[] buffer = null;
            bool success = false;

            for (long currentOffset = Offset, loopTo = HexStorage.Memory.LongLength - Offset; currentOffset <= loopTo; currentOffset++)
            {
                buffer = BasicHelper.TrimByteArray(HexStorage.Memory, (int)currentOffset, (int)StopBytes.LongLength);
                if (ToNullableArray(buffer).SequenceEqual(StopBytes, _NullableByteEquality))
                {
                    length = currentOffset - Offset;
                    success = true;
                    break;
                }
            }

            return new MemoryOperation((int)(Offset + length), success ? buffer : (new byte[] { }));
        }

        public static MemoryOperation Scan(int Offset, byte[] StopBytes)
        {
            return Scan(Offset, ToNullableArray(StopBytes));
        }

        private static void Write(MemoryOperationAction Action)
        {
            int Offset = Action.NewValue.Offset;
            byte[] Buffer = Action.NewValue.Buffer;

            if (Buffer == null || Buffer.LongLength + Offset > HexStorage.Memory.LongLength)
            {
                var Memory = HexStorage.Memory;
                Array.Resize(ref Memory, Buffer == null ? Offset : Offset + Buffer.Length);
                HexStorage.Memory = Memory;
            }
            else
            {
                int WriteOffset = Offset;

                for (int Index = 0, loopTo = (int)(Buffer.LongLength - 1L); Index <= loopTo; Index++)
                {
                    HexStorage.Memory[WriteOffset] = Buffer[Index];
                    WriteOffset += 1;
                }
            }

            OnWrite?.Invoke(typeof(MemoryLiterator), Action);
        }

        public static MemoryOperationAction Write(object Sender, MemoryOperation Operation)
        {
            Control Control = (Control)Sender;
            if (Control != null && Conversions.ToBoolean(ControlExtension.IsTemporaryControl(ref Control)))
            {
                return null;
            }

            var Original = Operation.Buffer == null || Operation.Buffer.LongLength > Operation.Offset
                ? new MemoryOperation(Operation.Buffer == null ? HexStorage.Memory.Length : Operation.Buffer.Length, null)
                : Read(Operation.Offset, Operation.Buffer.LongLength);

            if (Original != Operation)
            {
                var Action = new MemoryOperationAction(Sender, Original, Operation, 2);

                Write(Action);

                if (!Operation.IsPermanent)
                {
                    ActionModule.AddItem(Action);
                }

                return Action;
            }

            return null;
        }

        public static void WriteAll(object Sender, MemoryOperation[] Operations)
        {
            foreach (MemoryOperation Operation in Operations)
                Write(Sender, Operation);
        }

        public static void WriteAll(object Sender, byte[] Buffer, int Offset = -1)
        {
            foreach (MemoryDifference Difference in GetAllDifferences(Buffer))
            {
                byte[] Change = Difference.GetChange(Buffer);
                Write(Sender, new MemoryOperation(Offset < 0 ? Difference.Offset : Difference.Offset + Offset, Change));
            }
        }

        public static MemoryOperationAction Resize(object Sender, int NewSize, int Offset = -1)
        {
            if (Offset < 0) Offset = HexStorage.Memory.LongLength < NewSize
                     ? HexStorage.Memory.Length : NewSize;
            var Action = Write(Sender, new MemoryOperation(Offset,
                HexStorage.Memory.LongLength < NewSize ? new byte[NewSize - Offset] : null as byte[]
            ));

            OnResize?.Invoke(typeof(MemoryLiterator), new MemoryOperationResize(Sender, NewSize));

            return Action;
        }

        public static IEnumerable<MemoryDifference> GetAllDifferences(byte[] Buffer)
        {
            int DifferenceStart = -1;
            for (int Index = 0, loopTo = HexStorage.Memory.Length - 1; Index <= loopTo; Index++)
            {
                if (HexStorage.Memory[Index] != Buffer[Index])
                {
                    if (DifferenceStart == -1)
                    {
                        DifferenceStart = Index;
                    }
                }
                else if (DifferenceStart != -1)
                {
                    yield return new MemoryDifference(DifferenceStart, Index - DifferenceStart);
                    DifferenceStart = -1;
                }
            }
        }

        private static void Execute(MemoryOperationAction Action)
        {
            Write(Action);
            if(Action.SubActions != null)
            {
                foreach (var SubAction in Action.SubActions)
                {
                    Write(SubAction);
                }
            }
            if (Action.SubControl is IHexReader)
            {
                IHexReader HexReader = (IHexReader)Action.SubControl;
                HexReader.Load();
            }
            else if (Action.Writer is IHexReader)
            {
                IHexReader HexReader = (IHexReader)Action.Writer;
                HexReader.Load();
            }
        }

        private static void Undo(object Sender, UndoRedoActionArgs<MemoryOperationAction> Args)
        {
            Execute(Args.CurrentItem.Reverse());
        }

        private static void Redo(object Sender, UndoRedoActionArgs<MemoryOperationAction> Args)
        {
            Execute(Args.CurrentItem);
        }
    }

    #endregion

    #region  MemoryDifference 

    public struct MemoryDifference
    {

        public int Offset { get; private set; }
        public int Length { get; private set; }

        public MemoryDifference(int Offset, int Length)
        {
            this.Offset = Offset;
            this.Length = Length;
        }

        public byte[] GetChange(byte[] Buffer)
        {
            byte[] Change = new byte[Length];
            for (int Index = 0, loopTo = Length - 1; Index <= loopTo; Index++)
                Change[Index] = Buffer[Offset];
            return Change;
        }

    }

    #endregion

    #region MemoryOperationWrite

    public class MemoryOperationWrite
    {
        public object Writer { get; private set; }
        public MemoryOperation Operation { get; private set; }

        public MemoryOperationWrite(object writer, MemoryOperation operation)
        {
            Writer = writer;
            Operation = operation;
        }
    }

    #endregion

    #region MemoryOperationResize

    public class MemoryOperationResize
    {
        public object Writer { get; private set; }
        public int OldLength { get; private set; }
        public int NewLength { get; private set; }

        public MemoryOperationResize(object writer, int length)
        {
            Writer = writer;
            OldLength = HexStorage.Memory.Length;
            NewLength = length;
        }
    }

    #endregion

    #region  MemoryOperationAction 

    public class MemoryOperationAction : UndoRedoAction<MemoryOperation>, IMemoryLiterator
    {
        public object Writer { get; private set; }
        public Control Control
        {
            get
            {
                if (Writer is Control)
                {
                    Control _Control = (Control)Writer;
                    return _Control;
                }
                return null;
            }
        }
        public Control SubControl
        {
            get
            {
                if (Control != null)
                {
                    var target = Control;
                    Control Table = (Control)ControlExtension.FindParent(ref target);
                    if (Table != null)
                    {
                        return Table;
                    }
                }
                return null;
            }
        }
        public ListBox ListBox
        {
            get
            {
                if (Control != null)
                {
                    var target = Control;
                    IBasicListBoxAssociate Associate = (IBasicListBoxAssociate)ControlExtension.FindParent(ref target);
                    if (Associate != null)
                    {
                        return Associate.ListBox;
                    }
                }
                return null;
            }
        }
        private string _Name;
        public string Name
        {
            get
            {
                if (_Name != null)
                    return _Name;
                if (Control != null)
                {
                    if (SubControl != null)
                    {
                        var target = Control;
                        return Conversions.ToString(BasicHelper.ToTitleCase(Conversions.ToString(Operators.AddObject(SubControl.Name + " ", ControlExtension.FriendlyName(ref target)))));
                    }
                    return Conversions.ToString(BasicHelper.ToTitleCase(Control.Name));
                }
                return Conversions.ToString(BasicHelper.ToTitleCase(Writer.GetType().Name));
            }
        }
        public object Owner
        {
            get
            {
                if (Control != null)
                {
                    var target = Control;
                    Control ControlContainer = (Control)ControlExtension.FindTopParent<Control>(ref target);
                    if (ControlContainer != null)
                    {
                        return ControlContainer;
                    }
                    var Form = Control.FindForm();
                    if (Form != null)
                    {
                        return Form;
                    }
                    return null;
                }
                return Writer.GetType().ReflectedType;
            }
        }
        private string _OwnerName;
        public string OwnerName
        {
            get
            {
                if (_OwnerName != null)
                    return _OwnerName;
                Control Control = (Control)Owner;
                if (Control != null)
                {
                    string ControlName = Control is Form ? Control.Text.ExtractBetween('[', ']') : Control.Name;
                    if (ListBox != null)
                    {
                        return Conversions.ToString(BasicHelper.ToTitleCase(Conversions.ToString(Operators.AddObject(ControlName + " ", ((dynamic) ListBox.SelectedItem).Text))));
                    }
                    return Conversions.ToString(BasicHelper.ToTitleCase(ControlName));
                }
                Type Type = (Type)Owner;
                if (Type != null)
                {
                    return Conversions.ToString(BasicHelper.ToTitleCase(Type.Name));
                }
                return "?";
            }
        }
        public StackFrame Trace { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string Caller
        {
            get
            {
                return Trace.GetMethod().ReflectedType.Name;
            }
        }
        public string Method
        {
            get
            {
                return Caller + " " + Trace.GetMethod().Name;
            }
        }
        public string Changes
        {
            get
            {
                return "Hex Offset " + NewValue.HexOffset + " value " + OldValue.HexChanges + " -> " + NewValue.HexChanges;
            }
        }
        public string RawChanges
        {
            get
            {
                return NewValue.HexOffset + ": " + OldValue.HexChanges + " -> " + NewValue.HexChanges;
            }
        }
        public string Author
        {
            get
            {
                return "Changed by " + Name + " control inside window " + OwnerName + " at " + Modified;
            }
        }
        public string Modified
        {
            get
            {
                return TimeStamp.ToLongTimeString() + " on " + TimeStamp.ToShortDateString();
            }
        }
        private bool _IsReversed = false;
        public bool IsReversed
        {
            get
            {
                return _IsReversed;
            }
            protected set
            {
                if (value != _IsReversed)
                {
                    _IsReversed = true;
                }
            }
        }

        public List<MemoryOperationAction> SubActions { get; private set; }

        public MemoryOperationAction(object Writer, MemoryOperation OldValue, MemoryOperation NewValue, int StackTrace) : this(Writer, OldValue, NewValue, new StackFrame(StackTrace, false))
        {
        }

        public MemoryOperationAction(object Writer, MemoryOperation OldValue, MemoryOperation NewValue, StackFrame Trace) : base(OldValue, NewValue)
        {
            this.Writer = Writer;
            this.Trace = Trace;
            TimeStamp = DateTime.Now;
            MiscellanousExtension.WaitThenExecute(500, (x, y) =>
                {
                    _Name = Name;
                    _OwnerName = OwnerName;
                });
        }

        public override string ToString()
        {
            return Changes + " " + Author;
        }

        public MemoryOperationAction Reverse()
        {
            var Action = new MemoryOperationAction(Writer, NewValue, OldValue, Trace);
            Action.IsReversed = true;
            if(Action.SubActions != null)
            {
                var NewSubActions = new Queue<MemoryOperationAction>();
                foreach (var SubAction in SubActions)
                {
                    SubAction.Reverse();
                    NewSubActions.Enqueue(SubAction);
                }
                Action.SubActions = NewSubActions.ToList();
            }
            return Action;
        }

        private void AddSubAction(MemoryOperationAction action)
        {
            if (action == null) return;
            if (SubActions == null) SubActions = new List<MemoryOperationAction>();
            SubActions.Add(action);
        }

        public void Write(object writer, MemoryOperation operation)
        {
            AddSubAction(MemoryLiterator.Write(writer, operation));
        }

        public void Resize(object resizer, int newSize, int offset = -1)
        {
            AddSubAction(MemoryLiterator.Resize(Writer, newSize, offset));
        }
    }

    #endregion

    #region  MemoryOperation 

    public class MemoryOperation
    {
        public int Offset { get; set; }
        public byte[] Buffer { get; private set; }
        public bool IsPermanent { get; private set; }

        public string HexOffset
        {
            get
            {
                return HexConvert.IntToHexRaw(Offset, 5);
            }
        }

        public string HexChanges
        {
            get
            {
                return HexConvert.BytesToHex(Buffer);
            }
        }

        public MemoryOperation(int Offset, byte[] Buffer, bool IsPermanent = false)
        {
            this.Offset = Offset;
            this.Buffer = Buffer;
            this.IsPermanent = IsPermanent;
        }

        public static bool operator !=(MemoryOperation OperationA, MemoryOperation OperationB)
        {
            return !(OperationA == OperationB);
        }

        public static bool operator ==(MemoryOperation OperationA, MemoryOperation OperationB)
        {
            return OperationA.Offset == OperationB.Offset
                && (OperationA.Buffer != null || OperationB.Buffer == null)
                && ((OperationA.Buffer == null && OperationB.Buffer == null) || OperationA.Buffer.SequenceEqual(OperationB.Buffer));
        }

        public override string ToString()
        {
            return HexOffset + ": " + HexChanges;
        }

    }

    #endregion

    #region MemoryConflict

    public class MemoryConflict
    {
        public MemoryOperation Operation { get; set; }
        public long OriginalOffset { get; set; }
        public long OriginalLength { get; set; }

        public byte[] OriginalData => MemoryLiterator.Read((int) OriginalOffset, OriginalLength).Buffer;

        public MemoryConflict(MemoryOperation operation, long originalLength)
        {
            Operation = operation;
            OriginalOffset = operation.Offset;
            OriginalLength = originalLength;
        }

        public void Resolve(object sender, bool clearOriginal = false, long prepare = 0)
        {
            if(prepare > 0)
            {
                MemoryLiterator.Write(sender, new MemoryOperation(Operation.Offset + HexStorage.GlobalOffset, new byte[prepare]));
            }
            if (clearOriginal)
            {
                MemoryLiterator.Write(sender, new MemoryOperation((int)OriginalOffset, new byte[OriginalLength]));
            }
            MemoryLiterator.Write(sender, new MemoryOperation(Operation.Offset + HexStorage.GlobalOffset, Operation.Buffer));
        }
    }

    #endregion

    #region  HexStorage 

    public class MemoryChunk
    {
        public long Address { get; private set; }
        public long Length { get; private set; }

        public MemoryChunk(long address, long length, long offset = 0)
        {
            Address = address + offset;
            Length = length;
        }

        public void Use(long size)
        {
            if (Length < size) throw new ArgumentOutOfRangeException("Memory Chunks used size cannot be greater than its length!");
            Address += size;
            Length -= size;
        }
    }

    public class HexStorage
    {
        private const bool UseMemoryMap = true;
        private static readonly string MemoryMappedFilePath = Application.ExecutablePath;

        private static string _Checksum;

        private static byte[] _Memory;
        public static byte[] Memory
        {
            get
            {
                return _Memory;
            }
            set
            {
                if (!ReferenceEquals(value, Memory))
                {
                    if (UseMemoryMap)
                    {
                        _Memory = value;
                    }
                    else
                    {
                        _Memory = value;
                    }
                    RegenerateChecksum();
                }
            }
        }

        public HexStorage()
        {
            OnDataStore += Me_Stored;
        }

        public static void Save()
        {
            OnSave?.Invoke();
            RegenerateChecksum();
        }

        private static void RegenerateChecksum()
        {
            if (!IsOpen)
                return;
            _Checksum = Conversions.ToString(GenerateChecksum(Memory));
        }

        private static object GenerateChecksum(byte[] Buffer)
        {
            var Checksum = SHA256.Create();
            return BufferToChecksum(Checksum.ComputeHash(Buffer));
        }

        private static object BufferToChecksum(byte[] array)
        {
            string hex_value = "";
            int i;
            var loopTo = array.Length - 1;
            for (i = 0; i <= loopTo; i++)
                hex_value += array[i].ToString("X2");
            return hex_value.ToUpper();
        }

        public static string GlobalHexOffset = "&H000000";
        public static int GlobalOffset => HexConvert.HexToInt(GlobalHexOffset);
        public static int GlobalConversion = 0;

        public static event StorageEventHandler OnDataStore;

        public static event StorageEventHandler OnDataRetrieve;

        public static event StorageEventHandler OnSave;

        public static event StorageEventHandler OnLoad;

        public delegate void StorageEventHandler();

        public static void Me_Stored()
        {
            // TBD
            if (Debugger.IsAttached)
            {
                // MsgBox("Received Event")
            }
        }

        public static void DataStore(object sender)
        {
            OnDataStore?.Invoke();
        }

        public static void DataRetrieve(object sender, OnControlProgress progress)
        {
            foreach (Control Control in BasicHelper.GetAllControls<IHexReader>((Control)sender))
            {
                IHexReader HexControl = (IHexReader)Control;
                HexControl.Load();
                progress?.Invoke();
            }
            OnDataRetrieve?.Invoke();
        }

        public static void Load()
        {
            ScanEmptyChunks(0x64, 2, new byte[] { 0x00, 0xFF });
            RaiseLoad();
        }

        public static void RaiseLoad()
        {
            OnLoad?.Invoke();
        }

        private static int _MinChunkSize;
        private static byte[] _ChunkPatterns;

        private static void ScanEmptyChunks(int minChunkSize, byte skip, byte[] chunkPatterns)
        {
            if (!IsOpen) return;

            _MinChunkSize = minChunkSize;
            _ChunkPatterns = chunkPatterns;

            _AllEmptyChunks = new List<MemoryChunk>();
            var chunkMatch = new long[chunkPatterns.Length];

            for (long offset = GlobalOffset; offset < Memory.LongLength; offset++)
            {
                for (int i = 0; i < chunkPatterns.Length; i++)
                {
                    if(chunkPatterns[i] == Memory[offset])
                    {
                        chunkMatch[i]++;
                    }
                    else
                    {
                        if(chunkMatch[i] > minChunkSize)
                        {
                            var currentChunk = chunkMatch[i] - skip;
                            _AllEmptyChunks.Add(new MemoryChunk(
                                    offset - currentChunk,
                                    currentChunk,
                                    -GlobalOffset
                                ));
                        }
                        chunkMatch[i] = 0;
                    }
                }
            }

            AllEmptyChunks = _AllEmptyChunks.AsReadOnly();
        }

        private static List<MemoryChunk> _AllEmptyChunks;
        public static IReadOnlyCollection<MemoryChunk> AllEmptyChunks { get; private set; }

        public static MemoryChunk PartialEmptyChunk(long address)
        {
            return AllEmptyChunks.FirstOrDefault(x => x.Address < address && (x.Address + x.Length) >= address);
        }

        public static MemoryChunk EmptyChunk(long size)
        {
            return AllEmptyChunks.FirstOrDefault(x => x.Length >= size);
        }

        public static IReadOnlyCollection<MemoryChunk> EmptyChunks(long size)
        {
            return AllEmptyChunks.Where(x => x.Length >= size).ToList().AsReadOnly();
        }

        public static MemoryChunk Chunk(long address)
        {
            return AllEmptyChunks.FirstOrDefault(x => x.Address == address);
        }

        public static void UseChunk(MemoryChunk memoryChunk, long size)
        {
            memoryChunk.Use(size);

            if (memoryChunk.Length == 0 || memoryChunk.Length < _MinChunkSize)
            {
                _AllEmptyChunks.Remove(memoryChunk);
                AllEmptyChunks = _AllEmptyChunks.AsReadOnly();
            }
        }

        public static void FreeChunk(MemoryChunk memoryChunk)
        {
            Array.Clear(Memory, (int)memoryChunk.Address, (int)memoryChunk.Length);

            if (memoryChunk.Length >= _MinChunkSize)
            {
                _AllEmptyChunks.Add(memoryChunk);
                AllEmptyChunks = _AllEmptyChunks.AsReadOnly();
            }

            RaiseLoad();
        }

        public static void MoveChunk(MemoryChunk sourceChunk, long address)
        {
            var destinationChunk = Chunk(address);

            if(destinationChunk != null)
            {
                Array.Copy(Memory, sourceChunk.Address, Memory, destinationChunk.Address, sourceChunk.Length);

                FreeChunk(sourceChunk);

                UseChunk(destinationChunk, destinationChunk.Length);
            }
        }

        public static bool IsOpen
        {
            get
            {
                return Memory != null && Memory.LongLength > 0L;
            }
        }

        public static bool HasUnsavedWork
        {
            get
            {
                return Conversions.ToBoolean(IsOpen && Operators.ConditionalCompareObjectNotEqual(_Checksum, GenerateChecksum(Memory), false));
            }
        }

    }

    #endregion

    #region  HexQuery 

    public class HexQuery
    {

        #region  Public 

        public static bool IsKeyCode(char param_char)
        {
            bool lv_value = false;
            var lv_keycode = HexConvert.CharToKeyCode(Conversions.ToString(char.ToUpper(param_char)));
            if (char.IsDigit(param_char))
            {
                lv_value = true;
            }
            else if ((int)lv_keycode >= 65 & (int)lv_keycode <= 70)
            {
                lv_value = true;
            }
            return lv_value;
        }

        public static bool IsNumericCode(char param_char)
        {
            bool lv_value = false;
            var lv_keycode = HexConvert.CharToKeyCode(Conversions.ToString(char.ToUpper(param_char)));
            if (char.IsDigit(param_char))
            {
                lv_value = true;
            }
            return lv_value;
        }

        public static string HexAt(int param_offset, int param_length)
        {
            string lv_value = "";
            byte[] lv_bytes = MemoryLiterator.Read(param_offset, param_length).Buffer;
            lv_value = HexConvert.BytesToHex(lv_bytes);
            return lv_value;
        }

        #endregion

    }

    #endregion

    #region  HexConvert 

    public class HexConvert
    {

        #region  Public 

        public static int CharToInt(char param_char)
        {
            return Strings.Asc(param_char);
        }

        public static char IntToChar(int param_int)
        {
            return Strings.Chr(param_int);
        }

        public static string ByteToStringRaw(byte param_byte, int param_offset)
        {
            return Conversions.ToString(Convert.ToChar(param_byte + param_offset));
        }

        public static string ByteToBitRaw(byte param_byte)
        {
            return Convert.ToString(param_byte, 2).PadLeft(8, '0');
        }

        public static int BitToIntRaw(string param_string)
        {
            return Convert.ToInt32(param_string, 2);
        }

        public static string ByteToHexRaw(byte param_byte)
        {
            return param_byte.ToString("X2");
        }

        public static int HexToInt(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            text = text.Trim();

            if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                return HexToIntRaw(text.Substring(2));
            }

            if (text.StartsWith("&H", StringComparison.OrdinalIgnoreCase))
            {
                return HexToIntRaw(text.Substring(2));
            }
            else
            {
                return Convert.ToInt32(text);
            }
        }

        public static int HexToIntRaw(string param_string)
        {
            return Convert.ToInt32(param_string, 16);
        }

        public static string IntToAddress(int integer, string format)
        {
            return HexToAddress(string.Format($"0x{{0:{format}}}", integer));
        }

        public static string IntToAddress(int integer, int padding = 5)
        {
            return HexToAddress(IntToHex(integer, padding));
        }

        public static string HexToAddress(string hex)
        {
            return hex?.Replace("&H", "0x");
        }

        public static string AddressToHex(string address)
        {
            return address?.Replace("0x", "&H");
        }

        public static int AddressToInt(string address)
        {
            return HexToInt(AddressToHex(address));
        }

        public static string IntToHex(int integer, int padding = 0)
        {
            return "&H" + IntToHexRaw(integer, padding);
        }

        public static string IntToHexRaw(int param_int, int param_zeroes = 0)
        {
            return Conversion.Hex(param_int).PadLeft(param_zeroes + 1, '0');
        }

        public static string StringToByte(byte param_char, int param_offset)
        {
            return (param_char - param_offset).ToString();
        }

        public static string BytesToString(byte[] param_bytes)
        {
            string lv_string = "";
            for (int index = 0, loopTo = param_bytes.Length - 1; index <= loopTo; index++)
            {
                if (param_bytes[index] == 255)
                    break;
                lv_string += ByteToStringRaw(param_bytes[index], HexStorage.GlobalConversion);
            }
            return lv_string;
        }

        public static string BytesToHex(byte[] param_bytes, int param_length = -1, int maxLength = 100, string separator = "")
        {
            if (param_bytes == null) return "null";

            // Dim sSampleText As String = "dostuffandthings"
            // Dim aByte() As Byte = System.Text.Encoding.UTF8.GetBytes(sSampleText)
            // Dim sHex As String = BitConverter.ToString(aByte).Replace("-", "")

            // Dim byteArray As Byte() = BitConverter.GetBytes(15055065542)
            // Array.Reverse(byteArray)
            // MsgBox(BitConverter.ToString(byteArray).Replace("-", ""))

            string lv_string = "";
            for (int index = 0, loopTo = (param_length >= 0 && param_length < param_bytes.Length ? param_length : param_bytes.Length) - 1; index <= loopTo; index++)
            {
                lv_string += ByteToHexRaw(param_bytes[index]) + separator;
                if(index >= maxLength - 1)
                {
                    lv_string += $"...x{param_bytes.Length - maxLength}";
                    break;
                }
            }
            return lv_string;
        }

        public static string BytesToNumeric(byte[] param_bytes)
        {
            string lv_string = "";
            int lv_value;
            Array.Reverse(param_bytes);
            if (param_bytes.Length == 1)
            {
                lv_value = ReadInt8(param_bytes);
            }
            else if (param_bytes.Length == 2)
            {
                lv_value = BitConverter.ToUInt16(param_bytes, 0);
            }
            else if (param_bytes.Length == 3)
            {
                lv_value = ReadInt24(param_bytes);
            }
            else
            {
                lv_value = (int)BitConverter.ToUInt32(param_bytes, 0);
            }
            lv_string = lv_value.ToString();
            return lv_string;
        }

        private static int ReadInt8(byte[] buffer)
        {
            return buffer[0] & 0xFF;
        }

        private static int ReadInt16(byte[] buffer)
        {
            return (buffer[1] & 0xFF) << 8 | buffer[0] & 0xFF;
        }

        private static int ReadInt24(byte[] buffer)
        {
            return (buffer[2] & 0xFF) << 16 | (buffer[1] & 0xFF) << 8 | buffer[0] & 0xFF;
        }

        private static int ReadInt32(byte[] buffer)
        {
            return (buffer[3] & 0xFF) << 24 | (buffer[2] & 0xFF) << 16 | (buffer[1] & 0xFF) << 8 | buffer[0] & 0xFF;
        }

        public static byte[] StringToBytes(string param_string, bool param_reverse)
        {
            var lv_encoding = new UTF8Encoding();
            byte[] lv_bytes = lv_encoding.GetBytes(param_string);
            for (int index = 0, loopTo = lv_bytes.Length - 1; index <= loopTo; index++)
                lv_bytes[index] = Conversions.ToByte(StringToByte(lv_bytes[index], HexStorage.GlobalConversion));
            if (param_reverse == true)
                Array.Reverse(lv_bytes);
            return lv_bytes;
        }

        public static byte[] NumericToBytes(string param_string, bool param_reverse, int param_length)
        {
            byte[] lv_bytes = BitConverter.GetBytes(Conversions.ToInteger(param_string));
            Array.Resize(ref lv_bytes, param_length);
            if (param_reverse == false)
                Array.Reverse(lv_bytes);
            return lv_bytes;
        }

        public static string NumericToHex(int param_integer, string param_format = null)
        {
            if (param_format == null)
            {
                return Conversion.Hex(param_integer);
            }
            else
            {
                return param_integer.ToString(param_format);
            }
        }

        public static byte?[] HexToNullableBytes(string Hex, bool param_reverse)
        {
            if (Hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte?[] lv_bytes = new byte?[(Hex.Length >> 1)];

            for (int i = 0, loopTo = (Hex.Length >> 1) - 1; i <= loopTo; i++)
            {
                if (Conversions.ToString(Hex[i << 1]) == "X" && Conversions.ToString(Hex[(i << 1) + 1]) == "X")
                {
                    lv_bytes[i] = default;
                    continue;
                }
                lv_bytes[i] = (byte)((HexCharToInt(Hex[i << 1]) << 4) + HexCharToInt(Hex[(i << 1) + 1]));
            }

            if (param_reverse == true)
                Array.Reverse(lv_bytes);
            return lv_bytes;
        }

        public static byte[] HexToBytes(string Hex, bool param_reverse)
        {
            if (Hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] lv_bytes = new byte[(Hex.Length >> 1)];

            for (int i = 0, loopTo = (Hex.Length >> 1) - 1; i <= loopTo; i++)
                lv_bytes[i] = (byte)((HexCharToInt(Hex[i << 1]) << 4) + HexCharToInt(Hex[(i << 1) + 1]));

            if (param_reverse == true)
                Array.Reverse(lv_bytes);
            return lv_bytes;
        }

        public static int HexCharToInt(char hex)
        {
            int val = Strings.AscW(hex);
            // For uppercase A-F letters:
            // return val - (val < 58 ? 48 : 55);
            // For lowercase a-f letters:
            // return val - (val < 58 ? 48 : 87);
            // Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : val < 97 ? 55 : 87);
        }

        public static Keys CharToKeyCode(string param_string)
        {
            var kc = new KeysConverter();
            try
            {
                return (Keys)Conversions.ToInteger(kc.ConvertFromString(param_string));
            }
            catch
            {
                return default;
            }
        }

        public static int PCToSnes(string param_integer, bool IgnoreHeader)
        {
            param_integer = param_integer.Replace("&H", "");
            int lv_address = Convert.ToInt32(param_integer, 16);
            if (Conversions.ToDouble(HexStorage.GlobalHexOffset) > lv_address)
                return 0;
            if (IgnoreHeader == false)
                lv_address = (int)Math.Round(lv_address - Conversions.ToDouble(HexStorage.GlobalHexOffset));
            lv_address = ((lv_address & 0x7F8000) << 1) + 0x8000 + (lv_address & 0x7FFF);

            // ORIGINAL FORMULAS
            // lorom lv_address = ((lv_address & 0x7f8000) << 1) + 0x8000 + (lv_address & 0x7fff)
            // hirom lv_address = 0xc00000 + (lv_address & 0x3fffff)
            return lv_address;
        }

        public static int SnesToPC(string param_integer, bool IgnoreHeader)
        {
            param_integer = param_integer.Replace("&H", "");
            int lv_address = Convert.ToInt32(param_integer, 16);
            switch (lv_address % 0x10000)
            {
                case var @case when @case < 0x2000:
                    {
                        // RAM
                        return 0;
                    }
                // Return lv_address Mod &H2000 + &HC13
                case var case1 when case1 < 0x8000:
                    {
                        // Reserved
                        return 0;
                    }
            }
            if (IgnoreHeader == false)
                lv_address = (int)Math.Round(lv_address + Conversions.ToDouble(HexStorage.GlobalHexOffset));
            lv_address = ((lv_address & 0x7F0000) >> 1) + (lv_address & 0x7FFF);

            // ORIGINAL FORMULAS
            // lorom lv_address = ((lv_address & 0x7f0000) >> 1) + (lv_address & 0x7fff)
            // hirom lv_address = lv_address & 0x3fffff
            return lv_address;
        }

        public static T BytesToInt<T>(byte[] bytes) where T : struct
        {
            if (typeof(T) == typeof(sbyte))
            {
                return (T)(object) bytes[0];
            }
            else if (typeof(T) == typeof(byte))
            {
                return (T)(object) bytes[0];
            }
            else if (typeof(T) == typeof(short))
            {
                return (T) (object) BitConverter.ToInt16(bytes, 0);
            }
            else if (typeof(T) == typeof(ushort))
            {
                return (T)(object)BitConverter.ToUInt16(bytes, 0);
            }
            else if (typeof(T) == typeof(int))
            {
                if(bytes.Length == 3) return (T)(object)((sbyte)bytes[0] << 16 | bytes[1] << 8 | bytes[2]);
                return (T)(object)BitConverter.ToInt32(bytes, 0);
            }
            else if (typeof(T) == typeof(uint))
            {
                if (bytes.Length == 3) return (T)(object)(bytes[0] << 16 | bytes[1] << 8 | bytes[2]);
                return (T)(object)BitConverter.ToUInt32(bytes, 0);
            }
            else if (typeof(T) == typeof(long))
            {
                return (T)(object)BitConverter.ToInt64(bytes, 0);
            }
            else if (typeof(T) == typeof(ulong))
            {
                return (T)(object)BitConverter.ToUInt64(bytes, 0);
            }

            throw new Exception($"BytesToInt converter does not exist for {typeof(T).Name}!");
        }

        public static byte[] IntToBytes<T>(T value, int length) where T : struct
        {
            var buffer = IntToBytes(value);
            Array.Resize(ref buffer, length);
            return buffer;
        }

        public static byte[] IntToBytes<T>(T value) where T : struct
        {
            if (typeof(T) == typeof(sbyte))
            {
                return BitConverter.GetBytes((sbyte)(object)value);
            }
            else if (typeof(T) == typeof(byte))
            {
                return BitConverter.GetBytes((byte)(object)value);
            }
            else if (typeof(T) == typeof(short))
            {
                return BitConverter.GetBytes((short)(object)value);
            }
            else if (typeof(T) == typeof(ushort))
            {
                return BitConverter.GetBytes((ushort)(object)value);
            }
            else if (typeof(T) == typeof(int))
            {
                return BitConverter.GetBytes((int)(object)value);
            }
            else if (typeof(T) == typeof(uint))
            {
                return BitConverter.GetBytes((uint)(object)value);
            }
            else if (typeof(T) == typeof(long))
            {
                return BitConverter.GetBytes((long)(object)value);
            }
            else if (typeof(T) == typeof(ulong))
            {
                return BitConverter.GetBytes((ulong)(object)value);
            }

            throw new Exception($"BytesToInt converter does not exist for {typeof(T).Name}!");
        }

        public static string GetOffset(string offset, HexAddressFormatType type, int index = 0)
        {
            string hexOffset = offset.Replace("0x", "&H");
            switch (type)
            {
                case HexAddressFormatType.Index:
                    hexOffset = IntToHex(index, 5);
                    break;
                case HexAddressFormatType.PC:
                    hexOffset = IntToHex(SnesToPC(hexOffset, true), 5);
                    break;
                case HexAddressFormatType.SNES_LoROM:
                    hexOffset = IntToHex(PCToSnes(hexOffset, true), 5);
                    break;
                case HexAddressFormatType.Raw:
                default:
                    break;
            }

            return hexOffset;
        }

        #endregion

    }

    #endregion

    #region  MSScript 

    // Mesiah's Simple Script
    public static class MSScript
    {
        public readonly static decimal Version = 1.0m;
        public readonly static string[] Commands = new[] { "HEX", "ENVIRONMENT" };

        private static EndianType Environment;
        private static int Index;

        public static void ExecuteCode(StringCollection param_code, int param_index = 0)
        {
            var lv_strings = new string[param_code.Count];
            param_code.CopyTo(lv_strings, 0);
            ExecuteCode(lv_strings, param_index);
        }

        public static void ExecuteCode(string[] param_code, int param_index = 0)
        {
            foreach (string line in param_code)
                Eval(line, param_index);
        }

        public static void Eval(string param_string, int param_index)
        {
            string[] lv_statement = param_string.Split(new[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);
            string lv_command = lv_statement[0];
            string lv_code = lv_statement[1];
            Reset();
            switch (GetCommand(lv_command) ?? "")
            {
                case "HEX":
                    {
                        HEX(lv_code);
                        break;
                    }

                default:
                    {
                        HEX(lv_command + lv_code);
                        break;
                    }
            }
        }

        private static string GetCommand(string param_string)
        {
            string GetCommandRet = default;
            if (Commands.Contains(param_string.ToUpper()) == true)
            {
                GetCommandRet = param_string.ToUpper();
            }
            else
            {
                GetCommandRet = "";
            }

            return GetCommandRet;
        }

        private static void Reset()
        {
            Environment = EndianType.Little_Endian;
            Index = 0;
        }

        private static void HEX(string param_code)
        {
            // Format: (Endian) Offset > Value (Skip)
            EndianType lv_type;
            int lv_offset;
            string lv_value;
            int lv_skip;
            string[] lv_info = SetupCode(param_code, 4, 1, 1);

            lv_type = (EndianType)Conversions.ToInteger(string.IsNullOrEmpty(lv_info[0]) ? Environment.ToString() : lv_info[0]);
            lv_offset = (int)Math.Round(Conversions.ToDouble(lv_info[1]) + HexStorage.GlobalOffset);
            lv_value = lv_info[2];
            lv_skip = (int)Math.Round(string.IsNullOrEmpty(lv_info[3]) ? lv_value.Length / 2d : Conversions.ToInteger(lv_info[3]));

            Write(lv_offset + Index * lv_skip, HexConvert.HexToBytes(lv_value, Conversions.ToBoolean(lv_type)));
        }

        private static string[] SetupCode(string param_code, int param_max, int param_front = 0, int param_back = 0)
        {
            string[] lv_code = param_code.Replace(" ", "").Split(new[] { ">" }, param_max, StringSplitOptions.None);
            CheckForErrors(lv_code.Length, param_max - param_front - param_back, param_max);
            var lv_result = new string[param_max];
            if (lv_code.Length < param_max)
                Array.Resize(ref lv_code, param_max - 1 - param_front + 1);
            if (lv_code.Length < param_max)
                lv_code.CopyTo(lv_result, param_front);
            return lv_result;
        }

        private static void CheckForErrors(int param_length, int param_min, int param_max)
        {
            if (param_length < param_min)
                throw new Exception("MSScript Error: Code is too short to be valid");
            if (param_length > param_max)
                throw new Exception("MSScript Error: Code is too long to be valid");
        }

        private static void Write(int param_offset, byte[] param_bytes)
        {
            byte[] buffer = new byte[(int)(param_bytes.LongLength - 1L + 1)];
            for (int index = 0, loopTo = param_bytes.Length - 1; index <= loopTo; index++)
                buffer[index] = param_bytes[index];
            MemoryLiterator.Write(typeof(MSScript), new MemoryOperation(param_offset, buffer));
        }

    }

    #endregion

    #region  PreWrite 

    public delegate void OnPreWrite(object sender, PreWriteArgs e);

    public class PreWriteArgs : EventArgs
    {

        #region  Constructor 

        public PreWriteArgs(decimal Value) : base()
        {
            NewValue = Value;
        }

        #endregion

        #region  Properties 

        public decimal NewValue;

        #endregion

    }

    #endregion

    #region  BitOffset 

    public enum BitOffset
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8
    }

    #endregion

    #region  IHexDefinition 

    public interface IHexDefinition
    {
        string Definition { get; }
        void GetDefinition();
    }

    public interface IHexDefinitionWriter : IHexDefinition
    {
        void SetDefinition(object value);
    }

    #endregion

    #region  IHexPanel 

    public interface IHexPanel
    {

        string HexOffset { get; set; }

    }

    #endregion

    #region  IHexListBox 

    public interface IHexListBox
    {

        string CurrentOffset();

        int CurrentIndex();

    }

    #endregion

    #region  IHexReader 

    public interface IHexReader
    {

        void Load();

    }

    #endregion

    #region  IHexWriter 

    public interface IHexWriter
    {

        void Save(int Offset = -1);

    }

    #endregion

    #region  IHexEditor 

    public interface IHexEditor : IHexReader, IHexWriter
    {

    }

    #endregion

    #region  IHexTable 

    public interface IHexTable : IHexEditor
    {

        int Index { get; set; }

        void Reload(int Row = -1);

    }

    #endregion

    #region  IHexControl 

    public interface IHexControl : IHexEditor
    {

        string HexOffset { get; set; }

    }

    #endregion

    #region  IHexControlContainer 

    public interface IHexControlContainer : IBasicUseControl
    {


    }

    #endregion

    #region  IBitControl 

    public interface IBitControl
    {

        BitOffset BitOffset { get; set; }

    }

    #endregion

    #region  HexUtility 

    public class HexUtility
    {

        public static HexListBox GetListBox(Control Control)
        {
            dynamic Associate = ControlExtension.FindParent<BasicListBoxAssociate>(ref Control);
            if (Associate != null)
                return (HexListBox)Associate.ListBox;
            return null;
        }
    }

    public class HexUtility<T> where T : Control, IHexControl
    {
        public T HexControl { get; private set; }
        public IBitControl WithBitOffset { get; private set; }

        private HexListBox ListBox { get; set; }
        private bool SetListBox { get; set; }
        private bool IgnoreListBox { get; set; }

        public HexUtility(T HexControl)
        {
            this.HexControl = HexControl;
            WithBitOffset = (IBitControl)(HexControl is IBitControl ? HexControl : null);
        }

        public int GetBitOffset()
        {
            return WithBitOffset != null ? (int)WithBitOffset.BitOffset : 0;
        }

        public int GetGlobalOffset()
        {
            return HexConvert.HexToInt(HexStorage.GlobalHexOffset);
        }

        public int GetControlOffset()
        {
            return HexConvert.HexToInt(HexControl.HexOffset);
        }

        public int GetPanelsOffset()
        {
            int lv_offset = 0;
            if (SetListBox == false)
            {
                ListBox = HexUtility.GetListBox(HexControl);
                SetListBox = true;
            }
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = HexControl;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (HexConvert.HexToInt(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & HexConvert.HexToInt(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * (double) HexConvert.HexToInt(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + ((double) HexConvert.HexToInt(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            //Control argStartingContainer1 = HexControl;
            //foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer1, "HexPanel"))
            //{
            //    if (HexConvert.HexToInt(ctrl.IndexOffset) > 0d)
            //        IgnoreListBox = true;
            //    if (ListBox != null & HexConvert.HexToInt(ctrl.Pointer) == 0d)
            //        lv_offset = (int)Math.Round(lv_offset + (lv_index * (double)HexConvert.HexToInt(ctrl.IndexOffset) + Math.Floor((GetBitOffset() + lv_index) * (int)ctrl.IndexBitOffset / 8d)));
            //    lv_offset = (int)Math.Round(lv_offset + ((double)HexConvert.HexToInt(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            //}
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + (double) HexConvert.HexToInt(ListBox.CurrentOffset()));
            return lv_offset;
        }

        public int GetHexOffset()
        {
            return GetHexOffset(GetControlOffset());
        }

        public int GetHexOffset(int offset)
        {
            return GetGlobalOffset() + offset + GetPanelsOffset() + GetListOffset();
        }

        public int GetHexOffset(string offset)
        {
            return GetGlobalOffset() + HexConvert.HexToInt(offset);
        }

        private int GetPanelBitOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = HexControl;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
                lv_offset += ctrl.CurrentBitOffset(lv_index + GetBitOffset());
            return lv_offset;
        }

    }

    #endregion

    #region  HexOffsetType 

    public class HexOffsetType
    {

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
                if ((_HexOffset ?? "") != (value ?? ""))
                {
                    _HexOffset = value;
                }
            }
        }

        private int _MaxLength = 1;
        [Category("Behavior")]
        [Description("The length of the value loaded in this control.")]
        [DefaultValue(1)]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                if (MaxLength != value)
                {
                    _MaxLength = value;
                }
            }
        }

        private string _HexValueTrue = "&H000001";
        [Category("Function")]
        [Description("The value of the HexOffset")]
        [DefaultValue("&H000001")]
        public string HexValueTrue
        {
            get
            {
                return _HexValueTrue;
            }
            set
            {
                if ((_HexValueTrue ?? "") != (value ?? ""))
                {
                    _HexValueTrue = value;
                }
            }
        }

        private string _HexValueFalse = "&H000000";
        [Category("Function")]
        [Description("The value of the HexOffset")]
        [DefaultValue("&H000000")]
        public string HexValueFalse
        {
            get
            {
                return _HexValueFalse;
            }
            set
            {
                if ((_HexValueFalse ?? "") != (value ?? ""))
                {
                    _HexValueFalse = value;
                }
            }
        }

    }

    #endregion

    #region HexAppDefinition

    public interface IHexPartialDefinition
    {
        byte[] DefaultDefinition { get; }
        string DefinitionFileName { get; }
        object Template { get; }

        void Reload();
    }

    public abstract class HexAppPartialDefinition<T> : IHexPartialDefinition
        where T : class, new()
    {
        public HexAppPartialDefinition(ISynchronizeInvoke synchronizingObject)
        {
            var templateDefinition = new BasicJsonDefinition<T>(
                    DefaultDefinition
                );

            Container = new BasicDefinitionContainer<T>(templateDefinition, CurrentDefinition);

            ContainerChanged();

            WatchDirectory(DefinitionFileName.GetResourceFileDirectory());

            Watcher.SynchronizingObject = synchronizingObject;
            HexDefinitionManager.OnSave += OnSave;
        }

        public abstract string DefinitionFileName { get; }

        public abstract byte[] DefaultDefinition { get; }

        public void Reload()
        {
            Container.Reload();

            OnSave();

            ContainerChanged();
        }

        public BasicDefinitionContainer<T> Container { get; set; }
        public object Template => Container.Default;

        private BasicJsonDefinition<T> CurrentDefinition => new BasicJsonDefinition<T>(
                    File.ReadAllBytes(DefinitionFileName.GetResourceFilePath())
                );

        public abstract void ContainerChanged();

        private FileSystemWatcher Watcher;

        private void WatchDirectory(string directoryPath)
        {
            Watcher = new FileSystemWatcher();
            Watcher.Path = directoryPath;
            Watcher.NotifyFilter = NotifyFilters.LastWrite;
            Watcher.Filter = "*.*";
            Watcher.Changed += new FileSystemEventHandler(OnChanged);
            Watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs args)
        {
            // OnChanged will get called many times, some of which might be right after the file was
            // changed but before the other processor had a chance to disconnect, therefore we might
            // get an access violationg error: so we simply try on a different step. This could result
            // in some changes never getting processed but we'll cross that bridge if we get there.
            if (args.FullPath == DefinitionFileName.GetResourceFilePath()
                && !IsFileLocked(new FileInfo(args.FullPath)))
            {
                Container.Reload(CurrentDefinition);

                ContainerChanged();
            }
        }

        private void OnSave()
        {
            File.WriteAllBytes(DefinitionFileName.GetResourceFilePath(), Container.Current.ToBytes());
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
    
    public abstract class HexAppSingletonDefinition<T> : HexAppPartialDefinition<T>
        where T : class, new()
    {
        public HexAppSingletonDefinition(ISynchronizeInvoke synchronizingObject) : base(synchronizingObject) { }

        public override string DefinitionFileName => "Definitions.json";

        public override void ContainerChanged()
        {
            HexDefinitionManager.Instance.Context = Container.Current.Definition;
        }
    }

    public abstract class HexAppDefinition
    {
        public HexAppDefinition(ISynchronizeInvoke synchronizingObject) { }

        private List<IHexPartialDefinition> _PartialDefinitions { get; set; } = new List<IHexPartialDefinition>();
        public IReadOnlyCollection<IHexPartialDefinition> PartialDefinitions => _PartialDefinitions.AsReadOnly();

        public int PartialDepth { get; protected set; } = 1;

        public void Register(IHexPartialDefinition partialDefinition)
        {
            _PartialDefinitions.Add(partialDefinition);
        }
    }

    public abstract class HexAppDefinitionContext
    {
        public HexAppDefinition AppDefinition { get; private set; }

        public HexAppDefinitionContext(HexAppDefinition appDefinition)
        {
            AppDefinition = appDefinition;
        }
    }

    #endregion

    #region HexDefinitionManager

    public class HexDefinitionManager : BasicDefinitionManager<HexDefinitionManager, object>
    {
        static HexDefinitionManager()
        {
            HexStorage.OnLoad += Load;
        }

        public int Nesting { get; set; } = 1;

        public dynamic Context
        {
            get => Instances.First();
            set
            {
                UnregisterAll();
                Register(value);
                Load();
            }
        }

        public HexAppDefinition AppDefinition => (Context as HexAppDefinitionContext)?.AppDefinition;

        public delegate void DefinitionEventHandler();
        public delegate void DefinitionChangeEventHandler(string fieldPath);

        public static event DefinitionEventHandler OnLoad;
        public static event DefinitionEventHandler OnSave;
        public static event DefinitionChangeEventHandler OnChange;

        public static void Load()
        {
            OnLoad?.Invoke();
        }

        public static void Save()
        {
            OnSave?.Invoke();
        }

        public static void Change(string fieldPath)
        {
            if(!string.IsNullOrEmpty(fieldPath))
            {
                OnChange?.Invoke(fieldPath);
            }
        }

        public static IHexPartialDefinition GetPartialDefinition(string fieldPath)
        {
            var moduleName = fieldPath.Contains('.') 
                ? string.Join("_", fieldPath.Split('.').Take(Instance.AppDefinition.PartialDepth))
                : fieldPath;
            return Instance.AppDefinition.PartialDefinitions
                .First(x => Path.GetFileNameWithoutExtension(x.DefinitionFileName) == moduleName);
        }

        private static T GetDefinition<T>(object context, string fieldPath) where T : class
        {
            var wrapped = BasicPropertyManager.Wrap(context);
            var value = wrapped[fieldPath];
            T casted = null;
            if (value is IEnumerable enumerable)
            {
                casted = enumerable.Cast<dynamic>().ToList() as T;
            }
            else
            {
                casted = value as T;
            }
            return casted;
        }

        public static T GetTemplateDefinition<T>(string fieldPath) where T : class
        {
            var partialDefinition = GetPartialDefinition(fieldPath).Template;
            var partialPath = string.Join(".", fieldPath.Split('.').Skip(1));
            return GetDefinition<T>(partialDefinition, partialPath);
        }

        public static dynamic GetTemplateDefinition(string fieldPath)
        {
            return GetTemplateDefinition<dynamic>(fieldPath);
        }

        public static ICollection<T> GetTemplateCollectionDefinition<T>(string fieldPath)
        {
            return GetTemplateDefinition<ICollection<T>>(fieldPath);
        }

        public static ICollection<dynamic> GetTemplateCollectionDefinition(string fieldPath)
        {
            return GetTemplateCollectionDefinition<dynamic>(fieldPath).ToList();
        }

        public static T GetDefinition<T>(string fieldPath) where T : class
        {
            return GetDefinition<T>(Instance.Context, fieldPath);
        }

        public static dynamic GetDefinition(string fieldPath)
        {
            return GetDefinition<dynamic>(fieldPath);
        }

        public static ICollection<T> GetCollectionDefinition<T>(string fieldPath)
        {
            return GetDefinition<ICollection<T>>(fieldPath);
        }

        public static ICollection<dynamic> GetCollectionDefinition(string fieldPath)
        {
            return fieldPath.Split(',')
                .SelectMany(x => GetCollectionDefinition<dynamic>(x))
                .ToList();
        }

        public static void SetDefinition(string fieldPath, object value)
        {
            if (value is IEnumerable enumerable)
            {
                var type = GetCollectionDefinitionElementType(fieldPath);
                value = typeof(Enumerable)
                    .GetMethod("Cast")
                    .MakeGenericMethod(type)
                    .Invoke(null, new object[] { enumerable });
                value = typeof(Enumerable)
                    .GetMethod("ToList")
                    .MakeGenericMethod(type)
                    .Invoke(null, new object[] { value });
            }
            var wrapped = BasicPropertyManager.Wrap(Instance.Context);
            wrapped[fieldPath] = value;
            Save();
            Change(fieldPath);
        }

        public static Type GetDefinitionType(string fieldPath)
        {
            return BasicPropertyManager.Type(Instance.Context, fieldPath);
        }

        public static Type GetCollectionDefinitionElementType(string fieldPath)
        {
            var collectionType = GetDefinitionType(fieldPath);
            if(collectionType.GetInterfaces()
                            .Any(x => x.IsGenericType &&
                                x.GetGenericTypeDefinition() == typeof(ICollection<>)))
            {
                return collectionType.GetGenericArguments()[0];
            }
            return null;
        }
    }

    #endregion

    #endregion

    #region  HexFontTable 

    public class HexFontTable : Component
    {

        private static HexFontTable _GlobalFontTable;
        public static HexFontTable GlobalFontTable
        {
            get
            {
                return _GlobalFontTable;
            }
        }

        private FontTable _FontTable;
        [Browsable(false)]
        public FontTable FontTable
        {
            get
            {
                if (!DesignMode && _FontTable == null)
                {
                    _FontTable = new FontTable();
                    TrySetGlobal();
                }
                return _FontTable;
            }
            set
            {
                if (!ReferenceEquals(value, _FontTable))
                {
                    _FontTable = value;
                    TrySetGlobal();
                }
            }
        }

        [DefaultValue(false)]
        public bool IsGlobal { get; set; }

        #region  Component Designer 

        public HexFontTable(IContainer Container) : this()
        {
            Container.Add(this);
        }

        public HexFontTable() : base()
        {
            InitializeComponent();
            TrySetGlobal();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private IContainer components;

        private void InitializeComponent()
        {
            components = new Container();
        }

        private void TrySetGlobal()
        {
            if (IsGlobal)
            {
                if (GlobalFontTable != null && !ReferenceEquals(GlobalFontTable, this))
                {
                    throw new ArgumentException("Cannot have multiple global font tables!");
                }
                _GlobalFontTable = this;
            }
        }

        #endregion

    }

    #endregion

    #region  HexUserControl 

    public class HexUserControl : BasicUserControl, IHexControlContainer
    {


        public override void Active(OnControlProgress Progress)
        {
            base.Active(Progress);
            ReloadControls(Progress);
        }

        public void ReloadControls(OnControlProgress Progress)
        {
            HexStorage.DataRetrieve(this, Progress);
        }

    }

    #endregion

    #region  HexForm 

    public class HexForm : BasicForm
    {

        public override void Active(OnControlProgress Progress)
        {
            base.Active(Progress);
            var self = this as Form;
            foreach (Control ctrl in FormExtension.FindAllChildren(ref self))
            {
                if (ctrl is IBasicUseControl)
                {
                    IBasicUseControl basic = (IBasicUseControl)ctrl;
                    if (basic != null)
                        basic.Active(Progress);
                }
            }
        }

        public override void Inactive(OnControlProgress Progress)
        {
            base.Inactive(Progress);
            var self = this as Form;
            foreach (Control ctrl in FormExtension.FindAllChildren(ref self))
            {
                if (ctrl is IBasicUseControl)
                {
                    IBasicUseControl basic = (IBasicUseControl)ctrl;
                    if (basic != null)
                        basic.Inactive(Progress);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int WM_MDIDESTROY = 0xF060;

            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam)
                {
                    case var @case when @case.ToInt32() == WM_MDIDESTROY:
                        {
                            Hide();
                            m.Msg = 0;
                            break;
                        }
                }
            }

            base.WndProc(ref m);
        }

    }

    #endregion

    #region  HexTextBox 

    public class HexTextBox : BasicTextBox, IHexControl
    {

        private string LoadedText = "";
        public int WriteLength = 0;
        private bool IgnoreListBox = false;
        private HexListBox ListBox = null;
        private bool IsLoading = false;

        protected Label label = new Label();

        #region  Constructor 

        public HexTextBox()
        {
            SetStyle(ControlStyles.UserPaint, Spacing.Width > 0f | Spacing.Height > 0f);
            label.BackColor = Color.LightGray;
            label.Cursor = Cursors.Default;
            label.TextAlign = ContentAlignment.MiddleRight;
            Controls.Add(label);
            TextChanged += Me_TextChanged;
            KeyPress += Me_KeyPress;
            KeyDown += Me_KeyDown;
            Leave += Me_Leave;
            UserInput += Me_UserInput;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
        }

        #endregion

        #region  Properties 

        private HexFontTable _FontTable;
        [Category("Function")]
        [Description("Sets up a Font Table to use for this box.")]
        [DefaultValue(default(string))]
        public HexFontTable FontTable
        {
            get
            {
                if (FontTableUsesGlobal)
                    return HexFontTable.GlobalFontTable;
                return _FontTable;
            }
            set
            {
                _FontTable = value;
            }
        }

        [Category("Function")]
        [Description("Uses the Global Font Table to display text in this box")]
        [DefaultValue(false)]
        public bool FontTableUsesGlobal { get; set; }

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

        private InputType _Input = InputType.Normal;
        [Category("Function")]
        [Description("Limits the input type to a specific set of keys based this setting.")]
        [DefaultValue(InputType.Normal)]
        public InputType Input
        {
            get
            {
                return _Input;
            }
            set
            {
                if (value != _Input)
                {
                    _Input = value;
                }
            }
        }

        private string _OpeningTag = "";
        [Category("Function")]
        [Description("Allows this textbox to begin reading after finding the specified bytes, use X to reprent any digit!")]
        [DefaultValue("")]
        public string OpeningTag
        {
            get
            {
                return _OpeningTag;
            }
            set
            {
                if ((value ?? "") != (_OpeningTag ?? ""))
                {
                    _OpeningTag = value;
                }
            }
        }

        private string _ClosingTag = "";
        [Category("Function")]
        [Description("Allows this textbox to append a value after it's text")]
        [DefaultValue("")]
        public string ClosingTag
        {
            get
            {
                return _ClosingTag;
            }
            set
            {
                if ((value ?? "") != (_ClosingTag ?? ""))
                {
                    _ClosingTag = value;
                }
            }
        }

        private bool _ClosingTagIsEnd = false;
        [Category("Function")]
        [Description("Allows this textboxes character length to be dynamically determined by the next appearance of a closing tag.")]
        [DefaultValue(false)]
        public bool ClosingTagIsEnd
        {
            get
            {
                return _ClosingTagIsEnd;
            }
            set
            {
                if (value != _ClosingTagIsEnd)
                {
                    _ClosingTagIsEnd = value;
                }
            }
        }

        private int _DynamicLength = 0;
        [Category("Function")]
        [Description("Allows this textboxes character length to be dynamically determined from the first few byte/s of this size.")]
        [DefaultValue(0)]
        public int DynamicLength
        {
            get
            {
                return _DynamicLength;
            }
            set
            {
                if (value != _DynamicLength)
                {
                    _DynamicLength = value;
                }
            }
        }

        private bool _OvertypeMode = false;
        [Category("Function")]
        [Description("Allows this textbox to Overtype text where possible.")]
        [DefaultValue(false)]
        public bool OvertypeMode
        {
            get
            {
                return _OvertypeMode;
            }
            set
            {
                if (value != _OvertypeMode)
                {
                    _OvertypeMode = value;
                }
            }
        }

        private bool _AllowEmpty = false;
        [Category("Function")]
        [Description("Allows this textbox to have an empty value, otherwise it will add a 0 when it's non-text.")]
        [DefaultValue(false)]
        public bool AllowEmpty
        {
            get
            {
                return _AllowEmpty;
            }
            set
            {
                if (value != _AllowEmpty)
                {
                    _AllowEmpty = value;
                }
            }
        }

        private DisplayType _Display = DisplayType.Text;
        [Category("Function")]
        [Description("Determines what format will be used for the data when used by the end user.")]
        [DefaultValue(DisplayType.Text)]
        public DisplayType Display
        {
            get
            {
                return _Display;
            }
            set
            {
                if (_Display != value)
                {
                    _Display = value;
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

        private SizeF _Spacing = new SizeF(0f, 0f);
        [Category("Appearance")]
        [Description("Determines how much spacing will be used betwen characters.")]
        [DefaultValue(typeof(SizeF), "0, 0")]
        public SizeF Spacing
        {
            get
            {
                return _Spacing;
            }
            set
            {
                if (_Spacing != value)
                {
                    _Spacing = value;
                    SetStyle(ControlStyles.UserPaint, value.Width > 0f | value.Height > 0f);
                    Invalidate();
                }
            }
        }

        private Size _SpacingGroups = new Size(1, 1);
        [Category("Appearance")]
        [Description("Determines how many characters will be grouped together, when they are being spaced.")]
        [DefaultValue(typeof(Size), "1, 1")]
        public Size SpacingGroups
        {
            get
            {
                return _SpacingGroups;
            }
            set
            {
                if (_SpacingGroups != value)
                {
                    _SpacingGroups = value;
                    Invalidate();
                }
            }
        }

        protected override int VisibleWidth
        {
            get
            {
                return Width - label.Width - label.Margin.Left - label.Margin.Right - (ScrollBars != ScrollBars.None ? 32 : 0);
            }
        }

        protected string LabelText
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
                // changes the labels right margin to stick inside the right side of the textbox
                SendMessage(Handle, 0xD3, (IntPtr)2, (IntPtr)(LabelWidth() << 16));
                OnResize(EventArgs.Empty);
                label.Left = Right - label.Width - 8;
                if (Parent?.Parent?.GetType() == typeof(HexMessageBox))
                {
                    // FIXME: Remove this ugly hardcoding, why does the textbox in the HexMessageBox
                    // not have the correct "Right" value?
                    label.Left = 500 - label.Width;
                }
                label.BackColor = Bytes.Count(@byte => @byte != 0x0) > WriteLength ? Color.Red : Color.LightGray;
            }
        }

        [Category("Function")]
        [Description("If true then the control will automatically display the current and max length of characters.")]
        [DefaultValue(false)]
        public bool MaxLengthLabel { get; set; } = false;

        [Category("Function")]
        [Description("The text which will be interpreted as a line break.")]
        [DefaultValue("<LINE BREAK>")]
        public string LineBreakText { get; set; } = "<LINE BREAK>";

        [Category("Function")]
        [Description("The text which will be interpreted as a page break.")]
        [DefaultValue("<NEW PAGE>")]
        public string PageBreakText { get; set; } = "<NEW PAGE>";

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected byte[] Bytes { get; set; } = new byte[] { };

        private int[] _PageOffsets;
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] PageOffsets
        {
            get
            {
                return _PageOffsets;
            }
            protected set
            {
                if (!ReferenceEquals(_PageOffsets, value))
                {
                    _PageOffsets = value;
                }
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageCount
        {
            get
            {
                return PageOffsets.Length;
            }
        }

        private int _CurrentPage = 0;
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                if (value != _CurrentPage)
                {
                    _CurrentPage = Math.Max(1, Math.Min(value, PageCount));
                    RedrawPage();
                }
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CanUseLastPage
        {
            get
            {
                return Conversions.ToInteger(CurrentPage > 1);
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CanUseNextPage
        {
            get
            {
                return Conversions.ToInteger(CurrentPage < PageCount);
            }
        }

        [Browsable(false)]
        [System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPaging
        {
            get
            {
                return CurrentPage != 0;
            }
            set
            {
                if (Conversions.ToInteger(value) == CurrentPage != value)
                {
                    _CurrentPage = value ? 1 : 0;
                    RedrawPage();
                }
            }
        }

        public override bool CanCopyData
        {
            get
            {
                return Text.Length > 0;
            }
        }

        #endregion

        #region  Events 

        private void Me_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading) return;

            PerformUserInput();
            DrawMaxLengthLabel();
        }

        private void Me_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Strings.AscW(e.KeyChar))
            {
                case (int)Keys.Left:
                case (int)Keys.Right:
                    {
                        e.Handled = false;
                        break;
                    }
                case (int)Keys.Delete:
                case (int)Keys.Back:
                    {
                        if (OvertypeMode == true)
                        {
                            e.Handled = true;
                        }
                        else if ((AllowEmpty == false && Input != InputType.Normal) & Text.Length <= 1)
                        {
                            Text = "0";
                            MoveCaret((HexTextBox)sender, false);
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                        PerformUserInput();
                        break;
                    }

                default:
                    {
                        e.Handled = true;
                        if (Input == InputType.Hex)
                        {
                            if (HexQuery.IsKeyCode(e.KeyChar))
                            {
                                TypeText((HexTextBox)sender, char.IsLower(e.KeyChar) ? char.ToUpper(e.KeyChar) : e.KeyChar);
                            }
                        }
                        else if (Input == InputType.Numeric)
                        {
                            if (HexQuery.IsNumericCode(e.KeyChar))
                            {
                                TypeText((HexTextBox)sender, e.KeyChar);
                            }
                        }
                        else
                        {
                            TypeText((HexTextBox)sender, e.KeyChar);
                        }

                        break;
                    }
            }
        }

        private void Me_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                Text = LoadedText;
                PerformUserInput();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                TypeTextComplete((HexTextBox)sender);
            }
            else if (OvertypeMode == true & e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                PerformUserInput();
            }
            else if ((AllowEmpty == false && Input != InputType.Normal) & Text.Length < 1)
            {
                Text = "0";
                MoveCaret((HexTextBox)sender, false);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void Me_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LoadedText))
                TypeTextComplete((HexTextBox)sender);
        }

        private void Me_UserInput(object sender, EventArgs e)
        {
            DrawMaxLengthLabel();
        }

        public event EventHandler UserInput;

        protected void PerformUserInput()
        {
            if (Display == DisplayType.Text)
            {
                Bytes = FontTable != null ? FontTable.FontTable.GetBytes(ReplaceNewLine(Text, true).ToString()) : HexConvert.StringToBytes(Text, false);
            }
            UserInput?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region  Private 

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            OnResize(EventArgs.Empty);
        }

        private object ReplaceNewLine(string text, bool reverse)
        {
            return Replace(text, LineBreakText, Environment.NewLine, reverse);
        }

        private object Replace(string text, string from, string to, bool reverse)
        {
            if (!string.IsNullOrEmpty(from))
            {
                text = text.Replace(reverse ? to : from, reverse ? from : to);
            }
            return text;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int labelWidth = LabelWidth();
            label.Left = RightToLeft == RightToLeft.Yes ? 0 : ClientSize.Width - labelWidth;
            label.Top = (int)Math.Round(ClientSize.Height / 2d - label.Height / 2d);
            label.Width = labelWidth;
            label.Height = ClientSize.Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);

            if (TextLength == 0)
                return;

            var measure = g.MeasureString("0", Font);

            float charWidth = measure.Width / 2f + 2f;
            float lineHeight = measure.Height;

            float groupX = Math.Min(SpacingGroups.Width, 1);
            float groupY = Math.Min(SpacingGroups.Height, 1);

            float charSpacing = Spacing.Width * groupX;
            float lineSpacing = Spacing.Height * groupY;

            int charsOnLine = (int)Math.Round(Math.Min(Size.Width / (charWidth * groupX + Spacing.Width), TextLength));
            int totalLines = (int)Math.Round(TextLength / (double)charsOnLine);
            charsOnLine = (int)Math.Round(Math.Floor(TextLength / (double)totalLines));

            using (Brush brush = new SolidBrush(ForeColor))
            {
                float ys = 0f;
                for (int y = 0, loopTo = totalLines - 1; y <= loopTo; y++)
                {
                    // ys += If(y Mod groupY <> 0, lineSpacing, 0)
                    float xs = 0f;
                    for (int x = 0, loopTo1 = charsOnLine - 1; x <= loopTo1; x++)
                    {
                        xs += (x + 1) % groupX == 0f ? charSpacing : 0f;
                        int i = x + y + y * (charsOnLine - 1);
                        using (var sf = TextAlign.ToStringFormat())
                        {
                            g.DrawString(Conversions.ToString(Text[i]), Font, brush, x * charWidth + xs, y * lineHeight + ys);
                        }
                    }
                }
            }
        }

        private int LabelWidth()
        {
            return TextRenderer.MeasureText(label.Text, label.Font).Width;
        }

        private void TypeText(HexTextBox param_text, char param_char)
        {
            int lv_last = param_text.SelectionStart;
            if (OvertypeMode == true)
            {
                if (param_text.SelectionStart < param_text.Text.Length)
                {
                    param_text.Text = param_text.Text.ReplaceAt(param_text.SelectionStart, Conversions.ToString(param_char));
                    param_text.SelectionStart = lv_last + 1;
                }
            }
            else if (param_text.Text.Length - param_text.SelectionLength < param_text.MaxLength)
            {
                param_text.SelectedText = Conversions.ToString(param_char);
            }
            else if (param_text.Text.Length < param_text.MaxLength)
            {
                param_text.Text += Conversions.ToString(param_char);
                param_text.SelectionStart = lv_last + 1;
            }
            PerformUserInput();
        }

        private void MoveCaret(HexTextBox param_text, bool param_forward)
        {
            if (param_forward == true)
            {
                param_text.SelectionStart = Math.Min(param_text.SelectionStart + 1, param_text.Text.Length);
            }
            else
            {
                param_text.SelectionStart = Math.Max(param_text.SelectionStart - 1, 0);
            }
        }

        private void TypeTextComplete(HexTextBox param_text)
        {
            // This is where the writing of the text to memory should go
            Save();
        }

        private void Write(int param_offset, byte[] param_bytes)
        {
            int lv_offmax = WriteLength;
            int closingTagLength = (int)Math.Round(Math.Ceiling(ClosingTag.Length / 2d));
            if (!string.IsNullOrEmpty(ClosingTag))
                lv_offmax += closingTagLength;
            byte[] buffer = new byte[lv_offmax];
            for (int index = 0, loopTo = lv_offmax - 1; index <= loopTo; index++)
            {
                if (!string.IsNullOrEmpty(ClosingTag) && index == WriteLength)
                {
                    byte[] closingTags = HexConvert.HexToBytes(ClosingTag, false);
                    for (int i = 0, loopTo1 = closingTagLength - 1; i <= loopTo1; i++)
                    {
                        buffer[index] = closingTags[i];
                        index += 1;
                    }
                }
                else
                {
                    buffer[index] = param_bytes[index];
                }
            }
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        private int GetHexOffset()
        {
            return (int)Math.Round(GetGlobalOffset() + Conversions.ToDouble(HexOffset) + GetPanelOffset() + GetListOffset());
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = Conversions.ToInteger(lv_offset + Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index) + (ctrl.PointerIgnoreHeader ? HexConvert.HexToInt(HexStorage.GlobalHexOffset) : 0));
            }
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private void DrawMaxLengthLabel(byte[] bytes = null)
        {
            if (!MaxLengthLabel)
                return;
            byte[] lv_bytes = bytes != null ? bytes : Bytes;
            Bytes = lv_bytes;
            LabelText = $"{lv_bytes.Count(@byte => @byte != 0x0)}/{WriteLength.ToString()}";
        }

        private int _OriginalOffset;
        private string ConvertText(int offset)
        {
            _OriginalOffset = offset;
            return DrawPage(0).Item1;
        }

        private void RedrawPage()
        {
            var result = DrawPage(CurrentPage);
            Text = result.Item1;
            Bytes = result.Item2;
        }

        private Tuple<string, byte[]> DrawPage(int page)
        {
            var result = GetPageData(page);

            if (page == 0 && !string.IsNullOrEmpty(PageBreakText))
            {
                DrawMaxLengthLabel(result.Item2);
            }

            return result;
        }

        private Tuple<string, byte[]> GetPageData(int page)
        {
            int offset = (page > 0 ? PageOffsets[page - 1] : 0) + _OriginalOffset;
            if (Multiline)
            {
                Debug.WriteLine("Get page: " + CurrentPage.ToString() + " from: " + offset.ToString());
            }
            if (!string.IsNullOrEmpty(OpeningTag))
            {
                var scanResult = MemoryLiterator.Scan(offset, HexConvert.HexToNullableBytes(OpeningTag, false));
                offset += scanResult.Offset - offset + scanResult.Buffer.Length;
            }
            if (ClosingTagIsEnd)
            {
                if (page == 0)
                {
                    WriteLength = MemoryLiterator.Scan(offset, HexConvert.HexToBytes(ClosingTag, false)).Offset - offset;
                }
            }
            else if (DynamicLength > 0)
            {
                if (page == 0)
                {
                    WriteLength = Conversions.ToInteger(HexConvert.BytesToNumeric(MemoryLiterator.Read(offset, DynamicLength).Buffer));
                }
                offset += DynamicLength;
            }
            else if (WriteLength == 0)
            {
                if (page == 0)
                {
                    WriteLength = MaxLength;
                }
            }
            else
            {
                LabelText = "";
            }
            byte[] lv_bytes = MemoryLiterator.Read(offset, page <= 0 ? WriteLength : (page == PageCount ? WriteLength : PageOffsets[page]) - PageOffsets[page - 1] - 2).Buffer;
            if (page == 0 && !string.IsNullOrEmpty(PageBreakText))
            {
                var pageOffsets = new List<int>() { 0 };
                int additiveOffset = offset - _OriginalOffset;
                int[] pageBreaks = FontTable != null ? FontTable.FontTable.AllEntries(PageBreakText) : (new int[] { });
                for (int i = 0, loopTo = lv_bytes.Length - 1; i <= loopTo; i++)
                {
                    if (pageBreaks.Contains(lv_bytes[i]))
                    {
                        int skip = i + 1 < lv_bytes.Length - 1 && lv_bytes[i + 1] == 0xFF ? 2 : 0;
                        pageOffsets.Add(i + additiveOffset + skip);
                    }
                }
                PageOffsets = pageOffsets.ToArray();
            }
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            string lv_text = "";
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        lv_text = FontTable != null ? FontTable.FontTable.GetString(lv_bytes) : HexConvert.BytesToString(lv_bytes);
                        lv_text = Conversions.ToString(ReplaceNewLine(lv_text, false));
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        lv_text = HexConvert.BytesToNumeric(lv_bytes);
                        if (WriteLength % 2 != 0)
                        {
                            MaxLength = (int)Math.Round(3 + (WriteLength - 1) * 2 + Math.Floor(WriteLength / 2d));
                        }
                        else
                        {
                            MaxLength = (int)Math.Round(2 + (WriteLength - 1) * 2 + Math.Floor(WriteLength / 2d));
                        }

                        break;
                    }
                case DisplayType.Hex:
                    {
                        lv_text = HexConvert.BytesToHex(lv_bytes);
                        MaxLength = WriteLength;
                        break;
                    }
            }

            return new Tuple<string, byte[]>(lv_text, lv_bytes);
        }

        private void LoadText()
        {
            IsLoading = true;
            Text = ConvertText(GetHexOffset());
            IsLoading = false;
        }

        private void SaveText(int Offset)
        {
            if (DynamicLength > 0)
            {
                Offset += DynamicLength;
            }
            bool lv_reverse = Endian == EndianType.Little_Endian;
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        Write(Offset, Bytes);
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        Write(Offset, HexConvert.NumericToBytes(Text, lv_reverse, WriteLength));
                        break;
                    }
                case DisplayType.Hex:
                    {
                        Write(Offset, HexConvert.HexToBytes(Text, lv_reverse));
                        break;
                    }
            }
        }

        #endregion

        #region  Public 

        public void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            LoadedText = "";
            if (MaxLength > 0)
                LoadText();
            LoadedText = Text;
        }

        public string GetText(int Offset)
        {
            if (MaxLength > 0)
                return ConvertText(Offset);
            return Text;
        }

        public string GetDisplay(int Offset)
        {
            if (MaxLength > 0)
                return ConvertText(Offset);
            return Text;
        }

        public void Save(int Offset = -1)
        {
            if (Offset < 0)
            {
                if ((Text ?? "") == (LoadedText ?? ""))
                    return;
                if (MaxLength > 0)
                    SaveText(GetHexOffset());
                LoadedText = Text;
            }
            else if (MaxLength > 0)
                SaveText(Offset);

            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        public string GetInternalHexOffset()
        {
            string lv_string;
            lv_string = GetHexOffset().ToString();
            return lv_string;
        }

        public override void CopyData()
        {
            Clipboard.SetData("Text", HexConvert.BytesToHex(Bytes, WriteLength));
        }

        #endregion

    }

    #endregion

    #region  HexListBox 

    public class NewDefinitionArgs : EventArgs
    {
        public dynamic Item { get; set; }
        public bool NewIteration { get; set; }
    }

    // A List Box which is capable of redirecting the offset of all other Hex-based forms
    public class HexListBox : ListBox, IHexListBox, IHexDefinitionWriter, IBasicItemCollector
    {
        private object LastIndex = 0;

        #region  Constructor 

        private void Initialize()
        {
            if (Items.Count > 0 & AlwaysSelected == true & SelectedIndex == -1)
                SelectedIndex = LastSelectedIndex < 0 ? 0 : LastSelectedIndex;
            if (!DesignMode)
            {
                CreateControls();
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            Initialize();
        }

        public HexListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            _Items = new HexListBoxItemCollection(this);
            SelectedIndexChanged += Me_SelectedIndexChanged;
            KeyDown += Me_KeyUp;
            HexDefinitionManager.OnLoad += GetDefinition;
        }

        ~HexListBox()
        {
            HexDefinitionManager.OnLoad -= GetDefinition;
        }

        #endregion

        #region  Properties 

        private HexListBoxItemCollection _Items;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new HexListBoxItemCollection Items
        {
            get
            {
                return _Items;
            }
        }

        // The original items that the user will never see.
        private ObjectCollection baseItems
        {
            get
            {
                return base.Items;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new HexListBoxItem SelectedItem
        {
            get
            {
                return (HexListBoxItem)base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new HexListBoxSelectedItemCollection SelectedItems
        {
            get
            {
                var items = new HexListBoxSelectedItemCollection();
                foreach (object item in base.SelectedItems)
                    items.Add((HexListBoxItem)item);
                return items;
            }
        }

        private bool _AlwaysSelected = true;
        [Description("If this is true then this Listbox will automatically start with an item selected")]
        [DefaultValue(true)]
        public bool AlwaysSelected
        {
            get
            {
                return _AlwaysSelected;
            }
            set
            {
                if (_AlwaysSelected != value)
                {
                    _AlwaysSelected = value;
                    // BUG: AlwaysSelected
                    // If _AlwaysSelected = True And Items.Count > 0 Then Me.SelectedIndex = 0 Else Me.SelectedIndex = -1
                    Invalidate();
                }
            }
        }

        private bool _IndexDisplay = false;
        [Description("Determines if the index number of each item should be displayed.")]
        [DefaultValue(false)]
        public bool IndexDisplay
        {
            get
            {
                return _IndexDisplay;
            }
            set
            {
                if (_IndexDisplay != value)
                {
                    _IndexDisplay = value;
                    Invalidate();
                }
            }
        }

        private bool _ShowImages = false;
        [Description("Should images be shown by default")]
        [DefaultValue(false)]
        public bool ShowImages
        {
            get
            {
                return _ShowImages;
            }
            set
            {
                if (_ShowImages != value)
                {
                    _ShowImages = value;
                    Invalidate();
                }
            }
        }

        private ContentAlignment _TextAlign = ContentAlignment.MiddleLeft;
        [Description("Determine how the text content will be aligned when drawn.")]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                if (_TextAlign != value)
                {
                    _TextAlign = value;
                    Invalidate();
                }
            }
        }

        private string _Definition = "";
        [Description("Determines the field path that will be used to bind list items data from a definition (user settings) file.")]
        [DefaultValue("")]
        public string Definition
        {
            get
            {
                return _Definition;
            }
            set
            {
                if (_Definition != value)
                {
                    _Definition = value;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BasicTextBox LabelEditBox { get; private set; }

        [Description("Allows the user to modify the name of the list items which will get persisted to a definition (user settings) file.")]
        [DefaultValue(false)]
        public bool CanEditLabel { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BasicButton ControlPlus { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BasicButton ControlMinus { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BasicButton ControlResize { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BasicButton ControlReload { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTip ControlTooltip { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasDefinition => !string.IsNullOrEmpty(Definition);

        #endregion

        #region  Events 

        public event EventHandler<EventArgs> OnItemsChanged;

        public void RaiseItemsChanged()
        {
            OnItemsChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            // Retrieve new value of selected index.
            int currentSelectedIndex = SelectedIndex;

            // For i As Integer = 0 To SelectedIndices.Count - 1
            // ' unselect items that are already disabled.
            // If disabledIndices.Contains(SelectedIndices(i)) Then
            // selectedDisabledIndices.Add(SelectedIndices(i))
            // SelectedIndices.Remove(SelectedIndices(i))
            // End If
            // Next
            // For Each index As Integer In selectedDisabledIndices
            // ' Fire DisabledItemSelected event for each 
            // ' disabled item that has been selected.
            // Dim args As New IndexEventArgs(index)
            // OnDisabledItemSelected(Me, args)
            // Next
            // if updated selected index is different than the 
            // original one then bubble up the event
            if (Operators.ConditionalCompareObjectNotEqual(LastIndex, SelectedIndex, false) && currentSelectedIndex >= 0 && Items[currentSelectedIndex].Enabled == true)
            {
                LastIndex = SelectedIndex;
                base.OnSelectedIndexChanged(e);
            }
            else
            {
                SelectedIndex = Math.Min(Conversions.ToInteger(LastIndex), Items.Count - 1);
            }
        }

        private void Me_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void Me_KeyUp(object sender, KeyEventArgs e)
        {
            100.Wait();
            switch (e.KeyCode)
            {
                case Keys.Down:
                    {
                        e.Handled = true;
                        SelectedIndex = GetNextEnabledIndex(SelectedIndex);
                        break;
                    }
                case Keys.Up:
                    {
                        e.Handled = true;
                        SelectedIndex = GetNextEnabledIndex(SelectedIndex, true);
                        break;
                    }
            }
        }

        private int GetNextEnabledIndex(int StartingIndex, bool Reverse = false)
        {
            int lv_start = Reverse == true ? 0 : Math.Min(StartingIndex + 1, Items.Count);
            int lv_end = Reverse == true ? StartingIndex - 1 : Items.Count - 1;
            for (int index = lv_start, loopTo = lv_end; index <= loopTo; index++)
            {
                if (Reverse == true)
                {
                    if (Items[lv_end - index].Enabled == true)
                        return lv_end - index;
                }
                else if (Items[index].Enabled == true)
                    return index;
            }
            return Conversions.ToInteger(LastIndex);
        }

        #endregion

        #region  Private 

        private int LastSelectedIndex = -1;

        private void CreateControls()
        {
            CreateTooltip();
            CreateDragAndDropReorder();
            CreateLabelEditBox();
            CreateControlPlus();
            CreateControlMinus();
            CreateControlResize();
            CreateControlReload();
            OnCreateControls?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs> OnCreateControls;

        private void CreateTooltip()
        {
            if (ControlTooltip != null) return;

            ControlTooltip = new ToolTip();
        }

        private void CreateDragAndDropReorder()
        {
            if (AllowDrop) return;

            AllowDrop = true;
            MouseDown += Me_DragDown;
            MouseMove += Me_DragMove;
            MouseUp += Me_DragCancel;
            DragOver += Me_DragOver;
            DragDrop += Me_DragDrop;
        }

        private bool Dragging = false;

        private void Me_DragDown(object sender, MouseEventArgs e)
        {
            if (!CanEditLabel || SelectedIndex < 0 || e.Button != MouseButtons.Left || MenuOpened) return;

            Dragging = true;
        }

        private void Me_DragMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons != MouseButtons.Left) Me_DragCancel(sender, e);
            if (!Dragging) return;

            DoDragDrop(SelectedItem, DragDropEffects.Move);
        }

        private void Me_DragCancel(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Me_DragOver(object sender, DragEventArgs e)
        {
            if (!Dragging) return;

            e.Effect = DragDropEffects.Move;
        }

        private void Me_DragDrop(object sender, DragEventArgs e)
        {
            if (!Dragging) return;

            int newIndex = IndexFromPoint(PointToClient(new Point(e.X, e.Y)));

            Move(newIndex);

            Dragging = false;
        }

        private void Move(int newIndex)
        {
            if (!HasDefinition) return;

            if (newIndex < 0) newIndex = Items.Count - 1;

            var collection = HexDefinitionManager.GetCollectionDefinition(Definition);

            var selectedIndex = SelectedIndex;
            var item = collection.ElementAt(selectedIndex);

            Remove(collection, selectedIndex);

            Insert(collection, newIndex, item);

            SetDefinition(collection);

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Index = i;
            }

            ForceRefresh(newIndex);
        }

        private void CreateLabelEditBox()
        {
            if (LabelEditBox != null) return;

            LabelEditBox = new BasicTextBox();
            LabelEditBox.Location = new Point(0, 0);
            LabelEditBox.Size = new Size(0, 0);
            LabelEditBox.Margin = new Padding(0);
            LabelEditBox.Hide();
            LabelEditBox.Text = "";
            LabelEditBox.Font = Font;
            LabelEditBox.BackColor = Color.FromKnownColor(KnownColor.Control);
            LabelEditBox.ForeColor = Color.FromKnownColor(KnownColor.ActiveCaptionText);
            LabelEditBox.BorderStyle = BorderStyle.FixedSingle;
            LabelEditBox.KeyPress += LabelEditModifyEnd;
            LabelEditBox.LostFocus += LabelEditFocusOver;
            Controls.Add(LabelEditBox);

            KeyDown += Me_KeyDown;
            KeyPress += Me_KeyPress;
            DoubleClick += Me_DoubleClick;
            MouseDown += Me_MouseDown;
            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Opening += Me_ContextMenu_Opening;
            ContextMenuStrip.Closing += Me_ContextMenu_Closing;
        }

        private void CreateControlPlus()
        {
            if (ControlPlus != null) return;

            ControlPlus = new BasicButton();
            ControlPlus.Size = new Size(32, 32);
            ControlPlus.Location = new Point(Size.Width + Location.X - 72, 
                Size.Height + Location.Y + 136 - ControlPlus.Size.Height);
            ControlPlus.Image = BasicTools.My.Resources.Resources.Add;
            ControlPlus.ImageAlign = ContentAlignment.MiddleCenter;
            ControlTooltip.SetToolTip(ControlPlus, "Add a new item at the end of the list.");

            if (!CanEditLabel) ControlPlus.Hide();

            Parent.Controls.Add(ControlPlus);
            ControlPlus.Click += Me_ControlPlus_Click;
        }

        private void Me_ControlPlus_Click(object sender, EventArgs e)
        {
            Plus(Items.Count);
        }

        private void Plus(int index)
        {
            if (!HasDefinition) return;

            var collection = HexDefinitionManager.GetCollectionDefinition(Definition);
            var type = HexDefinitionManager.GetCollectionDefinitionElementType(Definition);

            Insert(collection, index, type, true);

            SetDefinition(collection);

            ForceRefresh(index);
        }

        private void Insert(ICollection<dynamic> collection, int index, object newItem)
        {
            CollectionExtension.Insert(collection, index, newItem);
            Items.Insert(index, HexListBoxItem.FromDefinition(newItem));
        }

        private void Insert(ICollection<dynamic> collection, int index, Type type, bool first)
        {
            dynamic newItem = Activator.CreateInstance(type);
            newItem.Name = $"Index {index + 1:D3}";

            var args = new NewDefinitionArgs() { Item = newItem, NewIteration = first };
            OnNewInstance?.Invoke(this, args);
            newItem = args.Item;

            Insert(collection, index, newItem);
        }

        public event EventHandler<NewDefinitionArgs> OnNewInstance;

        private void CreateControlMinus()
        {
            if (ControlMinus != null) return;

            ControlMinus = new BasicButton();
            ControlMinus.Size = new Size(32, 32);
            ControlMinus.Location = new Point(ControlPlus.Location.X + ControlPlus.Width,
                Size.Height + Location.Y + 136 - ControlMinus.Size.Height);
            ControlMinus.Image = BasicTools.My.Resources.Resources.Remove;
            ControlMinus.ImageAlign = ContentAlignment.MiddleCenter;
            ControlTooltip.SetToolTip(ControlMinus, "Remove the item at the end of the list.");

            if (!CanEditLabel) ControlMinus.Hide();

            Parent.Controls.Add(ControlMinus);
            ControlMinus.Click += Me_ControlMinus_Click;
        }

        private void Me_ControlMinus_Click(object sender, EventArgs e)
        {
            Minus(Items.Count - 1);
        }

        private void Minus(int index)
        {
            if (!HasDefinition) return;

            var collection = HexDefinitionManager.GetCollectionDefinition(Definition);

            Remove(collection, index);

            SetDefinition(collection);

            ForceRefresh(index);
        }

        private void Remove(ICollection<dynamic> collection, int index)
        {
            collection.Remove(collection.ElementAt(index));
            Items.RemoveAt(index);
        }

        private void CreateControlResize()
        {
            if (ControlResize != null) return;

            ControlResize = new BasicButton();
            ControlResize.Size = new Size(32, 32);
            ControlResize.Location = new Point(ControlMinus.Location.X + ControlMinus.Width,
                Size.Height + Location.Y + 136 - ControlResize.Size.Height);
            ControlResize.Image = BasicTools.My.Resources.Resources.OrderedList;
            ControlResize.ImageAlign = ContentAlignment.MiddleCenter;
            ControlTooltip.SetToolTip(ControlResize, "Resizes the list to the specified amount, items will either be added or removed depending on the new size you provide.");

            if (!CanEditLabel) ControlResize.Hide();

            Parent.Controls.Add(ControlResize);
            ControlResize.Click += Me_ControlResize_Click;
        }

        private void Me_ControlResize_Click(object sender, EventArgs e)
        {
            if (!HasDefinition) return;

            var collection = HexDefinitionManager.GetCollectionDefinition(Definition);

            var initialSize = collection.Count;
            var newSize = BasicSizeForm.ShowAsTool(initialSize).Size;

            if (newSize < 0) return;

            // Could be optimized to use AddRange, RemoveRange if we care about the performance.
            if(newSize > initialSize)
            {
                var type = HexDefinitionManager.GetCollectionDefinitionElementType(Definition);
                for (int i = 0; i < newSize - initialSize; i++)
                {
                    Insert(collection, initialSize + i, type, i == 0);
                }
            }
            else if(newSize < initialSize)
            {
                for (int i = 0; i < initialSize - newSize; i++)
                {
                    Remove(collection, collection.Count - 1);
                }
            }
            else
            {
                return;
            }

            SetDefinition(collection);
        }

        private void CreateControlReload()
        {
            if (ControlReload != null) return;

            ControlReload = new BasicButton();
            ControlReload.Size = new Size(32, 32);
            ControlReload.Location = new Point(ControlResize.Location.X + ControlResize.Width,
                Size.Height + Location.Y + 136 - ControlResize.Size.Height);
            ControlReload.Image = BasicTools.My.Resources.Resources.Reload;
            ControlReload.ImageAlign = ContentAlignment.MiddleCenter;
            ControlTooltip.SetToolTip(ControlReload, "Reloads all of the items in this list, setting them back to their default values.");

            if (!CanEditLabel) ControlReload.Hide();

            Parent.Controls.Add(ControlReload);
            ControlReload.Click += Me_ControlReload_Click;
        }

        private void Me_ControlReload_Click(object sender, EventArgs e)
        {
            if (!HasDefinition) return;

            if(MessageBox.Show("All of your current settings will be lost and cannot be restored.\n\n" +
                "Are you sure you wish to reload the data in this list to the default? ", "Confirm Reload", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ReloadDefinition();
            }
        }

        private void Me_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                SelectedIndex = IndexFromPoint(e.Location);
                if (SelectedIndex != -1)
                {
                    ContextMenuStrip.Show();
                }
            }
        }

        private bool MenuOpened = false;

        private void Me_ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (MenuOpened) return;

            ContextMenuStrip.Items.Clear();
            if (CanEditLabel)
            {
                var labelItem = new ToolStripMenuItem($"{SelectedItem.ToString()}");
                labelItem.Enabled = false;
                ContextMenuStrip.Items.Add(labelItem);

                var separatorItem = new ToolStripSeparator();
                ContextMenuStrip.Items.Add(separatorItem);

                var renameItem = new ToolStripMenuItem($"Rename", null, (x, y) =>
                {
                    ShowLabelEditBox(x);
                });
                renameItem.ShortcutKeys = Keys.F2;
                ContextMenuStrip.Items.Add(renameItem);

                var insertItem = new ToolStripMenuItem($"Insert Before", null, (x, y) =>
                {
                    Plus(SelectedIndex);
                });
                insertItem.ShortcutKeys = Keys.Insert;
                insertItem.ShortcutKeyDisplayString = "INSERT";
                ContextMenuStrip.Items.Add(insertItem);

                var deleteItem = new ToolStripMenuItem($"Delete", null, (x, y) =>
                {
                    Minus(SelectedIndex);
                });
                deleteItem.ShortcutKeys = Keys.Control | Keys.Delete;
                deleteItem.ShortcutKeyDisplayString = "CTRL + DELETE";
                ContextMenuStrip.Items.Add(deleteItem);

                var reloadItem = new ToolStripMenuItem($"Reload", null, (x, y) =>
                {
                    Reload(SelectedIndex);
                });
                reloadItem.ShortcutKeys = Keys.Delete;
                reloadItem.ShortcutKeyDisplayString = "DELETE";
                ContextMenuStrip.Items.Add(reloadItem);

                var sendToTopItem = new ToolStripMenuItem($"Send to Top", null, (x, y) =>
                {
                    Move(0);
                });
                sendToTopItem.ShortcutKeys = Keys.Control | Keys.PageUp;
                sendToTopItem.ShortcutKeyDisplayString = "CTRL + PGUP";
                ContextMenuStrip.Items.Add(sendToTopItem);

                var sendToBottomItem = new ToolStripMenuItem($"Send to Bottom", null, (x, y) =>
                {
                    Move(Items.Count - 1);
                });
                sendToBottomItem.ShortcutKeys = Keys.Control | Keys.PageUp;
                sendToBottomItem.ShortcutKeyDisplayString = "CTRL + PGDN";
                ContextMenuStrip.Items.Add(sendToBottomItem);
            }

            MenuOpened = true;
        }

        private void Me_ContextMenu_Closing(object sender, CancelEventArgs e)
        {
            MenuOpened = false;
            Dragging = false;
        }

        private void Reload(int index)
        {
            var collection = HexDefinitionManager.GetCollectionDefinition(Definition);
            var templateCollection = HexDefinitionManager.GetTemplateCollectionDefinition(Definition);
            var newItem = Clone(templateCollection, index);

            Replace(collection, index, newItem);
        }

        private dynamic Clone(ICollection<dynamic> collection, int index)
        {
            return ObjectExtension.Clone(collection.ElementAt(index));
        }

        private void Replace(ICollection<dynamic> collection, int index, dynamic newItem)
        {
            Remove(collection, index);

            Insert(collection, index, newItem);

            SetDefinition(collection);

            ForceRefresh(index);
        }

        private void Me_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                ShowLabelEditBox(sender);
        }

        private void Me_DoubleClick(object sender, EventArgs e)
        {
            ShowLabelEditBox(sender);
        }

        private void Me_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void ShowLabelEditBox(object sender)
        {
            if (LabelEditBox == null || LabelEditBox.Visible || !CanEditLabel)
            {
                return;
            }
            var itemSelected = SelectedIndex;
            Rectangle r = GetItemRectangle(itemSelected);
            string itemText = Items[itemSelected].ToString();
            int deltaX = 24;
            int deltaY = 2;
            LabelEditBox.Location = new Point(r.X + deltaX, r.Y - deltaY);
            LabelEditBox.Size = new Size(r.Width - deltaX, r.Height - deltaY);
            LabelEditBox.Show();
            LabelEditBox.Text = itemText;
            LabelEditBox.Focus();
            LabelEditBox.SelectAll();
            LabelEditBox.KeyPress += new KeyPressEventHandler(LabelEditModifyEnd);
            LabelEditBox.LostFocus += new EventHandler(LabelEditFocusOver);
            LastSelectedIndex = SelectedIndex;
        }

        public void HideLabelEditBox(object sender, bool acceptChanges, int index = -1)
        {
            if(LabelEditBox == null)
            {
                return;
            }
            if (acceptChanges)
            {
                var itemSelected = index < 0 ? SelectedIndex : index;
                Items[itemSelected].Text = LabelEditBox.Text;

                var list = HexDefinitionManager.GetCollectionDefinition(Definition);
                if(list.ElementAt(SelectedIndex).Name != LabelEditBox.Text)
                {
                    list.ElementAt(SelectedIndex).Name = LabelEditBox.Text;
                    SetDefinition(list);
                }
            }
            LabelEditBox.Hide();
        }

        private void LabelEditFocusOver(object sender, EventArgs e)
        {
            HideLabelEditBox(sender, true);
        }

        private void LabelEditModifyEnd(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                HideLabelEditBox(sender, true);
                e.Handled = true;
            }
            else if (e.KeyChar == 27)
            {
                HideLabelEditBox(sender, false);
                e.Handled = true;
            }
        }

        #endregion

        #region  Public 

        public void ForceRefresh(int newIndex, int topIndex = -1)
        {
            if (topIndex >= 0) TopIndex = topIndex;
            SelectedIndex = Math.Min(newIndex, Items.Count - 1);
            base.OnSelectedIndexChanged(new EventArgs());
        }

        public void ReloadDefinition()
        {
            if (string.IsNullOrEmpty(Definition)) return;
            
            var index = SelectedIndex;
            var topIndex = TopIndex;

            HexDefinitionManager.GetPartialDefinition(Definition).Reload();

            GetDefinition();
            
            ForceRefresh(index, topIndex);
        }

        public void GetDefinition()
        {
            if (string.IsNullOrEmpty(Definition)) return;
            
            Items.Clear();
            foreach (var item in HexDefinitionManager.GetCollectionDefinition(Definition))
            {
                //Allows data to be set which is unused because the fields don't exist
                if (item.Offset == null && item.Name == null) continue;
                var listItem = HexListBoxItem.FromDefinition(item);
                Items.Add(listItem);
            }
            Initialize();
        }

        public void SetDefinition(object value)
        {
            if (!HasDefinition) return;

            HexDefinitionManager.SetDefinition(Definition, value);

            RaiseItemsChanged();
        }

        public dynamic GetSelectedDefinition()
        {
            if (!HasDefinition || SelectedIndex < 0) return null;

            var selected = HexDefinitionManager.GetCollectionDefinition(Definition).ElementAt(SelectedIndex);
            return selected;
        }

        public void SetSelectedDefinition(Func<dynamic, bool> action)
        {
            var selected = GetSelectedDefinition();

            if(selected != null)
            {
                var collection = HexDefinitionManager.GetCollectionDefinition(Definition);
                if(action?.Invoke(selected))
                {
                    SetDefinition(collection);
                    Replace(collection, SelectedIndex, selected);
                }
            }
        }

        public string CurrentOffset()
        {
            if (SelectedIndex >= 0 && HexConvert.HexToInt(Items[SelectedIndex].HexOffset) > 0)
                return Items[SelectedIndex].HexOffset;
            return "0";
        }

        public int CurrentIndex()
        {
            return SelectedIndex;
        }

        public void FormProgress()
        {

        }

        public void UpdateForm()
        {
            HexStorage.DataRetrieve(Parent, FormProgress);
        }

        #endregion

        #region  Draw 

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // MyBase.OnDrawItem(e)

            if (DesignMode && Items.Count == 0)
                return;

            if (e.Index != NoMatches)
            {
                if (e.Index >= 0 && e.Index < Items.Count)
                {
                    var item = Items[e.Index];
                    if (item != null)
                    {
                        if (ShowImages == true & item.Image != null)
                        {
                            // Draw the image
                            e.Graphics.DrawImage(item.Image, e.Bounds.X, e.Bounds.Y, ItemHeight, ItemHeight);
                        }
                    }

                    // Draw the item text
                    if (Enabled == false | item.Enabled == false)
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                        if (item != null)
                        {
                            DrawItemText(e, item, SystemBrushes.GrayText);
                        }
                    }
                    else if (SelectionMode == SelectionMode.None)
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                        if (item != null)
                        {
                            DrawItemText(e, item);
                        }
                    }
                    else if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                        e.DrawFocusRectangle();
                        if (item != null)
                        {
                            DrawItemText(e, item, SystemBrushes.HighlightText);
                        }
                    }
                    else
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                        if (item != null)
                        {
                            DrawItemText(e, item);
                        }
                    }
                }
            }
        }

        private void DrawItemText(DrawItemEventArgs e, HexListBoxItem item, Brush style = null)
        {
            string tag = "";
            if (IndexDisplay == true)
            {
                if (FormattingEnabled == true)
                {
                    tag = item.Index.ToString(FormatString) + ": ";
                }
                else
                {
                    tag = item.Index + ": ";
                }
            }

            float x = default, y = default;
            var textSize = e.Graphics.MeasureString(tag + item.Text, Font);
            float w = textSize.Width;
            float h = textSize.Height;

            var bounds = e.Bounds;
            // If we are showing images, make some room for them and adjust the bounds width.
            if (ShowImages == true)
            {
                bounds.X += ItemHeight;
                bounds.Width -= ItemHeight;
            }

            // Depending on which TextAlign is chosen, determine the x and y position of the text.
            switch (TextAlign)
            {
                case ContentAlignment.BottomCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.BottomLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.BottomRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y + bounds.Height - h;
                        break;
                    }
                case ContentAlignment.MiddleCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.MiddleLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.MiddleRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y + (bounds.Height - h) / 2f;
                        break;
                    }
                case ContentAlignment.TopCenter:
                    {
                        x = bounds.X + (bounds.Width - w) / 2f;
                        y = bounds.Y;
                        break;
                    }
                case ContentAlignment.TopLeft:
                    {
                        x = bounds.X;
                        y = bounds.Y;
                        break;
                    }
                case ContentAlignment.TopRight:
                    {
                        x = bounds.X + bounds.Width - w;
                        y = bounds.Y;
                        break;
                    }
            }

            // Finally draw the text.
            if (style == null)
            {
                e.Graphics.DrawString(tag + item.Text, Font, new SolidBrush(item.Color), x, y);
            }
            else
            {
                e.Graphics.DrawString(tag + item.Text, Font, style, x, y);
            }
        }

        #endregion

        #region  Nested classes 

        // A collection of ColorListBoxItems
        // Inherits System.Collections.iList(Of HexListBoxItem)
        public class HexListBoxItemCollection : System.Collections.ObjectModel.Collection<HexListBoxItem>
        {

            private int HiddenItems = 0;

            #region  Fields 

            // Keep a reference to the HexListBox so we can update its baseItems list
            private HexListBox _listBox;

            #endregion

            #region  Constructor 

            public HexListBoxItemCollection(HexListBox listBox)
            {
                _listBox = listBox;
            }

            #endregion

            #region  Methods 

            public HexListBoxItem Add(string text)
            {
                return Add(text, "", Color.Black, null);
            }

            public HexListBoxItem Add(string text, string hexoffset)
            {
                return Add(text, hexoffset, Color.Black, null);
            }

            public HexListBoxItem Add(string text, string hexoffset, Color color)
            {
                return Add(text, hexoffset, color, null);
            }

            public HexListBoxItem Add(string text, string hexoffset, Color color, Image img)
            {
                var item = new HexListBoxItem(text, hexoffset, color, img);
                InsertItem(Items.Count, item);
                return item;
            }

            protected override void ClearItems()
            {
                base.ClearItems();
                _listBox.baseItems.Clear();
            }

            protected override void InsertItem(int index, HexListBoxItem item)
            {
                item.Index = index + HiddenItems;
                if (_listBox.DesignMode == false & item.Visible == false)
                {
                    HiddenItems += 1;
                    return;
                }
                if (item.Text == "Index")
                    item.Text = item.Text + string.Format("{0:0#}", Items.Count);
                base.InsertItem(index, item);
                _listBox.baseItems.Insert(index, item);
                // BUG: AlwaysSelected
                // If Items.Count > 0 And _listBox.AlwaysSelected = True Then _listBox.SelectedIndex = 0
            }

            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);
                _listBox.baseItems.RemoveAt(index);
            }

            protected override void SetItem(int index, HexListBoxItem item)
            {
                base.SetItem(index, item);
                _listBox.baseItems[index] = item;
            }

            public void AddRange(IEnumerable<HexListBoxItem> items)
            {
                foreach (HexListBoxItem item in items)
                    InsertItem(Items.Count, item);
            }

            #endregion

        }

        // A collection containing the selected items
        public class HexListBoxSelectedItemCollection : System.Collections.ObjectModel.Collection<HexListBoxItem>
        {
        }

        #endregion

    }

    #endregion

    #region  HexListBoxItem 

    // An item that is added to the HexListBox
    public class HexListBoxItem
    {

        public int Index = 0;

        #region  Constructors 

        public HexListBoxItem() : this("Index", "", Color.Black, null)
        {
        }

        public HexListBoxItem(string text) : this(text, "", Color.Black, null)
        {
        }

        public HexListBoxItem(string text, string hexOffset) : this(text, hexOffset, Color.Black, null)
        {
        }

        public HexListBoxItem(string text, string hexOffset, Color color) : this(text, hexOffset, color, null)
        {
        }

        public HexListBoxItem(string text, string hexOffset, Color color, Image img)
        {
            Text = text;
            HexOffset = hexOffset;
            Color = color;
            Image = img;
        }

        public static HexListBoxItem FromDefinition(dynamic item)
        {
            var offset = item.Offset != null ? ((string)item.Offset).Replace("0x", "&H") : "&H000000";
            var listItem = new HexListBoxItem(item.Name, offset);
            listItem.Value = HexConvert.IntToHex(HexConvert.PCToSnes(listItem.HexOffset, false), 5);
            return listItem;
        }

        #endregion

        #region  Properties 

        private string _Text;
        [DefaultValue(typeof(string), null)]
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private Color _Color = Color.FromKnownColor(KnownColor.Black);
        [DefaultValue(typeof(Color), "Black")]
        public Color Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }

        private Image _Image;
        [DefaultValue(typeof(Image), null)]
        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }

        private string _HexOffset = "";
        [DefaultValue("")]
        public string HexOffset
        {
            get
            {
                return _HexOffset;
            }
            set
            {
                _HexOffset = value;
            }
        }

        private bool _Enabled = true;
        [Description("Determines whether this item is usable.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }

        private bool _Visible = true;
        [Description("Determines whether this item is visible.")]
        [DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
            }
        }

        private string _Value = "";
        [DefaultValue("")]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private StringCollection _Strings;
        [Editor(BasicConstants.StringCollectionEditor, typeof(UITypeEditor))]
        [DefaultValue(typeof(StringCollection), null)]
        public StringCollection Strings
        {
            get
            {
                return _Strings;
            }
            set
            {
                _Strings = value;
            }
        }

        #endregion

        public override string ToString()
        {
            return Text;
        }

    }

    #endregion

    #region  HexPanel 

    public class HexPanel : Panel, IHexPanel
    {

        #region  Constructor 

        public HexPanel() : base()
        {
            _HexOffset = "&H000000";
            _Pointer = "&H000000";
            _IndexOffset = "&H000000";
        }

        #endregion

        #region  Properties 

        private string _HexOffset;
        [Description("")]
        [DefaultValue("&H000000")]
        public string HexOffset
        {
            get
            {
                return _HexOffset;
            }
            set
            {
                if ((_HexOffset ?? "") != (value ?? ""))
                {
                    _HexOffset = value;
                }
            }
        }

        private string _Pointer;
        [Description("")]
        [DefaultValue("&H000000")]
        public string Pointer
        {
            get
            {
                return _Pointer;
            }
            set
            {
                if ((_Pointer ?? "") != (value ?? ""))
                {
                    _Pointer = value;
                    if (PointerLength == 0)
                    {
                        var hexIndexOffset = HexConvert.HexToInt(IndexOffset);
                        PointerLength = Conversions.ToInteger(Conversions.ToDouble(hexIndexOffset) > 0d ? hexIndexOffset : 1);
                    }
                }
            }
        }

        private int _PointerBank;
        [Description("")]
        [DefaultValue(0)]
        public int PointerBank
        {
            get
            {
                return _PointerBank;
            }
            set
            {
                if (_PointerBank != value)
                {
                    _PointerBank = value;
                }
            }
        }

        private int _PointerLength;
        [Description("")]
        [DefaultValue(0)]
        public int PointerLength
        {
            get
            {
                return _PointerLength;
            }
            set
            {
                if (_PointerLength != value)
                {
                    _PointerLength = value;
                }
            }
        }

        private bool _PointerIgnoreHeader = false;
        [Description("")]
        [DefaultValue(false)]
        public bool PointerIgnoreHeader
        {
            get
            {
                return _PointerIgnoreHeader;
            }
            set
            {
                if (_PointerIgnoreHeader != value)
                {
                    _PointerIgnoreHeader = value;
                }
            }
        }

        private string _IndexOffset;
        [Description("This will automatically add to the offset of all associated controls when the selected index changes for a HexListBox")]
        [DefaultValue("&H000000")]
        public string IndexOffset
        {
            get
            {
                return _IndexOffset;
            }
            set
            {
                if ((_IndexOffset ?? "") != (value ?? ""))
                {
                    _IndexOffset = value;
                }
            }
        }

        private BitOffset _IndexBitOffset;
        [Description("")]
        [DefaultValue(BitOffset.Zero)]
        public BitOffset IndexBitOffset
        {
            get
            {
                return _IndexBitOffset;
            }
            set
            {
                if (_IndexBitOffset != value)
                {
                    _IndexBitOffset = value;
                }
            }
        }

        private string _Definition = "";
        [Description("Determines the field path that will be used to bind panel offset data from a definition (user settings) file.")]
        [DefaultValue("")]
        public string Definition
        {
            get
            {
                return _Definition;
            }
            set
            {
                if (_Definition != value)
                {
                    _Definition = value;
                }
            }
        }

        private HexAddressFormatType _DefinitionOffsetFormat = HexAddressFormatType.Raw;
        [Description("Determines the format to convert offsets from the definition (user settings) file.")]
        [DefaultValue(HexAddressFormatType.Raw)]
        public HexAddressFormatType DefinitionOffsetFormat
        {
            get
            {
                return _DefinitionOffsetFormat;
            }
            set
            {
                if (_DefinitionOffsetFormat != value)
                {
                    _DefinitionOffsetFormat = value;
                }
            }
        }

        #endregion

        #region  Public 

        public int CurrentOffset(int param_index)
        {
            if(!string.IsNullOrEmpty(Definition))
            {
                var definition = HexDefinitionManager.GetDefinition(Definition);
                var hexOffset = HexConvert.GetOffset((string)definition.Table.Offset, DefinitionOffsetFormat, param_index);
                return Conversions.ToInteger(hexOffset);
            }

            if (Conversions.ToDouble(Pointer) == 0d)
                return 0;
            // The(SnesToPC) is hard coded at this time
            int pointerOffset = Conversions.ToInteger(Operators.AddObject(Conversions.ToInteger(Pointer) + param_index * Conversions.ToDouble(IndexOffset) + Math.Floor(param_index * (int)IndexBitOffset / 8d), PointerIgnoreHeader ? (object)0 : HexStorage.GlobalHexOffset));
            byte[] lv_bytes = MemoryLiterator.Read(pointerOffset, PointerLength).Buffer;
            if(PointerBank >= 0)
            {
                Array.Resize(ref lv_bytes, lv_bytes.Length + 1);
                lv_bytes[lv_bytes.Length - 1] = (byte)PointerBank;
            }
            Array.Reverse(lv_bytes);
            return Conversions.ToInteger(Operators.SubtractObject(HexConvert.SnesToPC(HexConvert.BytesToHex(lv_bytes), true), PointerIgnoreHeader ? HexStorage.GlobalHexOffset : (object)0));
        }

        public int CurrentBitOffset(int param_index)
        {
            if (!string.IsNullOrEmpty(Definition))
            {
                var definition = HexDefinitionManager.GetDefinition(Definition);
                return (byte)definition.Table.Bit;
            }

            return param_index * (int)IndexBitOffset % 8;
        }

        #endregion

    }

    #endregion

    #region  HexComboBox 

    public class HexComboBox : BasicComboBox, IHexControl, IHexDefinition
    {
        public string Value = "";
        private string LoadedValue = "";
        private bool IgnoreListBox = false;
        private int Index = 0;
        private HexListBox ListBox = null;

        #region  Constructor 

        public HexComboBox() : base()
        {
            SelectedIndexChanged += Me_SelectedIndexChanged;
            HexDefinitionManager.OnLoad += GetDefinition;
            HexDefinitionManager.OnChange += GetDefinition;
        }

        ~HexComboBox()
        {
            HexDefinitionManager.OnLoad -= GetDefinition;
            HexDefinitionManager.OnChange -= GetDefinition;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
        }

        #endregion

        #region  Properties 

        private string _HexOffset = "&H000000";
        [DefaultValue("&H000000")]
        public string HexOffset
        {
            get
            {
                return _HexOffset;
            }
            set
            {
                if ((_HexOffset ?? "") != (value ?? ""))
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

        private DisplayType _Display = DisplayType.Text;
        [Category("Function")]
        [Description("Determines what format will be used for the data when used by the end user.")]
        [DefaultValue(DisplayType.Text)]
        public DisplayType Display
        {
            get
            {
                return _Display;
            }
            set
            {
                if (_Display != value)
                {
                    _Display = value;
                }
            }
        }

        [Category("Appearance")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsNull
        {
            get => Value == HexConvert.HexToInt(NullValue).ToString();
        }

        private string _Definition = "";
        [Description("Determines the field path that will be used to bind combobox items data from a definition (user settings) file. You can use a ',' comma to select mutliple sources. ")]
        [DefaultValue("")]
        public string Definition
        {
            get
            {
                return _Definition;
            }
            set
            {
                if (_Definition != value)
                {
                    _Definition = value;
                }
            }
        }

        private HexAddressFormatType _DefinitionOffsetFormat = HexAddressFormatType.Raw;
        [Description("Determines the format to convert offsets from the definition (user settings) file.")]
        [DefaultValue(HexAddressFormatType.Raw)]
        public HexAddressFormatType DefinitionOffsetFormat
        {
            get
            {
                return _DefinitionOffsetFormat;
            }
            set
            {
                if (_DefinitionOffsetFormat != value)
                {
                    _DefinitionOffsetFormat = value;
                }
            }
        }

        #endregion

        #region  Events 

        private void Me_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex >= 0)
            {
                var NewItem = Items[SelectedIndex];
                if (NewItem != null)
                {
                    Value = NewItem.Value;
                }
                if (!string.IsNullOrEmpty(LoadedValue))
                    Save();
            }
        }

        #endregion

        #region  Private 

        private int GetHexOffset()
        {
            return (int)Math.Round(GetGlobalOffset() + Conversions.ToDouble(HexOffset) + GetPanelOffset() + GetListOffset());
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private string ConvertText(int Offset)
        {
            byte[] lv_bytes = MemoryLiterator.Read(Offset, MaxLength).Buffer;
            string lv_text = "";
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        lv_text = HexConvert.BytesToString(lv_bytes);
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        lv_text = HexConvert.BytesToNumeric(lv_bytes);
                        break;
                    }
                case DisplayType.Hex:
                    {
                        lv_text = HexConvert.BytesToHex(lv_bytes);
                        break;
                    }
            }
            return lv_text;
        }

        private void LoadText()
        {
            Value = ConvertText(GetHexOffset());
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        SelectedIndex = Items.GetValue(Value);
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        SelectedIndex = Items.GetValue(Value);
                        break;
                    }
                case DisplayType.Hex:
                    {
                        SelectedIndex = Items.GetValue(HexConvert.HexToIntRaw(Value).ToString());
                        break;
                    }
            }
        }

        private void SaveText(int Offset)
        {
            bool lv_reverse = Endian == EndianType.Little_Endian;
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        Write(Offset, HexConvert.StringToBytes(Value, lv_reverse), MaxLength);
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        Write(Offset, HexConvert.NumericToBytes(Value, lv_reverse, MaxLength), MaxLength);
                        break;
                    }
                case DisplayType.Hex:
                    {
                        Write(Offset, HexConvert.NumericToBytes(Value, lv_reverse, MaxLength), MaxLength);
                        break;
                    }
                    // Write(Offset, Convert.HexToBytes(Value, lv_reverse), MaxLength)
            }
            // MSScript.ExecuteCode(Items(SelectedIndex).Strings, Index)
        }

        private void Write(int param_offset, byte[] param_bytes, int param_length)
        {
            byte[] buffer = new byte[param_length];
            for (int index = 0, loopTo = param_length - 1; index <= loopTo; index++)
                buffer[index] = param_bytes[index];
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        #endregion

        #region  Public 

        public void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            RestockItems(true);
            LoadedValue = "";
            if (MaxLength > 0)
                LoadText();
            LoadedValue = Value;
        }

        public string GetText(int Offset)
        {
            if (MaxLength > 0)
            {
                RestockItems();
                return ConvertText(Offset);
            }
            return Text;
        }

        public string GetDisplay(int Offset)
        {
            if (MaxLength > 0)
            {
                RestockItems();
                if (ValueDisplay)
                {
                    string HexValue = ConvertText(Offset);
                    if (Items.Count > 0)
                    {
                        var SelectedItem = Items.FirstOrDefault(Item => Operators.ConditionalCompareObjectEqual(GetFormattedText(Item.Value), HexValue, false));
                        return Conversions.ToString(GetValueText(SelectedItem));
                    }
                }
                return ConvertText(Offset);
            }
            return Text;
        }

        public void Save(int Offset = -1)
        {
            if (Offset < 0)
            {
                if ((Value ?? "") == (LoadedValue ?? ""))
                    return;
                if (MaxLength > 0)
                    SaveText(GetHexOffset());
                LoadedValue = Value;
            }
            else if (MaxLength > 0)
                SaveText(Offset);

            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        public override void RestockItems(bool Clear = false)
        {
            if (!string.IsNullOrEmpty(Definition))
            {
                if (!Clear) return;

                Items.Clear();
                var i = 0;
                foreach (var item in HexDefinitionManager.GetCollectionDefinition(Definition))
                {
                    string hexOffset = "";
                    if(item.Offset != null)
                    {
                        hexOffset = HexConvert.GetOffset((string)item.Offset, DefinitionOffsetFormat, i);
                    }
                    else
                    {
                        hexOffset = HexConvert.IntToHex(i, 5);
                    }

                    var listItem = new BasicComboBoxItem(item.Name, hexOffset);
                    Items.Add(listItem);
                    i++;
                }
                if (!string.IsNullOrEmpty(NullValue))
                {
                    var extraItem = new BasicComboBoxItem("Null", Conversions.ToInteger(NullValue).ToString());
                    Items.Add(extraItem);
                }

                SelectedItem = LoadedValue;
            }
            else
            {
                base.RestockItems(Clear);
            }
        }

        public void GetDefinition()
        {
            if(!string.IsNullOrEmpty(Definition) && Items.Count == 0)
            {
                RestockItems(true);
            }
        }

        public void GetDefinition(string fieldPath)
        {
            if (Definition == fieldPath)
            {
                RestockItems(true);
            }
        }

        #endregion

    }

    #endregion

    #region  HexTableViewCollectionEditor 

    [DisplayName("Hex CheckBox")]
    public class HexCheckBoxItemControl : EditableListViewItemControl<HexCheckBox>
    {
    }

    [DisplayName("Hex TextBox")]
    public class HexTextBoxItemControl : EditableListViewItemControl<HexTextBox>
    {
    }

    [DisplayName("Hex NumericBox")]
    public class HexNumericBoxItemControl : EditableListViewItemControl<HexNumericBox>
    {
    }

    [DisplayName("Hex ComboBox")]
    public class HexComboBoxItemControl : EditableListViewItemControl<HexComboBox>
    {
    }

    public class HexTableViewCollectionEditor : BasicTableViewCollectionEditor
    {

        public override Type[] Types
        {
            get
            {
                return new Type[] { EmptyType, new DisplayTypeDelegator(typeof(HexCheckBoxItemControl)), new DisplayTypeDelegator(typeof(HexTextBoxItemControl)), new DisplayTypeDelegator(typeof(HexNumericBoxItemControl)), new DisplayTypeDelegator(typeof(HexComboBoxItemControl)) };
            }
        }

    }

    #endregion

    #region  HexTableView 

    public class HexTableView : BasicTableView, IHexTable
    {

        private HexListBox ListBox = null;
        private bool UsesMemory = false;

        #region Constructor 

        public HexTableView() : base()
        {
            Enabled = false;
            SystemFormEvent.OnLifeCycleInitialize += (_, __) => Me_Activate();
            SubItemBeginEditing += Me_BeginEditing;
            SubItemEndEditing += Me_EndEditing;
            Editors = new EditableListViewCollection(this);
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
        }

        #endregion

        #region  Properties 

        [Category("Behavior")]
        [Editor(typeof(HexTableViewCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Contains a collection of editors for each column.")]
        public override EditableListViewCollection Editors { get; set; }

        private string _HexOffsetBase = "&H000000";
        [Description("The start of the Table Bank.")]
        [DefaultValue("&H000000")]
        public string HexOffsetBase
        {
            get
            {
                return _HexOffsetBase;
            }
            set
            {
                if ((_HexOffsetBase ?? "") != (value ?? ""))
                {
                    _HexOffsetBase = value;
                }
            }
        }

        private OffsetType _HexOffsetBaseType = OffsetType.Constant;
        [Description("")]
        [DefaultValue(OffsetType.Constant)]
        public OffsetType HexOffsetBaseType
        {
            get
            {
                return _HexOffsetBaseType;
            }
            set
            {
                if (_HexOffsetBaseType != value)
                {
                    _HexOffsetBaseType = value;
                    HexOffsetBasePointer = value == OffsetType.Pointer | value == OffsetType.Indexed ? Math.Max(HexOffsetBasePointer, 1) : 0;
                }
            }
        }

        private int _HexOffsetBasePointer = 0;
        [Description("")]
        [DefaultValue(0)]
        public int HexOffsetBasePointer
        {
            get
            {
                return _HexOffsetBasePointer;
            }
            set
            {
                if (_HexOffsetBasePointer != value)
                {
                    if (HexOffsetBaseType == OffsetType.Pointer | HexOffsetBaseType == OffsetType.Indexed)
                        value = Math.Max(value, 1);
                    else
                        value = 0;
                    _HexOffsetBasePointer = value;
                }
            }
        }

        private string _HexOffsetLength = "&H000000";
        [Description("")]
        [DefaultValue("&H000000")]
        public string HexOffsetLength
        {
            get
            {
                return _HexOffsetLength;
            }
            set
            {
                if ((_HexOffsetLength ?? "") != (value ?? ""))
                {
                    _HexOffsetLength = value;
                }
            }
        }

        private OffsetType _HexOffsetLengthType = OffsetType.Constant;
        [Description("")]
        [DefaultValue(OffsetType.Constant)]
        public OffsetType HexOffsetLengthType
        {
            get
            {
                return _HexOffsetLengthType;
            }
            set
            {
                if (_HexOffsetLengthType != value)
                {
                    _HexOffsetLengthType = value;
                    HexOffsetLengthPointer = value == OffsetType.Pointer | value == OffsetType.Indexed ? Math.Max(HexOffsetLengthPointer, 1) : 0;
                }
            }
        }

        private int _HexOffsetLengthPointer = 0;
        [Description("")]
        [DefaultValue(0)]
        public int HexOffsetLengthPointer
        {
            get
            {
                return _HexOffsetLengthPointer;
            }
            set
            {
                if (_HexOffsetLengthPointer != value)
                {
                    if (HexOffsetLengthType == OffsetType.Pointer | HexOffsetLengthType == OffsetType.Indexed)
                        value = Math.Max(value, 1);
                    else
                        value = 0;
                    _HexOffsetLengthPointer = value;
                }
            }
        }

        private string _HexOffsetCount = "&H000000";
        [Description("")]
        [DefaultValue("&H000000")]
        public string HexOffsetCount
        {
            get
            {
                return _HexOffsetCount;
            }
            set
            {
                if ((_HexOffsetCount ?? "") != (value ?? ""))
                {
                    _HexOffsetCount = value;
                }
            }
        }

        private OffsetType _HexOffsetCountType = OffsetType.Constant;
        [Description("")]
        [DefaultValue(OffsetType.Constant)]
        public OffsetType HexOffsetCountType
        {
            get
            {
                return _HexOffsetCountType;
            }
            set
            {
                if (_HexOffsetCountType != value)
                {
                    _HexOffsetCountType = value;
                    HexOffsetCountPointer = value == OffsetType.Pointer | value == OffsetType.Indexed ? Math.Max(HexOffsetCountPointer, 1) : 0;
                }
            }
        }

        private int _HexOffsetCountPointer = 0;
        [Description("")]
        [DefaultValue(0)]
        public int HexOffsetCountPointer
        {
            get
            {
                return _HexOffsetCountPointer;
            }
            set
            {
                if (_HexOffsetCountPointer != value)
                {
                    if (HexOffsetCountType == OffsetType.Pointer | HexOffsetCountType == OffsetType.Indexed)
                        value = Math.Max(value, 1);
                    else
                        value = 0;
                    _HexOffsetCountPointer = value;
                }
            }
        }

        private TableType _TempTableType = TableType.None;
        [Description("")]
        [DefaultValue(TableType.None)]
        public TableType TempTableType
        {
            get
            {
                return _TempTableType;
            }
            set
            {
                if (_TempTableType != value)
                {
                    _TempTableType = value;
                }
            }
        }

        public enum TableType
        {
            None,
            Stat,
            Skill,
            Map
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int Index
        {
            get
            {
                return SelectedIndices.Count > 0 ? SelectedIndices[0] : -1;
            }
            set
            {
                if (Index == -1 | (value != SelectedIndices[0] && value < Items.Count - 1))
                {
                    SelectedIndices.Clear();
                    if (value >= 0)
                    {
                        SelectedIndices.Add(value);
                        GoToRow(value);
                    }
                }
            }
        }

        #endregion

        #region  Events 

        private void Me_Activate()
        {
            Enabled = true;
        }

        private void Me_BeginEditing(object sender, SubItemEventArgs e)
        {
            var Control = e.Control;
            if (Control is HexNumericBox)
            {
                HexNumericBox HexNumericBox = (HexNumericBox)Control;
                int Row = e.Item.Index == Items.Count - 1 ? -1 : e.Item.Index;
                if (HexNumericBox != null && HexNumericBox.Ranged && !HexNumericBox.ExcludeRanged.Contains(Row))
                {
                    var Minimum = Items[e.Item.Index - 1].SubItems[e.Column].Tag;
                    HexNumericBox.Minimum = Conversions.ToInteger(Minimum);
                }
            }
        }

        private void Me_EndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (e.Changed == true & e.Cancel == false)
            {
                if (e.Control is HexNumericBox)
                {
                    HexNumericBox HexNumericBox = (HexNumericBox)e.Control;
                    decimal NewValue = HexNumericBox.Value - HexNumericBox.Minimum;
                    HexNumericBox.Minimum = 0m;
                    HexNumericBox.Value = NewValue;
                }
                SaveRow(e.Control, e.Item.Index, e.Column);
            }
        }

        #endregion

        #region  Public 

        public void Load()
        {
            if (UsesMemory == false)
                UsesMemory = true;
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            Items.Clear();
            if (IsDisabledTable())
            {
                TemporarilyDisabled = true;
                return;
            }
            TemporarilyDisabled = false;
            int MaxRows = GetCount();
            string[] RunningTotals = new string[Editors.Count];
            for (int Row = 1, loopTo = MaxRows; Row <= loopTo; Row++)
            {
                var Item = new ListViewItem();
                string Display = "";
                string Text = "";
                for (int Column = 0, loopTo1 = Editors.Count - 1; Column <= loopTo1; Column++)
                {
                    if (Editors[Column].Editor == null)
                    {
                        if (Column == 0)
                            Item.Text = Row.ToString();
                        continue;
                    }
                    var Editor = Editors[Column].Editor;
                    Text = GetText(Editor, Row - 1);
                    bool CustomDrawn = false;
                    if (Editor is HexNumericBox)
                    {
                        HexNumericBox HexNumericBox = (HexNumericBox)Editor;
                        if (HexNumericBox != null && HexNumericBox.Ranged == true && Row > 1)
                        {
                            int[] Excluded = new int[HexNumericBox.ExcludeRanged.Length];
                            if (Column == 1)
                            {
                                for (int CurrentIndex = 0, loopTo2 = Excluded.Length - 1; CurrentIndex <= loopTo2; CurrentIndex++)
                                {
                                    int ExcludedIndex = HexNumericBox.ExcludeRanged[CurrentIndex];
                                    Excluded[CurrentIndex] = ExcludedIndex >= 0 | Conversions.ToInteger(Text) == 0 ? ExcludedIndex : MaxRows;
                                }
                            }
                            if (Row == 2)
                                RunningTotals[Column] = GetDisplay(Editor, 0);
                            RunningTotals[Column] = (!Excluded.Contains(Row) && !IsSetRow(Row - 1) ? Conversions.ToInteger(RunningTotals[Column]) + Conversions.ToInteger(Text) : Conversions.ToInteger(Text)).ToString();

                            Display = HexNumericBox.FormatText(RunningTotals[Column]);
                            Text = RunningTotals[Column];
                            CustomDrawn = true;
                        }
                    }
                    if (!CustomDrawn)
                    {
                        Display = GetDisplay(Editor, Row - 1);
                    }
                    if (Column == 0)
                    {
                        Item.Text = Display;
                        Item.Tag = Text;
                    }
                    else
                    {
                        var SubItem = new ListViewItem.ListViewSubItem();
                        SubItem.Text = Display;
                        SubItem.Tag = Text;
                        Item.SubItems.Add(SubItem);
                    }
                }
                Items.Add(Item);
            }
            ScrollToIndex();
        }

        public void Save(int Offset = -1)
        {
            for (int Row = 1, loopTo = GetCount(); Row <= loopTo; Row++)
            {
                for (int Column = 0, loopTo1 = Editors.Count - 1; Column <= loopTo1; Column++)
                {
                    IHexEditor Editor = (IHexEditor)Editors[Column].Editor;
                    if (Editor == null)
                    {
                        continue;
                    }
                    SaveRow(Editor, Row, Column);
                }
            }
        }

        public void Reload(int Row = -1)
        {
            int Index = Row >= 0 & Row <= Items.Count - 1 ? Row : this.Index;
            Load();
            this.Index = Index;
        }

        #endregion

        #region  Private 

        private int GetIndex()
        {
            int lv_index = 0;
            if (ListBox != null)
                lv_index += ListBox.CurrentIndex();
            return lv_index;
        }

        private string GetDisplay(object sender, int Row)
        {
            if (!(sender is IHexEditor))
                return "";
            dynamic target = sender;
            int lv_offset = GetOffsetBase(Conversions.ToInteger(Operators.AddObject(target.HexOffset, Conversions.ToDouble(HexOffsetLength) * (double)Row)));
            return Conversions.ToString(target.GetDisplay(lv_offset));
        }

        private string GetText(object sender, int Row)
        {
            if (!(sender is IHexEditor))
                return "";
            dynamic target = sender;
            int lv_offset = GetOffsetBase(Conversions.ToInteger(Operators.AddObject(target.HexOffset, Conversions.ToDouble(HexOffsetLength) * (double)Row)));
            return Conversions.ToString(target.GetText(lv_offset));
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetListOffset()
        {
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private bool IsSetRow(int Row)
        {
            if (TempTableType == TableType.Stat)
            {
                bool[] Additive = new[] { false, false, false, true, true, true, true, false, false, false, false, false, false };
                return Additive[GetIndex()] ? GetStartingRow() >= Row : GetStartingRow() == Row;
            }
            return false;
        }

        private int GetStartingRow()
        {
            if (TempTableType == TableType.Stat)
            {
                int[] StartingRows = new[] { 1, 5, 1, 8, 8, 8, 8, 25, 25, 15, 15, 1, 1 };
                int index = GetIndex();
                return index >= 0 ? StartingRows[index] - 1 : 1;
            }
            return 0;
        }

        private bool IsDisabledTable()
        {
            if (TempTableType == TableType.Stat)
            {
                bool[] Disabled = new[] { false, false, false, false, false, false, false, false, true, true, true, false, true };
                int index = GetIndex();
                return index >= 0 ? Disabled[index] : true;
            }
            return false;
        }

        private void ScrollToIndex()
        {
            int lv_value = GetStartingRow();
            GoToRow(lv_value);
        }

        private int GetCount()
        {
            int lv_value = 0;
            int[] lv_offset = Array.Empty<int>();
            if (TempTableType == TableType.Stat)
            {
                lv_offset = new[] { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
                lv_value = lv_offset[GetIndex()];
            }
            return Math.Min(Math.Max(GetOffsetCount() + lv_value, 1), 1000);
        }

        private int GetOffsetBase(int HexOffset)
        {
            return GetHexOffset((int)Math.Round(HexOffset + Conversions.ToDouble(HexOffsetBase)), HexOffsetBaseType, HexOffsetBasePointer);
        }

        private int GetOffsetCount()
        {
            return GetHexOffset(Conversions.ToInteger(HexOffsetCount), HexOffsetCountType, HexOffsetCountPointer);
        }

        private int GetOffsetLength()
        {
            return GetHexOffset(Conversions.ToInteger(HexOffsetLength), HexOffsetLengthType, HexOffsetLengthPointer);
        }

        private int GetHexOffset(int HexOffset, OffsetType Type, int PointerLength)
        {
            int lv_value = HexOffset;
            int lv_offset = 0;
            if (Type == OffsetType.Relative | Type == OffsetType.Indexed)
            {
                lv_offset += GetIndex();
            }
            else if (Type == OffsetType.Temporary)
            {
                lv_offset += GetListOffset();
            }
            if (!(Type == OffsetType.Value))
            {
                lv_value += GetGlobalOffset() + GetPanelOffset() + lv_offset;
            }
            if (Type == OffsetType.Pointer | Type == OffsetType.Indexed)
            {
                byte[] lv_bytes = MemoryLiterator.Read(lv_value, PointerLength).Buffer;
                lv_value = Conversions.ToInteger(HexConvert.BytesToNumeric(lv_bytes));
            }
            return lv_value;
        }

        private void SaveRow(object sender, int Row, int Column, int Offset = 0)
        {
            if (UsesMemory == false)
                return;
            if (!(sender is IHexWriter))
                return;
            dynamic target = sender;
            int lv_offset = GetOffsetBase(Conversions.ToInteger(Operators.AddObject(Operators.AddObject(Offset, target.HexOffset), Conversions.ToDouble(HexOffsetLength) * Row)));
            var Name = target.Name;
            target.Name = "";
            target.Save(lv_offset);
            target.Name = Name;
            Reload(Row);
        }

        #endregion

    }

    #endregion

    #region  HexColorSwatch 

    public class HexColorSwatch : BasicColorSwatch, IHexControl
    {

        private int LoadedValue = 0;
        private bool IgnoreListBox = false;
        private HexListBox ListBox = null;

        #region  Constructor 

        public HexColorSwatch()
        {
            Click += Me_Click;

        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
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
                if ((_HexOffset ?? "") != (value ?? ""))
                {
                    _HexOffset = value;
                }
            }
        }

        private int _MaxLength = 1;
        [Category("Behavior")]
        [Description("The length of the value loaded in this control.")]
        [DefaultValue(1)]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                if (MaxLength != value)
                {
                    _MaxLength = value;
                }
            }
        }

        private int _Value = 1;
        [Category("Appearance")]
        [Description("The value of the control.")]
        [DefaultValue(1)]
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                }
            }
        }

        private Component _Editor = null;
        [Category("Appearance")]
        [Description("The editor of the control.")]
        [DefaultValue(default(string))]
        public Component Editor
        {
            get
            {
                return _Editor;
            }
            set
            {
                _Editor = value;
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

        #region  Events 

        private void Me_Click(object sender, EventArgs e)
        {
            dynamic lv_dialog = Editor;
            lv_dialog.OldColor = SwatchColor;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(lv_dialog.ShowDialog(FindForm()), DialogResult.OK, false)))
            {
                SwatchColor = (Color)lv_dialog.NewColor;
                UpdateValue();
                Save();
                Load();
            }

        }

        #endregion

        #region  Private 

        private int GetHexOffset()
        {
            return (int)Math.Round(GetGlobalOffset() + Conversions.ToDouble(HexOffset) + GetPanelOffset() + GetListOffset());
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private int ConvertValue(int Offset)
        {
            byte[] lv_bytes = MemoryLiterator.Read(Offset, MaxLength).Buffer;
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            int lv_value = Conversions.ToInteger(HexConvert.BytesToNumeric(lv_bytes));
            return lv_value;
        }

        private void ExtractValue()
        {
            Value = ConvertValue(GetHexOffset());
            UpdatePalette();
        }

        private void UpdatePalette()
        {
            byte r, g, b;
            r = (byte)Math.Round(Math.Truncate((decimal)(Value % 32)) * 8m);
            g = (byte)Math.Round(Math.Truncate(Value / 32d % 32d) * 8d);
            b = (byte)Math.Round(Math.Truncate(Value / 1024d % 32d) * 8d);
            r = (byte)Math.Round(r + Math.Truncate(r / 32d));
            g = (byte)Math.Round(g + Math.Truncate(g / 32d));
            b = (byte)Math.Round(b + Math.Truncate(b / 32d));
            SwatchColor = Color.FromArgb(255, r, g, b);
        }

        private void UpdateValue()
        {
            byte r, g, b;
            r = (byte)Math.Round(Math.Truncate(SwatchColor.R / 8d));
            g = (byte)Math.Round(Math.Truncate(SwatchColor.G / 8d));
            b = (byte)Math.Round(Math.Truncate(SwatchColor.B / 8d));
            Value = b * 1024 + g * 32 + r;
        }

        private void StashValue(int Offset)
        {
            bool lv_reverse = Endian == EndianType.Little_Endian;
            Write(Offset, HexConvert.NumericToBytes(Value.ToString(), lv_reverse, MaxLength), MaxLength);
        }

        private void Write(int param_offset, byte[] param_bytes, int param_length)
        {
            byte[] buffer = new byte[param_length];
            for (int index = 0, loopTo = param_length - 1; index <= loopTo; index++)
                buffer[index] = param_bytes[index];
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        #endregion

        #region  Public 

        public new void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            LoadedValue = 0;
            if (MaxLength > 0)
                ExtractValue();
            LoadedValue = Value;
        }

        // Public Function GetValue(Offset As Integer) As String
        // Return ConvertValue(Offset)
        // End Function

        public void Save(int Offset = -1)
        {
            if (Offset < 0)
            {
                if (MaxLength > 0)
                    StashValue(GetHexOffset());
                LoadedValue = Value;
            }
            else if (MaxLength > 0)
                StashValue(Offset);

            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        public void SetValue(Color color)
        {
            Value = To15BitInt(From15Bit(color));
            if(Value != LoadedValue)
            {
                UpdatePalette();
                Save();
            }
        }

        public static Color From15Bit(Color color)
        {
            var r = (byte)Math.Round(Math.Truncate(color.R / 8d));
            var g = (byte)Math.Round(Math.Truncate(color.G / 8d));
            var b = (byte)Math.Round(Math.Truncate(color.B / 8d));
            return Color.FromArgb(0xFF, r, g, b);
        }

        public static Color From15BitInt(ushort color)
        {
            var r = (byte)Math.Round(Math.Truncate((decimal)(color % 32d)) * 8m);
            var g = (byte)Math.Round(Math.Truncate(color / 32d % 32d) * 8d);
            var b = (byte)Math.Round(Math.Truncate(color / 1024d % 32d) * 8d);
            return To15Bit(Color.FromArgb(0xFF, r, g, b));
        }

        public static Color To15Bit(Color color)
        {
            var r = (byte)Math.Round(color.R + Math.Truncate(color.R / 32d));
            var g = (byte)Math.Round(color.G + Math.Truncate(color.G / 32d));
            var b = (byte)Math.Round(color.B + Math.Truncate(color.B / 32d));
            return Color.FromArgb(0xFF, r, g, b);
        }

        public static ushort To15BitInt(Color color)
        {
            return (ushort) (color.B * 1024 + color.G * 32 + color.R);
        }

        #endregion

    }

    #endregion

    #region  HexCheckBox 

    public class HexCheckBox : CheckBox, IHexEditor
    {

        private bool LoadedValue = false;
        private bool IgnoreListBox = false;
        private HexListBox ListBox = null;

        #region  Constructor 

        public HexCheckBox()
        {
            Click += Me_Clicked;

        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
        }

        #endregion

        #region  Properties 

        private HexOffsetType[] _HexOffsets;
        [Category("Function")]
        [Description("Appends a hex value to be associated with this box")]
        public HexOffsetType[] HexOffsets
        {
            get
            {
                return _HexOffsets;
            }
            set
            {
                if (!ReferenceEquals(_HexOffsets, value))
                {
                    _HexOffsets = value;
                }
            }
        }

        private bool _Value = false;
        [Category("Appearance")]
        [Description("The value of the control.")]
        [DefaultValue(false)]
        public bool Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                }
            }
        }

        private Component _Editor = null;
        [Category("Appearance")]
        [Description("The value of the control.")]
        [DefaultValue(default(string))]
        public Component Editor
        {
            get
            {
                return _Editor;
            }
            set
            {
                _Editor = value;
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

        #region  Events 

        private void Me_Clicked(object sender, EventArgs e)
        {
            UpdateValue();
            Save();
        }

        #endregion

        #region  Private 

        private int GetHexOffset(int Index)
        {
            return (int)Math.Round(Math.Max(GetGlobalOffset() + Conversions.ToDouble(HexOffsets[Index].HexOffset) + GetPanelOffset() + GetListOffset(), 0d));
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private bool ConvertValue(int Offset, int Index)
        {
            byte[] lv_bytes = MemoryLiterator.Read(Offset, HexOffsets[Index].MaxLength).Buffer;
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            int lv_value = Conversions.ToInteger(HexConvert.BytesToNumeric(lv_bytes));
            return lv_value == Conversions.ToInteger(HexOffsets[Index].HexValueTrue);
        }

        private void ExtractValue()
        {
            for (int Index = 0, loopTo = HexOffsets.Length - 1; Index <= loopTo; Index++)
            {
                if (!ConvertValue(GetHexOffset(Index), Index))
                {
                    Value = false;
                    return;
                }
            }
            Value = true;
        }

        private void UpdateValue()
        {
            Value = !Value;
            Checked = Value;
        }

        private void StashValue(int Offset, int Index)
        {
            bool lv_reverse = Endian == EndianType.Little_Endian;
            var Item = HexOffsets[Index];
            var Args = new PreWriteArgs(Conversions.ToDecimal(Value ? Item.HexValueTrue : Item.HexValueFalse));
            PreWrite?.Invoke(this, Args);
            decimal NewValue = Args.NewValue;
            Write(Offset, HexConvert.NumericToBytes(NewValue.ToString(), lv_reverse, Item.MaxLength), Item.MaxLength);
        }

        private void Write(int param_offset, byte[] param_bytes, int param_length)
        {
            byte[] buffer = new byte[param_length];
            for (int index = 0, loopTo = param_length - 1; index <= loopTo; index++)
                buffer[index] = param_bytes[index];
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        #endregion

        #region  Public 

        public event OnPreWrite PreWrite;

        public void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            LoadedValue = false;
            if (HexOffsets.Length > 0)
                ExtractValue();
            LoadedValue = Value;
            Checked = Value;
        }

        // Public Function GetValue(Offset As Integer) As String
        // Return ConvertValue(Offset)
        // End Function

        public void Save(int Offset = -1)
        {
            for (int Index = 0, loopTo = HexOffsets.Length - 1; Index <= loopTo; Index++)
            {
                if (Offset < 0)
                {
                    StashValue(GetHexOffset(Index), Index);
                    LoadedValue = Value;
                }
                else
                {
                    StashValue(Offset, Index);
                }
            }

            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        #endregion

    }

    #endregion

    #region  HexNumericBox 

    public class HexNumericBox : BasicNumericBox, IHexControl
    {

        private string LoadedText = "";
        public int WriteLength = 0;
        private bool IgnoreListBox = false;
        private HexListBox ListBox = null;

        #region  Constructor 

        public HexNumericBox()
        {
            KeyPress += Me_KeyPress;
            KeyDown += Me_KeyDown;
            Leave += Me_TextChanged;

        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
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

        private InputType _Input = InputType.Numeric;
        [Category("Function")]
        [Description("Limits the input type to a specific set of keys based this setting.")]
        [DefaultValue(InputType.Numeric)]
        public InputType Input
        {
            get
            {
                return _Input;
            }
            set
            {
                if (value != _Input)
                {
                    Hexadecimal = value == InputType.Hex;
                    _Input = value;
                }
            }
        }

        private bool _AutoTag = false;
        [Category("Function")]
        [Description("If true then the control will automatically append a closing tag.")]
        [DefaultValue(false)]
        public bool AutoTag
        {
            get
            {
                return _AutoTag;
            }
            set
            {
                if (value != _AutoTag)
                {
                    _AutoTag = value;
                }
            }
        }

        private string _ClosingTag = "";
        [Category("Function")]
        [Description("Allows this textbox to append a value after it's text")]
        [DefaultValue("")]
        public string ClosingTag
        {
            get
            {
                return _ClosingTag;
            }
            set
            {
                if ((value ?? "") != (_ClosingTag ?? ""))
                {
                    _ClosingTag = value;
                }
            }
        }

        private bool _OvertypeMode = false;
        [Category("Function")]
        [Description("Allows this textbox to Overtype text where possible")]
        [DefaultValue(false)]
        public bool OvertypeMode
        {
            get
            {
                return _OvertypeMode;
            }
            set
            {
                if (value != _OvertypeMode)
                {
                    _OvertypeMode = value;
                }
            }
        }

        private DisplayType _Display = DisplayType.Numeric;
        [Category("Function")]
        [Description("Determines what format will be used for the data when used by the end user.")]
        [DefaultValue(DisplayType.Numeric)]
        public DisplayType Display
        {
            get
            {
                return _Display;
            }
            set
            {
                if (_Display != value)
                {
                    _Display = value;
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

        private bool _Ranged = false;
        [Category("Function")]
        [Description("Determines if the control will display within a range driven by a parent control.")]
        [DefaultValue(false)]
        public bool Ranged
        {
            get
            {
                return _Ranged;
            }
            set
            {
                if (_Ranged != value)
                {
                    _Ranged = value;
                }
            }
        }

        private int[] _ExcludeRanged = new int[] { };
        [Category("Function")]
        [Description("Determines the indecies which will not be driven by the parent control with 0 being the first, and -1 being the last.")]
        [DefaultValue(new int[] { })]
        public int[] ExcludeRanged
        {
            get
            {
                return _ExcludeRanged;
            }
            set
            {
                if (!ReferenceEquals(_ExcludeRanged, value))
                {
                    _ExcludeRanged = value;
                }
            }
        }

        private string TextWithoutSeparators
        {
            get
            {
                if(int.TryParse(
                    Text,
                    NumberStyles.AllowThousands,
                    CultureInfo.InvariantCulture,
                    out var numberText
                ))
                {
                    return numberText.ToString();
                }

                return Text;
            }
        }

        #endregion

        #region  Events 

        private void Me_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Strings.AscW(e.KeyChar))
            {
                case (int)Keys.Left:
                case (int)Keys.Right:
                    {
                        e.Handled = false;
                        break;
                    }
                case (int)Keys.Delete:
                case (int)Keys.Back:
                    {
                        if (OvertypeMode == true)
                        {
                            e.Handled = true;
                        }
                        else if (Text.Length <= 1)
                        {
                            Text = "0";
                            MoveCaret((HexNumericBox)sender, false);
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }

                        break;
                    }

                default:
                    {
                        e.Handled = true;
                        if (Input == InputType.Hex)
                        {
                            if (HexQuery.IsKeyCode(e.KeyChar))
                            {
                                TypeText((HexNumericBox)sender, char.IsLower(e.KeyChar) ? char.ToUpper(e.KeyChar) : e.KeyChar);
                            }
                        }
                        else if (Input == InputType.Numeric)
                        {
                            if (HexQuery.IsNumericCode(e.KeyChar))
                            {
                                TypeText((HexNumericBox)sender, e.KeyChar);
                            }
                        }
                        else
                        {
                            TypeText((HexNumericBox)sender, e.KeyChar);
                        }

                        break;
                    }
            }
        }

        private void Me_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                Text = LoadedText;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                TypeTextComplete((HexNumericBox)sender);
            }
            else if (OvertypeMode == true & e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
            }
            else if (Text.Length < 1)
            {
                Text = "0";
                MoveCaret((HexNumericBox)sender, false);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void Me_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LoadedText))
                TypeTextComplete((HexNumericBox)sender);
        }

        #endregion

        #region  Private 

        private void TypeText(HexNumericBox param_text, char param_char)
        {
            int lv_last = param_text.SelectionStart;
            int text_length = TextWithoutSeparators.Length;
            if (OvertypeMode == true)
            {
                if (param_text.SelectionStart < param_text.Text.Length)
                {
                    param_text.Text = param_text.Text.ReplaceAt(param_text.SelectionStart, Conversions.ToString(param_char));
                    param_text.SelectionStart = lv_last + 1;
                }
            }
            else if (text_length - param_text.SelectionLength < param_text.MaxLength)
            {
                param_text.SelectedText = Conversions.ToString(param_char);
            }
            else if (text_length < param_text.MaxLength)
            {
                param_text.Text += Conversions.ToString(param_char);
                param_text.SelectionStart = lv_last + 1;
            }
        }

        private void MoveCaret(HexNumericBox param_text, bool param_forward)
        {
            if (param_forward == true)
            {
                param_text.SelectionStart = Math.Min(param_text.SelectionStart + 1, param_text.Text.Length);
            }
            else
            {
                param_text.SelectionStart = Math.Max(param_text.SelectionStart - 1, 0);
            }
        }

        private void RemoveText(HexNumericBox param_text)
        {
            if (param_text.Text.Length == 0)
                return;
            int lv_last = param_text.SelectionStart;
            param_text.Text = param_text.Text.Remove(param_text.SelectionStart, 1);
            param_text.SelectionStart = lv_last;
        }

        private void TypeTextComplete(HexNumericBox sender)
        {
            Save();
        }

        private void Write(int param_offset, byte[] param_bytes)
        {
            int lv_offmax = WriteLength;
            if (!string.IsNullOrEmpty(ClosingTag))
                lv_offmax = (int)Math.Round(lv_offmax + ClosingTag.Length / 2d);
            byte[] buffer = new byte[lv_offmax];
            for (int index = 0, loopTo = lv_offmax - 1; index <= loopTo; index++)
            {
                if (!string.IsNullOrEmpty(ClosingTag) & index == param_bytes.Length)
                {
                    buffer[index] = (byte)Convert.ToInt32(ClosingTag, 16);
                }
                else if (AutoTag == true & index > param_bytes.Length)
                {
                    buffer[index] = (byte)Convert.ToInt32("00", 16);
                }
                else
                {
                    buffer[index] = param_bytes[index];
                }
            }
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        private int GetHexOffset()
        {
            return (int)Math.Round(GetGlobalOffset() + Conversions.ToDouble(HexOffset) + GetPanelOffset() + GetListOffset());
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private string ConvertText(int Offset)
        {
            if (WriteLength == 0)
                WriteLength = MaxLength;
            byte[] lv_bytes = MemoryLiterator.Read(Offset, WriteLength).Buffer;
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            string lv_text = "";
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        lv_text = HexConvert.BytesToString(lv_bytes);
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        lv_text = FormatText(HexConvert.BytesToNumeric(lv_bytes));
                        if (WriteLength % 2 != 0)
                        {
                            MaxLength = (int)Math.Round(3 + (WriteLength - 1) * 2 + Math.Floor(WriteLength / 2d));
                        }
                        else
                        {
                            MaxLength = (int)Math.Round(2 + (WriteLength - 1) * 2 + Math.Floor(WriteLength / 2d));
                        }

                        break;
                    }
                case DisplayType.Hex:
                    {
                        lv_text = HexConvert.BytesToHex(lv_bytes);
                        MaxLength = WriteLength;
                        break;
                    }
            }
            return lv_text;
        }

        private void LoadText()
        {
            Text = ConvertText(GetHexOffset());
        }

        private void SaveText(int Offset)
        {
            Debug.WriteLine(Text);
            bool lv_reverse = Endian == EndianType.Little_Endian;
            switch (Display)
            {
                case DisplayType.Text:
                    {
                        Write(Offset, HexConvert.StringToBytes(Text, lv_reverse));
                        break;
                    }
                case DisplayType.Numeric:
                    {
                        Write(Offset, HexConvert.NumericToBytes(Text, lv_reverse, WriteLength));
                        break;
                    }
                case DisplayType.Hex:
                    {
                        Write(Offset, HexConvert.HexToBytes(Text, lv_reverse));
                        break;
                    }
            }
        }

        protected override void UpdateEditText()
        {
            base.UpdateEditText();
        }

        #endregion

        #region  Public 

        public void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            LoadedText = "";
            if (MaxLength > 0)
                LoadText();
            LoadedText = Text;
            if (AutoTag == true & string.IsNullOrEmpty(ClosingTag) & HexStorage.Memory[GetHexOffset() + Text.Length] == 255)
            {
                ClosingTag = "FF";
                MaxLength = (int)Math.Round(Math.Max(MaxLength - Math.Ceiling(ClosingTag.Length / 2d), 0d));
            }
        }

        public string GetText(int Offset)
        {
            if (MaxLength > 0)
                return ConvertText(Offset);
            return Text;
        }

        public string GetDisplay(int Offset)
        {
            if (MaxLength > 0)
                return ConvertText(Offset);
            return Text;
        }

        public string FormatText(string Text)
        {
            if (ThousandsSeparator == true)
            {
                Text = Conversions.ToString(BasicHelper.ToNumericText(Text));
            }
            return Text;
        }

        public void Save(int Offset = -1)
        {
            if (Offset < 0)
            {
                if ((Text ?? "") == (LoadedText ?? ""))
                    return;
                if (MaxLength > 0)
                    SaveText(GetHexOffset());
                LoadedText = Text;
            }
            else if (MaxLength > 0)
                SaveText(Offset);
            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        public string GetInternalHexOffset()
        {
            string lv_string;
            lv_string = GetHexOffset().ToString();
            return lv_string;
        }

        #endregion

    }

    #endregion

    #region  HexBitFlag 

    public class HexBitFlag : BasicBitFlag, IHexControl
    {

        private int LoadedValue = 0;
        private bool IgnoreListBox = false;
        private HexListBox ListBox = null;

        #region  Constructor 

        public HexBitFlag()
        {
            ControlClick += Me_Click;

        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
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
                if ((_HexOffset ?? "") != (value ?? ""))
                {
                    _HexOffset = value;
                }
            }
        }

        private int _MaxLength = 1;
        [Category("Behavior")]
        [Description("The length of the value loaded in this control.")]
        [DefaultValue(1)]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
        }

        private Component _Editor = null;
        [Category("Appearance")]
        [Description("The value of the control.")]
        [DefaultValue(default(string))]
        public Component Editor
        {
            get
            {
                return _Editor;
            }
            set
            {
                _Editor = value;
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

        #region  Events 

        private void Me_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        #region  Private 

        private int GetHexOffset()
        {
            return (int)Math.Round(GetGlobalOffset() + Conversions.ToDouble(HexOffset) + GetPanelOffset() + GetListOffset());
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + (lv_index * Conversions.ToDouble(ctrl.IndexOffset) + Math.Floor((Order + lv_index) * (int)ctrl.IndexBitOffset / 8d)));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private int GetPanelBitOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
                lv_offset += ctrl.CurrentBitOffset(lv_index + Order);
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private int ConvertValue(int Offset)
        {
            byte[] lv_bytes = MemoryLiterator.Read(Offset, MaxLength).Buffer;
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            int lv_value = Conversions.ToInteger(HexConvert.BytesToNumeric(lv_bytes));
            return lv_value;
        }

        private void ExtractValue()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            string Bits = HexConvert.ByteToBitRaw((byte)ConvertValue(GetHexOffset()));
            if (Endian == EndianType.Little_Endian)
                Bits = new string(Bits.Reverse().ToArray());
            int Index = GetPanelBitOffset();
            Value = Conversions.ToString(Bits[Index]) == "1" ? true : false;
        }

        private void StashValue(int Offset)
        {
            bool lv_reverse = Endian == EndianType.Little_Endian;
            var Bits = new StringBuilder(HexConvert.ByteToBitRaw((byte)ConvertValue(GetHexOffset())));
            if (Endian == EndianType.Little_Endian)
                Bits = new StringBuilder(Conversions.ToString(Bits.ToString().Reverse().ToArray()));
            int Index = GetPanelBitOffset();
            Bits[Index] = Conversions.ToChar(Value == true ? "1" : "0");
            if (Endian == EndianType.Little_Endian)
                Bits = new StringBuilder(Conversions.ToString(Bits.ToString().Reverse().ToArray()));
            int Bytes = HexConvert.BitToIntRaw(Bits.ToString());
            Write(Offset, HexConvert.NumericToBytes(Bytes.ToString(), !lv_reverse, MaxLength), MaxLength);
        }

        private void Write(int param_offset, byte[] param_bytes, int param_length)
        {
            byte[] buffer = new byte[param_length];
            for (int index = 0, loopTo = param_length - 1; index <= loopTo; index++)
                buffer[index] = param_bytes[index];
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        #endregion

        #region  Public 

        public new void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            LoadedValue = 0;
            if (MaxLength > 0)
                ExtractValue();
            LoadedValue = Conversions.ToInteger(Value);
        }

        public void Save(int Offset = -1)
        {
            if (Offset < 0)
            {
                if (MaxLength > 0)
                    StashValue(GetHexOffset());
                LoadedValue = Conversions.ToInteger(Value);
            }
            else if (MaxLength > 0)
                StashValue(Offset);

            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        #endregion

    }

    #endregion

    #region  HexBitFlags 

    public class HexBitFlags : BasicBitFlags, IHexControl
    {

        private int LoadedValue = 0;
        private bool IgnoreListBox = false;
        private HexListBox ListBox = null;

        #region  Constructor 

        public HexBitFlags()
        {
            ControlClick += Me_BitsChange;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
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
                if ((_HexOffset ?? "") != (value ?? ""))
                {
                    _HexOffset = value;
                }
            }
        }

        private int _MaxLength = 1;
        [Category("Behavior")]
        [Description("The length of the value loaded in this control.")]
        [DefaultValue(1)]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
        }

        private int _Value = 1;
        [Category("Appearance")]
        [Description("The value of the control.")]
        [DefaultValue(1)]
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                }
            }
        }

        private Component _Editor = null;
        [Category("Appearance")]
        [Description("The value of the control.")]
        [DefaultValue(default(string))]
        public Component Editor
        {
            get
            {
                return _Editor;
            }
            set
            {
                _Editor = value;
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

        #region  Events 

        private void Me_BitsChange(object sender, EventArgs e)
        {
            UpdateValue();
            Save();
        }

        #endregion

        #region  Private 

        private int GetHexOffset()
        {
            return (int)Math.Round(GetGlobalOffset() + Conversions.ToDouble(HexOffset) + GetPanelOffset() + GetListOffset());
        }

        private int GetGlobalOffset()
        {
            return HexStorage.GlobalOffset;
        }

        private int GetPanelOffset()
        {
            int lv_offset = 0;
            int lv_index = ListBox != null ? ListBox.CurrentIndex() : 0;
            Control argStartingContainer = this;
            foreach (HexPanel ctrl in ControlExtension.FindAllParents(ref argStartingContainer, "HexPanel"))
            {
                if (Conversions.ToDouble(ctrl.IndexOffset) > 0d)
                    IgnoreListBox = true;
                if (ListBox != null & Conversions.ToDouble(ctrl.Pointer) == 0d)
                    lv_offset = (int)Math.Round(lv_offset + lv_index * Conversions.ToDouble(ctrl.IndexOffset));
                lv_offset = (int)Math.Round(lv_offset + (Conversions.ToDouble(ctrl.HexOffset) + ctrl.CurrentOffset(lv_index)));
            }
            return lv_offset;
        }

        private int GetListOffset()
        {
            if (IgnoreListBox == true)
                return 0;
            int lv_offset = 0;
            if (ListBox != null)
                lv_offset = (int)Math.Round(lv_offset + Conversions.ToDouble(ListBox.CurrentOffset()));
            return lv_offset;
        }

        private int ConvertValue(int Offset)
        {
            byte[] lv_bytes = MemoryLiterator.Read(Offset, MaxLength).Buffer;
            if (Endian == EndianType.Little_Endian)
                Array.Reverse(lv_bytes);
            int lv_value = Conversions.ToInteger(HexConvert.BytesToNumeric(lv_bytes));
            return lv_value;
        }

        private void ExtractValue()
        {
            Value = ConvertValue(GetHexOffset());
            UpdateView();
        }

        private void UpdateView()
        {
            string Bits = HexConvert.ByteToBitRaw((byte)Value);
            for (int Index = 0, loopTo = Bits.Length - 1; Index <= loopTo; Index++)
                Flags[Index].Value = Conversions.ToString(Bits[Index]) == "1" ? true : false;
        }

        private void UpdateValue()
        {
            var NewBits = new StringBuilder(Flags.Length);
            for (int Index = 0, loopTo = NewBits.Capacity - 1; Index <= loopTo; Index++)
                NewBits.Append(Flags[Index].Value == true ? "1" : "0");
            Value = HexConvert.BitToIntRaw(NewBits.ToString());
        }

        private void StashValue(int Offset)
        {
            bool lv_reverse = Endian == EndianType.Little_Endian;
            Write(Offset, HexConvert.NumericToBytes(Value.ToString(), lv_reverse, MaxLength), MaxLength);
        }

        private void Write(int param_offset, byte[] param_bytes, int param_length)
        {
            byte[] buffer = new byte[param_length];
            for (int index = 0, loopTo = param_length - 1; index <= loopTo; index++)
                buffer[index] = param_bytes[index];
            MemoryLiterator.Write(this, new MemoryOperation(param_offset, buffer));
        }

        #endregion

        #region  Public 

        public new void Load()
        {
            if (ListBox == null)
            {
                ListBox = HexUtility.GetListBox(this);
            }
            LoadedValue = 0;
            if (MaxLength > 0)
                ExtractValue();
            LoadedValue = Value;
        }

        public void Save(int Offset = -1)
        {
            if (Offset < 0)
            {
                if (MaxLength > 0)
                    StashValue(GetHexOffset());
                LoadedValue = Value;
            }
            else if (MaxLength > 0)
                StashValue(Offset);

            // This should raise the stored event so that we can be informed of the changes globally
            HexStorage.DataStore(this);
        }

        #endregion

    }

    #endregion

    #region HexListBoxAssociate

    public class HexListBoxAssociate : BasicListBoxAssociate
    {
        #region Constructor

        public HexListBoxAssociate()
        {
            
        }

        #endregion

        protected override void OnListBoxChanged()
        {
            if (!DesignMode)
            {
                CreateControls();
            }
        }
        
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HexAddressBox AddressBox { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Label AddressLabel { get; private set; }

        [DefaultValue("")]
        //[Editor(typeof(BasicFormatStringEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Category("Misc")]
        [Description("Allows the user to provide a custom format for the address when it is displayed & stored.")]
        public string FormatAddress { get; set; } = "X6";

        private void CreateControls()
        {
            CreateAddressBox();
        }

        private void CreateAddressBox()
        {
            if (AddressBox != null && AddressLabel != null) return;

            AddressLabel = new Label();
            AddressLabel.Location = new Point(0, 0);
            AddressLabel.Size = new Size(70, 30);
            AddressLabel.TextAlign = ContentAlignment.MiddleRight;
            AddressLabel.Text = "Address";

            Controls.Add(AddressLabel);

            AddressBox = new HexAddressBox();
            AddressBox.Location = new Point(AddressLabel.Location.X + AddressLabel.Width, AddressLabel.Location.Y);
            AddressBox.Size = new Size(140, 30);
            AddressBox.Margin = new Padding(0);
            AddressBox.Hide();

            if (ListBox is HexListBox hexListBox)
            {
                hexListBox.OnCreateControls += HexListBox_OnCreateControls;
                hexListBox.SelectedIndexChanged += HexListBox_SelectedIndexChanged;
                AddressBox.OnValueChange += AddressBox_OnValueChange;
            }

            Controls.Add(AddressBox);
        }

        private void AddressBox_OnValueChange(object sender, EventArgs e)
        {
            if (!(sender == AddressBox)) return;

            SetOffsetValue();
        }

        private void HexListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(sender is HexListBox hexListBox && hexListBox.CanEditLabel)) return;

            GetOffsetValue(hexListBox);
        }

        private void HexListBox_OnCreateControls(object sender, EventArgs e)
        {
            if (!(sender is HexListBox hexListBox && hexListBox.CanEditLabel)) return;

            GetOffsetValue(hexListBox);
            AddressBox.Show();
        }

        private void GetOffsetValue(HexListBox hexListBox = null)
        {
            if (hexListBox == null) hexListBox = ListBox as HexListBox;
            if (hexListBox == null) return;

            AddressBox.Value = HexConvert.HexToInt(hexListBox.CurrentOffset());
        }

        private void SetOffsetValue(HexListBox hexListBox = null)
        {
            if (hexListBox == null) hexListBox = ListBox as HexListBox;
            if (hexListBox == null) return;

            var padding = int.Parse(FormatAddress.Replace("X", "")) - 1;

            hexListBox.SetSelectedDefinition((item) =>
            {
                var oldOffset = item.Offset;
                item.Offset = HexConvert.IntToAddress(AddressBox.Value, FormatAddress);

                return oldOffset != item.Offset;
            });
        }
    }

    #endregion

    #region HexImage

    public class HexImage : BasicImage, IHexControl
    {
        private HexListBox ListBox = null;
        private HexUtility<HexImage> HexUtility;
        private int[] LastOffsets;

        private bool _AutoLoad = true;
        public bool AutoLoad
        {
            get
            {
                return _AutoLoad && LoadedValue;
            }
            set => _AutoLoad = value;
        }

        #region  Constructor 

        public HexImage()
        {
            HexUtility = new HexUtility<HexImage>(this);
            MouseMove += HexImage_MouseMove;
            DragEnter += HexImage_DragEnter;
            DragDrop += HexImage_DragDrop;
        }

        #endregion

        #region Properties

        private string _HexOffset = "&H000000";
        [Category("Function")]
        [Description("Provides a hex value to be associated with this image.")]
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
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private string _PaletteOffset = "&H000000";
        [Category("Function")]
        [Description("Provides a hex value to be associated with the palette of this image.")]
        [DefaultValue("&H000000")]
        public string PaletteOffset
        {
            get
            {
                return _PaletteOffset;
            }
            set
            {
                if ((value ?? "") != (_PaletteOffset ?? ""))
                {
                    _PaletteOffset = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private string _SpritesetOffset = "&H000000";
        [Category("Function")]
        [Description("Provides a hex value to be associated with the sprites frames of this image.")]
        [DefaultValue("&H000000")]
        public string SpritesetOffset
        {
            get
            {
                return _SpritesetOffset;
            }
            set
            {
                if ((value ?? "") != (_SpritesetOffset ?? ""))
                {
                    _SpritesetOffset = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private int _Stride = 0;
        [Category("Function")]
        [Description("Provides a way to ensure a certain length is loaded for the image, otherwise the width is used.")]
        [DefaultValue(0)]
        public int Stride
        {
            get
            {
                return _Stride;
            }
            set
            {
                if (value != _Stride)
                {
                    _Stride = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private int _Length = 0;
        [Category("Function")]
        [Description("Provides a way to ensure a certain length is loaded for the image, otherwise the height is used.")]
        [DefaultValue(0)]
        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (value != _Length)
                {
                    _Length = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private SizeF _Zoom = new SizeF(1.0f, 1.0f);
        [Category("Function")]
        [Description("Provides a way to zoom in on the pixels of this image when rendering it.")]
        [DefaultValue(typeof(SizeF), "1, 1")]
        public SizeF Zoom
        {
            get
            {
                return _Zoom;
            }
            set
            {
                if (value != _Zoom)
                {
                    _Zoom = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private Size _Scale = new Size(1, 1);
        [Category("Function")]
        [Description("Provides a way to scale the pixels of this image before we render it.")]
        [DefaultValue(typeof(Size), "1, 1")]
        public Size Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                if (value != _Scale)
                {
                    _Scale = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private Sprite _Sprite = new Sprite(8, 8);
        [Category("Function")]
        [Description("The sprites used by this image.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(typeof(Sprite), "8, 8")]
        public Sprite Sprite
        {
            get
            {
                return _Sprite;
            }
            set
            {
                if (value != _Sprite)
                {
                    _Sprite = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private CompressionType _Compression = CompressionType.LZ77_RLE;
        [Category("Function")]
        [Description("Provides a de/compression algorithim to be used when reading this image.")]
        [DefaultValue(CompressionType.LZ77_RLE)]
        public CompressionType Compression
        {
            get
            {
                return _Compression;
            }
            set
            {
                if (value != _Compression)
                {
                    _Compression = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private ImageEncodingType _Encoding = ImageEncodingType.FormatPlanar4BPP;
        [Category("Function")]
        [Description("Provides a pixel format algorithim to be used when rendering or storing this image.")]
        [DefaultValue(ImageEncodingType.FormatPlanar4BPP)]
        public ImageEncodingType Encoding
        {
            get
            {
                return _Encoding;
            }
            set
            {
                if (value != _Encoding)
                {
                    _Encoding = value;
                    if(!DesignMode && AutoLoad) Load();
                }
            }
        }

        private ColorDepthType _ColorDepth = ColorDepthType.FormatRGB555;
        [Category("Function")]
        [Description("Provides a palette color size and arrangement to be used when rendering the pixels from the image.")]
        [DefaultValue(ColorDepthType.FormatRGB555)]
        public ColorDepthType ColorDepth
        {
            get
            {
                return _ColorDepth;
            }
            set
            {
                if (value != _ColorDepth)
                {
                    _ColorDepth = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        private bool _Single = true;
        [Category("Function")]
        [Description("Ensures that only a single image is decompressed and rendered.")]
        [DefaultValue(true)]
        public bool Single
        {
            get
            {
                return _Single;
            }
            set
            {
                if (value != _Single)
                {
                    _Single = value;
                    if (!DesignMode && AutoLoad) Load();
                }
            }
        }

        [Flags]
        [Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
        public enum HexImageFeatureType
        {
            Nothing = FlagConstants.None,
            Export = FlagConstants.Flag1,
            Import = FlagConstants.Flag2,
            DragAndDrop = FlagConstants.Flag3,
            Previewer = FlagConstants.Flag4,
            Everything = Export | Import | DragAndDrop | Previewer
        }

        private HexImageFeatureType _Features = HexImageFeatureType.Everything;
        [Category("Function")]
        [DefaultValue(HexImageFeatureType.Everything)]
        public HexImageFeatureType Features
        {
            get
            {
                return _Features;
            }
            set
            {
                if (value != _Features)
                {
                    _Features = value;
                }
            }
        }

        private Image _Original = null;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Image Original
        {
            get
            {
                return _Original;
            }
            protected set
            {
                if (value != _Original)
                {
                    _Original = value;
                }
            }
        }

        private Image _Unscaled = null;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Image Unscaled
        {
            get
            {
                return _Unscaled;
            }
            protected set
            {
                if (value != _Unscaled)
                {
                    _Unscaled = value;
                }
            }
        }

        private Color[] _DefaultPalette;
        [Category("Function")]
        [Description("Defines the default color palette to use, when no PaletteOffset is provided.")]
        [DefaultValue(typeof(Color), null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color[] DefaultPalette
        {
            get
            {
                if(_DefaultPalette == null)
                {
                    _DefaultPalette = new Color[0x100]
                        .Select((x, y) => y == 0 ? Color.FromKnownColor(KnownColor.Fuchsia)
                            : y == 1 ? Color.FromKnownColor(KnownColor.Black)
                            : y == 0x100 ? Color.FromKnownColor(KnownColor.White)
                            : Color.FromArgb(y, y, y))
                        .ToArray();
                }
                return _DefaultPalette;
            }
            set
            {
                if (value != _DefaultPalette)
                {
                    _DefaultPalette = value;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int UnscaledWidth
        {
            get => Width / Scale.Width;
            set => Width = value * Scale.Width;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int UnscaledHeight
        {
            get => Height / Scale.Height;
            set => Height = value * Scale.Height;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int StrideWidth
        {
            get => Stride > 0 ? Stride : Width / Scale.Width;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int LengthHeight
        {
            get => Length > 0 ? Length : Height / Scale.Height;
        }

        #endregion

        #region Events

        [Category("Behavior")]
        public event EventHandler<MemoryConflict> ImportConflict;
        public event EventHandler<EventArgs> ViewAddress;

        #endregion

        #region Private

        private void HexImage_MouseMove(object sender, MouseEventArgs e)
        {            
            if (AllowDrop && e.Button == MouseButtons.Left)
            {
                var pb = (HexImage)sender;
                if (pb.BackgroundImage != null)
                {
                    pb.DoDragDrop(pb, DragDropEffects.Move);
                }
            }
        }

        private void HexImage_DragEnter(object sender, DragEventArgs e)
        {
            if (GetFilename(out _, e))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private bool GetFilename(out string filename, DragEventArgs e)
        {
            bool result = false;
            filename = string.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = e.Data.GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is string))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".gif") || (ext == ".tiff") || (ext == ".png") || (ext == ".bmp"))
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        private void HexImage_DragDrop(object sender, DragEventArgs e)
        {
            if(GetFilename(out var filename, e))
            {
                ReplaceBitmap(filename);
            }
        }

        private void NewBitmap(out Bitmap bitmap)
        {
            bitmap = Width > 0 && Height > 0
                ? new Bitmap(StrideWidth, Math.Max(Height / Scale.Height, Sprite?.TotalSize.Height ?? 0), PixelFormat.Format8bppIndexed)
                : null;
        }

        private bool GetBitmap(ref Bitmap bitmap)
        {
            if (bitmap == null)
            {
                NewBitmap(out bitmap);
            }
            return bitmap != null;
        }

        private void GetPalette(ref Bitmap bitmap, ColorPalette palette = null)
        {
            if (!GetBitmap(ref bitmap))
                return;

            if(palette == null)
            {
                var isDefaultPalette = HexConvert.HexToInt(PaletteOffset) == 0x0;
                palette = bitmap.Palette;

                var colors = (Encoding == ImageEncodingType.FormatPlanar4BPP ? 16 : 4);
                var factor = DefaultPalette.Length / colors;

                for (int i = 0; i < colors; i++)
                {
                    palette.Entries[i] = isDefaultPalette ? DefaultPalette[i * factor]
                        : HexColorSwatch.From15BitInt(GetPaletteColor(i * 2));
                }
            }

            bitmap.Palette = palette;
        }

        private void SetDefaultPalette(ref Bitmap bitmap)
        {
            if (!GetBitmap(ref bitmap))
                return;

            var palette = bitmap.Palette;

            var colors = (Encoding == ImageEncodingType.FormatPlanar4BPP ? 16 : 4);
            var factor = DefaultPalette.Length / colors;

            for (int i = 0; i < colors; i++)
            {
                palette.Entries[i] = DefaultPalette[i * factor];
            }

            bitmap.Palette = palette;
        }

        private ushort GetPaletteColor(int offset = 0)
        {
            return HexConvert.BytesToInt<ushort>(MemoryLiterator.Read(HexUtility.GetHexOffset(PaletteOffset) + offset, 2).Buffer);
        }

        private void GetPixels(ref Bitmap bitmap, byte[] pixels = null)
        {
            if (!GetBitmap(ref bitmap))
                return;

            var minHeight = Math.Max(Sprite?.TotalSize.Height ?? 0, Height / Scale.Height);
            var boundsRect = new Rectangle(0, 0, StrideWidth, Height / Scale.Height);
            BitmapData bmpData = bitmap.LockBits(boundsRect,
                                            ImageLockMode.WriteOnly,
                                            bitmap.PixelFormat);
            IntPtr pixelPointer = bmpData.Scan0;
            var offset = HexUtility.GetHexOffset();

            int totalBytes = Math.Max(0, Math.Min(
                bmpData.Stride * bitmap.Height,
                HexStorage.Memory.Length - HexUtility.GetHexOffset()
            ));
            var rgbValues = new byte[totalBytes];

            if(rgbValues.Length > 0)
            {
                rgbValues = pixels ?? MemoryLiterator.Read(HexUtility.GetHexOffset(), totalBytes).Buffer;

                if (Compression == CompressionType.LZ77_RLE)
                {
                    rgbValues = HexCompressions.Instance.Implemented[Compression]
                        .Decompress(new MemoryStream(rgbValues), out var information, new HexDecompressionOptions() { Single = Single });

                    LastOffsets = information.Offsets;

                    //var testDecompressed = HexConvert.BytesToHex(rgbValues, -1, rgbValues.Length + 1, " ");
                }

                if (BitFormats.Instance.Implemented.ContainsKey(Encoding))
                {
                    var codec = BitFormats.Instance.Implemented[Encoding];

                    rgbValues = codec.Decode(rgbValues);
                }

                var original = new Bitmap(StrideWidth, LengthHeight, PixelFormat.Format8bppIndexed);
                SetDefaultPalette(ref original);
                var originalRect = new Rectangle(0, 0, StrideWidth, LengthHeight);
                BitmapData originalBitmapData = original.LockBits(originalRect,
                                            ImageLockMode.WriteOnly,
                                            original.PixelFormat);
                IntPtr originalPixelPointer = originalBitmapData.Scan0;
                Marshal.Copy(rgbValues, 0, originalPixelPointer, Math.Min(rgbValues.Length, StrideWidth * LengthHeight));
                original.UnlockBits(originalBitmapData);
                Original = original;

                if (Sprite?.Assembly != null)
                {
                    var newPixelData = new byte[rgbValues.Length];

                    for (int i = 0; i < Sprite.Assembly.Length; i++)
                    {
                        var assembly = Sprite.Assembly[i];

                        newPixelData = BitmapHandler.CutBmpRegion(
                            new Canvas()
                            {
                                Stride = bmpData.Stride,
                                Height = bitmap.Height,
                                Width = bitmap.Width
                            },
                            rgbValues,
                            new Rectangle(
                                assembly.Source.X * Sprite.Size.Width,
                                assembly.Source.Y * Sprite.Size.Height,
                                assembly.Source.Width * Sprite.Size.Width,
                                assembly.Source.Height * Sprite.Size.Height
                            ),
                            new Point(
                                assembly.Destination.X * Sprite.Size.Width,
                                assembly.Destination.Y * Sprite.Size.Height
                            ),
                            newPixelData
                        );
                    }

                    rgbValues = newPixelData;
                }

                if (rgbValues.Length < totalBytes)
                {
                    Array.Resize(ref rgbValues, totalBytes);
                }

                Marshal.Copy(rgbValues, 0, pixelPointer, totalBytes);
            }

            bitmap.UnlockBits(bmpData);

            if (Sprite?.Assembly != null)
            {
                var region = new Rectangle(new Point(), Sprite.TotalSize);
                BitmapHandler.CropBmpRegion(bitmap, region, ref bitmap);
            }
        }

        private void GetZoom(ref Bitmap bitmap)
        {
            Unscaled = bitmap;

            bitmap = PixelPerfectZoom(bitmap, new SizeF(Zoom.Width + Scale.Width - 1, Zoom.Height + Scale.Height - 1));
        }

        private void SetPalette(Bitmap bitmap = null, ColorPalette palette = null)
        {
            if (!GetBitmap(ref bitmap))
                return;

            var isDefaultPalette = HexConvert.HexToInt(PaletteOffset) == 0x0;

            if (palette == null && !isDefaultPalette)
            {
                palette = bitmap.Palette;

                var colors = Encoding == ImageEncodingType.FormatPlanar4BPP ? 16 : 4;
                var factor = 16 / colors;

                for (int i = 0; i < colors; i++)
                {
                    SetPaletteColor(i * 2, HexColorSwatch.To15BitInt(palette.Entries[i * factor]));
                }
            }
        }

        private void SetPaletteColor(int offset = 0, ushort color = 0)
        {
            MemoryLiterator.Write(this, new MemoryOperation(HexUtility.GetHexOffset(PaletteOffset) + offset, HexConvert.IntToBytes(color)));
        }

        private void SetPixels(ref Bitmap bitmap, int offset = -1)
        {
            if (!GetBitmap(ref bitmap))
                return;

            Bitmap newBitmap = null;

            if (Sprite?.Assembly != null)
            {
                newBitmap = new Bitmap(StrideWidth, LengthHeight, PixelFormat.Format8bppIndexed);

                var newBitmapBoundsRect = new Rectangle(0, 0, newBitmap.Width, newBitmap.Height);
                BitmapData newBitmapData = newBitmap.LockBits(newBitmapBoundsRect,
                                            ImageLockMode.WriteOnly,
                                            newBitmap.PixelFormat);
                int newBitmapTotalBytes = Math.Max(0, Math.Min(
                    newBitmapData.Stride * newBitmapData.Height,
                    HexStorage.Memory.Length - offset
                ));
                IntPtr newBitmapPixelPointer = newBitmapData.Scan0;
                var newBitmapPixelData = new byte[newBitmapTotalBytes];

                var originalBoundsRect = new Rectangle(0, 0, StrideWidth, LengthHeight);
                var original = Original as Bitmap;
                BitmapData originalBitmapData = original.LockBits(originalBoundsRect,
                                            ImageLockMode.ReadOnly,
                                            Original.PixelFormat);
                IntPtr originalBitmapPixelPointer = originalBitmapData.Scan0;
                Marshal.Copy(originalBitmapPixelPointer, newBitmapPixelData, 0, StrideWidth * LengthHeight);
                original.UnlockBits(originalBitmapData);

                var bitmapBoundsRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(bitmapBoundsRect,
                                            ImageLockMode.ReadOnly,
                                            bitmap.PixelFormat);
                IntPtr bitmapPixelPointer = bitmapData.Scan0;
                int bitmapTotalBytes = Math.Max(0, Math.Min(
                    bitmapData.Stride * bitmap.Height,
                    HexStorage.Memory.Length - offset
                ));
                var bitmapPixelData = new byte[bitmapTotalBytes];
                Marshal.Copy(bitmapPixelPointer, bitmapPixelData, 0, bitmapTotalBytes);
                bitmap.UnlockBits(bitmapData);

                for (int i = 0; i < Sprite.Assembly.Length; i++)
                {
                    var assembly = Sprite.Assembly[i];

                    newBitmapPixelData = BitmapHandler.CutBmpRegion(
                        new Canvas()
                        {
                            Stride = bitmapData.Stride,
                            StrideTarget = newBitmapData.Stride,
                            Height = assembly.Source.Height * Sprite.Size.Height,
                            Width = assembly.Source.Width * Sprite.Size.Width
                        },
                        bitmapPixelData,
                        new Rectangle(
                            assembly.Destination.X * Sprite.Size.Width,
                            assembly.Destination.Y * Sprite.Size.Height,
                            assembly.Source.Width * Sprite.Size.Width,
                            assembly.Source.Height * Sprite.Size.Height
                        ),
                        new Point(
                            assembly.Source.X * Sprite.Size.Width,
                            assembly.Source.Y * Sprite.Size.Height
                        ),
                        newBitmapPixelData
                    );
                }

                Marshal.Copy(newBitmapPixelData, 0, newBitmapPixelPointer, newBitmapTotalBytes);
                newBitmap.UnlockBits(newBitmapData);
            }
            else
            {
                newBitmap = bitmap;
            }

            if (offset < 0) offset = HexUtility.GetHexOffset();

            var boundsRect = new Rectangle(0, 0, newBitmap.Width, newBitmap.Height);
            BitmapData bmpData = newBitmap.LockBits(boundsRect,
                                            ImageLockMode.WriteOnly,
                                            newBitmap.PixelFormat);
            bmpData.Stride = StrideWidth;
            IntPtr pixelPointer = bmpData.Scan0;

            int totalBytes = Math.Max(0, Math.Min(
                bmpData.Stride * newBitmap.Height,
                HexStorage.Memory.Length - offset
            ));
            var rgbValues = new byte[totalBytes];

            Marshal.Copy(pixelPointer, rgbValues, 0, totalBytes);
            newBitmap.UnlockBits(bmpData);

            if (BitFormats.Instance.Implemented.ContainsKey(Encoding))
            {
                var codec = BitFormats.Instance.Implemented[Encoding];
                rgbValues = codec.Encode(rgbValues);
            }

            if (Compression == CompressionType.LZ77_RLE)
            {
                rgbValues = HexCompressions.Instance.Implemented[Compression].Compress(rgbValues, out _).ToArray();

                //var testCompressed = HexConvert.BytesToHex(rgbValues, -1, rgbValues.Length + 1, " ");
            }

            if(LastOffsets.Length <= 1 || rgbValues.Length < LastOffsets[1])
            {
                if(LastOffsets.Length > 1) Array.Resize(ref rgbValues, LastOffsets[1]);
                MemoryLiterator.Write(this, new MemoryOperation(offset, rgbValues));
            }
            else if (ImportConflict != null)
            {
                var operation = new MemoryOperation(offset, rgbValues);
                ImportConflict.Invoke(this, new MemoryConflict(operation, LastOffsets[1]));
            }
        }

        private void GetFeatures()
        {
            if (Features.HasFlag(HexImageFeatureType.Export))
            {
                ContextMenuStrip.Items.Add("Export", Resources.Export, Export);
            }
            if (Features.HasFlag(HexImageFeatureType.Import))
            {
                ContextMenuStrip.Items.Add("Import", Resources.Import, Import);
            }
            if(Features.HasFlag(HexImageFeatureType.Previewer))
            {
                ContextMenuStrip.Items.Add("View Memory", Resources.Preview, ViewMemory);
            }
            if (!Single)
            {
                ContextMenuStrip.Items.Add("Find Spritesheet", Resources.Edit, ViewAddresses);
            }

            AllowDrop = Features.HasFlag(HexImageFeatureType.DragAndDrop);
        }

        private void ReplaceBitmap(string fileName)
        {
            var newImage = BitmapHandler.LoadBitmap(fileName);
            ReplaceBitmap(newImage);
        }

        private void ReplaceBitmap(Bitmap newBitmap)
        {
            if (newBitmap.PixelFormat != Unscaled.PixelFormat)
            {
                ShowError($"The provided image must be a {BasicHelper.ToTitleCase(Unscaled.PixelFormat.ToString())} format.");
                return;
            }
            else if (!(BitmapHandler.CompareSize(newBitmap, Unscaled as Bitmap)))
            {
                ShowError("The imported image must be the same dimensions as the image being replaced!");
                return;
            }

            GetZoom(ref newBitmap);
            Image = newBitmap;

            Save();
            Load();
        }

        #endregion

        #region Public

        private bool LoadedValue = false;

        public new void Load()
        {
            LoadedValue = true;

            ContextMenuStrip = new ContextMenuStrip();
            
            if (Width == 0 || Height == 0 || !MemoryLiterator.HaveData) return;

            GetFeatures();

            if (ListBox == null)
            {
                ListBox = HexTools.HexUtility.GetListBox(this);
            }

            NewBitmap(out var bitmap);

            GetPalette(ref bitmap);

            GetPixels(ref bitmap);

            GetZoom(ref bitmap);

            Image = bitmap;
        }

        public void Save(int Offset = -1)
        {
            var newImage = Unscaled as Bitmap;

            SetPixels(ref newImage, Offset);
        }

        public void ViewMemory(object sender, EventArgs args)
        {
            new Editor_ImagePreview().ShowAsTool(Original);
        }

        public void ViewAddresses(object sender, EventArgs args)
        {
            var result = new Editor_AddressPicker()
                .ShowAsTool(LastOffsets.Select(x => new MemoryChunk(HexConvert.HexToInt(HexOffset) + x, 0)).ToArray())
                .Selected.Address;
            if(result >= 0)
            {
                HexOffset = HexConvert.IntToHex((int) result, 5);
                ViewAddress?.Invoke(this, new EventArgs());
            }
        }

        public void Export(object sender, EventArgs args)
        {
            var exportDialog = new SaveFileDialog();
            exportDialog.Filter = "Bitmap Image|*.bmp|Portable Network Graphics|*.png|Tagged Image File Format|*.tiff|Graphics Interchange Format|*.gif";
            exportDialog.FilterIndex = 2;
            exportDialog.Title = "Saves an Image File";
            if (exportDialog.ShowDialog() == DialogResult.OK)
            {
                switch (exportDialog.FilterIndex)
                {
                    case 1:
                        Unscaled.Save(exportDialog.FileName, ImageFormat.Bmp);
                        break;
                    case 2:
                        Unscaled.Save(exportDialog.FileName, ImageFormat.Png);
                        break;
                    case 3:
                        Unscaled.Save(exportDialog.FileName, ImageFormat.Tiff);
                        break;
                    case 4:
                        Unscaled.Save(exportDialog.FileName, ImageFormat.Gif);
                        break;
                }
            }
        }

        public void Import(object sender, EventArgs args)
        {
            var importDialog = new OpenFileDialog();
            importDialog.Filter = "Bitmap Image|*.bmp|Portable Network Graphics|*.png|Tagged Image File Format|*.tiff|Graphics Interchange Format|*.gif"
                + "|All Graphics Types|*.bmp;*.png;*.tiff;*.gif";
            importDialog.FilterIndex = 5;
            importDialog.Title = "Loads an Image File";
            if (importDialog.ShowDialog() == DialogResult.OK)
            {
                ReplaceBitmap(importDialog.FileName);
            }
        }

        private static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static Bitmap PixelPerfectZoom(Image original, SizeF scale)
        {
            if (original == null) return original as Bitmap;
            
            Bitmap newimage = new Bitmap((int)(original.Width * scale.Width), (int)(original.Height * scale.Height));

            using (Graphics g = Graphics.FromImage(newimage))
            {
                // Makes the new image pixel perfect
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                // Scale the image, by drawing it on the larger bitmap
                g.DrawImage(original, new Rectangle(Point.Empty, newimage.Size));
            }

            return newimage;
        }

        #endregion
    }

    #endregion

    #region BitmapHandler

    public static class Crc32
    {
        static uint[] table;

        public static uint ComputeChecksum(byte[] bytes, int startIndex = 0, int length = -1)
        {
            uint crc = 0xffffffff;
            if (length < 0) length = bytes.Length;

            for (int i = startIndex; i < length; ++i)
            {
                byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
                crc = (uint)((crc >> 8) ^ table[index]);
            }
            return ~crc;
        }

        public static byte[] ComputeChecksumBytes(byte[] bytes)
        {
            return BitConverter.GetBytes(ComputeChecksum(bytes));
        }

        static Crc32()
        {
            uint poly = 0xedb88320;
            table = new uint[256];
            uint temp = 0;
            for (uint i = 0; i < table.Length; ++i)
            {
                temp = i;
                for (int j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (uint)((temp >> 1) ^ poly);
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }
        }
    }

    /// <summary>
    /// Image loading toolset class which corrects the bug that prevents paletted PNG images with transparency from being loaded as paletted.
    /// https://learn.microsoft.com/en-us/previous-versions/dotnet/articles/aa479306(v=msdn.10)?redirectedfrom=MSDN
    /// http://www.libpng.org/pub/png/spec/1.2/PNG-Chunks.html
    /// </summary>
    public class BitmapHandler
    {
        private static byte[] PNG_IDENTIFIER = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        /// <summary>
        /// Writes a png data chunk.
        /// </summary>
        /// <param name="target">Target array to write into.</param>
        /// <param name="offset">Offset in the array to write the data to.</param>
        /// <param name="chunkName">4-character chunk name.</param>
        /// <param name="chunkData">Data to write into the new chunk.</param>
        /// <returns>The new offset after writing the new chunk. Always equal to the offset plus the length of chunk data plus 12.</returns>
        private static int WritePngChunk(byte[] target, int offset, string chunkName, byte[] chunkData)
        {
            if (offset + chunkData.Length + 12 > target.Length)
                throw new ArgumentException("Data does not fit in target array!", "chunkData");
            if (chunkName.Length != 4)
                throw new ArgumentException("Chunk must be 4 characters!", "chunkName");
            byte[] chunkNamebytes = Encoding.ASCII.GetBytes(chunkName);
            if (chunkNamebytes.Length != 4)
                throw new ArgumentException("Chunk must be 4 bytes!", "chunkName");
            int curLength;
            WriteIntToByteArray(target, offset, curLength = 4, false, (uint)chunkData.Length);
            offset += curLength;
            int nameOffset = offset;
            Array.Copy(chunkNamebytes, 0, target, offset, curLength = 4);
            offset += curLength;
            Array.Copy(chunkData, 0, target, offset, curLength = chunkData.Length);
            offset += curLength;
            uint crcval = Crc32.ComputeChecksum(target, nameOffset, chunkData.Length + 4);
            WriteIntToByteArray(target, offset, curLength = 4, false, crcval);
            offset += curLength;
            return offset;
        }

        public static void WriteIntToByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian, UInt32 value)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to write a " + bytes + "-byte value at offset " + startIndex + ".");
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                data[offs] = (Byte)(value >> (8 * index) & 0xFF);
            }
        }

        public static UInt32 ReadIntFromByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to read a " + bytes + "-byte value at offset " + startIndex + ".");
            UInt32 value = 0;
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                value += (UInt32)(data[offs] << (8 * index));
            }
            return value;
        }

        public static Bitmap LoadBitmap(string fileName)
        {
            return LoadBitmap(File.ReadAllBytes(fileName));
        }

        /// <summary>
        /// Loads an image, checks if it is a PNG containing palette transparency, and if so, ensures it loads correctly.
        /// The theory on the png internals can be found at http://www.libpng.org/pub/png/book/chapter08.html
        /// </summary>
        /// <param name="data">File data to load.</param>
        /// <returns>The loaded image.</returns>
        public static Bitmap LoadBitmap(Byte[] data)
        {
            Byte[] transparencyData = null;
            if (data.Length > PNG_IDENTIFIER.Length)
            {
                // Check if the image is a PNG.
                Byte[] compareData = new Byte[PNG_IDENTIFIER.Length];
                Array.Copy(data, compareData, PNG_IDENTIFIER.Length);
                if (PNG_IDENTIFIER.SequenceEqual(compareData))
                {
                    // Check if it contains a palette.
                    // I'm sure it can be looked up in the header somehow, but meh.
                    Int32 plteOffset = FindChunk(data, "PLTE");
                    if (plteOffset != -1)
                    {
                        // Check if it contains a palette transparency chunk.
                        Int32 trnsOffset = FindChunk(data, "tRNS");
                        if (trnsOffset != -1)
                        {
                            // Get chunk
                            Int32 trnsLength = GetChunkDataLength(data, trnsOffset);
                            transparencyData = new Byte[trnsLength];
                            Array.Copy(data, trnsOffset + 8, transparencyData, 0, trnsLength);
                            // filter out the palette alpha chunk, make new data array
                            Byte[] data2 = new Byte[data.Length - (trnsLength + 12)];
                            Array.Copy(data, 0, data2, 0, trnsOffset);
                            Int32 trnsEnd = trnsOffset + trnsLength + 12;
                            Array.Copy(data, trnsEnd, data2, trnsOffset, data.Length - trnsEnd);
                            data = data2;
                        }
                    }
                }
            }
            using (MemoryStream ms = new MemoryStream(data))
            using (Bitmap loadedImage = new Bitmap(ms))
            {
                if (loadedImage.Palette.Entries.Length != 0 && transparencyData != null)
                {
                    ColorPalette pal = loadedImage.Palette;
                    for (int i = 0; i < pal.Entries.Length; i++)
                    {
                        if (i >= transparencyData.Length)
                            break;
                        Color col = pal.Entries[i];
                        pal.Entries[i] = Color.FromArgb(transparencyData[i], col.R, col.G, col.B);
                    }
                    loadedImage.Palette = pal;
                }
                // Images in .Net often cause odd crashes when their backing resource disappears.
                // This prevents that from happening by copying its inner contents into a new Bitmap object.
                return CloneImage(loadedImage);
            }
        }

        /// <summary>
        /// Finds the start of a png chunk. This assumes the image is already identified as PNG.
        /// It does not go over the first 8 bytes, but starts at the start of the header chunk.
        /// </summary>
        /// <param name="data">The bytes of the png image.</param>
        /// <param name="chunkName">The name of the chunk to find.</param>
        /// <returns>The index of the start of the png chunk, or -1 if the chunk was not found.</returns>
        private static Int32 FindChunk(Byte[] data, String chunkName)
        {
            if (data == null)
                throw new ArgumentNullException("data", "No data given!");
            if (chunkName == null)
                throw new ArgumentNullException("chunkName", "No chunk name given!");
            // Using UTF-8 as extra check to make sure the name does not contain > 127 values.
            Byte[] chunkNamebytes = Encoding.UTF8.GetBytes(chunkName);
            if (chunkName.Length != 4 || chunkNamebytes.Length != 4)
                throw new ArgumentException("Chunk name must be 4 ASCII characters!", "chunkName");
            Int32 offset = PNG_IDENTIFIER.Length;
            Int32 end = data.Length;
            Byte[] testBytes = new Byte[4];
            // continue until either the end is reached, or there is not enough space behind it for reading a new chunk
            while (offset + 12 < end)
            {
                Array.Copy(data, offset + 4, testBytes, 0, 4);
                if (chunkNamebytes.SequenceEqual(testBytes))
                    return offset;
                Int32 chunkLength = GetChunkDataLength(data, offset);
                // chunk size + chunk header + chunk checksum = 12 bytes.
                offset += 12 + chunkLength;
            }
            return -1;
        }

        private static Int32 GetChunkDataLength(Byte[] data, Int32 offset)
        {
            if (offset + 4 > data.Length)
                throw new IndexOutOfRangeException("Bad chunk size in png image.");
            // Don't want to use BitConverter; then you have to check platform endianness and all that mess.
            Int32 length = data[offset + 3] + (data[offset + 2] << 8) + (data[offset + 1] << 16) + (data[offset] << 24);
            if (length < 0)
                throw new IndexOutOfRangeException("Bad chunk size in png image.");
            return length;
        }

        /// <summary>
        /// Clones an image object to free it from any backing resources.
        /// Code taken from http://stackoverflow.com/a/3661892/ with some extra fixes.
        /// </summary>
        /// <param name="sourceImage">The image to clone.</param>
        /// <returns>The cloned image.</returns>
        public static Bitmap CloneImage(Bitmap sourceImage)
        {
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            Bitmap targetImage = new Bitmap(rect.Width, rect.Height, sourceImage.PixelFormat);
            targetImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
            BitmapData sourceData = sourceImage.LockBits(rect, ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            BitmapData targetData = targetImage.LockBits(rect, ImageLockMode.WriteOnly, targetImage.PixelFormat);
            Int32 actualDataWidth = ((Image.GetPixelFormatSize(sourceImage.PixelFormat) * rect.Width) + 7) / 8;
            Int32 h = sourceImage.Height;
            Int32 origStride = sourceData.Stride;
            Int32 targetStride = targetData.Stride;
            Byte[] imageData = new Byte[actualDataWidth];
            IntPtr sourcePos = sourceData.Scan0;
            IntPtr destPos = targetData.Scan0;
            // Copy line by line, skipping by stride but copying actual data width
            for (Int32 y = 0; y < h; y++)
            {
                Marshal.Copy(sourcePos, imageData, 0, actualDataWidth);
                Marshal.Copy(imageData, 0, destPos, actualDataWidth);
                sourcePos = new IntPtr(sourcePos.ToInt64() + origStride);
                destPos = new IntPtr(destPos.ToInt64() + targetStride);
            }
            targetImage.UnlockBits(targetData);
            sourceImage.UnlockBits(sourceData);
            // For indexed images, restore the palette. This is not linking to a referenced
            // object in the original image; the getter of Palette creates a new object when called.
            if ((sourceImage.PixelFormat & PixelFormat.Indexed) != 0)
                targetImage.Palette = sourceImage.Palette;
            // Restore DPI settings
            targetImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
            return targetImage;
        }

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public static bool CompareSize(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride1 = bd1.Stride;
                int len1 = stride1 * b1.Height;
                int stride2 = bd2.Stride;
                int len2 = stride2 * b2.Height;

                return len1 == len2;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        public static bool CompareMemory(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride1 = bd1.Stride;
                int len1 = stride1 * b1.Height;
                int stride2 = bd2.Stride;
                int len2 = stride2 * b2.Height;

                return len1 == len2 && memcmp(bd1scan0, bd2scan0, len1) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        public static void CropBmpRegion(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap)
        {
            if(srcRegion.Width > srcBitmap.Width || destBitmap.Width > srcBitmap.Width
                || srcRegion.Height > srcBitmap.Height || destBitmap.Height > srcBitmap.Height)
            {
                //throw new Exception("Will run out of memory!");
                return;
            }
            destBitmap = srcBitmap.Clone(srcRegion, srcBitmap.PixelFormat);
        }

        public static void CopyBmpRegion(Bitmap image, Rectangle srcRect, Point destLocation)
        {
            //do some argument sanitising.
            if (!((srcRect.X >= 0 && srcRect.Y >= 0) && ((srcRect.X + srcRect.Width) <= image.Width) && ((srcRect.Y + srcRect.Height) <= image.Height)))
                throw new ArgumentException("Source rectangle isn't within the image bounds.");

            if ((destLocation.X < 0 || destLocation.X > image.Width) || (destLocation.Y < 0 || destLocation.Y > image.Height))
                throw new ArgumentException("Destination must be within the image.");

            // Lock the bits into memory
            BitmapData bmpData = image.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadWrite, image.PixelFormat);
            int pxlSize = (bmpData.Stride / bmpData.Width); //calculate the pixel width (in bytes) of the current image.
            int src = 0; int dest = 0; //source/destination pixels.

            //account for the fact that not all of the source rectangle may be able to copy into the destination:
            int width = (destLocation.X + srcRect.Width) <= image.Width ? srcRect.Width : (image.Width - (destLocation.X + srcRect.Width));
            int height = (destLocation.Y + srcRect.Height) <= image.Height ? srcRect.Height : (image.Height - (destLocation.Y + srcRect.Height));

            //managed buffer to hold the current pixel data.
            byte[] buffer = new byte[pxlSize];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //calculate the start of the current source pixel and destination pixel.
                    src = ((srcRect.Y + y) * bmpData.Stride) + ((srcRect.X + x) * pxlSize);
                    dest = ((destLocation.Y + y) * bmpData.Stride) + ((destLocation.X + x) * pxlSize);

                    // Can replace this with unsafe code, but that's up to you.
                    Marshal.Copy(new IntPtr(bmpData.Scan0.ToInt32() + src), buffer, 0, pxlSize);
                    Marshal.Copy(buffer, 0, new IntPtr(bmpData.Scan0.ToInt32() + dest), pxlSize);
                }
            }

            image.UnlockBits(bmpData); //unlock the data.
        }

        public static byte[] CutBmpRegion(Canvas canvas, byte[] pixelData, Rectangle srcRect, Point destLocation, byte[] output = null)
        {
            int pxlSize = (canvas.Stride / canvas.Width); //calculate the pixel width (in bytes) of the current image.
            int src = 0; int dest = 0; //source/destination pixels.

            //account for the fact that not all of the source rectangle may be able to copy into the destination:
            int width = (destLocation.X + srcRect.Width) <= canvas.Width ? srcRect.Width : (canvas.Width - (destLocation.X + srcRect.Width));
            int height = (destLocation.Y + srcRect.Height) <= canvas.Height ? srcRect.Height : (canvas.Height - (destLocation.Y + srcRect.Height));
            width = canvas.Width;
            height = canvas.Height;

            //managed buffer to hold the current pixel data.
            byte[] buffer = new byte[pxlSize];

            //managed buffer to hold the output of the new pixel data.
            if(output == null)
            {
                output = new byte[pixelData.Length];
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //calculate the start of the current source pixel and destination pixel.
                    src = ((srcRect.Y + y) * canvas.Stride) + ((srcRect.X + x) * pxlSize);
                    dest = ((destLocation.Y + y) * (canvas.StrideTarget ?? canvas.Stride)) + ((destLocation.X + x) * pxlSize);
                    
                    Array.Copy(pixelData, src, buffer, 0, pxlSize);
                    Array.Copy(buffer, 0, output, dest, pxlSize);
                }
            }

            return output;
        }
    }

    public struct Canvas
    {
        public int Stride { get; set; }
        public int? StrideTarget { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    #endregion
}
