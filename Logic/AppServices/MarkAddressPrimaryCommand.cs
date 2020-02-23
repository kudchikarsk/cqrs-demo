using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unit = System.ValueTuple;
using Logic.Data;
using Logic.Utils;
using Logic.Repositories;
using System.Linq;

namespace Logic.AppServices
{
    public sealed class MarkAddressPrimaryCommand :ICommand<Task<Validation<Unit>>>
    {
        public MarkAddressPrimaryCommand(long customerId, long addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        public long CustomerId { get; }
        public long AddressId { get; }

        public sealed class MarkPrimaryAddressCommandHandler : ICommandHandler<MarkAddressPrimaryCommand, Task<Validation<Unit>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public MarkPrimaryAddressCommandHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }
            public async Task<Validation<Unit>> Handle(MarkAddressPrimaryCommand command)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);
                var customer = await customerRepository.GetByIdAsync(command.CustomerId);
                if (customer == null) return Error("Customer not found");

                var address = customer.Addresses.SingleOrDefault(a => a.Id == command.AddressId);
                if (address == null) return Error("Address not found");

                customer.MarkPrimay(address);

                customerRepository.Update(customer);
                await unitOfWork.CommitAsync();

                return Unit();
            }
        }
    }
}
