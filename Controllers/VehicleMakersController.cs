using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using VehicleApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakersController : ControllerBase
    {
        private readonly VehicleContext _context;
        public VehicleMakersController(VehicleContext context)
        {
            _context = context;
        }

        // GET: api/<VehicleMakersController>
        [HttpGet]
        public ActionResult Get()
        {
            var vehicles = _context.VehicleMakers.OrderBy(p => p.Name).ToArray();
            return Ok(vehicles);
        }

        // GET api/<VehicleMakersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var vehicle = await _context.VehicleMakers.Where(p => p.Id == id).SingleOrDefaultAsync();
            if (vehicle == null)
            {
                return BadRequest("Fail to get vehicle maker with id " + id.ToString());
            }

            return Ok(vehicle);
        }

        // POST api/<VehicleMakersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string name)
        {
            try
            {
                VehicleMaker vehicleMaker = new VehicleMaker();
                vehicleMaker.Name = name;

                await _context.VehicleMakers.AddAsync(vehicleMaker);
                await _context.SaveChangesAsync();
            } 
            catch
            {
                return BadRequest("Fail to create vehicle maker");
            }

            return Ok("Vehicle maker is created successfully");
        }

        // PUT api/<VehicleMakersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] string name)
        {
            try
            {
                var vehicleMaker =await _context.VehicleMakers.Where(p => p.Id == id).SingleOrDefaultAsync();
                if (vehicleMaker == null)
                {
                    return BadRequest("Invalid vehicle maker data");
                }
                vehicleMaker.Name = name;

                _context.VehicleMakers.Update(vehicleMaker);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Fail to update vehicle maker");
            }

            return Ok("Vehicle maker is updated successfully");
        }

        // DELETE api/<VehicleMakersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var vehicleMaker = await _context.VehicleMakers.Where(p => p.Id == id).SingleOrDefaultAsync();
                if (vehicleMaker == null)
                {
                    return BadRequest("Invalid vehicle maker data");
                }

                _context.VehicleMakers.Remove(vehicleMaker);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Fail to delete vehicle maker");
            }

            return Ok("Vehicle maker is deleted successfully");
        }
    }
}
