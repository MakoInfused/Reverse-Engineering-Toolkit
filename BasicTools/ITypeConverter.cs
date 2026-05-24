using System.ComponentModel;

namespace BasicTools
{
    public interface ITypeConverter
    {
        string ToTypeConverter(ITypeDescriptorContext context);
    }
}
