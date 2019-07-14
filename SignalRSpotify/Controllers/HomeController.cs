using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRSpotify.Classes.Database;
using SignalRSpotify.Classes.Database.Entities;
using SignalRSpotify.Models;

namespace SignalRSpotify.Controllers
{
    public class HomeController : Controller
    {
        SandSContext _db;
        public HomeController(SandSContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            return RedirectToAction("Index");
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult CheckIfUserExists(LoginModel lm)
        {
            var userFromDatabase = _db.Users.FirstOrDefault(x => x.UserName == lm.UserName && x.Password == lm.Password);
            if (userFromDatabase != null)
            {
                Response.Cookies.Append("userId", userFromDatabase.Id, new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddHours(1) });
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("userId");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            Response.Cookies.Append("userId", user.Id, new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddHours(1) });
            return Redirect("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
