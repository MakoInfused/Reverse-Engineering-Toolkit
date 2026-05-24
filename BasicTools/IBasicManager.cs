using System.Linq;
using System.Collections.Generic;

namespace BasicTools
{
    public interface IBasicManager<T>
    {
        IReadOnlyCollection<T> Instances { get; }

        void Register(T instance);
        void Unregister(T instance);
        bool IsRegistered(T instance);
    }

    public interface IBasicDefinitionContainer
    {
        object Default { get; }
    }
}
