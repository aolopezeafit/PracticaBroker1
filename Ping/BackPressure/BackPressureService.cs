using Aolh.Lib.BackPressure.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ping.BackPressure
{
    public static class BackPressureService
    {
        public static BasicBackPressure backPressure;
        static BackPressureService()
        {
            backPressure = new BasicBackPressure();
        }



    }
}
