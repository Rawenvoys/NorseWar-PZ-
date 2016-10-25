using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Gra.Models
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> accountContext { get; set; }
    }
}