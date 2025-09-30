using System.ComponentModel;

namespace Academico.Models
{
    public class Departamento
    {
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Nome { get; set; }
        public Instituicao? Instituicao { get; set; }
        public long InstituicaoID { get; set; }
    }
}
