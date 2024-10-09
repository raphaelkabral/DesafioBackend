using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Domain
{
    public class Lease
    {
        public int Id { get; private set; }
        public Guid DeliveryManId { get; private set; }
        public Guid MotorcycleId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal ValueTotal { get; private set; }
        public Lease(int id, Guid deliveryManId, Guid motorcycleId, DateTime startDate, DateTime expectedEndDate)
        {
            Id = id;
            DeliveryManId = deliveryManId;
            MotorcycleId = motorcycleId;
            StartDate = StartDate;
            ExpectedEndDate = expectedEndDate;
        }
    }
}

