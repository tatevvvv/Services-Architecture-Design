using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly BankDbContext _context;

        public BankAccountController(BankDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<BankAccount>> CreateBankAccount(BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                return BadRequest();
            }

            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBankAccount), new { id = bankAccount.Id }, bankAccount);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(Guid id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return bankAccount;
        }

        [HttpPut("Debit")]
        public async Task<ActionResult> DebitMoney(BalanceInfo balanceInfo)
        {
            var bankAccount = _context.BankAccounts.FirstOrDefault(b => b.AccountNumber == balanceInfo.AccountNumber);

            if (bankAccount == null)
            {
                return NotFound();
            }

            bankAccount.Balance -= balanceInfo.Amount;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Credit")]
        public async Task<ActionResult> CreditMoney(BalanceInfo balanceInfo)
        {
            var bankAccount = _context.BankAccounts.FirstOrDefault(b => b.AccountNumber == balanceInfo.AccountNumber);

            if (bankAccount == null)
            {
                return NotFound();
            }

            bankAccount.Balance += balanceInfo.Amount;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
