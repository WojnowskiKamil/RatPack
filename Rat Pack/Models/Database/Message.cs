using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rat_Pack.Models.Database
{
    public class Message
    {
        public Guid Id { get; set; }

        [DisplayName("Oferta")]
        public string Content { get; set; }

        [DisplayName("Data utworzenia")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Package")]
        public Guid? Package_Id { get; set; }

        public virtual Package Package { get; set; }

        [DisplayName("Utworzone przez")]
        public virtual IdentityUser CreatedBy { get; set; }
    }
}