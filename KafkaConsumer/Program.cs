using Aolh.MessageBroker.Fabrica.Mensajeria;
using Aolh.MessageBroker.Interfaces.Mensajeria;
using System;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Esperando respuestas:");
                IConsumidor consumidor = MessageBrokerFabrica.CrearConsumidor();
                consumidor.MensajeRecibido += consumidor_MensajeRecibido;
                Console.ReadLine();
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

        private static void consumidor_MensajeRecibido(object sender, Mensaje mensaje)
        {
            try
            {
                string str = String.Format("{0} {1}", DateTime.Now.ToString(), mensaje.Contenido);
                Console.WriteLine(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }
    }
}
