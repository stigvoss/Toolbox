using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading.Base;

namespace Toolbox.Threading
{
    public class Bundle<T> : IntermediateBlock<T, IEnumerable<T>>
    {
        List<T> _bundle = null;

        private const int DEFAULT_SIZE = 1000;

        private int _bundleSize = DEFAULT_SIZE;

        public override void Initialize(BlockArgs args)
        {
            base.Initialize(args);

            if (args is BundleArgs)
            {
                BundleArgs bundleArgs = (BundleArgs)Arguments;
                _bundleSize = bundleArgs.Size;
            }
        }

        protected override IEnumerable<T> Process(T item)
        {
            List<T> result = null;

            if (_bundle == null)
            {
                _bundle = new List<T>();
            }

            _bundle.Add(item);

            if (_bundle.Count == _bundleSize || Source.IsCompleted)
            {
                result = _bundle;
                _bundle = null;
            }

            return result;
        }

        public override void Done()
        {
            if (_bundle != null)
            {
                Output.Add(_bundle);
            }
            base.Done();
        }
    }
}
