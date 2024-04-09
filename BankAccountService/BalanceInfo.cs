using System.ComponentModel.DataAnnotations;

namespace BankAccountService
{
    public class BalanceInfo
    {
        public string AccountNumber { get; set; }

        public double Amount { get; set; }
    }
}
