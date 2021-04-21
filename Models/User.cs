using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentRoom.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = ("varchar(250)"))]
        public string Login { get; set; }
        [Required]
        [Column(TypeName = ("varchar(250)"))]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = ("varchar(250)"))]
        public string Email { get; set; }
        [Column(TypeName = ("varchar(250)"))]
        public string PhoneNumber { get; set; }
        [Column(TypeName = ("integer"))]
        public int Admin { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<Build> Builds { get; set; } = new List<Build>();
    }
}