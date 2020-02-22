using LaYumba.Functional;
using Logic.Data;
using Logic.Models;
using Logic.Repositories;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static LaYumba.Functional.F;
using Unit = System.ValueTuple;

namespace Logic.AppServices
{
    public sealed class EditCustomerInfoCommand : ICommand<Task<Validation<Unit>>>
    {
        public EditCustomerInfoCommand(long id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public long Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public sealed class EditCustomerInfoCommandHandler : ICommandHandler<EditCustomerInfoCommand, Task<Validation<Unit>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public EditCustomerInfoCommandHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }

            public async Task<Validation<Unit>> Handle(EditCustomerInfoCommand command)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);

                var customer = await customerRepository.GetByIdAsync(command.Id);
                if (customer == null) return Error("Customer not found!");

                customer.Update(
                    command.FirstName,
                    command.LastName,
                    command.Age
                    );

                customerRepository.Update(customer);
                await unitOfWork.CommitAsync();

                return Unit();
            }
        }
    }
}
