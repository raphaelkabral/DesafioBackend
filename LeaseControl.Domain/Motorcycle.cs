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
        private int v1;
        private string v2;
        private string v3;

        public Motorcycle(int v1, string v2, string v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public Guid Id { get;  set; }
        public int Year { get;  set; }
        public string Model { get;  set; }
        public string Plate { get;  set; }

        public ICollection<Lease>  Leases { get; set; } = new List<Lease>();
    }
   
}
