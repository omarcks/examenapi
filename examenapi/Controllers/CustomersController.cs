using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using examenapi.Contexts;
using examenapi.Entities;

namespace examenapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetProduct()
        {
            try
            {
                var customer = await _context.customers.ToListAsync();

                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return Problem(ex.Message);
            }
        }

        //// GET: api/Customers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomers(int id)
        //{
        //    var customers = await _context.customers.FindAsync(id);

        //    if (customers == null)
        //    {
        //        return NotFound();
        //    }

        //    return customers;
        //}





        private bool CustomersExists(int id)
        {
            return _context.customers.Any(e => e.CustomerID == id);
        }
    }
}
