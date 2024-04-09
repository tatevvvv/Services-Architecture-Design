using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BankAccountService
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }

}
