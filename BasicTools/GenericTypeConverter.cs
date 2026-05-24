using System;
using System.Globalization;
using System.ComponentModel;

namespace BasicTools
{
    public class GenericTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            return (destType == typeof(string) && value is ITypeConverter converter) ? converter.ToTypeConverter(context) : base.ConvertTo(context, culture, value, destType);
        }
    }
}
