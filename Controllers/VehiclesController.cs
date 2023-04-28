using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleContext _context;
        public VehiclesController(VehicleContext context)
        {
            _context = context;
        }

        // GET: api/<VehiclesController>
        [HttpGet]
        public ActionResult Get()
        {
            var data = _context.Vehicles.Include(p => p.Maker).OrderBy(p => p.VIN)
                .Select(p => new { 
                    p.VIN, 
                    p.Year,
                    p.Model,
                    p.Pass,
                    p.InspectionDate,
                    p.InspectorName,
                    p.InspectionLocation,
                    p.Notes,
                    MakerName = p.Maker.Name
                }).ToArray();

            return Ok(data);
        }

        // GET api/<VehiclesController>/5
        [HttpGet("{vin}")]
        public async Task<ActionResult> Get(string vin)
        {
            var data = await _context.Vehicles.Include(p => p.Maker).Where(p => p.VIN == vin).Select(p => new {
                p.VIN,
                p.Year,
                p.Model,
                p.Pass,
                p.InspectionDate,
                p.InspectorName,
                p.InspectionLocation,
                p.Notes,
                MakerName = p.Maker.Name,
                MakerId = p.Maker.Id
            }).SingleOrDefaultAsync();
            if (data == null)
            {
                return BadRequest("Invalid vin");
            }

            return Ok(data);
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleModel model)
        {
            try
            {
                Vehicle vehicle = new Vehicle();
                vehicle.VIN = model.VIN;
                vehicle.Year = model.Year;
                vehicle.Model = model.Model;
                vehicle.Pass = model.Pass;
                vehicle.InspectionDate = model.InspectionDate;
                vehicle.InspectorName = model.InspectorName;
                vehicle.InspectionLocation = model.InspectionLocation;
                vehicle.Notes = model.Notes;
                vehicle.MakerId = model.MakerId;

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                return Ok(vehicle);

            }
            catch
            {
                return BadRequest("Fail to create vehicle");
            }
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{vin}")]
        public async Task<ActionResult> Put(string vin, [FromBody] VehicleModel model)
        {
            var vehicle = await _context.Vehicles.Where(p => p.VIN == vin).SingleOrDefaultAsync();
            if (vehicle == null)
            {
                return BadRequest("Invalid vin");
            }

            try
            {
                vehicle.VIN = model.VIN;
                vehicle.Year = model.Year;
                vehicle.Model = model.Model;
                vehicle.Pass = model.Pass;
                vehicle.InspectionDate = model.InspectionDate;
                vehicle.InspectorName = model.InspectorName;
                vehicle.InspectionLocation = model.InspectionLocation;
                vehicle.Notes = model.Notes;
                vehicle.MakerId = model.MakerId;

                _context.Update(vehicle);
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch
            {
                return BadRequest("Fail to update vehicle");
            }
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{vin}")]
        public async Task<ActionResult> Delete(string vin)
        {
            try
            {
                var vehicle = await _context.Vehicles.Where(p => p.VIN == vin).SingleOrDefaultAsync();
                if (vehicle == null)
                {
                    return BadRequest("Invalid vin");
                }

                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();

                return Ok("Vehicle is removed");
            }
            catch
            {
                return BadRequest("Fail to delete vehicle");
            }
        }
    }

    public class VehicleModel
    {
        [Required]
        public string VIN { get; set; }
        public int Year { get; set; }

        [StringLength(100)]
        public string Model { get; set; }
        public bool Pass { get; set; }

        [DataType(DataType.Date)]
        public DateTime InspectionDate { get; set; }

        [StringLength(100)]
        public string InspectorName { get; set; }

        [StringLength(100)]
        public string InspectionLocation { get; set; }

        [StringLength(32768)]
        public string Notes { get; set; }

        [Required]
        public int MakerId { get; set; }
    }
}
