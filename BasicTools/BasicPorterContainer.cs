using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BasicTools
{
    public interface IBasicPorterContainer : IEnumerable<IBasicPorter>
    {
        object Import(byte[] data, IBasicPorterArgs args);
        byte[] Export(object source, IBasicPorterArgs args);
    }

    public class BasicPorterContainer : IBasicPorterContainer
    {
        protected IBasicPorter[] Porters { get; set; }

        public BasicPorterContainer(params IBasicPorter[] porters)
        {
            Porters = porters;
        }

        protected virtual IBasicPorter FindPorter(IBasicPorterArgs args)
        {
            return Porters.FirstOrDefault(x => x.IsMatch(args) && x.Extension == args.Extension);
        }

        public virtual object Import(byte[] data, IBasicPorterArgs args)
        {
            return FindPorter(args).Import(data);
        }

        public virtual byte[] Export(object source, IBasicPorterArgs args)
        {
            return FindPorter(args).Export(source);
        }

        public IEnumerator<IBasicPorter> GetEnumerator()
        {
            foreach (var porter in Porters)
            {
                yield return porter;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => Porters.GetEnumerator();
    }
}