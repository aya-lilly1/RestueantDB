 namespace RestueantDB.Models
{
    public class Meal : Parent
    {
        public int Id { get; set; }
        public int ResturantId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public string ImageURL { get; set; }
        public Resturant Resturant { get; set; }



    }
}
