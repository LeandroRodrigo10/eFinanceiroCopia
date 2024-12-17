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
    public class BankStatementsController : Controller
    {
        private readonly DataContext _context;

        public BankStatementsController(DataContext context)
        {
            _context = context;
        }

        // GET: BankStatements
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.BankStatement.Include(b => b.BankAccount).Include(b => b.TransactionType);
            return View(await dataContext.ToListAsync());
        }

        // GET: BankStatements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankStatement = await _context.BankStatement
                .Include(b => b.BankAccount)
                .Include(b => b.TransactionType)
                .FirstOrDefaultAsync(m => m.BankStatementID == id);
            if (bankStatement == null)
            {
                return NotFound();
            }

            return View(bankStatement);
        }

        // GET: BankStatements/Create
        public IActionResult Create()
        {
            ViewData["BankAccountID"] = new SelectList(_context.BankAccount, "BankAccountID", "BankAccountID");
            ViewData["TransactionTypeID"] = new SelectList(_context.Set<TransactionType>(), "TransactionTypeID", "TransactionTypeID");
            return View();
        }

        // POST: BankStatements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankStatementID,BankAccountID,TransactionTypeID,TransactionDate,Description,Amount,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] BankStatement bankStatement)
        {
            if (ModelState.IsValid)
            {
                bankStatement.BankStatementID = Guid.NewGuid();
                _context.Add(bankStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankAccountID"] = new SelectList(_context.BankAccount, "BankAccountID", "BankAccountID", bankStatement.BankAccountID);
            ViewData["TransactionTypeID"] = new SelectList(_context.Set<TransactionType>(), "TransactionTypeID", "TransactionTypeID", bankStatement.TransactionTypeID);
            return View(bankStatement);
        }

        // GET: BankStatements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankStatement = await _context.BankStatement.FindAsync(id);
            if (bankStatement == null)
            {
                return NotFound();
            }
            ViewData["BankAccountID"] = new SelectList(_context.BankAccount, "BankAccountID", "BankAccountID", bankStatement.BankAccountID);
            ViewData["TransactionTypeID"] = new SelectList(_context.Set<TransactionType>(), "TransactionTypeID", "TransactionTypeID", bankStatement.TransactionTypeID);
            return View(bankStatement);
        }

        // POST: BankStatements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BankStatementID,BankAccountID,TransactionTypeID,TransactionDate,Description,Amount,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] BankStatement bankStatement)
        {
            if (id != bankStatement.BankStatementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankStatement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankStatementExists(bankStatement.BankStatementID))
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
            ViewData["BankAccountID"] = new SelectList(_context.BankAccount, "BankAccountID", "BankAccountID", bankStatement.BankAccountID);
            ViewData["TransactionTypeID"] = new SelectList(_context.Set<TransactionType>(), "TransactionTypeID", "TransactionTypeID", bankStatement.TransactionTypeID);
            return View(bankStatement);
        }

        // GET: BankStatements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankStatement = await _context.BankStatement
                .Include(b => b.BankAccount)
                .Include(b => b.TransactionType)
                .FirstOrDefaultAsync(m => m.BankStatementID == id);
            if (bankStatement == null)
            {
                return NotFound();
            }

            return View(bankStatement);
        }

        // POST: BankStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bankStatement = await _context.BankStatement.FindAsync(id);
            if (bankStatement != null)
            {
                _context.BankStatement.Remove(bankStatement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankStatementExists(Guid id)
        {
            return _context.BankStatement.Any(e => e.BankStatementID == id);
        }
    }
}
