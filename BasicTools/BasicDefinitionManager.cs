using System.Collections.Generic;

namespace BasicTools
{
    public class BasicDefinitionManager<X, T> : BasicSingleton<X>, IBasicManager<T>
        where X : BasicDefinitionManager<X, T>, new()
    {
        private List<T> _Instances = new List<T>();
        public IReadOnlyCollection<T> Instances => _Instances.AsReadOnly();

        public bool IsRegistered(T instance)
        {
            return _Instances.Contains(instance);
        }

        public void Register(T instance)
        {
            _Instances.Add(instance);
        }

        public void Unregister(T instance)
        {
            _Instances.Remove(instance);
        }

        public void UnregisterAll()
        {
            _Instances.Clear();
        }
    }
}
