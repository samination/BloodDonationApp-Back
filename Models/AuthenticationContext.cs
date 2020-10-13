using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationBlood.Models
{
    public class AuthenticationContext:IdentityDbContext<HospitalAdmin>
    {

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
             
        }

        public DbSet<HospitalAdmin> HospitalAdmins { get; set; }

    }
}
