using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportingGoods.Models;
using SportingGoods.Models.Repositories;

namespace SportingGoods.Controllers
{
    public class BillsController : Controller
    {
        private readonly AppDbContext _context;
        readonly IClientRepository _ClientRepository;


        public BillsController(AppDbContext context, IClientRepository clientRepository)
        {
            _context = context;
            _ClientRepository = clientRepository;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName");
            return View(await _context.Bills.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bill == null)
            {
                return NotFound();
            }

            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName");
            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,Client,ClientID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName", bill.Client);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName");
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Client,ClientID")] Bill bill)
        {
            if (id != bill.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.ID))
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
            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName", bill.Client);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bill == null)
            {
                return NotFound();
            }

            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName");
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            ViewBag.ClientID = new SelectList(_ClientRepository.GetAll(), "ClientID", "FirstName", bill.Client);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.ID == id);
        }
    }
}
