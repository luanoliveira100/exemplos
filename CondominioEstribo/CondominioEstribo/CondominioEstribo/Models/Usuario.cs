using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondominioEstribo.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Display(Name = "Login")]
        public string LoginUsuario { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string SenhaUsuario { get; set; }

        
        [Display(Name = "Nova Senha")]
        [DataType(DataType.Password)]
        public string NovaSenhaUsuario { get; set; }

        [Display(Name = "Ativo")]
        public bool StatusUsuario { get; set; }

        [Display(Name = "Perfil")]
        public string PerfilUsuario { get; set; }

        public int ProprietarioDentroDeUsuario { get; set; }

        public int InquilinoDentroDeUsuario { get; set; }

        public int FuncionarioDentroDeUsuario { get; set; }





    }
}