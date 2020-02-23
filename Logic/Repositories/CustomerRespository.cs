using Logic.Models;
using Logic.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public sealed class CustomerRespository
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerRespository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Customer> GetByIdAsync(long id)
        {
            return _unitOfWork.Query<Customer>()
                .Include(nameof(Customer.Addresses))
                .SingleOrDefaultAsync(c=>c.Id == id);
        }

        public void Add(Customer customer)
        {
            _unitOfWork.Add(customer);
        }

        public void Update(Customer customer)
        {
            _unitOfWork.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _unitOfWork.Delete(customer);
        }

        public IReadOnlyCollection<Customer> GetAll()
        {
            return _unitOfWork.Query<Customer>().Include("Addresses").ToList();
        }

        
    }
}
