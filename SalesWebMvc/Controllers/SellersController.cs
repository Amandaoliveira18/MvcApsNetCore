using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService) //construtor com a dependencia passada como argumento
        {
            _sellerService = sellerService; //injeção de dependencia
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list); //ao passar como argumento o list, é gerado um action Result

        }


    }
}
