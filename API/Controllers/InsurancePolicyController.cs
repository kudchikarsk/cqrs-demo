using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Logic.Dtos;
using Logic.Models;
using Logic.Repositories;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly InsurancePolicyRespository insuranceRepository;
        private readonly NomineeRepository nomineeRepository;

        public InsurancePolicyController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            insuranceRepository = new InsurancePolicyRespository(unitOfWork);
            nomineeRepository = new NomineeRepository(unitOfWork);
        }

        // GET: api/InsurancePolicy
        [HttpGet]
        public IActionResult Get()
        {
            var policies = insuranceRepository.GetAll();
            return Ok(policies);
        }

        // GET: api/InsurancePolicy/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var policy = insuranceRepository.GetByIdAsync(id);
            if (policy == null) return NotFound();
            return Ok(policy);
        }

        // POST: api/InsurancePolicy
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsurancePolicyDto value)
        {
            var policy = new InsurancePolicy(
                value.PolicyHolderName,
                value.SumInsured,
                value.PremiumAmount,
                GetNominees(value.Nominees)
                );

            insuranceRepository.Add(policy);
            await unitOfWork.CommitAsync();

            var policyDto = mapper.Map<InsurancePolicyDto>(policy);
            return Created($"api/InsurancePolicy/{policy.Id}", policyDto);
        }

        // PUT: api/InsurancePolicy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] InsurancePolicyDto value)
        {
            var policy = await insuranceRepository.GetByIdAsync(id);
            if (policy == null) return NotFound();
            policy.Update(
                value.PolicyHolderName,
                value.SumInsured,
                value.PremiumAmount,
                GetNominees(value.Nominees)
                );

            insuranceRepository.Update(policy);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        // DELETE: api/InsurancePolicy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var policy = await insuranceRepository.GetByIdAsync(id);
            if (policy == null) return NotFound();

            insuranceRepository.Delete(policy);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        private ICollection<Nominee> GetNominees(ICollection<NomineeDto> nominees)
        {
            var ids = nominees.Select(i => i.Id).ToArray();
            return nomineeRepository.GetNomiees(ids).ToList();
        }
    }
}
