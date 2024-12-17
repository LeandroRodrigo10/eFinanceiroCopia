using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;

namespace eFinanceiro.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly DataContext _context;

        public BankAccountsController(DataContext context)
        {
            _context = context;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.BankAccount.Include(b => b.Bank).Include(b => b.Subscription);
            return View(await dataContext.ToListAsync());
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccount
                .Include(b => b.Bank)
                .Include(b => b.Subscription)
                .FirstOrDefaultAsync(m => m.BankAccountID == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID");
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankAccountID,SubscriptionID,BankID,Agency,AccountNumber,InitialBalance,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                bankAccount.BankAccountID = Guid.NewGuid();
                _context.Add(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID", bankAccount.BankID);
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID", bankAccount.SubscriptionID);
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccount.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID", bankAccount.BankID);
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID", bankAccount.SubscriptionID);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BankAccountID,SubscriptionID,BankID,Agency,AccountNumber,InitialBalance,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] BankAccount bankAccount)
        {
            if (id != bankAccount.BankAccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.BankAccountID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID", bankAccount.BankID);
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID", bankAccount.SubscriptionID);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccount
                .Include(b => b.Bank)
                .Include(b => b.Subscription)
                .FirstOrDefaultAsync(m => m.BankAccountID == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bankAccount = await _context.BankAccount.FindAsync(id);
            if (bankAccount != null)
            {
                _context.BankAccount.Remove(bankAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(Guid id)
        {
            return _context.BankAccount.Any(e => e.BankAccountID == id);
        }
    }
}
