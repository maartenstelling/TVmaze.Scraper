﻿using System;
using Newtonsoft.Json;

namespace TVmaze.Scraper.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime? Birthday { get; set; }
    }
}
