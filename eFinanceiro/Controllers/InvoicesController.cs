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
    public class InvoicesController : Controller
    {
        private readonly DataContext _context;

        public InvoicesController(DataContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Invoice.Include(i => i.Partner).Include(i => i.StatusInvoice);
            return View(await dataContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.StatusInvoice)
                .FirstOrDefaultAsync(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerID");
            ViewData["StatusInvoiceID"] = new SelectList(_context.Set<StatusInvoice>(), "StatusInvoiceID", "StatusInvoiceID");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceID,PartnerID,StatusInvoiceID,Amount,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.InvoiceID = Guid.NewGuid();
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerID", invoice.PartnerID);
            ViewData["StatusInvoiceID"] = new SelectList(_context.Set<StatusInvoice>(), "StatusInvoiceID", "StatusInvoiceID", invoice.StatusInvoiceID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerID", invoice.PartnerID);
            ViewData["StatusInvoiceID"] = new SelectList(_context.Set<StatusInvoice>(), "StatusInvoiceID", "StatusInvoiceID", invoice.StatusInvoiceID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceID,PartnerID,StatusInvoiceID,Amount,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Invoice invoice)
        {
            if (id != invoice.InvoiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceID))
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
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerID", invoice.PartnerID);
            ViewData["StatusInvoiceID"] = new SelectList(_context.Set<StatusInvoice>(), "StatusInvoiceID", "StatusInvoiceID", invoice.StatusInvoiceID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Partner)
                .Include(i => i.StatusInvoice)
                .FirstOrDefaultAsync(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoice.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(Guid id)
        {
            return _context.Invoice.Any(e => e.InvoiceID == id);
        }
    }
}
