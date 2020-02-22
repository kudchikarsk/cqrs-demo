using LaYumba.Functional;
using static LaYumba.Functional.F;
using Logic.Data;
using Logic.Repositories;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unit = System.ValueTuple;

namespace Logic.AppServices
{
    public sealed class DeleteCustomerCommand : ICommand<Task<Validation<Unit>>>
    {

        public DeleteCustomerCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, Task<Validation<Unit>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public DeleteCustomerCommandHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }
            public async Task<Validation<Unit>> Handle(DeleteCustomerCommand command)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);

                var customer = await customerRepository.GetByIdAsync(command.Id);
                if (customer == null) return Error("Customer not found");

                customerRepository.Delete(customer);
                await unitOfWork.CommitAsync();
                return Unit();
            }
        }
    }
}
