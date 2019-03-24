using consolAPP.Model;
using consolAPP.rabbitMQ;
using RabbitMQ.Client;
using System;
using System.Text;


namespace consolAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Producer producer = new Producer();
            Consumer consumer = new Consumer();



            Console.WriteLine("Enter person name");
            string username = Console.ReadLine();
            using (var db = new PersonDbContext())
            {
                var person = new Person
                {
                    Name = username
                };

                db.persons.Add(person);
                db.SaveChanges();
                producer.produceMesaage(person);
            }
            

            
            consumer.consumeMessages();
            Console.ReadLine();
           

        }
    }
}
