using Logic.Models;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Repositories
{
    public sealed class AddressesRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public AddressesRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Address> GetByIds(params long[] ids)
        {
            return _unitOfWork.Query<Address>()
                    .Where(n => ids.Contains(n.Id))
                    .ToList();
        }
    }
}
