using Microsoft.AspNetCore.Mvc;
using BookingsBackend.Data;
using BookingsBackend.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using BookingsBackend.Configuration;

namespace BookingsBackend.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TwilioSettings _twilioSettings;

        public BookingController(ApplicationDbContext context, IOptions<TwilioSettings> twilioSettings)
        {
            _context = context;
            _twilioSettings = twilioSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                // Send SMS notification
                SendSmsNotification(booking);

                return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // Method to send SMS notification
        private void SendSmsNotification(Booking booking)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            var message = MessageResource.Create(
                body: $"Your booking was successful. Your ticket number is: {booking.Id}",
                from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
                to: new Twilio.Types.PhoneNumber(booking.PhoneNumber)
            );

            Console.WriteLine($"SMS sent: {message.Sid}");
        }
    }
}
