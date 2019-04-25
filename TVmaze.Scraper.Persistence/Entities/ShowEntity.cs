using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TVmaze.Scraper.Persistence.Entities
{
    public class ShowEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public int ShowId { get; set; }
        
        public string Name { get; set; }
        
        public List<PersonEntity> Cast { get; set; }
    }
}
