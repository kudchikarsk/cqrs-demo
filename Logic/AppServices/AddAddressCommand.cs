using LaYumba.Functional;
using static LaYumba.Functional.F;
using Logic.Data;
using Logic.Models;
using Logic.Repositories;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.AppServices
{
    public sealed class AddAddressCommand : ICommand<Task<Validation<Address>>>
    {
        public AddAddressCommand(
            long customerId,
            string street,
            string city,
            string zipCode
            )
        {
            CustomerId = customerId;
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public long CustomerId { get; }
        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }

        public sealed class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, Task<Validation<Address>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public AddAddressCommandHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }
            public async Task<Validation<Address>> Handle(AddAddressCommand command)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);

                var customer = await customerRepository.GetByIdAsync(command.CustomerId);
                if (customer == null) return Error("Customer not found.");

                Address address;
                try
                {
                    address = new Address(
                                command.Street,
                                command.City,
                                command.ZipCode
                                );
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }

                customer.AddAddress(address);
                customerRepository.Update(customer);
                await unitOfWork.CommitAsync();

                return address;
            }
        }
    }
}
