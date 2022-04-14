using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterBase
{
    internal class Counter
    {

        private int min;
        private int max;

        private int current;

        public int Max
        {
            get => max;
            init => max = value > min ? value : min + 1;
        }
        public int Min
        {
            get => min;
            init => min = value < max ? value : max - 1;
        }

        public Counter(int _max, int _min) { max = _max; min = _min; current = min; }

        public void Increment() => current = (current + 1) > max ? min : current + 1;
        public int GetCurrent() => current;
    }
}
