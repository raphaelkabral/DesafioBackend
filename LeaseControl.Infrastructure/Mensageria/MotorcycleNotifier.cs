using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseControl.Infrastructure.Mensageria
{
    public class MotorcycleNotifier
    {
        private readonly IModel _channel;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleNotifier(IModel channel,IMotorcycleRepository motorcycleRepository)
        {
            _channel = channel;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task NotifyMotorcyle(Motorcycle moto)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                var moto = JsonConvert.DeserializeObject<Motorcycle>(mensagem);

                if (moto!.Year == 2024)
                {
                    await _motorcycleRepository.AddAsync(moto);
                }
            };
            _channel.BasicConsume(queue: "moto_cadastrada", autoAck: true, consumer: consumer);

        }
    }
}
