using LeaseControl.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Infrastructure.Mensageria
{
    public class MotorcycleNotifier : IMotorcycleNotifier
    {
        private readonly IModel _channel;

        public MotorcycleNotifier(IModel channel)
        {
            _channel = channel;
        }

        public async Task NotifyMotorcyle(Motorcycle moto)
        {
            var message = JsonConvert.SerializeObject(moto);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                   routingKey: "moto_cadastrada",
                                   basicProperties: null,
                                   body: body);
        }
    }
}
