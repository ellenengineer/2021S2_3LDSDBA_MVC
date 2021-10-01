using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MVC.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Conta = new HashSet<Contum>();
        }

        public int CodCli { get; set; }
        public int CodTipoCli { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required(ErrorMessage = "O nome do cliente deve ser informado")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de nascimento do cliente deve ser informada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNascFund { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "A renda do cliente deve ser informada")]
        public decimal RendaLucro { get; set; }

        [StringLength(1, MinimumLength = 1)]
        [Required(ErrorMessage = "O sexo do cliente deve ser informado")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "O email do cliente deve ser informado")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O endereço do cliente deve ser informado")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O documento do cliente deve ser informado")]
        public string Documento { get; set; }
        public string TipoEmpresa { get; set; }

        public virtual TipoCli CodTipoCliNavigation { get; set; }
        public virtual ICollection<Contum> Conta { get; set; }
    }
}
