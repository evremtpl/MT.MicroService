using ClosedXML.Excel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MT.MicroService.Data;
using MT.MicroService.Services.Person.RabbitMQ;
using MT.MicroService.Core.Entity;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace MT.MicroService.Services.Person.BackGroundService
{
    public class FileCreateBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly ILogger<FileCreateBackgroundService> _logger;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        public FileCreateBackgroundService(RabbitMQClientService rabbitMQClientService,
            ILogger<FileCreateBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);


            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                await Task.Delay(5000); //çok hızlı olduğu için biraz gecikme verdim.
                var createdExcelMessage = JsonSerializer.Deserialize<CreateFileMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                using var ms = new MemoryStream();
                var wb = new XLWorkbook();
                var ds = new DataSet();
                ds.Tables.Add(GetTable("persons"));

                wb.Worksheets.Add(ds);
                wb.SaveAs(ms);

                MultipartFormDataContent multipartFormDataContent = new();
                multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

                var baseUrl = "http://localhost:5003/api/files/";
                using (var httpClient = new HttpClient())
                {
                    var reponse = await httpClient.PostAsync($"{baseUrl}?fileId={createdExcelMessage.FileId}", multipartFormDataContent);
                    if (reponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($" File (Id={createdExcelMessage.FileId} ) was created by successful");
                        _channel.BasicAck(@event.DeliveryTag, false);
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
            }
        }
        private DataTable GetTable(string tableName)
        {
            DataTable table = new DataTable { TableName = tableName };
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("PersonCount", typeof(int));
            table.Columns.Add("PhoneNumber", typeof(int));
            List<Core.Entity.Person> persons;
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //persons = context.Persons.ToList();


                var qry = from cont in context.ContactInfos
                          where cont.UUID != 0
                          group cont by cont.Location
                 into grp
                          select new
                          {
                              Location = grp.Key,
                              PersonCount = grp.Select(x => x.UUID).Distinct().Count(),
                              PhoneNumber= grp.Select(x => x.PhoneNumber).Distinct().Count()
                          };

                foreach (var row in qry.OrderBy(x => x.PersonCount))
                {
                    table.Rows.Add(row.Location, row.PersonCount, row.PhoneNumber);
                    Console.WriteLine("{0}: {1} : {2}", row.Location, row.PersonCount, row.PhoneNumber);
                }

            }

           


            return table;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
