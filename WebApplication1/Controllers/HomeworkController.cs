using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeworkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HW2()
        {
            return View();
        }
    }
}
