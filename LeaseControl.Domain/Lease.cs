using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Domain
{
    public class Lease
    {
        public Guid Id { get;  set; }
        public Guid DeliveryManId { get;  set; }
        public Guid MotorcycleId { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime ExpectedEndDate { get;  set; }
        public DateTime? EndDate { get;  set; }
        public decimal ValueTotal { get;  set; }
        public DeliveryMan DeliveryMan { get; set; }
        public Motorcycle Motorcycle { get; set; }
    
    }
}

