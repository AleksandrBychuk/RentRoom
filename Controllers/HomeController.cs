using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RentRoom.Models;

namespace RentRoom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string type, int? minprice, int? maxprice, string country, string city, int? rooms, int? roomstorent, int? floors, int? floorrent, int? totalarea, int? livingarea, int? kitchenarea, int? yearbuilt,
            string bathroom, string elevator, string furniture, string householdappliances, string internet, string garbagechute, string repairs) // главная страница
        {
            RentRoomContext db = new RentRoomContext(); // подключаемся к контексту
            IQueryable<Build> builds = db.Builds.Include(p => p.User).Where(b => b.Status == 0); // получаем список всех заказов прошедших модерацию
            if (!String.IsNullOrEmpty(type) && !type.Equals("Everything")) // условие при котором фильтр не применяется
                builds = builds.Where(p => p.Type == type && p.Status == 0); // присваиваем фильтр
            // Снизу всё по аналогии
            if (minprice != null && minprice != 0)
                builds = builds.Where(p => p.Price >= minprice && p.Status == 0);

            if (maxprice != null && maxprice != 0)
                builds = builds.Where(p => p.Price <= maxprice && p.Status == 0);

            if (rooms != null && rooms != 0)
                builds = builds.Where(p => p.Rooms == rooms && p.Status == 0);

            if (roomstorent != null && roomstorent != 0)
                builds = builds.Where(p => p.RoomsToRent == roomstorent && p.Status == 0);

            if (floors != null && floors != 0)
                builds = builds.Where(p => p.Floors == floors && p.Status == 0);

            if (floorrent != null && floorrent != 0)
                builds = builds.Where(p => p.FloorRent == floorrent && p.Status == 0);

            if (totalarea != null && totalarea != 0)
                builds = builds.Where(p => p.TotalArea == totalarea && p.Status == 0);

            if (livingarea != null && livingarea != 0)
                builds = builds.Where(p => p.LivingArea == livingarea && p.Status == 0);

            if (kitchenarea != null && kitchenarea != 0)
                builds = builds.Where(p => p.KitchenArea == kitchenarea && p.Status == 0);

            if (yearbuilt != null && yearbuilt != 0)
                builds = builds.Where(p => p.YearBuilt == yearbuilt && p.Status == 0);

            if (!String.IsNullOrEmpty(bathroom) && !bathroom.Equals("Not important"))
                builds = builds.Where(p => p.Bathroom == bathroom && p.Status == 0);

            if (!String.IsNullOrEmpty(elevator) && !elevator.Equals("Not important"))
                builds = builds.Where(p => p.Elevator == elevator && p.Status == 0);

            if (!String.IsNullOrEmpty(furniture) && !furniture.Equals("Not important"))
                builds = builds.Where(p => p.Furniture == furniture && p.Status == 0);

            if (!String.IsNullOrEmpty(householdappliances) && !householdappliances.Equals("Not important"))
                builds = builds.Where(p => p.HouseholdAppliances == householdappliances && p.Status == 0);

            if (!String.IsNullOrEmpty(internet) && !internet.Equals("Not important"))
                builds = builds.Where(p => p.Internet == internet && p.Status == 0);

            if (!String.IsNullOrEmpty(garbagechute) && !garbagechute.Equals("Not important"))
                builds = builds.Where(p => p.GarbageChute == garbagechute && p.Status == 0);

            if (!String.IsNullOrEmpty(repairs) && !repairs.Equals("Not important"))
                builds = builds.Where(p => p.Repairs == repairs && p.Status == 0);

            if (!String.IsNullOrEmpty(country) && country != "")
                builds = builds.Where(p => p.Country == country && p.Status == 0);

            if (!String.IsNullOrEmpty(city) && city != "")
                builds = builds.Where(p => p.City == city && p.Status == 0);

            List<Build> builds1 = db.Builds.Where(b => b.Status == 0).ToList(); // получаем список как начальное значение фильтра
            builds1.Insert(0, new Build { Type = "Everything", Id = 0}); // устанавливаем параметры начального списка

            FilterViewModel plvm = new FilterViewModel // создаём модель фильтра
            {
                Builds = builds.ToList(),
                MinPrice = null,
                MaxPrice = null,
                Rooms = null,
                RoomsToRent = null,
                FloorRent = null,
                Floors = null,
                TotalArea = null,
                LivingArea = null,
                KitchenArea = null,
                YearBuilt = null,
                Country = "",
                City = "",
                Types = new SelectList(new List<string>() // список фильтрафии
                {
                    "Everything",
                    "House",
                    "Apartment",
                    "Room",
                }),
                GarbageChute = new SelectList(new List<string>()
                {
                    "Not important",
                    "Yes",
                    "No",
                }),
                Elevator = new SelectList(new List<string>()
                {
                    "Not important",
                    "Yes",
                    "No",
                }),
                Furniture = new SelectList(new List<string>()
                {
                    "Not important",
                    "Yes",
                    "No",
                }),
                HouseholdAppliances = new SelectList(new List<string>()
                {
                    "Not important",
                    "Yes",
                    "No",
                }),
                Internet = new SelectList(new List<string>()
                {
                    "Not important",
                    "Yes",
                    "No",
                }),
                Repairs = new SelectList(new List<string>()
                {
                    "Not important",
                    "Good repair",
                    "Satisfactory repair",
                    "Bad repair",
                }),
                Bathroom = new SelectList(new List<string>()
                {
                    "Not important",
                    "Combined",
                    "Separate", 
                    "No", 
                    "too and more",
                }),
            };
            return View(plvm);
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddOrder() // добавление заказа
        {
            return PartialView(); // передаёт частичное представление для создания модального окна
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOrder(Build model, HttpPostedFileBase uploadImage, HttpPostedFileBase uploadImage2, HttpPostedFileBase uploadImage3)
        {
            if(ModelState.IsValid) // проверка на валидность(все поля ли заполнены по правилам) формы
            {
                byte[] imageData = null; // переменные для хранения байт изображений
                byte[] imageData2 = null;
                byte[] imageData3 = null;
                string path = Server.MapPath("~/Images/None_photo.jpg"); // путь к статической картинке если не указаны фото
                if (uploadImage == null) // если изображение не загрузили
                {
                    Image img = Image.FromFile(path); // получаем фото
                    MemoryStream ms = new MemoryStream(); // создаем экземпляр класса
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif); // преобразуем изображение в GIF
                    imageData = ms.ToArray(); // присваиваем переменной изображение конвертируемой в массив байтов
                }
                else
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength); // полученный файл преобразуем в массив байтов и запихиваем в переменную
                    }
                }
                //ниже всё по аналогии
                if (uploadImage2 == null)
                {
                    Image img = Image.FromFile(path);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    imageData2 = ms.ToArray();
                }
                else
                {
                    using (var binaryReader = new BinaryReader(uploadImage2.InputStream))
                    {
                        imageData2 = binaryReader.ReadBytes(uploadImage2.ContentLength);
                    }
                }
                if (uploadImage3 == null)
                {
                    Image img = Image.FromFile(path);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    imageData3 = ms.ToArray();
                }
                else
                {
                    using (var binaryReader = new BinaryReader(uploadImage3.InputStream))
                    {
                        imageData3 = binaryReader.ReadBytes(uploadImage3.ContentLength);
                    }
                }
                using (RentRoomContext db = new RentRoomContext())
                {
                    User user = db.Users.Where(u => u.Login == User.Identity.Name).FirstOrDefault(); // получаем пользователя в данный момент для присваивания модели как отправителя
                    db.Builds.Add(
                        new Build 
                        { 
                            Type = model.Type,
                            Rooms = model.Rooms,
                            RoomsToRent = model.RoomsToRent, 
                            FloorRent = model.FloorRent,
                            Floors = model.Floors,
                            TotalArea = model.TotalArea,
                            LivingArea = model.LivingArea,
                            KitchenArea = model.KitchenArea,
                            YearBuilt = model.YearBuilt,
                            GarbageChute = model.GarbageChute,
                            Elevator = model.Elevator,
                            Furniture = model.Furniture,
                            HouseholdAppliances = model.HouseholdAppliances,
                            Internet = model.Internet,
                            Repairs = model.Repairs,
                            Photo = imageData,
                            Photo2 = imageData2,
                            Photo3 = imageData3,
                            Bathroom = model.Bathroom,
                            Country = model.Country,
                            City = model.City,
                            Address = model.Address, 
                            Price = model.Price,
                            DateAdd = DateTime.Now,
                            Status = 1,
                            Description = model.Description,
                            IsNumberShow = model.IsNumberShow,
                            User = user
                            }
                    ); // присваиваем модели данные переданные из представления
                    db.SaveChanges(); // сохраняем изменения
                }
            }
            return RedirectToAction("RedirectToProfile", "Account", null);
        }
    }
}