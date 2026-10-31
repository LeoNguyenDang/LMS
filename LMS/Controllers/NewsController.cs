using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}