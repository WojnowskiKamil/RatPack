using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Rat_Pack.Models.Database
{
    public class Truck
    {
        public Guid Id { get; set; }

        [DisplayName("Marka samochodu")]
        public string CarBrand { get; set; }

        [DisplayName("Model samochodu")]
        public string CarName { get; set; }

        [DisplayName("Typ samochodu")]
        public CarType CarType { get; set;}

        [DisplayName("Lokalizacja")]
        public string HomeCity { get; set; }

        [DisplayName("Długość")]
        public decimal Lenght { get; set; }

        [DisplayName("Szerokość")]
        public decimal Width { get; set; }

        [DisplayName("Ładowność")]
        public decimal Load { get; set; }

        [DisplayName("Wysokość")]
        public decimal Height { get; set; }

        [DisplayName("Dodatkowe informacje")]
        public string AdditionalInfo { get; set; }

        public string Location { get; set; }

        [DisplayName("Właściciel")]
        public virtual IdentityUser CreatedBy { get; set; }

        public ICollection<Package> Packages { get; set; }
    }
}