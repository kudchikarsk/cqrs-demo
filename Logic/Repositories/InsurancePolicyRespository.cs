using Logic.Models;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public sealed class InsurancePolicyRespository
    {
        private readonly UnitOfWork _unitOfWork;

        public InsurancePolicyRespository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<InsurancePolicy> GetByIdAsync(long id)
        {
            return _unitOfWork.GetAsync<InsurancePolicy, long>(id);
        }

        public void Add(InsurancePolicy insurancePolicy)
        {
            _unitOfWork.Add(insurancePolicy);
        }

        public void Update(InsurancePolicy insurancePolicy)
        {
            _unitOfWork.Update(insurancePolicy);
        }

        public void Delete(InsurancePolicy insurancePolicy)
        {
            _unitOfWork.Delete(insurancePolicy);
        }

        public IEnumerable<InsurancePolicy> GetAll()
        {
            return _unitOfWork.Query<InsurancePolicy>().ToList();
        }

        
    }
}
