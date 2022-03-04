using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Features.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Features.Controllers
{
    public class ComparisonsController : Controller
    {
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        private bool loggedIn
        {
            get
            {
                return uid != null;
            }
        }

        private FeaturesContext db;
        public ComparisonsController(FeaturesContext context)
        {
            db = context;
        }

        [HttpGet("/comparisons/compare")]
        public IActionResult Compare()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            List<User> candidates = db.Users.Take(4).ToList();
            return View("Compare", candidates);
        }

        [HttpGet("/comparisons/compare2")]
        public IActionResult Compare2()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            List<User> candidates = db.Users.Take(4).ToList();
            
            // generate random order of the input candidate list
            var random = new Random();
            
            
            // Shuffle the list of candidates
            for (int i = 0; i<candidates.Count; i++)
            {
                int randomIndex = random.Next(candidates.Count);
                User temp = candidates[i];
                candidates[i] = candidates[randomIndex];
                candidates[randomIndex] = temp;
            }
            
            return View("Compare2", candidates);
        }

        [HttpPost("/comparisons/compare3")]
        public IActionResult Compare3()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
