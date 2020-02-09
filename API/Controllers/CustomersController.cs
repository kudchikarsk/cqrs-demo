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
    public class CustomersController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CustomerRespository insuranceRepository;
        private readonly AddressesRepository nomineeRepository;

        public CustomersController(UnitOfWork unitOfWork, 
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            insuranceRepository = new CustomerRespository(unitOfWork);
            nomineeRepository = new AddressesRepository(unitOfWork);
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult Get()
        {
            var policies = insuranceRepository.GetAll();
            return Ok(policies);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var policy = insuranceRepository.GetByIdAsync(id);
            if (policy == null) return NotFound();
            return Ok(policy);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto value)
        {
            var policy = new Customer(
               value.FirstName,
               value.LastName,
               value.Age,
               GetAddresses(value.Addresses)
                );

            insuranceRepository.Add(policy);
            await unitOfWork.CommitAsync();

            var policyDto = mapper.Map<CustomerDto>(policy);
            return Created($"api/InsurancePolicy/{policy.Id}", policyDto);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] CustomerDto value)
        {
            var policy = await insuranceRepository.GetByIdAsync(id);
            if (policy == null) return NotFound();
            policy.Update(
                value.FirstName,
                value.LastName,
                value.Age,
                GetAddresses(value.Addresses)
                );

            insuranceRepository.Update(policy);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var policy = await insuranceRepository.GetByIdAsync(id);
            if (policy == null) return NotFound();

            insuranceRepository.Delete(policy);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        private ICollection<Address> GetAddresses(ICollection<AddressDto> addresses)
        {
            var ids = addresses.Select(i => i.Id).ToArray();
            return nomineeRepository.GetByIds(ids).ToList();
        }
    }
}
