using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using iDecorate.Domain.Client.Contract;

namespace LearnWords.Controllers
{
    public class HomeController : Controller
    {
        public readonly IBusinessTopic _business;
        public HomeController(IBusinessTopic business)
        {
            _business = business;
        }

        public IActionResult Index()
        {
            var bla = _business.GetAll();

            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
