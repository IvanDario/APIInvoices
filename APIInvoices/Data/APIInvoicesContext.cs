using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIInvoices.Data
{


    public class APIInvoicesContext : DbContext
    {
        public APIInvoicesContext (DbContextOptions<APIInvoicesContext> options)
            : base(options)
        {
        }

        public DbSet<Models.CreditNote> CreditNote { get; set; }

        public DbSet<Models.Invoice> Invoice { get; set; }

    }
}
