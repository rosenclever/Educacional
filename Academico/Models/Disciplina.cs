using System.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace Academico.Models
{
    public class Disciplina
    {
        public int DisciplinaID { get; set; }
        public string Nome { get; set; }
        [IntegerValidator(MinValue = 10, MaxValue = 500)]
        public int CargaHoraria { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}
