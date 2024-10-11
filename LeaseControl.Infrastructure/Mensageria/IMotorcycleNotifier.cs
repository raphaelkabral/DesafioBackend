using LeaseControl.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Infrastructure.Mensageria
{
    internal interface IMotorcycleNotifier
    {
        Task NotifyMotorcyle(Motorcycle moto);
    }
}
