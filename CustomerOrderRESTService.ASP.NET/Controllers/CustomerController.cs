using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrderRESTService.ASP.NET.Models;
using CustomerOrderRESTService.BusinessLayer.Exceptions;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderRESTService.ASP.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private Manager manager;

        public CustomerController()
        {
            this.manager = new Manager(new UnitOfWork(new DataContext("Production")));
        }

        //TODO customer with list orders
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            try
            {
                return Ok(manager.FindCustomer(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            BusinessLayer.Models.Customer customerCreated = manager.AddCustomer(customer.Name, customer.Address);
            if (customerCreated != null)
            {
                //TODO CustomerId als link
                return CreatedAtAction(nameof(Get), new { id = customerCreated.Id }, customerCreated);
            }
            else
            {
                //TODO return exception
                return NotFound("Update Customer - This combination of Address and Name already exists");
            }
        }

      
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }
            if (manager.FindCustomer(customer.Id) == null)
            {
                manager.AddCustomer(customer.Name, customer.Address);
                //TODO CustomerId als link
                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            manager.UpdateCustomer(customer.Id, customer.Name, customer.Address);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int customerId)
        {
            manager.DeleteCustomer(customerId);

            return NoContent();
        }

        //TODO customer find ipv null,  customerlink ipv id, orderlink ipv id, producttype naam ipv getal
        [HttpGet("{customerId}/order/{orderId}")]
        public ActionResult<Order> Get(int customerId, int orderId)
        {
            try
            {
                
                return Ok(manager.FindOrder(orderId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("{id}/order")]
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            BusinessLayer.Models.Order orderCreated = manager.AddOrder(order.CustomerId, order.Product, order.Amount);
            if (orderCreated != null)
            {
                //TODO fix looping
                return CreatedAtAction(nameof(Get), new { id = orderCreated.Id }, orderCreated);
            }
            else
            {
                //TODO return exception
                return NotFound("Erroorrrrr");
            }
        }

        [HttpPut("{customerId}/order/{orderId}")]
        public IActionResult Put(int customerId, [FromBody] Order order, int orderId)
        {
            if (order == null || order.CustomerId != customerId)
            {
                return BadRequest();
            }
            if (manager.FindOrder(orderId) == null)
            {
                BusinessLayer.Models.Order orderCreated = manager.AddOrder(customerId, order.Product, order.Amount);
                //TODO fix looping
                //try 
               // return CreatedAtAction(nameof(Get), orderCreated);
                return CreatedAtAction(nameof(Get), new { id = orderCreated.Id }, orderCreated);
            }
            manager.UpdateOrder(orderId, customerId, order.Amount, order.Product);
            return new NoContentResult();
        }

        [HttpDelete("{customerId}/order/{orderId}")]
        public IActionResult Delete(int customerId, int orderId)
        {
            manager.DeleteOrder(orderId, customerId);

            return NoContent();
        }
    }
}
