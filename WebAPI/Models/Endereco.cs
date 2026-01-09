using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebAPI.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key, ForeignKey("Cliente")] // <-- Chave primária e estrangeira do Cliente
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(9)]
        public string CEP { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        public string UF { get; set; }

        [JsonIgnore] // evita loop de serialização
        public virtual Cliente Cliente { get; set; }
    }
}
