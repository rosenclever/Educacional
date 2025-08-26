using System.ComponentModel;

namespace Academico.Models
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        public string Nome { get; set; }

        [DisplayName("e-mail")]
        public string Email { get; set; }

        [DisplayName("CEP")]
        public long Cep { get; set; }
    }
}
