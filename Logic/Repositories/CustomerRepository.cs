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
    public sealed class CustomerRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            return await _unitOfWork.Query<Customer>()
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

        public async Task<IReadOnlyCollection<Customer>> GetAll()
        {
            return await _unitOfWork.Query<Customer>()
                .Include(nameof(Customer.Addresses))
                .ToListAsync();
        }

        
    }
}
