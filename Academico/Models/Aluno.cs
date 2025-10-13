using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Academico.Models
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        [Required(ErrorMessage = "O nome do aluno é obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O email do aluno é obrigatório!")]
        [DisplayName("e-mail")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O CEP do aluno é obrigatório!")]
        [DisplayName("CEP")]
        [RegularExpression(@"\d{5}-\d{3}", ErrorMessage = "O CEP deve estar no formato 99999-000")]
        public string Cep { get; set; }
        public ICollection<AlunoAvaliacao>? AlunosAvaliacoes { get; set; }
    }
}
