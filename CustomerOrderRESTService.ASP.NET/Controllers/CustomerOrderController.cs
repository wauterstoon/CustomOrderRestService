using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderRESTService.ASP.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrderController : Controller
    {
        private IManager manager;

        public CustomerOrderController()
        {
            this.manager = new Manager(new UnitOfWork(new DataContext("Production")));
        }

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
                //Response.StatusCode = 400;
                return NotFound(ex.Message);
            }
        }
    }
}
