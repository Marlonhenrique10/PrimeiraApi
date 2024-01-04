using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    [Table("infoApi")]
    public class InfoApi
    {
        [Key]
        public int id { get; private set; }

        public string name { get; private set; }

        public int age { get; private set; }

        public string? photo { get; private set; } // Estou permitindo que dentro da variavel, aceite o valor null

        // Construtor
        public InfoApi(string name, int age, string photo)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.age = age;
            this.photo = photo;
        }
    }
}
