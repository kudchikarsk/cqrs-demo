using Logic.Models;
using Logic.Utils;
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
            return _unitOfWork.GetAsync<Customer, long>(id);
        }

        public void Add(Customer insurancePolicy)
        {
            _unitOfWork.Add(insurancePolicy);
        }

        public void Update(Customer insurancePolicy)
        {
            _unitOfWork.Update(insurancePolicy);
        }

        public void Delete(Customer insurancePolicy)
        {
            _unitOfWork.Delete(insurancePolicy);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _unitOfWork.Query<Customer>().ToList();
        }

        
    }
}
