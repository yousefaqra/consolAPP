using consolAPP.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace consolAPP.rabbitMQ
{

    class Consumer
    {
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";


        public void consumeMessages()
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicQos(0, 1, false);


            MessageReceiver messageReceiver = new MessageReceiver(channel);
            channel.BasicConsume("DemoQueue", false, messageReceiver);
        }

     
    }
}
