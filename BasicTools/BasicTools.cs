using AnyClone;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace BasicTools
{
    #region Utilities

    public static class DynamicUtility
    {
        public static T SafeAccess<T>(Func<T> action, T @default)
        {
            try
            {
                return action();
            }
            catch (RuntimeBinderException)
            {
                return @default;
            }
        }
    }

    #endregion

    #region  Extensions 

    #region  Object 

    public static class ObjectExtension
    {
        public static IReadOnlyCollection<Form> AllForms { get; set; }

        public static T Clone<T>(this T obj)
        {
            return CloneExtensions.Clone(obj);
        }

        public static bool IsDerivedFrom(this object objSource, object objTarget)
        {
            return objSource.GetType().IsInstanceOfType(objTarget);
        }

        public static bool IsSubclassedFrom<T>(this object objSource, object objTarget)
        {
            return objSource.GetType().IsSubclassOf(objTarget.GetType());
        }

        public static bool IsIdenticalTo(this object objSource, object objTarget)
        {
            return ReferenceEquals(objSource, objTarget);
        }

        public static bool HasMethod(this object obj, string MethodName)
        {
            var type = obj.GetType();
            return type.GetMethod(MethodName) != null;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        private static IReadOnlyCollection<Form> _FormsCache;
        public static IReadOnlyCollection<Form> GetForms(this object obj)
        {
            if(AllForms != null)
            {
                return AllForms;
            }
            if(_FormsCache == null)
            {
                var myAssembly = Assembly.GetEntryAssembly();
                Type[] asmTypes = myAssembly.GetTypes();
                _FormsCache = (from t in asmTypes
                               where t.IsSubclassOf(typeof(Form)) && !t.IsAbstract
                               let f = (Form)Activator.CreateInstance(t)
                               select f).ToList().AsReadOnly();
            }
            return _FormsCache;
        }

        public static Form GetFormByName(this object obj, string name)
        {
            IReadOnlyCollection<Form> allForms = obj.GetForms();
            object ctrl = allForms.FindByName(name);
            return (Form)ctrl;
        }

        public static object GetControlByFullName(this object obj, string fullname)
        {
            string[] namespaces = fullname.Split('.');
            dynamic myObj = obj;
            dynamic ctrl = obj.GetFormByName(namespaces[0]);
            if (ctrl == null)
                throw new Exception(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Combobox: ", myObj.Name), " has an invalid Form reference.")));
            for (int Index = 1, loopTo = namespaces.Length - 1; Index <= loopTo; Index++)
            {
                ctrl = ctrl.Controls[namespaces[Index]];
                if (ctrl == null)
                    throw new Exception(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Combobox: ", myObj.Name), " has an invalid Control reference.")));
            }
            return ctrl;
        }

        private static UserControl[] _UserControlsCache;
        public static UserControl[] GetUserControls(this object obj)
        {
            if(_UserControlsCache == null)
            {
                var myAssembly = Assembly.GetEntryAssembly();
                Type[] asmTypes = myAssembly.GetTypes();
                _UserControlsCache = (from t in asmTypes
                                      where t.IsSubclassOf(typeof(UserControl)) && !t.IsAbstract
                                      let f = (UserControl)Activator.CreateInstance(t)
                                      select f).ToArray();
            }
            return _UserControlsCache;
        }

        public static UserControl GetUserControlByName(this object obj, string name)
        {
            UserControl[] allControls = obj.GetUserControls();
            object ctrl = allControls.FindByName(name);
            return (UserControl)ctrl;
        }

        public static object GetControlByName(this object obj, string name)
        {
            string[] namespaces = name.Split('.');
            dynamic myObj = obj;
            dynamic ctrl = obj.GetUserControlByName(namespaces[0]);
            if (ctrl == null)
                throw new Exception(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Combobox: ", myObj.Name), " has an invalid UserControl reference.")));
            for (int Index = 1, loopTo = namespaces.Length - 1; Index <= loopTo; Index++)
            {

                ctrl = DynamicUtility.SafeAccess(() => ctrl.Controls[namespaces[Index]], null);
                if (ctrl == null)
                    throw new Exception(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("Combobox: ", myObj.Name), " has an invalid Control reference.")));
            }
            return ctrl;
        }

    }

    #endregion

    #region  Array 

    public static class ArrayExtension
    {

        /// <summary>
        /// Validates that the specified <paramref name="startIndex"/> and <paramref name="length"/> are valid within the given <paramref name="array"/>.
        /// </summary>
        /// <param name="array">Array to validate.</param>
        /// <param name="startIndex">0-based start index into the <paramref name="array"/>.</param>
        /// <param name="length">Valid number of items within <paramref name="array"/> from <paramref name="startIndex"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> or <paramref name="length"/> is less than 0 -or- 
        /// <paramref name="startIndex"/> and <paramref name="length"/> will exceed <paramref name="array"/> length.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValidateParameters<T>(this T[] array, int startIndex, int length)
        {
            if ((object)array == null || startIndex < 0 || length < 0 || startIndex + length > array.Length)
                RaiseValidationError(array, startIndex, length);
        }

        // This method will raise the actual error - this is needed since .NET will not inline anything that might throw an exception
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void RaiseValidationError<T>(T[] array, int startIndex, int length)
        {
            if ((object)array == null)
                throw new ArgumentNullException(nameof(array));

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "cannot be negative");

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "cannot be negative");

            if (startIndex + length > array.Length)
                throw new ArgumentOutOfRangeException(nameof(length), $"startIndex of {startIndex} and length of {length} will exceed array size of {array.Length}");
        }

        public static Form FindByName(this IReadOnlyCollection<Form> ArrayContainer, string Name)
        {
            return Array.Find(ArrayContainer.ToArray(), item => (item.Name.ToLower() ?? "") == (Name.ToLower() ?? ""));
        }

        public static Form FindByName(this FormCollection ArrayContainer, string Name)
        {
            foreach (Form item in ArrayContainer)
            {
                if ((item.Name ?? "") == (Name ?? ""))
                    return item;
            }
            return null;
        }

        public static UserControl FindByName(this UserControl[] ArrayContainer, string Name)
        {
            return Array.Find(ArrayContainer, item => (item.Name.ToLower() ?? "") == (Name.ToLower() ?? ""));
        }

        public static string[] NoDuplicates(this string[] values)
        {
            return (from value in (from value in values
                                   select value).Distinct()
                    orderby value
                    select value).ToArray();
        }

        public static int[] NoDuplicates(this int[] values)
        {
            return (from value in (from value in values
                                   select value).Distinct()
                    orderby value
                    select value).ToArray();
        }

        const int ARRAY_COPY_THRESHOLD = 32;  // 16 ... 64 work equally well for all tested constellations
        const int L1_CACHE_SIZE = 1 << 15;

        public static T[] Fill<T>(this T[] array, int count, T value, int element_size)
        {
            int current_size = 0, keep_looping_up_to = Math.Min(count, ARRAY_COPY_THRESHOLD);

            while (current_size < keep_looping_up_to)
                array[current_size++] = value;

            int block_size = L1_CACHE_SIZE / element_size / 2;
            int keep_doubling_up_to = Math.Min(block_size, count >> 1);

            for (; current_size < keep_doubling_up_to; current_size <<= 1)
                Array.Copy(array, 0, array, current_size, current_size);

            for (int enough = count - block_size; current_size < enough; current_size += block_size)
                Array.Copy(array, 0, array, current_size, block_size);

            Array.Copy(array, 0, array, current_size, count - current_size);

            return array;
        }
    }

    #endregion

    #region Collection

    public static class CollectionExtension
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is List<T> list)
            {
                list.AddRange(items);
            }
            else
            {
                throw new NotSupportedException($"Type {collection?.GetType()?.Name} does not support AddRange.");
            }
        }

        public static void Insert<T>(this ICollection<T> collection, int index, T item)
        {
            if (index < 0 || index > collection.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range. Must be non-negative and less than the size of the collection.");

            if (collection is IList<T> list)
            {
                list.Insert(index, item);
            }
            else
            {
                throw new NotSupportedException($"Type {collection?.GetType()?.Name} does not support Insert.");
            }
        }

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int count)
        {
            return source.Skip(Math.Max(0, source.Count() - count));
        }
    }

    #endregion

    #region Enum

    public static class EnumExtension
    {
        public static string DisplayName(this Enum enumType)
        {
            var display = enumType.GetType().GetMember(enumType.ToString())
                           .FirstOrDefault().GetCustomAttribute<DisplayAttribute>();
            return display
                != null ? display.Name
                : enumType.ToString();
        }

        public static bool HasDisplayGroup(this Enum enumType, string groupName)
        {
            if (string.IsNullOrEmpty(groupName)) return true;

            var display = enumType.GetType().GetMember(enumType.ToString())
                           .FirstOrDefault().GetCustomAttribute<DisplayAttribute>();
            return display
                != null ? display.GroupName == groupName
                : true;
        }
    }

    #endregion

    #region  String 

    [Flags]
    public enum EllipsisFormat
    {
        None = 0,
        End = 1,
        Start = 2,
        Middle = 3
    }

    public static class StringExtension
    {

        public static int GetInt(this string str)
        {
            string input = str;
            var reg = new Regex("[^0-9]");
            input = reg.Replace(input, "");
            int output;
            int.TryParse(input, out output);
            return output;
        }

        public static string ReplaceAt(this string str, int index, string newStr)
        {
            return str.Remove(index, Math.Min(newStr.Length, str.Length - index)).Insert(index, newStr);
        }

        public static string Ellipsis(this string Text, Control Control, EllipsisFormat Options = EllipsisFormat.End, string Chars = "...")
        {
            using (var dc = Control.CreateGraphics())
            {
                var s = TextRenderer.MeasureText(dc, Text, Control.Font);
                if (s.Width <= Control.Width)
                    return Text;
                int len = 0;
                int seg = Text.Length;
                string fit = "";

                while (seg > 1)
                {
                    seg = (int)Math.Round(seg - seg / 2d);
                    int left = len + seg;
                    int right = Text.Length;
                    if (left > right)
                        continue;

                    if ((EllipsisFormat.Middle & Options) == EllipsisFormat.Middle)
                    {
                        right = (int)Math.Round(right - left / 2d);
                        left = (int)Math.Round(left - left / 2d);
                    }
                    else if ((EllipsisFormat.Start & Options) != 0)
                    {
                        right -= left;
                        left = 0;
                    }

                    string tst = Text.Substring(0, left) + Chars + Text.Substring(right);
                    s = TextRenderer.MeasureText(dc, tst, Control.Font);

                    if (s.Width <= Control.Width)
                    {
                        len += seg;
                        fit = tst;
                    }
                }

                if (len == 0)
                {
                    return Chars;
                }

                return fit;
            }
        }

        public static string[] Chunk(this string text, int size)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < text.Length; i += size)
            {
                // Handles odd-length strings gracefully by taking remaining length
                int length = Math.Min(size, text.Length - i);
                result.Add(text.Substring(i, length));
            }

            return result.ToArray();
        }

    }

    #endregion

    #region  Form 

    public static class FormExtension
    {

        /// <summary>
    /// Recursively find all child controls for a form
    /// </summary>
    /// <param name="StartingContainer"><c><seealso cref="System.Windows.Forms.Form">Form
    /// </seealso></c> that is the starting container to check for children.</param>
    /// <returns><c><seealso cref="List(Of System.Windows.Forms.Control)">List(Of Control)
    /// </seealso></c> that contains a reference to all child controls.</returns>
    /// <remarks>If you put this module in a separate namespace from your form, Visual Studio 
    /// 2010 does not recognize the extension to the form.</remarks>
        public static List<Control> FindAllChildren(ref Form StartingContainer)
        {
            var Children = new List<Control>();
            foreach (Control oControl in StartingContainer.Controls)
            {
                Children.Add(oControl);
                if (oControl.HasChildren)
                {
                    var myControl = oControl;
                    Children.AddRange(ControlExtension.FindAllChildren(ref myControl));
                }
            }

            return Children;
        }

        public static List<object> FindAllChildren(ref Form StartingContainer, string CheckedType)
        {
            var Children = new List<object>();
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if ((child.GetType().Name ?? "") == (CheckedType ?? ""))
                    Children.Add(child);
            }
            return Children;
        }

        public static Form FindMasterParent(ref Control StartingContainer)
        {
            var parent = StartingContainer;
            var nextparent = parent.Parent;
            while (!(nextparent == null))
            {
                if (parent.Parent != null)
                    parent = parent.Parent;
                nextparent = parent.Parent;
            }
            return (Form)parent;
        }

        public static Control GetFocusedControl(ref Form StartingContainer)
        {
            Control ctrl = StartingContainer;
            ContainerControl container = StartingContainer as ContainerControl;
            while (container != null)
            {
                ctrl = container.ActiveControl;
                container = ctrl as ContainerControl;
            }
            return ctrl;
        }

        public static DialogResult ShowFormAsTool(this Form Form, bool resizable = false)
        {
            Form.FormBorderStyle = resizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
            Form.MinimizeBox = false;
            Form.MaximizeBox = resizable;
            Form.ShowInTaskbar = false;
            return Form.ShowDialog();
        }

    }

    #endregion

    #region  Control 

    public static class ControlExtension
    {
        public static Rectangle GetBoundsRelativeToForm(this Control c)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c));

            var form = c.FindForm();
            if (form == null)
                throw new InvalidOperationException("The control is not located on a form.");

            var parent = c.Parent;
            if (parent == null)
                throw new InvalidOperationException("The control does not have a parent.");

            var p = form.PointToClient(parent.PointToScreen(c.Location));
            return new Rectangle(p, c.Size);
        }

        /// <summary>
        /// Recursively find all child controls for a control
        /// </summary>
        /// <param name="StartingContainer"><c><seealso cref="System.Windows.Forms.Control">Control
        /// </seealso></c> that is the starting container to check for children.</param>
        /// <returns><c><seealso cref="List(Of System.Windows.Forms.Control)">List(Of Control)
        /// </seealso></c> that contains a reference to all child controls.</returns>
        /// <remarks></remarks>
        public static List<Control> FindAllChildren(ref Control StartingContainer)
        {
            var Children = new List<Control>();

            if (StartingContainer.HasChildren == false)
            {
                return null;
            }
            else
            {
                foreach (Control oControl in StartingContainer.Controls)
                {
                    Children.Add(oControl);
                    if (oControl.HasChildren)
                    {
                        var myControl = oControl;
                        Children.AddRange(FindAllChildren(ref myControl));
                    }
                }
            }

            return Children;
        }

        public static List<object> FindAllChildren(ref Control StartingContainer, string CheckedType)
        {
            var Children = new List<object>();
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if ((child.GetType().Name ?? "") == (CheckedType ?? ""))
                    Children.Add(child);
            }
            return Children;
        }

        public static List<T> FindAllChildren<T>(ref Control StartingContainer) where T : Control
        {
            var Children = new List<T>();
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if (child is T)
                {
                    T typedChild = (T)child;
                    Children.Add(typedChild);
                }
            }
            return Children;
        }

        public static bool HasChildOfType(ref Control StartingContainer, string CheckedType)
        {
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if ((Information.TypeName(child) ?? "") == (CheckedType ?? ""))
                    return true;
            }
            return false;
        }

        public static Control GetChildOfType(ref Control StartingContainer, string CheckedType)
        {
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if ((Information.TypeName(child) ?? "") == (CheckedType ?? ""))
                    return child;
            }
            return null;
        }

        public static Control GetChildByType<T>(ref Control StartingContainer)
        {
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if (child is T)
                    return child;
            }
            return null;
        }

        public static Control GetChildByName(ref Control StartingContainer, string CheckedName)
        {
            foreach (Control child in FindAllChildren(ref StartingContainer))
            {
                if ((child.Name ?? "") == (CheckedName ?? ""))
                    return child;
            }
            return null;
        }

        public static List<object> FindAllParents(ref Control StartingContainer)
        {
            var Parents = new List<object>();
            var Parent = StartingContainer;
            while (!(Parent == null))
            {
                Parents.Add(Parent);
                Parent = Parent.Parent;
            }
            return Parents;
        }

        public static List<object> FindAllParents(ref Control StartingContainer, string CheckedType)
        {
            var Parents = new List<object>();
            var Parent = StartingContainer;
            while (!(Parent == null))
            {
                if ((Information.TypeName(Parent) ?? "") == (CheckedType ?? ""))
                    Parents.Add(Parent);
                Parent = Parent.Parent;
            }
            return Parents;
        }

        public static object FindParent(ref Control StartingContainer)
        {
            var Parent = StartingContainer;
            Parent = Parent.Parent;
            return null;
        }

        public static object FindParent(ref Control StartingContainer, string CheckedType)
        {
            var Parent = StartingContainer;
            while (!(Parent == null))
            {
                if ((Information.TypeName(Parent) ?? "") == (CheckedType ?? ""))
                    return Parent;
                Parent = Parent.Parent;
            }
            return null;
        }

        public static object FindParent<T>(ref Control StartingContainer)
        {
            var Parent = StartingContainer;
            while (!(Parent == null))
            {
                if (Parent is T)
                    return Parent;
                Parent = Parent.Parent;
            }
            return null;
        }

        public static object FindTopParent<T>(ref Control StartingContainer)
        {
            var Parent = StartingContainer;
            object LastParent = null;
            while (!(Parent == null))
            {
                if (Parent is T)
                    LastParent = Parent;
                Parent = Parent.Parent;
            }
            return LastParent;
        }

        public static void SetControl(ref Control StartingContainer, bool Status)
        {
            foreach (Control ctrl in BasicHelper.GetAllControls(StartingContainer))
                ctrl.Enabled = Status;
        }

        public const string TemporaryName = "TemporaryControl";

        public static string TemporaryControlName(ref Control Self)
        {
            return TemporaryName;
        }

        public static string IsTemporaryControl(ref Control Self)
        {
            return Conversions.ToString(Self.Name.StartsWith(TemporaryControlName(ref Self)));
        }

        public static string GetTemporaryControlName(ref Control Self, string Name)
        {
            return TemporaryName + Name;
        }

        public static string GetTemporaryControlName(ref Control Self, string Name, int Row)
        {
            return GetTemporaryControlName(ref Self, Name) + "_" + Row.ToString();
        }

        public static object FriendlyName(ref Control Self)
        {
            return Self.Name.Replace(TemporaryName, "");
        }

    }

    #endregion

    #region  UserControl 

    public static class UseControlExtension
    {

        public static void LoadViewFromUri(this UserControl userControl, string baseUri)
        {
            try
            {
                var resourceLocater = new Uri(baseUri, UriKind.Relative);
                PackagePart exprCa = (PackagePart)typeof(Application)?.GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, new object[] { resourceLocater });
                var stream = exprCa?.GetStream();
                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null, null), resourceLocater);
                var parserContext = new ParserContext() { BaseUri = uri };
                typeof(XamlReader)?.GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, new object[] { stream, parserContext, userControl, true });
            }
            catch (Exception __unusedException1__)
            {
            }
        }

    }

    #endregion

    #region Stream

    public static class StreamExtensions
    {
        public static byte[] ToArray(this Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }

    #endregion

    #region Expando

    public static class ExpandoExtensions
    {

        public static void Set(this ExpandoObject obj, string propertyName, object value)
        {
            IDictionary<string, object> dic = obj;
            dic[propertyName] = value;
        }

        public static ExpandoObject ToExpando(this object initialObj)
        {
            ExpandoObject obj = new ExpandoObject();
            IDictionary<string, object> dic = obj;
            Type tipo = initialObj.GetType();
            foreach (var prop in tipo.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                dic.Add(prop.Name, prop.GetValue(initialObj));
            }
            return obj;
        }
    }

    #endregion

    #region  Miscellaneous 

    public static class MiscellanousExtension
    {

        public static void Wait(this int Milliseconds)
        {
            Sleep(Milliseconds);
        }

        [DllImport("kernel32.dll")]
        private static extern void Sleep(int Milliseconds);

        public static void WaitThenExecute(this int Milliseconds, EventHandler OnComplete)
        {
            var Timer = new Timer();
            Timer.Interval = Milliseconds;
            Timer.Tick += (sender, e) =>
    {
        Timer.Enabled = false;
        OnComplete(sender, e);
    };
            Timer.Enabled = true;
        }

        public static StringFormat ToStringFormat(this HorizontalAlignment HorizontalAlignment)
        {
            var Alignment = default(StringAlignment);

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        Alignment = StringAlignment.Near;
                        break;
                    }
                case HorizontalAlignment.Center:
                    {
                        Alignment = StringAlignment.Center;
                        break;
                    }
                case HorizontalAlignment.Right:
                    {
                        Alignment = StringAlignment.Far;
                        break;
                    }
            }

            var Format = new StringFormat();
            Format.Alignment = Alignment;
            Format.LineAlignment = StringAlignment.Center;

            return Format;
        }

        public static string GetResourceFilePath(this string FileName)
        {
            return GetResourceFileDirectory(FileName) + $@"\{FileName}";
        }

        private static readonly string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetResourceFileDirectory(this string FileName)
        {
            return string.Format($@"{{0}}Resources", RunningPath);
        }

    }

    #endregion

    #endregion

    #region  ControlArrays 

    public class ControlArrayUtils
    {

        // Converts same type of controls on a form to a control 
        // array by using the notation ControlName_1, ControlName_2, 
        // where the _ can be replaced by any separator string

        public static Array getControlArray(Form frm, string controlName, string separator = "")
        {

            short i;
            short startOfIndex;
            var alist = new ArrayList();
            Type controlType = null;
            // Dim ctrls() As Control
            string strSuffix;
            short maxIndex = -1; // Default


            // Loop through all controls, looking for 
            // controls with the matching name pattern
            // Find the highest indexed control

            foreach (Control ctl in frm.Controls)
            {
                startOfIndex = (short)ctl.Name.ToLower().IndexOf(controlName.ToLower() + separator);
                if (startOfIndex == 0)
                {
                    strSuffix = ctl.Name.Substring(controlName.Length);
                    // Check that the suffix is an
                    // integer (index of the array)
                    if (IsInteger(strSuffix))
                    {
                        if (Conversion.Val(strSuffix) > maxIndex)
                            maxIndex = (short)Math.Round(Conversion.Val(strSuffix)); // Find the highest 
                                                                                     // indexed Element
                    }
                }
            }

            // Add to the list of controls in correct order
            if (maxIndex > -1)
            {

                var loopTo = maxIndex;
                for (i = 0; i <= loopTo; i++)
                {
                    var aControl = getControlFromName(ref frm, controlName, i, separator);
                    if (aControl != null)
                    {
                        // Save the object Type (uses the last 
                        // control found as the Type)
                        controlType = aControl.GetType();
                    }
                    alist.Add(aControl);
                }
            }
            return alist.ToArray(controlType);
        }

        // Converts any type of like named controls on a form 
        // to a control array by using the notation ControlName_1, 
        // ControlName_2, where the _ can be replaced by any 
        // separator string

        public static Control[] getMixedControlArray(Form frm, string controlName, string separator = "")
        {

            short i;
            short startOfIndex;
            // Dim controlType As Type
            var alist = new ArrayList();
            // Dim ctrls() As Control
            string strSuffix;
            short maxIndex = -1; // Default

            // Loop through all controls, looking for controls 
            // with the matching name pattern
            // Find the highest indexed control

            foreach (Control ctl in frm.Controls)
            {
                startOfIndex = (short)ctl.Name.ToLower().IndexOf(controlName.ToLower() + separator);
                if (startOfIndex == 0)
                {
                    strSuffix = ctl.Name.Substring(controlName.Length);
                    // Check that the suffix is an integer 
                    // (index of the array)
                    if (IsInteger(strSuffix))
                    {
                        if (Conversion.Val(strSuffix) > maxIndex)
                            maxIndex = (short)Math.Round(Conversion.Val(strSuffix)); // Find the highest 
                                                                                     // indexed Element
                    }
                }
            }

            // Add to the list of controls in correct order
            if (maxIndex > -1)
            {
                var loopTo = maxIndex;
                for (i = 0; i <= loopTo; i++)
                {
                    var aControl = getControlFromName(ref frm, controlName, i, separator);
                    alist.Add(aControl);
                }
            }
            return (Control[])alist.ToArray(typeof(Control));
        }

        private static Control getControlFromName(ref Form frm, string controlName, short index, string separator)
        {
            controlName = controlName + separator + index;
            foreach (Control ctl in frm.Controls)
            {
                if (string.Compare(ctl.Name, controlName, true) == 0)
                {
                    return ctl;
                }
            }
            return null; // Could not find this control by name
        }

        private static bool IsInteger(string Value)
        {
            if (string.IsNullOrEmpty(Value))
                return false;
            foreach (char chr in Value)
            {
                if (!char.IsDigit(chr))
                {
                    return false;
                }
            }
            return true;
        }
    }

    #endregion

    #region  BasicProgram 

    public class BasicProgram
    {

        public delegate void OnSelectedText(object sender, EventArgs e);
        public delegate void OnDeselectedText(object sender, EventArgs e);

        public static event OnSelectedText SelectedText;
        public static event OnDeselectedText DeselectedText;

        public static bool SelectStatus = false;

        public static void RaiseSelectedText(object sender)
        {
            if (SelectStatus == false)
                SelectStatus = true;
            else
                return;
            var e = new EventArgs();
            SelectedText?.Invoke(sender, e);
        }

        public static void RaiseDeselectedText(object sender)
        {
            if (SelectStatus == true)
                SelectStatus = false;
            else
                return;
            var e = new EventArgs();
            DeselectedText?.Invoke(sender, e);
        }

        public class HighlightTextEventArgs : EventArgs
        {

            public HighlightTextEventArgs(ListViewItem item, int subItem, Control control) : base()
            {
                _subItemIndex = subItem;
                _item = item;
                _control = control;
            }

            private int _subItemIndex = -1;
            public int SubItem
            {
                get
                {
                    return _subItemIndex;
                }
            }

            private ListViewItem _item = null;
            public ListViewItem Item
            {
                get
                {
                    return _item;
                }
            }

            private Control _control = null;
            public Control Control
            {
                get
                {
                    return _control;
                }
            }
        }

    }

    #endregion

    #region  BasicHelper 

    public class BasicHelper
    {

        public static object ToNumericText(string Text)
        {
            var Culture = CultureInfo.DefaultThreadCurrentUICulture;
            int Number = Conversions.ToInteger(Text);

            return Number.ToString("N0", Culture);
        }

        public static string ToPascalCase(string Text, bool AddSpaces = false)
        {
            var Culture = new CultureInfo("en-US", false).TextInfo;
            Text = Regex.Replace(Text, "[A-Z]*([A-Z0-9]+)[0-9]*", " $1");
            Text = Regex.Replace(Text, "_", " ");
            Text = Culture.ToTitleCase(Text);
            if (!AddSpaces)
            {
                Text = Text.Replace(" ", "");
            }
            else
            {
                Text = Text.Trim();
            }
            return Text;
        }

        public static object ToTitleCase(string Text)
        {
            return ToPascalCase(Text, true);
        }

        public static int DistanceBetween(Point param_location1, Point param_location2)
        {
            int DistanceBetweenRet = default;
            int Horizontal;
            int Vertical;
            Horizontal = Math.Abs(param_location2.X - param_location1.X);
            Vertical = Math.Abs(param_location2.Y - param_location1.Y);
            DistanceBetweenRet = (int)Math.Round(Math.Sqrt(Horizontal * Horizontal + Vertical * Vertical));
            return DistanceBetweenRet;
        }

        public static Point BoundCenter(Rectangle param_location)
        {
            Point BoundCenterRet = default;
            int cw = (int)Math.Round(param_location.X + param_location.Width / 2d);
            int ch = (int)Math.Round(param_location.Y + param_location.Height / 2d);
            BoundCenterRet = new Point(cw, ch);
            return BoundCenterRet;
        }

        public static List<Control> GetAllControls<T>(Control source)
        {
            var ctrls = new List<Control>();
            var ctrl = source;

            while (!(ctrl == null))
            {
                ctrl = source.GetNextControl(ctrl, true);
                if (ctrl != null & ctrl is T)
                    ctrls.Add(ctrl);
            }

            return ctrls;
        }

        public static List<Control> GetAllControls(Control source)
        {
            return GetAllControls<Control>(source);
        }

        public static Form GetParentForm(Control source)
        {
            Form lv_form = null;
            var lv_control = source;

            while (!(lv_control == null))
            {
                lv_control = lv_control.Parent;
                if (lv_control is Form == true)
                {
                    lv_form = (Form)lv_control;
                    break;
                }
            }

            return lv_form;
        }

        public static object GetArrayReverse(ArrayList param_array)
        {
            ArrayList lv_array = (ArrayList)param_array.Clone();
            lv_array.Reverse();
            return lv_array;
        }

        public static byte[] TrimByteArray(byte[] param_bytes, int param_start, int param_end)
        {
            var lv_bytes = new byte[param_end];
            Array.Copy(param_bytes, param_start, lv_bytes, 0, param_end);
            return lv_bytes;
        }

        public static object DeepClone(object list)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();

            bf.Serialize(ms, list);
            ms.Seek(0L, SeekOrigin.Begin);
            var copy = bf.Deserialize(ms);
            ms.Close();

            return copy;
        }

        public static object DeepCopy(object obj)
        {
            object DeepCopyRet = default;
            // copies original object to stream then 
            // deserializes that stream and returns the output
            // to create clone (copy) of object

            if (!obj.GetType().IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            var objMemStream = new MemoryStream(5000);
            var objBinaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));

            objBinaryFormatter.Serialize(objMemStream, obj);

            objMemStream.Seek(0L, SeekOrigin.Begin);

            DeepCopyRet = objBinaryFormatter.Deserialize(objMemStream);

            objMemStream.Close();
            return DeepCopyRet;
        }

        public static object Serialize(object obj)
        {
            var m = new MemoryStream();
            var f = new BinaryFormatter();
            f.Serialize(m, obj);
            m.Seek(0L, SeekOrigin.Begin);
            return f.Deserialize(m);
        }

    }

    #endregion

    #region  UndoRedo 

    public class UndoRedoClass<T>
    {
        private Stack<T> UndoStack;
        private Stack<T> RedoStack;

        public T CurrentItem;
        public event EventHandler<UndoRedoActionArgs<T>> UndoHappened;
        public event EventHandler<UndoRedoActionArgs<T>> RedoHappened;
        public event EventHandler<UndoRedoRefreshArgs> RefreshHappened;

        public T[] AllUndos
        {
            get
            {
                return UndoStack.ToArray();
            }
        }

        public T[] AllRedos
        {
            get
            {
                return RedoStack.ToArray();
            }
        }

        public T NextUndo
        {
            get
            {
                UndoStack.Peek();
                return default;
            }
        }

        public T NextRedo
        {
            get
            {
                RedoStack.Peek();
                return default;
            }
        }

        public bool CanUndo
        {
            get
            {
                return UndoStack.Count > 0;
            }
        }

        public bool CanRedo
        {
            get
            {
                return RedoStack.Count > 0;
            }
        }

        public int Undos
        {
            get
            {
                return UndoStack.Count;
            }
        }

        public int Redos
        {
            get
            {
                return RedoStack.Count;
            }
        }

        public UndoRedoClass()
        {
            UndoStack = new Stack<T>();
            RedoStack = new Stack<T>();
        }

        private void RaiseRefresh()
        {
            RefreshHappened?.Invoke(this, new UndoRedoRefreshArgs(CanUndo, CanRedo));
        }

        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
            CurrentItem = default;
            RaiseRefresh();
        }

        public void AddItem(T Item)
        {
            CurrentItem = Item;
            if (CurrentItem != null)
                UndoStack.Push(CurrentItem);
            RedoStack.Clear();
            RaiseRefresh();
        }

        public void Undo()
        {
            CurrentItem = UndoStack.Pop();
            RedoStack.Push(CurrentItem);
            UndoHappened?.Invoke(this, new UndoRedoActionArgs<T>(CurrentItem));
            RaiseRefresh();
        }

        public void Redo()
        {
            CurrentItem = RedoStack.Pop();
            UndoStack.Push(CurrentItem);
            RedoHappened?.Invoke(this, new UndoRedoActionArgs<T>(CurrentItem));
            RaiseRefresh();
        }
    }

    public class UndoRedoActionArgs<T> : EventArgs
    {

        public T CurrentItem { get; private set; }

        public UndoRedoActionArgs(T CurrentItem)
        {
            this.CurrentItem = CurrentItem;
        }
    }

    public class UndoRedoRefreshArgs : EventArgs
    {

        public bool HasUndo { get; private set; }
        public bool HasRedo { get; private set; }

        public UndoRedoRefreshArgs(bool HasUndo, bool HasRedo)
        {
            this.HasUndo = HasUndo;
            this.HasRedo = HasRedo;
        }
    }

    public class UndoRedoAction<T>
    {

        public T OldValue { get; private set; }
        public T NewValue { get; private set; }

        public UndoRedoAction(T OldValue, T NewValue)
        {
            this.OldValue = OldValue;
            this.NewValue = NewValue;
        }

    }

    #endregion

    #region  SystemKeyboardEvent 

    public class SystemKeyboardEvent : IMessageFilter, IDisposable
    {

        /// <summary>
    /// A hot key has been pressed.
    /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        [DllImport("user32.dll")]
        private static extern int MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public bool PreFilterMessage(ref Message m)
        {

            if (m.Msg == WM_KEYUP)
            {
                Keys Key = (Keys)MapVirtualKey((uint)m.WParam, 2U);

                KeyPressed?.Invoke(this, new KeyPressedEventArgs(KeyboardModifierKeys.None, Key));
            }
            return false;
        }

        #region  Internal Window 

        /// <summary>
    /// Represents the window that is used internally to get the messages.
    /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x312;

            public Window()
            {
                // create the handle for the window.
                CreateHandle(new CreateParams());
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            /// <summary>
        /// Overridden to get the notifications.
        /// </summary>
        /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    Keys key = (Keys)((int)m.LParam >> 16 & 0xFFFF);
                    KeyboardModifierKeys modifier = (KeyboardModifierKeys)(uint)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            #region  IDisposable

            public void Dispose()
            {
                DestroyHandle();
            }

            #endregion
        }

        #endregion

        private Window _Window = new Window();
        private int _CurrentId;

        public SystemKeyboardEvent()
        {
            // register the event of the inner native window.
            _Window.KeyPressed += (sender, args) => KeyPressed?.Invoke(this, args);
        }

        /// <summary>
    /// Registers a hot key in the system.
    /// </summary>
    /// <param name="modifier">The modifiers that are associated with the hot key.</param>
    /// <param name="key">The key itself that is associated with the hot key.</param>
        public void RegisterHotKey(KeyboardModifierKeys modifier, Keys key)
        {
            // increment the counter.
            _CurrentId = _CurrentId + 1;

            // register the hot key.
            if (!RegisterHotKey(_Window.Handle, _CurrentId, (uint)modifier, (uint)key))
            {
                // Throw New InvalidOperationException("Couldn’t register the hot key.")
                // or use MsgBox("Couldn’t register the hot key.")
            }
        }

        #region  IDisposable

        public void Dispose()
        {
            // unregister all the registered hot keys.
            int i = _CurrentId;
            while (i > 0)
            {
                UnregisterHotKey(_Window.Handle, i);
                Math.Max(System.Threading.Interlocked.Decrement(ref i), i + 1);
            }

            // dispose the inner native window.
            _Window.Dispose();
        }

        #endregion

    }

    /// <summary>
