using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic
{
    public class BasicBackPressure
    {

        public BasicBackPressure()
        {
        }

        public bool isFull()
        {
            int worker = 0;
            int io = 0;
            ThreadPool.GetAvailableThreads(out worker, out io);
            return io >= worker;
        }
    }
}
