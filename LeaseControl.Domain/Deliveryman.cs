using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LeaseControl.Domain
{
    public class DeliveryMan
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string CNPJ { get;  set; }
        public DateTime Birthdate { get;  set; }
        public string CNH { get; set; } // Único
        public string TypeCnh { get; set; } // A, B, A+B
        public string ImageCnh { get; set; } // URL para armazenamento

    }
}
