namespace Shop_API.Dto
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public string description { get; set; }
        public int price { get; set; }
    }
}
