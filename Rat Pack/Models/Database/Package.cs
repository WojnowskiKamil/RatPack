using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rat_Pack.Models.Database
{
    public class Package
    {
        public Guid Id { get; set; }

        [DisplayName("Kategoria")]
        public Category Category { get; set; }

        [DisplayName("Krótki opis paczki")]
        public string Description { get; set; }

        [DisplayName("Miejsce załadunku")]
        public string CityStart { get; set; }

        [DisplayName("Miejsce rozładunku")]
        public string CityEnd { get; set; }

        [DisplayName("Data załadunku")]
        public DateTime DateStart { get; set; }

        [DisplayName("Data dostawy")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Długość")]
        public decimal Lenght { get; set; }
        [DisplayName("Szerokość")]
        public decimal Width { get; set; }
        [DisplayName("Waga")]
        public decimal Weight { get; set; }
        [DisplayName("Wysokość")]
        public decimal Height { get; set; }

        [DisplayName("Dodatkowe informacje")]
        public string AdditionalInfo { get; set; }

        [DisplayName("Właściciel")]
        public virtual IdentityUser CreatedBy { get; set; }

        public ICollection<Message> Messages { get; set; }

        [ForeignKey("Truck")]
        public Guid? Truck_Id { get; set; }

        public virtual Truck Truck { get; set; }
    }
}