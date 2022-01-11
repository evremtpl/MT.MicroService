﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person.RabbitMQ
{
    public class RabbitMQPublisher
    {

        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(CreateFileMessage createFileMessage )

        {
            //var channel = _rabbitMQClientService.Connect();
            //var bodyString = JsonSerializer.Serialize(createFileMessage);
            //var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            //var properties = channel.CreateBasicProperties();
            //properties.Persistent = true;

            //channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingFile,
            //    basicProperties: properties, body: bodyByte);
        }
    }
}
