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
    public class StatusInvoicesController : Controller
    {
        private readonly DataContext _context;

        public StatusInvoicesController(DataContext context)
        {
            _context = context;
        }

        // GET: StatusInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusInvoice.ToListAsync());
        }

        // GET: StatusInvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusInvoice = await _context.StatusInvoice
                .FirstOrDefaultAsync(m => m.StatusInvoiceID == id);
            if (statusInvoice == null)
            {
                return NotFound();
            }

            return View(statusInvoice);
        }

        // GET: StatusInvoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusInvoiceID,Name,RegistrationUserID,RegistrationDate,Ordering,DesactivationUserID,DesactivationDate,IsActive")] StatusInvoice statusInvoice)
        {
            if (ModelState.IsValid)
            {
                statusInvoice.StatusInvoiceID = Guid.NewGuid();
                _context.Add(statusInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusInvoice);
        }

        // GET: StatusInvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusInvoice = await _context.StatusInvoice.FindAsync(id);
            if (statusInvoice == null)
            {
                return NotFound();
            }
            return View(statusInvoice);
        }

        // POST: StatusInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StatusInvoiceID,Name,RegistrationUserID,RegistrationDate,Ordering,DesactivationUserID,DesactivationDate,IsActive")] StatusInvoice statusInvoice)
        {
            if (id != statusInvoice.StatusInvoiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusInvoiceExists(statusInvoice.StatusInvoiceID))
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
            return View(statusInvoice);
        }

        // GET: StatusInvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusInvoice = await _context.StatusInvoice
                .FirstOrDefaultAsync(m => m.StatusInvoiceID == id);
            if (statusInvoice == null)
            {
                return NotFound();
            }

            return View(statusInvoice);
        }

        // POST: StatusInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var statusInvoice = await _context.StatusInvoice.FindAsync(id);
            if (statusInvoice != null)
            {
                _context.StatusInvoice.Remove(statusInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusInvoiceExists(Guid id)
        {
            return _context.StatusInvoice.Any(e => e.StatusInvoiceID == id);
        }
    }
}
