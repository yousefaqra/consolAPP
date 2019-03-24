using consolAPP.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace consolAPP.rabbitMQ
{
    class Producer
    {
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";
        PersonDbContext context = new PersonDbContext();


        public void produceMesaage(Person person)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.ExchangeDeclare("DemoExchange", ExchangeType.Direct);
            model.QueueDeclare("DemoQueue", true, false, false, null);
            model.QueueBind("DemoQueue", "DemoExchange", "directexchange_key");

            //publich a message



            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            
              
            byte[] messageBuffer = Encoding.Default.GetBytes(person.ID.ToString());
            Console.WriteLine(person.ID);
            model.BasicPublish("DemoExchange", "directexchange_key", properties, messageBuffer);
            

            
            Console.WriteLine("Message sent!");
        }
    }
}
