﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class BloodPatient
    {

        [Key]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string fullname { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string mobile { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string bloodgroup { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string urgency { get; set; }

        public int age { get; set; }

        public string address { get; set; }
    }
}

