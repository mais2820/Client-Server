using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class GridController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        
    }
}