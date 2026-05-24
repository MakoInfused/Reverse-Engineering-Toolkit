using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace BasicTools
{
    public class BasicFormatStringEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var type = Type.GetType(@"System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            dynamic editor = Activator.CreateInstance(type, new object[] { });
            var result = editor.EditValue(context, provider, value);
            // call your event here
            return result;
        }
    }
}
