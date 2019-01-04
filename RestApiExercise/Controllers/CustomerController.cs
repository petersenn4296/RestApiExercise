using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RestApiExercise.Models;
using RestApiExercise.Services;

namespace RestApiExercise.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Gets list of customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Customer>> Get()
        {
            return _customerService.Get();
        }
        
        /// <summary>
        /// Get customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = _customerService.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Customer> Post(Customer customer)
        {
            var newCustomer = _customerService.Create(customer);

            return Ok(newCustomer);
        }

        /// <summary>
        /// Update/Create Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Customer customer)
        {
            var customerExists = _customerService.Get(id);

            if (customerExists == null)
            {
                var newCustomer = _customerService.Create(customer);

                return Ok(newCustomer);
            }

            _customerService.Update(id, customer);

            return Ok(customer);
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var customer = _customerService.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerService.Remove(new ObjectId(customer.Id));

            return NoContent();
        }
    }
}
