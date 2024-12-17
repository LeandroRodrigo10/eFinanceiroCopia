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
    public class PartnersController : Controller
    {
        private readonly DataContext _context;

        public PartnersController(DataContext context)
        {
            _context = context;
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Partner.Include(p => p.Bank).Include(p => p.Subscription);
            return View(await dataContext.ToListAsync());
        }

        // GET: Partners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Bank)
                .Include(p => p.Subscription)
                .FirstOrDefaultAsync(m => m.PartnerID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Partners/Create
        public IActionResult Create()
        {
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID");
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID");
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerID,SubscriptionID,BankID,PartnerName,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                partner.PartnerID = Guid.NewGuid();
                _context.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID", partner.BankID);
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID", partner.SubscriptionID);
            return View(partner);
        }

        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID", partner.BankID);
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID", partner.SubscriptionID);
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartnerID,SubscriptionID,BankID,PartnerName,RegistrationUserID,RegistrationDate,DesactivationUserID,DesactivationDate,IsActive")] Partner partner)
        {
            if (id != partner.PartnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.PartnerID))
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
            ViewData["BankID"] = new SelectList(_context.Bank, "BankID", "BankID", partner.BankID);
            ViewData["SubscriptionID"] = new SelectList(_context.Set<Subscription>(), "SubscriptionID", "SubscriptionID", partner.SubscriptionID);
            return View(partner);
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Bank)
                .Include(p => p.Subscription)
                .FirstOrDefaultAsync(m => m.PartnerID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partner = await _context.Partner.FindAsync(id);
            if (partner != null)
            {
                _context.Partner.Remove(partner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(Guid id)
        {
            return _context.Partner.Any(e => e.PartnerID == id);
        }
    }
}
