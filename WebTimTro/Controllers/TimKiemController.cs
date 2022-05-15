using Microsoft.AspNetCore.Mvc;

namespace WebTimTro.Controllers
{
    public class TimKiemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
