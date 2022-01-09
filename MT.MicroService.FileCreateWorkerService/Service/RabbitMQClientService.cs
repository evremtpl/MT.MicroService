using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.FileCreateWorkerService.Service
{
    public class RabbitMQClientService : IDisposable
    {

        private readonly ConnectionFactory _connectionFactory; //dış dunyaya açılmasına gerek yok.
        private IConnection _connection;
        private IModel _channel;

       

        public static string QueueName = "queue-file";

        private readonly ILogger<RabbitMQClientService> _logger;
        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            Connect();
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true }) return _channel;

            _channel = _connection.CreateModel();

     
            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu.");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Close();
            _channel = default;
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı koptu.");
        }
    }
}
