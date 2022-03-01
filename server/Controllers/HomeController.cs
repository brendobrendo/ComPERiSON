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

namespace Features.Controllers
{
    public class HomeController : Controller
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
        public HomeController(FeaturesContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/register")]
        public IActionResult Register(User newUser)
        {
            if (db.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "is taken");
            }

            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);


            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("FullName", newUser.Fullname());

            return RedirectToAction("Success");
        }

        [HttpPost("/login")]
        public IActionResult LogIn(LoginUser loginUser)
        {

            // First confirm that the input email is already in the db
            User dbUser = db.Users
                .FirstOrDefault(u => u.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", "This email is not registered");
            }

            // If email not recognized, Show Login and reg page with flag for no registered email
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            // Confirm that the input password matches the hashed pw in the db
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser,
            dbUser.Password, loginUser.LoginPassword);

            // If hashed pw doesn't match the email, show l&r page with error
            if (pwCompareResult == 0)
            {
                ModelState.AddModelError("LoginEmail", "Incorrect password");
                return View("Index");
            }

            // If hashed pw does match, set session variables- UserId and FullName
            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            HttpContext.Session.SetString("FullName", dbUser.Fullname());

            // Redirect user to dashboard. They have succesfully logged in.
            return RedirectToAction("Success");
        }

        [HttpGet("/success")]
        public IActionResult Success()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Index");
            }

            int? currentUserId = HttpContext.Session.GetInt32("count");
            User currentUser = db.Users
            .FirstOrDefault(users => users.UserId == HttpContext.Session.GetInt32("UserId"));

            ViewBag.Address = currentUser.Address;

            return View("Success", currentUser);
        }

        [HttpPost("/logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
