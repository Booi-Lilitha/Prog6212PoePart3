using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaims.Models
{
    public class Document
    {
        public int Id { get; set; }

        public int ClaimId { get; set; }
        public Claim? Claim { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.Now;

        public string ContentType { get; set; } = string.Empty;
    }
}
