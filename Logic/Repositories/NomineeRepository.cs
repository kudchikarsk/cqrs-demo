using Logic.Models;
using Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Repositories
{
    public sealed class NomineeRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public NomineeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Nominee> GetNomiees(params long[] ids)
        {
            return _unitOfWork.Query<Nominee>()
                    .Where(n => ids.Contains(n.Id))
                    .ToList();
        }
    }
}
