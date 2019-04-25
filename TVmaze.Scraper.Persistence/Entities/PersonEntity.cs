using System;
using System.ComponentModel.DataAnnotations;

namespace TVmaze.Scraper.Persistence.Entities
{
    public class PersonEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public int PersonId { get; set; }
        
        public string Name { get; set; }
        
        public DateTime Birthday { get; set; }
    }
}
