using System.Collections.Generic;

namespace TVmaze.Scraper.Domain.Models
{
    public class Show
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Person> Cast { get; set; }

        public Show()
        {
            Cast = new List<Person>();
        }
    }
}
