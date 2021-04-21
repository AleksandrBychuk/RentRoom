using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentRoom.Models
{
    public class Build
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Select the type of accommodation!")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Select number of rooms!")]
        public int? Rooms { get; set; }

        public int? RoomsToRent { get; set; }

        [Required(ErrorMessage = "Choose a floor!")]
        public int? FloorRent { get; set; }

        public int? Floors { get; set; }

        public int? TotalArea { get; set; }

        public int? LivingArea { get; set; }
        public int? KitchenArea { get; set; }
        public int? YearBuilt { get; set; }
        public string GarbageChute { get; set; } // мусоропровод
        public string Elevator { get; set; }
        public string Furniture { get; set; }
        public string HouseholdAppliances { get; set; } //  бытовая техника
        public string Internet { get; set; }
        public string Repairs { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Photo2 { get; set; }
        public byte[] Photo3 { get; set; }
        public string Bathroom { get; set; }
        [Required(ErrorMessage = "Enter your country!")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Enter the city!")]
        public string City { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter the price in $!")]
        public int Price { get; set; }
        public DateTime DateAdd { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Fill in the description!")]
        public string Description { get; set; }

        public bool IsNumberShow { get; set; }
        public User User { get; set; }
    }
}