using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorCore
{
    public class Calculator : MarshalByRefObject
    {
        public delegate void OnAddedDelegate(int x, int y, int z);
        public delegate void OnSubtractedDelegate(int x, int y, int z);

        public event OnAddedDelegate OnAdded;
        public event OnSubtractedDelegate OnSubtracted;

        public int Add(int x, int y)
        {
            int z = x + y;
            if (OnAdded != null)
            {
                OnAdded.Invoke(x, y, z);
            }
            return z;
        }

        public int Subtract(int x, int y)
        {
            int z = x - y;
            if (OnSubtracted != null)
            {
                OnSubtracted.Invoke(x, y, z);
            }
            return z;
        }
    }
}
