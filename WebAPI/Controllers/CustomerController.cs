using DataAccess.Entities;
using DataAccess.Repo;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepo _repo;
        private readonly ILogger _logger;

        public CustomerController(IRepo repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("getall")]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            System.Diagnostics.Debug.WriteLine("--> Getting All ...");

            IEnumerable<Customer> customers = _repo.GetAll();

            if (customers == null)
            {
                _logger.Log(LogLevel.Error, "--> No Customers found ...");
            }

            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Customer> GetById(int id)
        {
            System.Diagnostics.Debug.WriteLine("--> Getting By Id ...");

            Customer customer = _repo.GetById(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            _logger.Log(LogLevel.Error, "--> No Customer found ...");
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            System.Diagnostics.Debug.WriteLine("--> Creating ...");

            _repo.Create(customer);

            return CreatedAtRoute(nameof(GetById), new { Id = customer.Id }, customer);
        }
    }
}
