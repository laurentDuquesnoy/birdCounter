using System;
using System.Collections.Generic;
using System.Linq;
using BirdCounter.Data;
using BirdCounter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BirdCounter.Controllers
{
    public class BirdController : Controller
    {
        private DataBase DataBase { get; set; }

        private List<Bird> _birds { get; set; }
        
        public BirdController(DataBase database)
        {
            DataBase = database;
            _birds = DataBase.Birds.ToList();
        }

        public IActionResult Index()
        {
            return View(this);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Bird bird)
        {
            if (!ModelState.IsValid)
            {
                return View(bird);
            }

            DataBase.Birds.Add(bird);
            DataBase.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(int birdId)
        {
            Bird bird = DataBase.Birds.FirstOrDefault(a => a.Id == birdId);
            if (bird != null)
            {
                DataBase.Birds.Remove(bird);
                DataBase.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int birdId)
        {
            var bird = DataBase.GetBirdById(birdId);
            return View(bird);
        }

        [HttpPost]
        public IActionResult Edit(Bird bird)
        {
            var dbBird = DataBase.Birds.FirstOrDefault(a => a.Id == bird.Id);
            if (!ModelState.IsValid)
            {
                return View(bird);
            }

            dbBird.Name = bird.Name;
            dbBird.Description = bird.Description;
            dbBird.Image = bird.Image;
            DataBase.SaveChanges();

            return RedirectToAction("Index");
        }

        public List<Bird> Birds => _birds;
    }
}