using BirdCounter.Data;
using Microsoft.AspNetCore.Mvc;

namespace BirdCounter.Controllers
{
    public class BirdController : Controller
    {
        private DataBase DataBase { get; set; }
        
        public BirdController(DataBase database)
        {
            DataBase = database;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}