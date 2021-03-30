using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Persistance.Entities
{
    public class Datagram
    {
        public string p1 { get; set; }
        public string signature { get; set; }
        public S0 s0 { get; set; }
        public S1 s1 { get; set; }
        public Datagram()
        {

        }
    }
}
