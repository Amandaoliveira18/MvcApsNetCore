using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) //construtor com a dependencia passada como argumento
        {
            _sellerService = sellerService; //injeção de dependencia
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list); //ao passar como argumento o list, é gerado um action Result

        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); //quando a tela for acionada, vai receber as listas de departments.
        }
        
        [HttpPost] //Para indicar que é uma requisição post, e não GET.
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) //Para fazer a requisição só passar como parametro o objeto
        {
            _sellerService.Insert(seller);//Fiz a inserção
            return RedirectToAction(nameof(Index)); //Para redirecionar a minha requisição para o inddex 
        }
    }
}
