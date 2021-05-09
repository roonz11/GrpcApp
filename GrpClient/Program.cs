using Grpc.Core;
using Grpc.Net.Client;
using GrpClient;
using GrpcServer.Protos;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            
            //var input = new HelloRequest { Name = "Aruna" };
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);

            var customerClient = new Customer.CustomerClient(channel);
            var customerLookupModel = new CustomerLookupModel { UserId = 1 };
            var customerResponse = await customerClient.GetCustomerInfoAsync(customerLookupModel);

            Console.WriteLine(customerResponse.FirstName);
            Console.WriteLine(customerResponse.LastName);
            Console.WriteLine(customerResponse.EmailAddress);
            Console.WriteLine(customerResponse.IsAtive);

            Console.WriteLine();
            Console.WriteLine("New Customers:");
            Console.WriteLine();

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCust = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCust.FirstName} {currentCust.LastName} {currentCust.EmailAddress} {currentCust.IsAtive}");
                }

            }

            Console.ReadLine();

        }
    }
}
