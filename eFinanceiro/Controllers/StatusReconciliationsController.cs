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
    public class StatusReconciliationsController : Controller
    {
        private readonly DataContext _context;

        public StatusReconciliationsController(DataContext context)
        {
            _context = context;
        }

        // GET: StatusReconciliations
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusReconciliation.ToListAsync());
        }

        // GET: StatusReconciliations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusReconciliation = await _context.StatusReconciliation
                .FirstOrDefaultAsync(m => m.StatusReconciliationID == id);
            if (statusReconciliation == null)
            {
                return NotFound();
            }

            return View(statusReconciliation);
        }

        // GET: StatusReconciliations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusReconciliations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusReconciliationID,Name,RegistrationUserID,RegistrationDate,Ordering,DesactivationUserID,DesactivationDate,IsActive")] StatusReconciliation statusReconciliation)
        {
            if (ModelState.IsValid)
            {
                statusReconciliation.StatusReconciliationID = Guid.NewGuid();
                _context.Add(statusReconciliation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusReconciliation);
        }

        // GET: StatusReconciliations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusReconciliation = await _context.StatusReconciliation.FindAsync(id);
            if (statusReconciliation == null)
            {
                return NotFound();
            }
            return View(statusReconciliation);
        }

        // POST: StatusReconciliations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StatusReconciliationID,Name,RegistrationUserID,RegistrationDate,Ordering,DesactivationUserID,DesactivationDate,IsActive")] StatusReconciliation statusReconciliation)
        {
            if (id != statusReconciliation.StatusReconciliationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusReconciliation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusReconciliationExists(statusReconciliation.StatusReconciliationID))
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
            return View(statusReconciliation);
        }

        // GET: StatusReconciliations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusReconciliation = await _context.StatusReconciliation
                .FirstOrDefaultAsync(m => m.StatusReconciliationID == id);
            if (statusReconciliation == null)
            {
                return NotFound();
            }

            return View(statusReconciliation);
        }

        // POST: StatusReconciliations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var statusReconciliation = await _context.StatusReconciliation.FindAsync(id);
            if (statusReconciliation != null)
            {
                _context.StatusReconciliation.Remove(statusReconciliation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusReconciliationExists(Guid id)
        {
            return _context.StatusReconciliation.Any(e => e.StatusReconciliationID == id);
        }
    }
}
