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

        [HttpPut("Debit/{value}")]
        public async Task<ActionResult> DebitMoney(Guid id, double value)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            bankAccount.Balance -= value;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Credit/{value}")]
        public async Task<ActionResult> CreditMoney(Guid id, double value)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            bankAccount.Balance += value;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