/// Event Args for the event that is fired after the hot key has been pressed.
/// </summary>
    public class KeyPressedEventArgs : EventArgs
    {

        public KeyboardModifierKeys Modifier { get; private set; }

        public Keys Key { get; private set; }

        internal KeyPressedEventArgs(KeyboardModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }

    }

    /// <summary>
/// The enumeration of possible modifiers.
/// </summary>
    [Flags]
    public enum KeyboardModifierKeys : uint
    {
        None = 0U,
        Alt = 1U,
        Control = 2U,
        Shift = 4U,
        Win = 8U
    }

    #endregion

    #region  SystemMouseEvent 

    public class SystemMouseEvent : IMessageFilter
    {

        /// <summary>
    /// A mouse has been used.
    /// </summary>
        public event EventHandler<MouseUsedEventArgs> MouseUsed;

        private const int WM_MOUSEACTIVATE = 0x21;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                MouseUsed?.Invoke(this, new MouseUsedEventArgs(MouseInputButtons.None, true));
            }
            else if (m.Msg == WM_LBUTTONDOWN)
            {
                MouseUsed?.Invoke(this, new MouseUsedEventArgs(MouseInputButtons.Left, false));
            }
            else if (m.Msg == WM_RBUTTONDOWN)
            {
                MouseUsed?.Invoke(this, new MouseUsedEventArgs(MouseInputButtons.Right, false));
            }
            return false;
        }
    }

    /// <summary>
