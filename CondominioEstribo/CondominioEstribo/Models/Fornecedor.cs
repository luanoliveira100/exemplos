using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Display(Name = "Empresa*")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "CNPJ*")]
        public string CNPJ_CPF { get; set; }

        [Display(Name = "Telefone Fixo*")]
        public string TelefoneFixo { get; set; }

        [Display(Name = "Telefone Celular")]
        public string TelefoneCelular { get; set; }

        [Display(Name = "Endereço*")]
        public string Endereco { get; set; }

        [Display(Name = "Número*")]
        public int Numero { get; set; }

        [Display(Name = "CEP*")]
        public string CEP { get; set; }

        [Display(Name = "Bairro*")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade*")]
        public string Cidade { get; set; }

        [Display(Name = "Estado*")]
        public string Estado { get; set; }

        [Display(Name = "Email")]
        public string EmailDoContato { get; set; }

        [Display(Name = "Contato")]
        public string NomeDoContato { get; set; }
                
        [Display(Name = "Status*")]
        public string FlagStatus { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }


    }
}