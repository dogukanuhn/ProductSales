using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSales.Application.Services
{
    public class BusService : IBusService
    {


        public void PublishToMessageQueue(string exchange,string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", VirtualHost="productsales" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(eventData);
                channel.BasicPublish(exchange: exchange,
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);


            }
        }
    }
}
