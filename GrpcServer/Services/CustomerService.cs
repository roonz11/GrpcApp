using Grpc.Core;
using GrpcServer.Protos;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel
            {
                EmailAddress = "aruna@mail.com",
                FirstName = "roonz",
                LastName = "moneypenny",
                IsAtive = true
            };

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> newCustomers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    EmailAddress = "new1@mail.com",
                    FirstName = "new1",
                    LastName = "new1 moneypenny",
                    IsAtive = true
                },
                new CustomerModel
                {
                    EmailAddress = "new2@mail.com",
                    FirstName = "new2",
                    LastName = "new2 moneypenny",
                    IsAtive = true
                },
                new CustomerModel
                {
                    EmailAddress = "new3@mail.com",
                    FirstName = "new3",
                    LastName = "new3 moneypenny",
                    IsAtive = true
                },
            };

            foreach (var cust in newCustomers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
