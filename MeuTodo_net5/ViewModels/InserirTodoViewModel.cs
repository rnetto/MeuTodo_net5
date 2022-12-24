using System.ComponentModel.DataAnnotations;

namespace MeuTodo_net5.ViewModels
{
    public class InserirTodoViewModel
    {
        [Required]
        public string Titulo { get; set; }
    }
}
