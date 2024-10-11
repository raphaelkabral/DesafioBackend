using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Domain
{
    public class PlanLease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DailyRate { get; set; }
        public int DurationDays { get; set; }
    }
}
