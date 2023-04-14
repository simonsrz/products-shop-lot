namespace Shop_API.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime editDateDate { get; set; }
        public string description { get; set; }
        public int price { get; set; }

    }
}
