using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unit = System.ValueTuple;
using Logic.Utils;
using Logic.Repositories;
using Logic.Data;
using System.Linq;

namespace Logic.AppServices
{
    public sealed class RemoveAddressCommand : ICommand<Task<Validation<Unit>>>
    {
        public RemoveAddressCommand(long customerId, long addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        public long CustomerId { get; }
        public long AddressId { get; }

        public sealed class RemoveAddressCommandHandler : ICommandHandler<RemoveAddressCommand, Task<Validation<Unit>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public RemoveAddressCommandHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }
            public async Task<Validation<Unit>> Handle(RemoveAddressCommand command)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);

                var customer = await customerRepository.GetByIdAsync(command.CustomerId);
                if (customer == null) return Error("Customer not found.");

                var address = customer.Addresses.SingleOrDefault(a => a.Id == command.AddressId);
                if (address == null) return Error("Address not found.");

                customer.RemoveAddress(address);

                customerRepository.Update(customer);
                await unitOfWork.CommitAsync();

                return Unit();
            }
        }
    }
}
