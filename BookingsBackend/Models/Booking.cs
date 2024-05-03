using System.ComponentModel.DataAnnotations;

namespace BookingsBackend.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public string IdNumber { get; set; } = "";

        [Required]
        public string PhoneNumber { get; set; } 

        [Required]
        [Range(1, 120)] // Assuming age should be between 1 and 120
        public int Age { get; set; }

        // Add more properties as needed for Event Selection, Ticket Type, etc.
    }
}
