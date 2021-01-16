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
        readonly AppDbContext _context;
        readonly IWebHostEnvironment hostingEnvironment;
        readonly ICategoryRepository _CategoryRepository;
        public ItemsController(AppDbContext context, IWebHostEnvironment hostingEnvironment, ICategoryRepository categoryRepository)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            _CategoryRepository = categoryRepository;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name");
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

            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name");
            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Category,Description,Picture,Price,Brand,CategoryID")] Item item, CreateItemViewModel model)
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
                    Picture = uniqueFileName,
                    CategoryID = model.CategoryID
                };

                _context.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name",item.Category);
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
            EditItemViewModel editItemViewModel = new EditItemViewModel
            {
                ID = item.ID,
                Name = item.Name,
                Brand = item.Brand,
                ExistingPicture = item.Picture,
                Price = item.Price,
                Description = item.Description,
                Category = item.Category,
                CategoryID = item.CategoryID

            };
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name");

            return View(editItemViewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditItemViewModel model)
        {
            /*
            if (id != model.ID)
            {
                return NotFound();
            }
            */
        

            if (ModelState.IsValid)
            {
                Item item = _context.Items.Find(model.ID);
                item.Name = model.Name;
                item.Description = model.Description;
                item.Price = model.Price;
                item.Brand = model.Brand;
                item.Category = model.Category;
                item.CategoryID = model.CategoryID;

               
                if (model.Picture != null)
                {
                    
                    if (model.ExistingPicture != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images/items", model.ExistingPicture);
                        System.IO.File.Delete(filePath);
                    }
                    
                    item.Picture = ProcessUploadedFile(model);
                }


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
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name", model.Category);

            return View(model);
        }

        [NonAction]
        private string ProcessUploadedFile(EditItemViewModel model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/items");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
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
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name");
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            ViewBag.CategoryID = new SelectList(_CategoryRepository.GetAll(), "CategoryID", "Name", item.Category);
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
