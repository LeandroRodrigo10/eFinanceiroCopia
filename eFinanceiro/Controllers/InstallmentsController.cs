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
    public class InstallmentsController : Controller
    {
        private readonly DataContext _context;

        public InstallmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: Installments
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Installment.Include(i => i.Invoice).Include(i => i.StatusInstallment);
            return View(await dataContext.ToListAsync());
        }

        // GET: Installments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installment = await _context.Installment
                .Include(i => i.Invoice)
                .Include(i => i.StatusInstallment)
                .FirstOrDefaultAsync(m => m.InstallmentID == id);
            if (installment == null)
            {
                return NotFound();
            }

            return View(installment);
        }

        // GET: Installments/Create
        public IActionResult Create()
        {
            ViewData["InvoiceID"] = new SelectList(_context.Set<Invoice>(), "InvoiceID", "InvoiceID");
            ViewData["StatusInstallmentID"] = new SelectList(_context.Set<StatusInstallment>(), "StatusInstallmentID", "StatusInstallmentID");
            return View();
        }

        // POST: Installments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstallmentID,InvoiceID,StatusInstallmentID,Amount,DueDate,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Installment installment)
        {
            if (ModelState.IsValid)
            {
                installment.InstallmentID = Guid.NewGuid();
                _context.Add(installment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceID"] = new SelectList(_context.Set<Invoice>(), "InvoiceID", "InvoiceID", installment.InvoiceID);
            ViewData["StatusInstallmentID"] = new SelectList(_context.Set<StatusInstallment>(), "StatusInstallmentID", "StatusInstallmentID", installment.StatusInstallmentID);
            return View(installment);
        }

        // GET: Installments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installment = await _context.Installment.FindAsync(id);
            if (installment == null)
            {
                return NotFound();
            }
            ViewData["InvoiceID"] = new SelectList(_context.Set<Invoice>(), "InvoiceID", "InvoiceID", installment.InvoiceID);
            ViewData["StatusInstallmentID"] = new SelectList(_context.Set<StatusInstallment>(), "StatusInstallmentID", "StatusInstallmentID", installment.StatusInstallmentID);
            return View(installment);
        }

        // POST: Installments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InstallmentID,InvoiceID,StatusInstallmentID,Amount,DueDate,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Installment installment)
        {
            if (id != installment.InstallmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(installment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstallmentExists(installment.InstallmentID))
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
            ViewData["InvoiceID"] = new SelectList(_context.Set<Invoice>(), "InvoiceID", "InvoiceID", installment.InvoiceID);
            ViewData["StatusInstallmentID"] = new SelectList(_context.Set<StatusInstallment>(), "StatusInstallmentID", "StatusInstallmentID", installment.StatusInstallmentID);
            return View(installment);
        }

        // GET: Installments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var installment = await _context.Installment
                .Include(i => i.Invoice)
                .Include(i => i.StatusInstallment)
                .FirstOrDefaultAsync(m => m.InstallmentID == id);
            if (installment == null)
            {
                return NotFound();
            }

            return View(installment);
        }

        // POST: Installments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var installment = await _context.Installment.FindAsync(id);
            if (installment != null)
            {
                _context.Installment.Remove(installment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstallmentExists(Guid id)
        {
            return _context.Installment.Any(e => e.InstallmentID == id);
        }
    }
}
