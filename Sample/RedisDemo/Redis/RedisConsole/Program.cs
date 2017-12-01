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
            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                customerNames.Clear();
                customerNames.Add("Joe");
                customerNames.Add("Bob");

            }

            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                foreach (var customerName in customerNames)
                {
                    Console.WriteLine(customerName);
                }

            }
        }
    }
}
