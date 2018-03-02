using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Pong.Negocio.Modelos
{
    public class Consulta
    {
        public int Peticiones { get; set; }
        public int Respuestas { get; set; }
        public Consulta()
        {
        }
        public Consulta(int peticiones, int respuestas)
        {
            Peticiones = peticiones;
            Respuestas = respuestas;
        }
    }
}
