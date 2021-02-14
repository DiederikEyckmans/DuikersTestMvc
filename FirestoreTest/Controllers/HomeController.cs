using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirestoreTest.Models;
using FirstoreTest.services;
using FirestoreTest.Domain;

namespace FirestoreTest.Controllers
{
    public class HomeController : Controller
    {
        private DuikerService duikerService;

        public HomeController()
        {
            duikerService = new DuikerService();
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Duiker> duikers = await duikerService.getDuikers();

            return View(duikers);
        }

   
    }
}
