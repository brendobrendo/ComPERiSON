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

        [HttpPost("/comparisons/compare")]
        public IActionResult Compare()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            // Get list of 4 random Users. These will be the candidates
            // for the comparison
            List<User> candidateUsers = db.Users.Take(4).ToList();
            List<Candidate> candidates = new List<Candidate>();
            
            // Convert the Users to Candidates so we can track the score through
            // the comparison process
            foreach (User user in candidateUsers) 
            {
                Candidate candidate = new Candidate { UserId = user.UserId, Score = 0};
                candidates.Add(candidate);
            }

            // Creating comparisonsession object to pass through the Comparison pages
            // That will track candidate tallies and the bo selected by the user
            ComparisonSession comparisonStage0 = new ComparisonSession { Candidates = candidates, WinningBoxNumber = 0};

            return View("Compare", comparisonStage0);
        }

        [HttpPost("/comparisons/compare2")]
        public IActionResult Compare2(ComparisonSession lastComparison)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }
            
            int myNumber = 9;

            // // generate random order of the input candidate list
            // var random = new Random();

            // // Shuffle the list of candidates
            // for (int i = 0; i < candidates.Count; i++)
            // {
            //     int randomIndex = random.Next(candidates.Count);
            //     User temp = candidates[i];
            //     candidates[i] = candidates[randomIndex];
            //     candidates[randomIndex] = temp;
            // }

            return View("Compare2");
        }

        [HttpPost("/comparisons/compare3")]
        public IActionResult Compare3(List<User> candidates, string winningCandidateBox)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            // generate random order of the input candidate list
            var random = new Random();

            // Shuffle the list of candidates
            for (int i = 0; i < candidates.Count; i++)
            {
                int randomIndex = random.Next(candidates.Count);
                User temp = candidates[i];
                candidates[i] = candidates[randomIndex];
                candidates[randomIndex] = temp;
            }

            return View("Compare3", candidates);
        }

        [HttpGet("/comparisons/winning-candidate")]
        public IActionResult Winner(List<User> candidates, int winningCandidateBox)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            var random = new Random();

            // Shuffle the list of candidates
            for (int i = 0; i < candidates.Count; i++)
            {
                int randomIndex = random.Next(candidates.Count);
                User temp = candidates[i];
                candidates[i] = candidates[randomIndex];
                candidates[randomIndex] = temp;
            }

            return View("Winner", candidates);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
