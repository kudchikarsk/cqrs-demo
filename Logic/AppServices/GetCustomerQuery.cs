using LaYumba.Functional;
using static LaYumba.Functional.F;
using Logic.Models;
using Logic.Repositories;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Logic.Data;

namespace Logic.AppServices
{
    public sealed class GetCustomerQuery:IQuery<Task<Validation<Customer>>>
    {
        public GetCustomerQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public sealed class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, Task<Validation<Customer>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public GetCustomerQueryHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }

            public async Task<Validation<Customer>> Handle(GetCustomerQuery query)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);
                var customer = await customerRepository.GetByIdAsync(query.Id);
                if (customer == null) return Error("Customer not found.");
                return customer;
            }
        }
    }
}
