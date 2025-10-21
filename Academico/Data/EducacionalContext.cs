using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Data
{
    public class EducacionalContext : DbContext
    {
        public EducacionalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<AlunoAvaliacao> AlunosAvaliacoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoAvaliacao>()
                .HasKey(aa => new { aa.AlunoID, aa.AvaliacaoID });
            modelBuilder.Entity<AlunoAvaliacao>()
                .HasOne(aa => aa.Aluno)
                .WithMany(a => a.AlunosAvaliacoes)
                .HasForeignKey(aa => aa.AlunoID);
            modelBuilder.Entity<AlunoAvaliacao>()
                .HasOne(aa => aa.Avaliacao)
                .WithMany(av => av.AvaliacoesAlunos)
                .HasForeignKey(aa => aa.AvaliacaoID);
        }       
    }
}
