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
        public IActionResult GetList()
        {
            var customers = customerRepository.GetAll();
            var customersDto = mapper.Map<List<CustomerDto>>(customers);
            return Ok(customersDto);
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetCustomer(long id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            var customerDto = mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto value)
        {
            var customer = new Customer(
               value.FirstName,
               value.LastName,
               value.Age
                );

            customerRepository.Add(customer);
            await unitOfWork.CommitAsync();

            var customerDto = mapper.Map<CustomerDto>(customer);
            return Created($"api/Customers/{customer.Id}", customerDto);
        }

        // POST: api/Customers/5/Addresses
        [HttpPost("{customerId}/Addresses")]
        public async Task<IActionResult> AddAddress(long customerId, [FromBody] CreateAddressDto value)
        {
            var customer = await customerRepository.GetByIdAsync(customerId);
            if (customer == null) return NotFound();

            var address = new Address(
                value.Street,
                value.City,
                value.ZipCode
                );

            customer.AddAddress(address);
            customerRepository.Update(customer);
            await unitOfWork.CommitAsync();

            var addressDto = mapper.Map<AddressDto>(address);
            return Ok(addressDto);
        }



        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomerInfo(long id, [FromBody] EditCustomerDto value)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            customer.Update(
                value.FirstName,
                value.LastName,
                value.Age
                );

            customerRepository.Update(customer);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            customerRepository.Delete(customer);
            await unitOfWork.CommitAsync();

            return NoContent();
        }

        // DELETE: api/Customers/5/Addresses/1
        [HttpDelete("{customerId}/Addresses/{addressId}")]
        public async Task<IActionResult> RemoveAddress(long customerId, long addressId)
        {
            var customer = await customerRepository.GetByIdAsync(customerId);
            if (customer == null) return NotFound();

            var address = customer.Addresses.SingleOrDefault(a => a.Id == addressId);
            if (address == null) return NotFound();

            customer.RemoveAddress(address);

            customerRepository.Update(customer);
            await unitOfWork.CommitAsync();

            return NoContent();
        }



    }
}
