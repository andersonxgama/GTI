using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(14)]
        public string CPF { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string RG { get; set; }

        public DateTime DataExpedicao { get; set; }

        [Required]
        public string OrgaoExpedicao { get; set; }

        [Required]
        [MaxLength(2)]
        public string UFExpedicao { get; set; }

        public DateTime DataNascimento { get; set; }

        [Required]
        [MaxLength(1)]
        public string Sexo { get; set; }

        [Required]
        public string EstadoCivil { get; set; }

        // Um cliente tem 1 endereço
        public virtual Endereco Endereco { get; set; }
    }
}
