using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public PlanLease PlanLease { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public Motorcycle Motorcycle { get; set; }


        public decimal CalculateTotalCost(DateTime actualReturnDate) 
        {
            if (actualReturnDate < ExpectedEndDate)
            {
                int notUsedDays = (ExpectedEndDate - actualReturnDate).Days;
                decimal penaltyRate = PlanLease.DurationDays switch
                {
                    7 => 0.2m,
                    15 => 0.4m,
                    _ => 0.0m
                };
                decimal penalty = notUsedDays > 0 ? (notUsedDays * PlanLease.DailyRate * penaltyRate) : 0;
                return (PlanLease.DurationDays * PlanLease.DailyRate) - penalty;
            }
            else
            {
                int additionalDays = (actualReturnDate - ExpectedEndDate).Days;
                return (PlanLease.DurationDays * PlanLease.DailyRate) + (additionalDays * 50);
            }
        }
    }
}

