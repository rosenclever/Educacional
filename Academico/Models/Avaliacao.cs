namespace Academico.Models
{
    public class Avaliacao
    {
        public int AvaliacaoID { get; set; }
        public string Titulo { get; set; }
        public ICollection<AlunoAvaliacao>? AvaliacoesAlunos { get; set; }
    }
}
