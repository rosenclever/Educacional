using Microsoft.AspNetCore.Mvc.Rendering;

namespace Academico.Models
{
    public class AvaliacaoAlunoViewModel
    {
        
            // Propriedades para o formulário
            public int AlunoId { get; set; }
            public int AvaliacaoId { get; set; }
            public float Nota { get; set; }

            // Listas para popular os dropdowns na view
            public SelectList? Alunos { get; set; }
            public SelectList? Avaliacoes { get; set; }
        
    }
}
