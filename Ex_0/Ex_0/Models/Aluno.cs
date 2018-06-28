using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ex_0.Models
{
    public class Aluno
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Curso { get; set; }
    }


    public class AlunoDBContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; } 
    }
}