using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace RedisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (IRedisNativeClient client = new RedisClient())
            //{
            //    client.Set("urn:messages:1", Encoding.UTF8.GetBytes("Hello"));
            //}

            //using (IRedisNativeClient client = new RedisClient())
            //{
            //    var res = Encoding.UTF8.GetString(client.Get("urn:messages:1"));
            //    Console.WriteLine(res);
            //}

            //using (IRedisClient client = new RedisClient())
            //{
            //    var customerNames = client.Lists["urn:customernames"];
            //    customerNames.Clear();
            //    customerNames.Add("Joe");
            //    customerNames.Add("Bob");

            //}

            //using (IRedisClient client = new RedisClient())
            //{
            //    var customerNames = client.Lists["urn:customernames"];
            //    foreach (var customerName in customerNames)
            //    {
            //        Console.WriteLine(customerName);
            //    }

            //}

            long lastId = 0;
            using (var client = new RedisClient())
            {
                var customerClient = client.As<Customer>();
                var customer = new Customer()
                {
                    Id = customerClient.GetNextSequence(),
                    Address = "123",
                    Name = "Bob",
                    Orders =
                        new List<Order>
                        {
                            new Order { OrderNumber = "1234112" },
                            new Order { OrderNumber = "123123123" }
                        }
                };
                var storedCustomer = customerClient.Store(customer);
                lastId = storedCustomer.Id;
            }

            using (var client = new RedisClient())
            {
                var customerClient = client.As<Customer>();
                var customer = customerClient.GetById(lastId);
                Console.WriteLine("Got customer {0} with name {1}", customer.Id, customer.Name);
            }

        }

        public class Customer
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public List<Order> Orders { get; set; }
        }

        public class Order
        {
            public string OrderNumber { get; set; }
        }
    }
}
