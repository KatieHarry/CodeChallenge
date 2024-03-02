﻿using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
