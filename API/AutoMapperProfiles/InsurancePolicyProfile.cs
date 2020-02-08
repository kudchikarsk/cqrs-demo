using AutoMapper;
using Logic.Dtos;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.AutoMapperProfiles
{
    public class InsurancePolicyProfile :Profile
    {
        public InsurancePolicyProfile()
        {
            CreateMap<InsurancePolicy, InsurancePolicyDto>();
        }
    }
}
