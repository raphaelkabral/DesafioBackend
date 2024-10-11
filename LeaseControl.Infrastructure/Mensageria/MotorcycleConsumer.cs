using LeaseControl.Domain;
using LeaseControl.Domain.InterfaceRepository;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LeaseControl.Infrastructure.Mensageria
{
    public class MotorcycleConsumer : IHostedService
    {

        private readonly IModel _channel;
        private readonly IMotorcycleRepository _motoRepository;


        public MotorcycleConsumer(IModel channel, IMotorcycleRepository motoRepository)
        {
            _channel = channel;
            _motoRepository = motoRepository;
            _channel.QueueDeclare("moto_cadastrada", durable: false, exclusive: false, autoDelete: false);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                var moto = JsonConvert.DeserializeObject<Motorcycle>(mensagem);

                if (moto!.Year == 2024)
                {
                    await _motoRepository.AddAsync(moto);
                }
            };
            _channel.BasicConsume(queue: "moto_cadastrada", autoAck: true, consumer: consumer);


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            return Task.CompletedTask;
        }
    }
}
