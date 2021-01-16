using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportingGoods.Models;
using SportingGoods.Models.Repositories;

namespace SportingGoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IClientRepository _ClientRepository;
        readonly IItemRepository _ItemRepository;
        readonly IBillRepository _BillRepository;
        readonly ICategoryRepository _CategoryRepository;

        public HomeController(ILogger<HomeController> logger, IClientRepository _ClientRepository, IItemRepository _ItemRepository, IBillRepository _BillRepository, ICategoryRepository _CategoryRepository)
        {
            _logger = logger;
            this._ClientRepository = _ClientRepository;
            this._ItemRepository = _ItemRepository;
            this._BillRepository = _BillRepository;
            this._CategoryRepository = _CategoryRepository;
        }

        public IActionResult Index()
        {
            ViewBag.ClientsCount = _ClientRepository.GetAll().Count();
            ViewBag.ItemsCount = _ItemRepository.GetAll().Count();
            ViewBag.CategoriesCount = _CategoryRepository.GetAll().Count();
            ViewBag.BillsCount = _BillRepository.GetAll().Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
