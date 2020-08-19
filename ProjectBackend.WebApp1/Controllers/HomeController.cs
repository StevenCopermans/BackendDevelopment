using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBackend.Models;
using ProjectBackend.Models.Repositories;

namespace ProjectBackend.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IQuizRepo quizRepo;

        public HomeController(ILogger<HomeController> logger, IQuizRepo quizRepo)
        {
            _logger = logger;
            this.quizRepo = quizRepo;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Quiz> quizes = await quizRepo.GetAllQuizesAsync();

            if (quizes is null)
            {
                return NotFound();
            }

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
