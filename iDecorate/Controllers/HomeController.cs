using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using iDecorate.Domain.Client.Contract;

namespace LearnWords.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
