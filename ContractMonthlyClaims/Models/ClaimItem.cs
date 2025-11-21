using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaims.Models
{
    public class ClaimItem
    {
        public int Id { get; set; }

        public int ClaimId { get; set; }
        public Claim? Claim { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Hours { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Rate { get; set; }

        // store amount in DB (we will compute in server before saving)
        public decimal Amount { get; set; }

    }
}

