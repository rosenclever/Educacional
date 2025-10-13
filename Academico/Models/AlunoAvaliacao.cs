namespace Academico.Models
{
    public class AlunoAvaliacao
    {
        public int AlunoID { get; set; }
        public int AvaliacaoID { get; set; }
        public float Nota { get; set; }
        public Aluno? Aluno { get; set; }
        public Avaliacao? Avaliacao { get; set; }
    }
}
