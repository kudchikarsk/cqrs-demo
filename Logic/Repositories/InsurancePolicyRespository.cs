using Logic.Models;
using Logic.Utils;
using System;
using System.Collections.Generic;
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

        public void Save(InsurancePolicy insurancePolicy)
        {
            _unitOfWork.Save(insurancePolicy);
        }

        public void Delete(InsurancePolicy insurancePolicy)
        {
            _unitOfWork.Delete(insurancePolicy);
        }
    }
}
