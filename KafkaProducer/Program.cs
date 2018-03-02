using Aolh.MessageBroker.Fabrica.Mensajeria;
using Aolh.MessageBroker.Interfaces.Mensajeria;
using System;

namespace KafkaProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enviar mensaje:");
                IProductor productor = MessageBrokerFabrica.CrearProductor();
                while (true)
                {
                    string mensaje = Console.ReadLine();
                    string str = String.Format("{0} {1}", DateTime.Now.ToString(), mensaje);
                    Console.WriteLine(str);
                    productor.EnviarMensaje(new Mensaje(MessageBrokerFabrica.CrearIdentificador(),str));
                }
            }
            catch(Exception ex)
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
