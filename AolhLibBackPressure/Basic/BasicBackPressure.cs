using Aolh.Lib.BackPressure.Basic.Requests;
using System;

namespace Aolh.Lib.BackPressure.Basic
{
    public class BasicBackPressure
    {
        private RequestPool provider;

        /// <summary>
        /// Clase para manejo del back pressure.
        /// </summary>
        /// <param name="numberThreads">Número de hilos que serán usados.</param>
        /// <param name="maxLatency">Máxima latencia permitida.</param>
        public BasicBackPressure(int numberThreads, double maxLatency)
        {
            provider = new RequestPool(  numberThreads,   maxLatency);
        }


        /// <summary>
        /// Agregar observador.
        /// </summary>
        public void AddObserver(IBasicBackPressureObserver observer)
        {
            provider.Subscribe(observer);
        }


        /// <summary>
        /// Elimina un observador.
        /// </summary>
        public void RemoveObserver(IBasicBackPressureObserver observer)
        {
            provider.Unsubscribe(observer);
        }


        /// <summary>
        /// Generación de guid.
        /// </summary>
        private static Guid GenerateId()
        {
            return Guid.NewGuid();
        }


        /// <summary>
        /// Crea una nueva petición con identificador único.
        /// </summary>
        public Request CreateRequest(object tag)
        {
            return new Request(new RequestId(GenerateId().ToString()), tag);
        }

        /// <summary>
        /// Agrega una nueva petición.
        /// </summary>

        public void AddRequest(Request request)
        {
            provider.AddRequest(request);
        }

        /// <summary>
        /// Elimina la petición que ha sido procesada.
        /// </summary>

        public void RemoveRequest(Request request)
        {
            provider.RemoveRequest(request);
        }
    }
}
