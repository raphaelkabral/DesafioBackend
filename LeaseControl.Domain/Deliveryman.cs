using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LeaseControl.Domain
{
    public class Deliveryman
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string DriversLicenseNumber { get; private set; }
        public string DriversLicenseType { get; private set; } // A, B, A+B
        public string DriversLicenseImage { get; private set; } // Image URL

        public Deliveryman(int id, string name, string cnpj, DateTime Birthdate, string driverLicenseNumber, string driversLicenseType, string driversLicenseImage)
{
            Id = id;
            Name = name;
            DriversLicenseNumber = driversLicenseType;
            DriversLicenseImage = driversLicenseImage;
        }
    }
}
