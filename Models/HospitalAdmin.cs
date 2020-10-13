
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DonationBlood.Models
{
    public class HospitalAdmin:IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string Fullname { get; set; }
    }
}
