using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime editDate { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        public Product(int id, string name, DateTime creationDate, DateTime editDate, string description, int price)
        {
            this.id = id;
            this.name = name;
            this.creationDate = creationDate;
            this.editDate = editDate;
            this.description = description;
            this.price = price;
        }
    }
}
