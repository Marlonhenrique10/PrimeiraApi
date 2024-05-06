using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain.Model
{
    [Table("infoApi")]
    public class InfoApi
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        public int age { get; set; }

        public string? photo { get; set; }

        public InfoApi() { }
        // Construtor
        public InfoApi(string name, int age, string photo)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.age = age;
            this.photo = photo;
        }
    }
}
