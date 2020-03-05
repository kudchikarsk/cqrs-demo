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
using Microsoft.Data.SqlClient;
using Dapper;
using Logic.Dtos;
using System.Linq;

namespace Logic.AppServices
{
    public sealed class GetAllCustomerQuery : IQuery<Task<Validation<IReadOnlyCollection<CustomerInfoDto>>>>
    {
        public GetAllCustomerQuery()
        {
                
        }

        public sealed class GetAllCustomerQueryHandler : IQueryHandler<GetAllCustomerQuery, Task<Validation<IReadOnlyCollection<CustomerInfoDto>>>>
        {
            private readonly ConnectionString connectionString;

            public GetAllCustomerQueryHandler(ConnectionString connectionString)
            {
                this.connectionString = connectionString;
            }

            public async Task<Validation<IReadOnlyCollection<CustomerInfoDto>>> Handle(GetAllCustomerQuery query)
            {
                string sql = @"
                    SELECT Customers.Id, FirstName + ' ' + LastName as Name, Age, Street, City, ZipCode FROM 
                    Customers INNER JOIN Addresses
                    ON Addresses.Id = (
                            SELECT  TOP 1 Id
                            FROM    Addresses
		                    WHERE CustomerId = Customers.Id
                            ORDER BY  IsPrimary DESC
                             );";

                using (SqlConnection connection = new SqlConnection(connectionString.Value))
                {
                    IReadOnlyCollection<CustomerInfoDto> customers = connection
                        .Query<CustomerInfoDto>(sql)
                        .ToList();

                    return Valid(customers);
                }
                
            }
        }
    }
}
