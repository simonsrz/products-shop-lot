using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public User(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
    }
}
