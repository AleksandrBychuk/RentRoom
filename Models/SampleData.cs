using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;


namespace RentRoom.Models
{
    public class SampleData
    {
        public static byte[] ImageConvert(string path)
        {
            Image img = Image.FromFile(HttpContext.Current.Server.MapPath(path));
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static void Initialize(RentRoomContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Login = "Admin",
                        Password = "5kt4/DvJG8vH3CMrqOxZ4A==",
                        Email = "adminmail@gmail.com",
                        PhoneNumber = "+1 132 178-13-66",
                        Builds = context.Builds.Where(b => b.User.Login == "Admin").ToList()
                    }
                );
                context.SaveChanges();
                User user = context.Users.FirstOrDefault(u => u.Login == "Admin");
                if (!context.Builds.Any())
                {
                    context.Builds.AddRange(
                        new Build
                        {
                            Type = "House",
                            Rooms = 3,
                            RoomsToRent = 1,
                            FloorRent = 2,
                            Floors = 2,
                            TotalArea = 150,
                            LivingArea = 140,
                            KitchenArea = 30,
                            YearBuilt = 2005,
                            GarbageChute = "Yes",
                            Elevator = "No",
                            Furniture = "Yes",
                            HouseholdAppliances = "Yes",
                            Internet = "Yes",
                            Repairs = "Good Repair",
                            Photo = ImageConvert("~/Images/image1.jpg"),
                            Photo2 = ImageConvert("~/Images/None_photo.jpg"),
                            Photo3 = ImageConvert("~/Images/None_photo.jpg"),
                            Bathroom = "Separate",
                            Country = "Belarus",
                            City = "Brest",
                            Address = "Sovetskaya, 73",
                            Price = 1500,
                            DateAdd = DateTime.Now,
                            Status = 0,
                            Description = "A room for rent in a large house.There are all conditions for life.",
                            IsNumberShow = true,
                            User = user
                        },
                        new Build
                        {
                            Type = "Room",
                            Rooms = 1,
                            RoomsToRent = 1,
                            FloorRent = 5,
                            Floors = 8,
                            TotalArea = 15,
                            LivingArea = 10,
                            KitchenArea = 3,
                            YearBuilt = 1990,
                            GarbageChute = "Yes",
                            Elevator = "Yes",
                            Furniture = "Yes",
                            HouseholdAppliances = "Yes",
                            Internet = "Yes",
                            Repairs = "Good Repair",
                            Photo = ImageConvert("~/Images/image2.jpg"),
                            Photo2 = ImageConvert("~/Images/None_photo.jpg"),
                            Photo3 = ImageConvert("~/Images/None_photo.jpg"),
                            Bathroom = "Separate",
                            Country = "Russia",
                            City = "Moscow",
                            Address = "Komsomolskaya, 75",
                            Price = 2500,
                            DateAdd = DateTime.Now,
                            Status = 0,
                            Description = "A room for rent in a nice renovated house in the center of Moscow. Please write to E-Mail.",
                            IsNumberShow = false,
                            User = user
                        },
                        new Build
                        {
                            Type = "Apartment",
                            Rooms = 3,
                            RoomsToRent = 3,
                            FloorRent = 4,
                            Floors = 5,
                            TotalArea = 50,
                            LivingArea = 40,
                            KitchenArea = 10,
                            YearBuilt = 2001,
                            GarbageChute = "Yes",
                            Elevator = "No",
                            Furniture = "Yes",
                            HouseholdAppliances = "Yes",
                            Internet = "Yes",
                            Repairs = "Good Repair",
                            Photo = ImageConvert("~/Images/image2.jpg"),
                            Photo2 = ImageConvert("~/Images/None_photo.jpg"),
                            Photo3 = ImageConvert("~/Images/None_photo.jpg"),
                            Bathroom = "Separate",
                            Country = "Italy",
                            City = "Rome",
                            Address = "Kerle, 25",
                            Price = 2000,
                            DateAdd = DateTime.Now,
                            Status = 0,
                            Description = "Apartment for rent on the outskirts of Rome. More details by phone",
                            IsNumberShow = true,
                            User = user
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}