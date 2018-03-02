using RESTService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Test");
                RestDemoServices DemoServices = new RestDemoServices();
                WebServiceHost _serviceHost = new WebServiceHost(DemoServices, new Uri("http://localhost:8000/DEMOService"));
                _serviceHost.Open();
                Console.ReadKey();
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Proceso finalizado.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
