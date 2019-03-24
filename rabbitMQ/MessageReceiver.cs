using consolAPP.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nest;
using Elasticsearch.Net;

namespace consolAPP.rabbitMQ
{
    class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;
        private readonly PersonDbContext _context = new PersonDbContext();


        public MessageReceiver(IModel channel)
        {
            _channel = channel;

        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {


            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("people");
            var client = new ElasticClient(settings);


            int id = Int32.Parse(Encoding.UTF8.GetString(body));
            Console.WriteLine(id);
            var person = _context.persons.Where(p => p.ID == id ).Single();
            var index = client.IndexDocument(person);
            _channel.BasicAck(deliveryTag, false);
            
        }
    }
}

