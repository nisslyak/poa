#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PurchaseOrderApp.Data;
using PurchaseOrderApp.Models;


namespace PurchaseOrderApp.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly PurchaseOrderAppContext _context;

        public PurchaseOrdersController(PurchaseOrderAppContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        public async Task<IActionResult> Index()
        {
            var purchaseOrderAppContext = _context.PurchaseOrder.Include(p => p.User);
            return View(await purchaseOrderAppContext.ToListAsync());
        }


        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .Include(p => p.User)
                .Include(p => p.LineItems)
                .FirstOrDefaultAsync(m => m.PurchaseOrderID == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }


        // GET: PurchaseOrders/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id");

            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationDate,Name,UserID,TotalAmount,CurrentStatus")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id", purchaseOrder.UserID);
            return View(purchaseOrder);
        }
 

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
            .Include(p => p.LineItems)
            .FirstOrDefaultAsync(m => m.PurchaseOrderID == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            if(purchaseOrder.Status == Status.DRAFT)
            {
                ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id", purchaseOrder.UserID);
                return View(purchaseOrder);
            }
            else
            {
                return Content("The purchase order has been submitted and cannot be updated.");
            }

        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationDate,Name,UserID,TotalAmount,CurrentStatus")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseOrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.PurchaseOrderID))
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
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "Id", "Id", purchaseOrder.UserID);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PurchaseOrderID == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            _context.PurchaseOrder.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: PurchaseOrders/Submit/5
        public async Task<IActionResult> Submit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            purchaseOrder.Status = Status.SUBMITTED;
            await _context.SaveChangesAsync();

            return View(purchaseOrder);
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrder.Any(e => e.PurchaseOrderID == id);
        }
    }
}