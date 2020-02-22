using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using Unit = System.ValueTuple;
using Logic.AppServices;
using Logic.Dtos;
using Logic.Models;
using Logic.Repositories;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;
using static Logic.AppServices.EditCustomerInfoCommand;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Messages messages;
        private readonly IMapper mapper;

        public CustomersController(Messages messages, IMapper mapper)
        {
            this.messages = messages;
            this.mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var query = new GetAllCustomerQuery();

            var result = await messages.Dispatch(query);

            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (customers) =>
                {
                    var customersDto = mapper.Map<List<CustomerDto>>(customers);
                    return Ok(customersDto);
                });
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetCustomer(long id)
        {
            var query = new GetCustomerQuery(id);

            var result = await messages.Dispatch(query);

            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (customer) =>
                {
                    var customerDto = mapper.Map<CustomerDto>(customer);
                    return Ok(customerDto);
                });
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto value)
        {


            var command = new CreateCustomerCommand(value.FirstName,
               value.LastName,
               value.Age);

            var result = await messages.Dispatch(command);

            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (customer) =>
                {
                    var customerDto = mapper.Map<CustomerDto>(customer);
                    return Created($"api/Customers/{customer.Id}", customerDto);
                });
        }

        // POST: api/Customers/5/Addresses
        [HttpPost("{customerId}/Addresses")]
        public async Task<IActionResult> AddAddress(long customerId, [FromBody] CreateAddressDto value)
        {
            var command = new AddAddressCommand(
                customerId,
                value.Street,
                value.City,
                value.ZipCode
                );

            var result = await messages.Dispatch(command);

            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (address) =>
                {
                    var addressDto = mapper.Map<AddressDto>(address);
                    return Ok(addressDto);
                });

        }



        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomerInfo(long id, [FromBody] EditCustomerDto value)
        {
            var command = new EditCustomerInfoCommand(id,
                value.FirstName,
                value.LastName,
                value.Age);


            var result = await messages.Dispatch(command);


            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (valid) => NoContent()
                );
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var command = new DeleteCustomerCommand(id);

            var result = await messages.Dispatch(command);

            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (valid) => NoContent()
                );
        }

        // DELETE: api/Customers/5/Addresses/1
        [HttpDelete("{customerId}/Addresses/{addressId}")]
        public async Task<IActionResult> RemoveAddress(long customerId, long addressId)
        {
            var command = new RemoveAddressCommand(customerId, addressId);

            var result = await messages.Dispatch(command);

            return result.Match<IActionResult>(
                (errors) => BadRequest(errors),
                (valid) => NoContent()
                );
        }



    }
}
