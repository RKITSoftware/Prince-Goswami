﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MobileNo { get; set; }
        public string City { get; set; }
        public bool isActive { get; set; }
    }
}