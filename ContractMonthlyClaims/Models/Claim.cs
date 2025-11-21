using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaims.Models
{
    public enum ClaimStatus
    {
        Submitted = 0,
        ApprovedByCoordinator = 1,
        ApprovedByManager = 2,
        Rejected = 3
    }

    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string LecturerUsername { get; set; } = string.Empty;

        public int? UserId { get; set; }
        public User? User { get; set; }

        public DateTime SubmitDate { get; set; } = DateTime.UtcNow;

        [Range(1, 12)]
        public int Month { get; set; }

        [Range(2000, 9999)]
        public int Year { get; set; }

        public decimal Hours { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Submitted;

        public decimal Amount { get; set; }

        public List<ClaimItem> ClaimItems { get; set; } = new();

        public List<Document> Documents { get; set; } = new();
    }
}
