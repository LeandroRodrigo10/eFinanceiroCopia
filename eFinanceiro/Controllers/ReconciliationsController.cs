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
    public class ReconciliationsController : Controller
    {
        private readonly DataContext _context;

        public ReconciliationsController(DataContext context)
        {
            _context = context;
        }

        // GET: Reconciliations
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Reconciliation.Include(r => r.BankStatement).Include(r => r.StatusReconciliation);
            return View(await dataContext.ToListAsync());
        }

        // GET: Reconciliations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reconciliation = await _context.Reconciliation
                .Include(r => r.BankStatement)
                .Include(r => r.StatusReconciliation)
                .FirstOrDefaultAsync(m => m.ReconciliationID == id);
            if (reconciliation == null)
            {
                return NotFound();
            }

            return View(reconciliation);
        }

        // GET: Reconciliations/Create
        public IActionResult Create()
        {
            ViewData["BankStatementID"] = new SelectList(_context.BankStatement, "BankStatementID", "BankStatementID");
            ViewData["StatusReconciliationID"] = new SelectList(_context.Set<StatusReconciliation>(), "StatusReconciliationID", "StatusReconciliationID");
            return View();
        }

        // POST: Reconciliations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReconciliationID,BankStatementID,StatusReconciliationID,ReconciliationDate,Amount,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Reconciliation reconciliation)
        {
            if (ModelState.IsValid)
            {
                reconciliation.ReconciliationID = Guid.NewGuid();
                _context.Add(reconciliation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankStatementID"] = new SelectList(_context.BankStatement, "BankStatementID", "BankStatementID", reconciliation.BankStatementID);
            ViewData["StatusReconciliationID"] = new SelectList(_context.Set<StatusReconciliation>(), "StatusReconciliationID", "StatusReconciliationID", reconciliation.StatusReconciliationID);
            return View(reconciliation);
        }

        // GET: Reconciliations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reconciliation = await _context.Reconciliation.FindAsync(id);
            if (reconciliation == null)
            {
                return NotFound();
            }
            ViewData["BankStatementID"] = new SelectList(_context.BankStatement, "BankStatementID", "BankStatementID", reconciliation.BankStatementID);
            ViewData["StatusReconciliationID"] = new SelectList(_context.Set<StatusReconciliation>(), "StatusReconciliationID", "StatusReconciliationID", reconciliation.StatusReconciliationID);
            return View(reconciliation);
        }

        // POST: Reconciliations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReconciliationID,BankStatementID,StatusReconciliationID,ReconciliationDate,Amount,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Reconciliation reconciliation)
        {
            if (id != reconciliation.ReconciliationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reconciliation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReconciliationExists(reconciliation.ReconciliationID))
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
            ViewData["BankStatementID"] = new SelectList(_context.BankStatement, "BankStatementID", "BankStatementID", reconciliation.BankStatementID);
            ViewData["StatusReconciliationID"] = new SelectList(_context.Set<StatusReconciliation>(), "StatusReconciliationID", "StatusReconciliationID", reconciliation.StatusReconciliationID);
            return View(reconciliation);
        }

        // GET: Reconciliations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reconciliation = await _context.Reconciliation
                .Include(r => r.BankStatement)
                .Include(r => r.StatusReconciliation)
                .FirstOrDefaultAsync(m => m.ReconciliationID == id);
            if (reconciliation == null)
            {
                return NotFound();
            }

            return View(reconciliation);
        }

        // POST: Reconciliations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reconciliation = await _context.Reconciliation.FindAsync(id);
            if (reconciliation != null)
            {
                _context.Reconciliation.Remove(reconciliation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReconciliationExists(Guid id)
        {
            return _context.Reconciliation.Any(e => e.ReconciliationID == id);
        }
    }
}
