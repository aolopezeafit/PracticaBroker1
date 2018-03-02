using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aolh.Pong.Domain.Entities
{
    public class Statistic
    {
        public int Requests { get; set; }
        public int Responses { get; set; }
        public Statistic()
        {
        }
        public Statistic(int requests, int responses)
        {
            Requests = requests;
            Responses = responses;
        }
    }
}
