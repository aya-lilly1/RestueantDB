using System.Collections.Generic;

namespace RestueantDB.Models
{
    public class Resturant: Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone  { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public string ImageURL { get; set; }
        public List<Meal> Meals { get; set; }

    }
}
