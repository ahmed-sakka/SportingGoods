using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using SportingGoods.Models;
using SportingGoods.Models.Repositories;
using SportingGoods.ViewModels;

namespace SportingGoods.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        //private readonly CategoryRepository _CategoryRepository;
        public ItemsController(AppDbContext context, IWebHostEnvironment hostingEnvironment/*, CategoryRepository categoryRepository*/)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            //_CategoryRepository = categoryRepository;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            //ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "ID", "Name");
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            //ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "ID", "Name");
            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            //ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Category,Description,Picture,Price,Brand")] Item item, CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Picture != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/items");

                    // To make sure the file name is unique we are appending a new
                    // GUID value and an underscore to the file name

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder

                    model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Item newItem = new Item
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Brand = model.Brand,
                    Category = model.Category,
                    Picture = uniqueFileName
                };

                _context.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "ID", "Name");
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Category,Description,Picture,Price,Brand")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }
    }
}
