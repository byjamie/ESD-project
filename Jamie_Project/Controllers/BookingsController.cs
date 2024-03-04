using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jamie_Project.Data;
using Jamie_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jamie_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Bookings);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int? id)
        {
            var bookings = _context.Bookings.FirstOrDefault(e => e.BookingId == id);
            if (bookings == null)
            {
                return Problem(detail: "Booking with id " + id + " cannot be not found.", statusCode: 404);
            }
            return Ok(bookings);
        }

        [HttpPost]
        public IActionResult Post(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return CreatedAtAction("GetAll", new { id = booking.BookingId }, booking);
        }

        [HttpPut]
        public IActionResult Put(int? id, Booking booking)
        {
            var entity = _context.Bookings.FirstOrDefault(e =>e.BookingId == id);
            if (entity == null)
            {
                return Problem(detail: "Booking with id " + id + " cannot be found.", statusCode: 404);
            }

            entity.FacilityDescription = booking.FacilityDescription;
            entity.BookingDateFrom = booking.BookingDateFrom;
            entity.BookingDateTo = booking.BookingDateTo;
            entity.BookedBy = booking.BookedBy;
            entity.BookingStatus = booking.BookingStatus;

            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete]
        public IActionResult Delete(int? id, Booking booking)
        {
            var entity = _context.Bookings.FirstOrDefault(e =>e.BookingId == id);
            if (entity == null)
            {
                return Problem(detail: "Booking with id " + id + " cannot be found.", statusCode: 404);
            }

            _context.Bookings.Remove(entity);
            _context.SaveChanges();

            return Ok(entity);
        }
    }
}
