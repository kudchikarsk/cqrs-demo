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
    public sealed class CreateCustomerCommand : ICommand<Task<Validation<Customer>>>
    {
        public CreateCustomerCommand(string firstName,
            string lastName,
            int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Task<Validation<Customer>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public CreateCustomerCommandHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }
            public async Task<Validation<Customer>> Handle(CreateCustomerCommand command)
            {

                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);

                Customer customer;
                try
                {
                    customer = new Customer(
                           command.FirstName,
                           command.LastName,
                           command.Age
                            );
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }

                customerRepository.Add(customer);
                await unitOfWork.CommitAsync();
                return customer;
            }
        }
    }
}
