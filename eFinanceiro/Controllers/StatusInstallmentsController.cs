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
    public class StatusInstallmentsController : Controller
    {
        private readonly DataContext _context;

        public StatusInstallmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: StatusInstallments
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusInstallment.ToListAsync());
        }

        // GET: StatusInstallments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusInstallment = await _context.StatusInstallment
                .FirstOrDefaultAsync(m => m.StatusInstallmentID == id);
            if (statusInstallment == null)
            {
                return NotFound();
            }

            return View(statusInstallment);
        }

        // GET: StatusInstallments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusInstallments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusInstallmentID,Name,RegistrationUserID,RegistrationDate,Ordering,DesactivationUserID,DesactivationDate,IsActive")] StatusInstallment statusInstallment)
        {
            if (ModelState.IsValid)
            {
                statusInstallment.StatusInstallmentID = Guid.NewGuid();
                _context.Add(statusInstallment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusInstallment);
        }

        // GET: StatusInstallments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusInstallment = await _context.StatusInstallment.FindAsync(id);
            if (statusInstallment == null)
            {
                return NotFound();
            }
            return View(statusInstallment);
        }

        // POST: StatusInstallments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StatusInstallmentID,Name,RegistrationUserID,RegistrationDate,Ordering,DesactivationUserID,DesactivationDate,IsActive")] StatusInstallment statusInstallment)
        {
            if (id != statusInstallment.StatusInstallmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusInstallment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusInstallmentExists(statusInstallment.StatusInstallmentID))
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
            return View(statusInstallment);
        }

        // GET: StatusInstallments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusInstallment = await _context.StatusInstallment
                .FirstOrDefaultAsync(m => m.StatusInstallmentID == id);
            if (statusInstallment == null)
            {
                return NotFound();
            }

            return View(statusInstallment);
        }

        // POST: StatusInstallments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var statusInstallment = await _context.StatusInstallment.FindAsync(id);
            if (statusInstallment != null)
            {
                _context.StatusInstallment.Remove(statusInstallment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusInstallmentExists(Guid id)
        {
            return _context.StatusInstallment.Any(e => e.StatusInstallmentID == id);
        }
    }
}
