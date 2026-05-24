using System.Collections.Generic;

namespace BasicTools
{
    public class BasicSingleton<T> where T : BasicSingleton<T>, new()
    {
        private static T _Instance;
        public static T Instance
        {
            get
            {
                if (_Instance == null) _Instance = new T();
                return _Instance;
            }
        }
    }

    public abstract class BasicSingletonFactory<TSingleton, TValue> : BasicSingleton<TSingleton>
        where TSingleton : BasicSingleton<TSingleton>, new()
    {
        public abstract IReadOnlyCollection<TValue> Available { get; }
    }

    public abstract class BasicSingletonFactory<TSingleton, TKey, TValue> : BasicSingletonFactory<TSingleton, TValue>
        where TSingleton : BasicSingleton<TSingleton>, new()
    {
        public abstract IReadOnlyDictionary<TKey, TValue> Implemented { get; }
    }
}
