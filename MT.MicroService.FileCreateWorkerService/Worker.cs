using ClosedXML.Excel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MT.MicroService.FileCreateWorkerService.Models;
using MT.MicroService.FileCreateWorkerService.Service;
using MT.MicroService.Services.Person.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQClientService = MT.MicroService.FileCreateWorkerService.Service.RabbitMQClientService;

namespace MT.MicroService.FileCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider; // scope olarak eklendiði için alýnamadý. Provider üzerinden alýndý
        private IModel _channel;
        
        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel=_rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false); //her bir subcriber a bir gönderilmiþtir. Boyutu önemli deðil.
            return base.StartAsync(cancellationToken);
        }
        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000); //çok hýzlý olduðu için biraz gecikme verdim.
            var createdExcelMessage=JsonSerializer.Deserialize<CreateFileMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            using var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable("persons"));

            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);

            MultipartFormDataContent multipartFormDataContent = new();
            multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

            var baseUrl = "http://localhost:43579/api/files/";
            using (var httpClient= new HttpClient()) 
            {
                var reponse = await httpClient.PostAsync($"{baseUrl}?fileId={createdExcelMessage}", multipartFormDataContent);
                if(reponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation($" File (Id={createdExcelMessage.FileId} ) was created by successful");
                    _channel.BasicAck(@event.DeliveryTag, false);
                }

            }
        }

        private DataTable GetTable(string tableName)
        {
            List<Person> persons;
            using (var scope= _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PhoneBookReportContext>();
                persons = context.Persons.ToList();

            }

            DataTable table = new DataTable {TableName=tableName };

            table.Columns.Add("UUID",typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Surname", typeof(string));
            table.Columns.Add("Company", typeof(string));

            persons.ForEach(x =>
            {
                table.Rows.Add(x.Uuid, x.Name, x.SurName, x.Company);

            });


            return table;
        }
    }
}
