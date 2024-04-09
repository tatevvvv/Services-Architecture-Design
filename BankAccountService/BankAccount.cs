using System.ComponentModel.DataAnnotations;

namespace BankAccountService
{
    public class BankAccount
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public double Balance { get; set; }
    }
}
