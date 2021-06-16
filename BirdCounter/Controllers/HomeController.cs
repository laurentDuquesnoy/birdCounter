using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BirdCounter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BirdCounter.Models;
using BirdCounter.Views.Home;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace BirdCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataBase DataBase { get; set; }
        private List<Bird> _birds { get; set; }
        private List<Session> _sessions { get; set; }
        
        private DetailObject _currentCount { get; set; }

        public HomeController(ILogger<HomeController> logger, DataBase database)
        {
            _logger = logger;
            DataBase = database;
            
            _birds = new List<Bird>();
            _birds.AddRange(DataBase.getBirds());
            _sessions = new List<Session>();
            _sessions.AddRange(DataBase.getSessions());
        }

        public IActionResult Index()
        {
            return View(this);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Count(int SessionId = 0)
        {
            if (SessionId != 0)
            {
                _currentCount = DataBase.CreateDetailObject(SessionId);
            }
            else
            {
                _currentCount = DataBase.CreateDetailObject(DataBase.CreateSession());
            }
            
            return View(this);
        }

        public IActionResult Detail(int id)
        {
            DetailObject detail = DataBase.CreateDetailObject(id);
            return View(detail);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult AddCount(int sessionId, int BirdId)
        {
            DataBase.AddCount(BirdId, sessionId);
            return RedirectToAction("Count" ,new{SessionId = sessionId});
        }

        public IActionResult SubtractCount(int sessionId, int birdId)
        {
            DataBase.SubtractCount(birdId, sessionId);
            return RedirectToAction("Count", new{SessionId = sessionId});
        }

        public IActionResult EndSession(int id)
        {
            DataBase.EndSession(id);
            return RedirectToAction("Index");
        }
        public List<Bird> Birds
        {
            get
            {
                return _birds;
            }
            set
            {
                _birds = value;
            }
        }

        public List<Session> Sessions => _sessions;

        public DetailObject CurrentCount
        {
            get
            {
                return _currentCount;
            }
            set => _currentCount = value;
        }
    }
}