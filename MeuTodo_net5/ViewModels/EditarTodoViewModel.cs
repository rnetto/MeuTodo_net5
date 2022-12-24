
using System.ComponentModel.DataAnnotations;

namespace MeuTodo_net5.ViewModels
{
    public class EditarTodoViewModel
    {
        [Required]
        public string Titulo { get; set; }
        public bool Realizado { get; set; }
    }
}
