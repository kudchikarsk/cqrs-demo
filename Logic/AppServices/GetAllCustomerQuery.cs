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
    public sealed class GetAllCustomerQuery : IQuery<Task<Validation<IReadOnlyCollection<Customer>>>>
    {
        public GetAllCustomerQuery()
        {
                
        }

        public sealed class GetAllCustomerQueryHandler : IQueryHandler<GetAllCustomerQuery, Task<Validation<IReadOnlyCollection<Customer>>>>
        {
            private readonly DbContextFactory dbContextFactory;

            public GetAllCustomerQueryHandler(DbContextFactory dbContextFactory)
            {
                this.dbContextFactory = dbContextFactory;
            }

            public async Task<Validation<IReadOnlyCollection<Customer>>> Handle(GetAllCustomerQuery query)
            {
                var unitOfWork = new UnitOfWork(dbContextFactory);
                var customerRepository = new CustomerRepository(unitOfWork);
                var customers = await customerRepository.GetAll();
                return Valid(customers);
            }
        }
    }
}
