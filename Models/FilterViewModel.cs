using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentRoom.Models
{
    public class FilterViewModel
    {
        public IEnumerable<Build> Builds { get; set; }
        public SelectList Types { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string Country { get; set;}
        public string City { get; set; }
        public int? Rooms { get; set; }
        public int? RoomsToRent { get; set; }
        public int? FloorRent { get; set; }
        public int? Floors { get; set; }
        public int? TotalArea { get; set; }
        public int? LivingArea { get; set; }
        public int? KitchenArea { get; set; }
        public int? YearBuilt { get; set; }
        public SelectList GarbageChute { get; set; } 
        public SelectList Elevator { get; set; }
        public SelectList Furniture { get; set; }
        public SelectList HouseholdAppliances { get; set; }
        public SelectList Internet { get; set; }
        public SelectList Repairs { get; set; }
        public SelectList Bathroom { get; set; }
    }
}