using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Lib.BackPressure.Basic.Requests
{
    public class RequestPool : IObservable<Request>
    {
        private List<IObserver<Request>> observers;
        private List<Request> requests;

        private int queueLength = 0; //The length of the input queue to set the maximum acceptable latency
        private int numberThreads = 0; //The number of threads in a pool that can execute transactions in parallel
        private double maxLatency = 0; //Max latency (milliseconds)
        private double transactionTime = 0; //The time taken for individual transactions on a thread (milliseconds)


        /// <summary>
        /// Crea un nuevo RequestPool
        /// </summary>
        /// <param name="numberThreads">Número de hilos que serán usados.</param>
        /// <param name="maxLatency">Máxima latencia permitida.</param>
        public RequestPool(int numberThreads, double maxLatency)
        {
            observers = new List<IObserver<Request>>();
            requests = new List<Request>();

            this.numberThreads = numberThreads;
            this.maxLatency = maxLatency;

            transactionTime = maxLatency;
            RecalculateQueueLength(maxLatency);
        }

        /// <summary>
        /// Agrega un observador.
        /// </summary>
        public IDisposable Subscribe(IObserver<Request> observer)
        {
            try
            {
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                    //foreach (var item in requests)
                    //    observer.OnNext(item);
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            return new RequestUnsubscriber<Request>(observers, observer);
        }

        /// <summary>
        /// Remueve un observador.
        /// </summary>
        public void Unsubscribe(IObserver<Request> observer)
        {
            try
            {
                observers.Remove(observer);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }

        /// <summary>
        /// Agrega una nueva petición siempre y cuando haya espacio en el pool.
        /// </summary>
        public void AddRequest(Request request)
        {
            if (requests.Count >= queueLength)
            {
                BackPressureFullPoolException bpex = new BackPressureFullPoolException();
                var listeners = observers.ToArray();
                foreach (IBasicBackPressureObserver observer in listeners)
                {
                    try
                    {
                        observer.OnError(bpex, request);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                requests.Add(request);
                var listeners = observers.ToArray();
                foreach (IBasicBackPressureObserver observer in listeners)
                {
                    try
                    {
                        observer.OnNext(request);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Elimina una petición que ha sido procesada.
        /// </summary>
        public void RemoveRequest(Request request)
        {
            requests.Remove(request);
            RecalculateQueueLength((DateTime.Now - request.CreationDate).TotalMilliseconds);
        }

        /// <summary>
        /// Recalcula el tamaño del pool de acuerdo al tiempo de la última operación.
        /// </summary>
        private void RecalculateQueueLength(double ellapsedTime)
        {
            transactionTime = (transactionTime + ellapsedTime) / 2;
            queueLength = ((int)(maxLatency / (transactionTime / numberThreads)))+1;

        }
    }
}
