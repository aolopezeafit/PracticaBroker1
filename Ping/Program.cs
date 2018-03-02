using Ping.Controllers;
using System;
using System.ServiceModel.Web;
using System.Threading;

namespace Ping
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ping iniciado");
                Console.WriteLine("Presione una tecla cuando desee salir.");
                Console.WriteLine();
                InitWebService();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Aplicación finalizada.");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }

        /// <summary>Inicialización del web service.</summary>
		private static void InitWebService()
        {
            WebServiceHost _serviceHost = new WebServiceHost(typeof(PingController), new Uri(string.Format("{0}:{1}", Properties.Settings.Default.Host, Properties.Settings.Default.Port)));
            _serviceHost.Open();
            Console.ReadKey();
            _serviceHost.Close();
        }
    }
}
