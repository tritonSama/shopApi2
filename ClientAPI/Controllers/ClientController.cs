using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientAPI.Data;

namespace ClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            return Ok(await _context.Clients.ToListAsync());
           
        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> CreateClient(Client cust)
        {
            _context.Clients.Add(cust);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Client>>> UpdateSuperHero(Client cust)
        {
            var dbCust = await _context.Clients.FindAsync(cust.Id);
            if (dbCust == null)
                return BadRequest("Person not found.");

            dbCust.Email = cust.Email;
            dbCust.FirstName = cust.FirstName;
            dbCust.LastName = cust.LastName;
            dbCust.PhoneNumber = cust.PhoneNumber;
            dbCust.BirthDate = cust.BirthDate;
            

            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Client>>> DeleteSuperHero(int id)
        {
            var dbCust = await _context.Clients.FindAsync(id);
            if (dbCust == null)
                return BadRequest("Client not found.");

            _context.Clients.Remove(dbCust);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }
    }
}
