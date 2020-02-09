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
        private readonly CustomerRespository customerRepository;

        public CustomersController(UnitOfWork unitOfWork, 
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            customerRepository = new CustomerRespository(unitOfWork);
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerRepository.GetAll();
            var customersDto = mapper.Map<List<CustomerDto>>(customers);
            return Ok(customersDto);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(long id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            var customerDto = mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto value)
        {
            var customer = new Customer(
               value.FirstName,
               value.LastName,
               value.Age,
               GetAddresses(value.Addresses)
                );

            customerRepository.Add(customer);
            await unitOfWork.CommitAsync();

            var policyDto = mapper.Map<CustomerDto>(customer);
            return Created($"api/InsurancePolicy/{customer.Id}", policyDto);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] CustomerDto value)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            customer.Update(
                value.FirstName,
                value.LastName,
                value.Age,
                GetAddresses(value.Addresses)
                );

            customerRepository.Update(customer);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            customerRepository.Delete(customer);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        private ICollection<Address> GetAddresses(ICollection<AddressDto> addresses)
        {
            return addresses.Select(a =>
            {
                return new Address(
                    a.Street,
                    a.City,
                    a.ZipCode
                    );
            }).ToList();
        }
    }
}
