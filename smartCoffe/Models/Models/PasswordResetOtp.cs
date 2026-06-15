using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class PasswordResetOtp
    {
        [Key]
        public int OtpID { get; set; }

        public int UserID { get; set; }

        public string OtpCode { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; } = false;

        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;
    }
}