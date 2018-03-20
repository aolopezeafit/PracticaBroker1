using Aolh.Lib.BackPressure.Basic;

namespace Ping.BackPressure
{
    /// <summary>
    /// Servicio para manejo del back pressure.
    /// </summary>
    /// <param name="numberThreads">Número de hilos que serán usados.</param>
    /// <param name="maxLatency">Máxima latencia permitida.</param>
    public static class BackPressureService
    {
        public static BasicBackPressure backPressure;
        static BackPressureService()
        {
            int numberThreads = Properties.Settings.Default.NumberThreads;
            double maxLatency = Properties.Settings.Default.MaxLatency;

            backPressure = new BasicBackPressure(numberThreads, maxLatency);
        }
    }
}
