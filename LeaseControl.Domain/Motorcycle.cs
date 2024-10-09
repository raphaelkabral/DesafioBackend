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
        public Guid Id { get; private set; }
        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }

        public Motorcycle(Guid id, int year, string model, string plate)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate;
        }
    }
   
}
