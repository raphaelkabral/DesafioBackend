using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeaseControl.Domain
{
    public class Motorcycle
    {
        public Guid Id { get;  set; }
        public int Year { get;  set; }
        public string Model { get;  set; }
        public string Plate { get;  set; }

        public ICollection<Lease>  Leases { get; set; } = new List<Lease>();
    }
   
}
