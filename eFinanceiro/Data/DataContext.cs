using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data;

    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Data.Bank> Bank { get; set; } = default!;

public DbSet<Data.BankAccount> BankAccount { get; set; } = default!;

public DbSet<Data.BankStatement> BankStatement { get; set; } = default!;

public DbSet<Data.Installment> Installment { get; set; } = default!;

public DbSet<Data.Invoice> Invoice { get; set; } = default!;

public DbSet<Data.Partner> Partner { get; set; } = default!;

public DbSet<Data.Reconciliation> Reconciliation { get; set; } = default!;

public DbSet<Data.StatusInstallment> StatusInstallment { get; set; } = default!;

public DbSet<Data.StatusInvoice> StatusInvoice { get; set; } = default!;

public DbSet<Data.StatusReconciliation> StatusReconciliation { get; set; } = default!;

public DbSet<Data.Subscription> Subscription { get; set; } = default!;
    }
