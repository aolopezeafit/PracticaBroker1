using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic
{

    /// <summary>
    /// Excepción lanzada cuando el pool rechaza una petición por back pressure.
    /// </summary>
    public class BackPressureFullPoolException:Exception
    {
    }
}