/// Event Args for the event that is fired after the mouse has been used.
/// </summary>
    public class MouseUsedEventArgs : EventArgs
    {

        public MouseInputButtons Buttons { get; private set; }

        public bool Activate { get; private set; }

        internal MouseUsedEventArgs(MouseInputButtons Buttons, bool Activate)
        {
            this.Buttons = Buttons;
            this.Activate = Activate;
        }

    }

    /// <summary>
/// The enumeration of possible modifiers.
/// </summary>
    [Flags]
    public enum MouseInputButtons : uint
    {
        None = 0U,
        Left = 1U,
        Right = 2U
    }

    #endregion

    #region  SystemFormEvent 

    public class SystemFormEvent
    {

        public static event EventHandler<SystemFormLifeCycle> OnLifeCycleInitialize;

        public static event EventHandler<SystemFormLifeCycle> OnLifeCycleInitialized;

        public static void RaiseInitialize(object Sender)
        {
            OnLifeCycleInitialize?.Invoke(Sender, new SystemFormLifeCycle());
            OnLifeCycleInitialized?.Invoke(Sender, new SystemFormLifeCycle());
        }

        public static event EventHandler<SystemFormLifeCycle> OnLifeCyclePreferencesSaved;

        public static void RaisePreferencesSaved(object Sender)
        {
            OnLifeCyclePreferencesSaved?.Invoke(Sender, new SystemFormLifeCycle());
        }

        public static void ConfirmDialog(object Sender, bool Required, string Action, EventHandler OnConfirm, string Message = "")
        {
            MsgBoxResult Result;
            Result = Required ? Interaction.MsgBox(Message + "Are you sure you want to " + Action + "?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo), "Confirm") : MsgBoxResult.Yes;
            if (Result == MsgBoxResult.Yes)
            {
                OnConfirm(Sender, null);
            }
            else if (Result == MsgBoxResult.No)
            {
                return;
            }
        }

    }

    public class SystemFormLifeCycle : EventArgs
    {

    }

    #endregion

    #region  PredicateEqualityComparer 

    public class PredicateEqualityComparer<T> : IEqualityComparer<T>
    {

        private Func<T, T, bool> _predicate;

        public PredicateEqualityComparer(Func<T, T, bool> predicate)
        {
            _predicate = predicate;
        }

        public new bool Equals(T a, T b)
        {
            return _predicate(a, b);
        }

        public new int GetHashCode(T a)
        {
            return a.GetHashCode();
        }
    }
}

#endregion
