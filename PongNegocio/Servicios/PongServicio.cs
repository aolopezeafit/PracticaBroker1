using System;
using Aolh.Pong.Negocio.Modelos;

namespace Aolh.Pong.Negocio.Servicios
{
    public static class PongServicio
    {
        private static Consulta consulta = new Consulta();
        public static Consulta InformeConsultas()
        {
            return consulta;
        } 

        public static void ProcesarPing()
        {
            consulta = new Consulta(consulta.Peticiones + 1, consulta.Respuestas);
        }
    }
}
