using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.EntityFrameworkCore;
using RentRoom.Models;
using System.Security.Cryptography;
using System.Text;

namespace RentRoom.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        private string GetHash(string input) // метод для шифрования пароля
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (RentRoomContext db = new RentRoomContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == GetHash(model.Password));

                }
                // если пользователь найден, проводим аунтефикацию, запоминаем куки и редиректим на главную страницу
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                // ищем пользователя по полученным данным
                using (RentRoomContext db = new RentRoomContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (RentRoomContext db = new RentRoomContext())
                    {
                        db.Users.Add(new User { Login = model.Login, Email = model.Email, Password = GetHash(model.Password), RegisterDate = DateTime.Now });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Login == model.Login && u.Password == GetHash(model.Password)).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff() // метод деаунтефикации пользователя
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RedirectToProfile() // метод редиректа на свой профиль, для _Layout
        {
            int i = 0;
            using (RentRoomContext db = new RentRoomContext())
            {
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id;
                foreach (var t in id)
                {
                    i = t;
                }
            }
            return RedirectToAction("UserProfile/" + i);
        }

        [HttpGet]
        public ActionResult UserProfile(int id)
        {
            IEnumerable<User> user_info = null;
            using (RentRoomContext db = new RentRoomContext())
            {
                user_info = db.Users.Include(b => b.Builds).Where(u => u.Id == id).ToList(); // получаем данные пользователя
                ViewBag.DataOrders = db.Builds.Include(u => u.User).Where(u => u.User.Id == id && u.Status == 0).ToList(); // получаем объявления пользователя
            }
            return View(user_info); // передаем данные в представление
        }

        [HttpGet]
        public ActionResult UserOrder(int Id)
        {
            int i = 0;
            IEnumerable<Build> build = null;
            using (RentRoomContext db = new RentRoomContext())
            {
                build = db.Builds.Include(u => u.User).Where(b => b.Id == Id).ToList(); // получаем конкретный заказ по его ID
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id;
                foreach (var t in id)
                {
                    i = t;
                }
                ViewBag.IsUserAdmin = db.Users.Where(u => u.Id == i).ToList(); // проверка на админа
            }
            return View(build);
        }

        [HttpGet]
        public ActionResult Settings()
        {
            int i = 0;
            using (RentRoomContext db = new RentRoomContext())
            {
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id;
                foreach (var t in id)
                {
                    i = t;
                }
                ViewBag.UserSett = db.Users.Where(u => u.Id == i).ToList(); // получаем настройки пользователя для частичного представления
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Settings(SettingsVM model)
        {
            User user = null;
            int i = 0;
            using (RentRoomContext db = new RentRoomContext())
            {
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id;
                foreach (var t in id)
                {
                    i = t;
                }
                user = db.Users.FirstOrDefault(u => u.Id == i);//получаем данные пользователя и если он не null, меняем настройки на новые и после сохраняем изменения в БД
                if (user != null)
                {
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                }
                db.SaveChanges();
            }
            return RedirectToAction("UserProfile/" + i);
        }
    }
}