using Microsoft.EntityFrameworkCore;
using RentRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentRoom.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        [HttpGet]
        public ActionResult Index() // главная страницы админ панели
        {
            int i = 0; // переменная для получения id пользователя
            int check_id = 0; // переменная для проверки пользователя является ли он администратором
            using (RentRoomContext db = new RentRoomContext()) // создаём блок с подключением к контексту моделей
            {
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id; // делаем выборку пользователя и получаем ID пользователя в данный момент
                foreach (var t in id)
                {
                    i = t; //получаем в переменную id пользователя
                }
                var check_adm = db.Users.Where(u => u.Id == i).Select(u => u.Admin); // получаем информацию является ли пользователь администратором
                foreach (var r in check_adm)
                {
                    check_id = r; // заносим в переменную информацию об адмике пользователя
                }
            }
            if (check_id == 0) // если пользователь не админ - выкидываем его на 404NOTFOUND
            {
                return HttpNotFound();
            }
            using (RentRoomContext db = new RentRoomContext())
            {
                ViewBag.ModerTable = db.Builds.Include(u => u.User).Where(b => b.Status == 1).ToList(); // получаем все заказы требующие модерации
            }
            return View(ViewBag.ModerTable); // передаем в представление модель данных заказов для модерации
        }

        [Authorize]
        public ActionResult DeleteOrder(int Id) // метод для удаления заказа
        {
            int i = 0; // переменная для получения id пользователя
            Build build = null; // создаём переменную модели заказа для удаления
            using (RentRoomContext db = new RentRoomContext())
            {
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id; // получаем ID пользователя
                build = db.Builds.Include(u => u.User).FirstOrDefault(b => b.Id == Id); // получаем заказ по указанному ID
                foreach (var t in id)
                {
                    i = t; // заносим ID в переменную
                }
                var check_adm = db.Users.Where(u => u.Id == i).ToList(); // получаем пользователя с ID
                foreach (var r in check_adm)
                {
                    if (r.Admin == 1 || r.Login == build.User.Login) // проверяем является ли этот пользователь администратором или владельцем аккаунта отправителя заказа
                    {
                        db.Builds.Remove(build); // удаляем заказ
                        db.SaveChanges(); // сохраняем изменения в БД
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            return RedirectToAction("UserProfile", "Account" , new { id = i }); // редирект на страницу пользователя
        }

        public ActionResult SuccessOrder(int Id) // одобрение заказа
        {
            int i = 0;// переменная для получения id пользователя
            int check_id = 0; // переменная для проверки ID администратора
            Build build = null;// создаём переменную модели заказа для одобрение
            using (RentRoomContext db = new RentRoomContext())
            {
                var id = from c in db.Users where c.Login == User.Identity.Name select c.Id; // получаем ID пользователя
                foreach (var t in id)
                {
                    i = t; // заносим ID в переменную
                }
                var check_adm = db.Users.Where(u => u.Id == i).Select(u => u.Admin); // делаем проверку на администратора
                foreach (var r in check_adm)
                {
                    check_id = r; // заносим результат проверки в переменную
                }
            }
            if (check_id == 0)
            {
                return HttpNotFound();
            }
            using (RentRoomContext db = new RentRoomContext())
            {
                build = db.Builds.FirstOrDefault(b => b.Id == Id); // получаем заказ на редактирование его статуса
                if(build != null)
                    build.Status = 0; // изменяем статус заказа на 0(отправляется в общий список)
                db.SaveChanges(); // сохраняем данные в БД
            }
            return RedirectToAction("Index");
        }
    }
}