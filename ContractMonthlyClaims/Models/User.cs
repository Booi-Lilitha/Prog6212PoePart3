using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaims.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(0, 9999)]
        public decimal HourlyRate { get; set; }

        public Role Role { get; set; } = Role.Lecturer;

        public List<Claim> Claims { get; set; } = new();
    }
}
