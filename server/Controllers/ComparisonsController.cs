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
            
            // Create variables for each candidates UserId and Tally in session so that we can keep track of 
            // scores during the comparison process.

            HttpContext.Session.SetInt32("Candidate1UserId", candidateUsers[0].UserId);
            HttpContext.Session.SetInt32("Candidate2UserId", candidateUsers[1].UserId);
            HttpContext.Session.SetInt32("Candidate3UserId", candidateUsers[2].UserId);
            HttpContext.Session.SetInt32("Candidate4UserId", candidateUsers[3].UserId);
            HttpContext.Session.SetInt32("Candidate1Score", 0);
            HttpContext.Session.SetInt32("Candidate2Score", 0);
            HttpContext.Session.SetInt32("Candidate3Score", 0);
            HttpContext.Session.SetInt32("Candidate4Score", 0);
            
            

            return View("Compare", candidateUsers);
        }

        [HttpPost("/comparisons/compare2")]
        public IActionResult Compare2(int boxNumberChosen)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            // Update tally for user associated with the last box chosen
            if (boxNumberChosen == 0)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate1Score");
                HttpContext.Session.SetInt32("Candidate1Score", ((int)IntVariable) +1);
            }
            else if (boxNumberChosen == 1)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate2Score");
                HttpContext.Session.SetInt32("Candidate2Score", ((int)IntVariable) +1);
            }
            else if (boxNumberChosen == 2)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate3Score");
                HttpContext.Session.SetInt32("Candidate3Score", ((int)IntVariable) +1);
            }
            else
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate3Score");
                HttpContext.Session.SetInt32("Candidate4Score", ((int)IntVariable) +1);
            }

            // Create new empty list to hold all the candidates being compared 
            // Repull candidates from db using userId numbers stored in session
            // Add them into the candidate list
            List<User> candidates = new List<User>();
            User candidate1 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate1UserId"));
            candidates.Add(candidate1);
            User candidate2 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate2UserId"));
            candidates.Add(candidate2);
            User candidate3 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate3UserId"));
            candidates.Add(candidate3);
            User candidate4 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate4UserId"));
            candidates.Add(candidate4);

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

            return View("Compare2", candidates);
        }

        [HttpPost("/comparisons/compare3")]
        public IActionResult Compare3(int boxNumberChosen)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            // Update tally for user associated with the last box chosen
            if (boxNumberChosen == 0)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate1Score");
                HttpContext.Session.SetInt32("Candidate1Score", ((int)IntVariable) +1);
            }
            else if (boxNumberChosen == 1)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate2Score");
                HttpContext.Session.SetInt32("Candidate2Score", ((int)IntVariable) +1);
            }
            else if (boxNumberChosen == 2)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate3Score");
                HttpContext.Session.SetInt32("Candidate3Score", ((int)IntVariable) +1);
            }
            else
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate3Score");
                HttpContext.Session.SetInt32("Candidate3Score", ((int)IntVariable) +1);
            }

            // Create new empty list to hold all the candidates being compared 
            // Repull candidates from db using userId numbers stored in session
            // Add them into the candidate list
            List<User> candidates = new List<User>();
            User candidate1 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate1UserId"));
            candidates.Add(candidate1);
            User candidate2 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate2UserId"));
            candidates.Add(candidate2);
            User candidate3 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate3UserId"));
            candidates.Add(candidate3);
            User candidate4 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate4UserId"));
            candidates.Add(candidate4);

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

            return View("Compare3", candidates);
        }

        [HttpPost("/comparisons/compare4")]
        public IActionResult Compare4(int boxNumberChosen)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            // Update tally for user associated with the last box chosen
            if (boxNumberChosen == 0)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate1Score");
                HttpContext.Session.SetInt32("Candidate1Score", ((int)IntVariable) +1);
            }
            else if (boxNumberChosen == 1)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate2Score");
                HttpContext.Session.SetInt32("Candidate2Score", ((int)IntVariable) +1);
            }
            else if (boxNumberChosen == 2)
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate3Score");
                HttpContext.Session.SetInt32("Candidate3Score", ((int)IntVariable) +1);
            }
            else
            {
                int? IntVariable = HttpContext.Session.GetInt32("Candidate3Score");
                HttpContext.Session.SetInt32("Candidate3Score", ((int)IntVariable) +1);
            }

            // Create new empty list to hold all the candidates being compared 
            // Repull candidates from db using userId numbers stored in session
            // Add them into the candidate list
            List<User> candidates = new List<User>();
            User candidate1 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate1UserId"));
            candidates.Add(candidate1);
            User candidate2 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate2UserId"));
            candidates.Add(candidate2);
            User candidate3 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate3UserId"));
            candidates.Add(candidate3);
            User candidate4 = db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("Candidate4UserId"));
            candidates.Add(candidate4);

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

            return View("Compare4", candidates);
        }

        [HttpPost("/comparisons/winner")]
        public IActionResult Winner(int boxNumberChosen)
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            // Find the index of the candidate with the highest points
            List<int> totalScores = new List<int>() 
            {
                (int)HttpContext.Session.GetInt32("Candidate1Score"),
                (int)HttpContext.Session.GetInt32("Candidate2Score"),
                (int)HttpContext.Session.GetInt32("Candidate3Score"),
                (int)HttpContext.Session.GetInt32("Candidate4Score")
            };

            int maxIndex = totalScores.IndexOf(totalScores.Max());

            Console.WriteLine(maxIndex);

            
            // Pull the candidate userId associated with that index pulled above
            List<int> candidateIds = new List<int>() 
            {
                (int)HttpContext.Session.GetInt32("Candidate1UserId"),
                (int)HttpContext.Session.GetInt32("Candidate2UserId"),
                (int)HttpContext.Session.GetInt32("Candidate3UserId"),
                (int)HttpContext.Session.GetInt32("Candidate4UserId")
            };

            int winningCandidateId = candidateIds[maxIndex];

            // Pull winning candidate user infomration from db using the userId

            User winningCandidate = db.Users.FirstOrDefault(u => u.UserId == winningCandidateId);

            // var random = new Random();

            // // Shuffle the list of candidates
            // for (int i = 0; i < candidates.Count; i++)
            // {
            //     int randomIndex = random.Next(candidates.Count);
            //     User temp = candidates[i];
            //     candidates[i] = candidates[randomIndex];
            //     candidates[randomIndex] = temp;
            // }

            return View("Winner", winningCandidate);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
