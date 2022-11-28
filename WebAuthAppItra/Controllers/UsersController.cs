using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAuthAppItra.Data;
using WebAuthAppItra.Models;

namespace WebAuthAppItra.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebAuthAppItraContext _context;

        private static string _userId = "empty";

        public UsersController(WebAuthAppItraContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CurrentUser"] = _userId;
            return View(await _context.User.ToListAsync());
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var existedUser = await _context.User.FirstOrDefaultAsync(
                u => u.Email == user.Email && u.Password == user.Password);
            if (existedUser != null && !existedUser.IsBlocked)
            {
                existedUser.LastLoginTime = DateTime.Now;
                _context.User.Update(existedUser);
                _context.SaveChanges();
                _userId = existedUser.Id.ToString();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Message"] = "User not found or blocked";
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,RegistrationDate,LastLoginTime,IsBlocked")] User user)
        {
            
            if (_context.User.FirstOrDefault(u => u.Email == user.Email) != null)
            {
                ViewData["Message"] = "Already registered";
                return View();
            }
            if (ModelState.IsValid)
            {
                user.RegistrationDate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Block(bool[] IsChecked)
        {
            var i = 0;
            bool IsCurrentUserBlocked = false;
            foreach (var item in _context.User)
            {
                if (IsChecked[i])
                {
                    item.IsBlocked = true;
                    if (item.Id == Int32.Parse(_userId))
                    {
                        IsCurrentUserBlocked = true;
                    }
                }
                i++;
            }
            _context.SaveChanges();
            return IsCurrentUserBlocked ? RedirectToAction(nameof(Login)) : RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Unblock(bool[] IsChecked)
        {
            var i = 0;
            foreach (var item in _context.User)
            {
                var x = IsChecked.Length;
                if (IsChecked[i] && item.IsBlocked)
                {
                    item.IsBlocked = false;
                }
                i++;

            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(bool[] IsChecked)
        {
            var i = 0;
            bool IsCurrentUserBlocked = false;
            foreach (var item in _context.User)
            {
                if (IsChecked[i])
                {
                    _context.User.Remove(item);
                    if (item.Id == Int32.Parse(_userId))
                    {
                        IsCurrentUserBlocked = true;
                    }
                }
                i++;
            }
            _context.SaveChanges();
            return IsCurrentUserBlocked ? RedirectToAction(nameof(Login)) : RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return _context.User.Any(e => e.Id == id);
        }
    }
}
