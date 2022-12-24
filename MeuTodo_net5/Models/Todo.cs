using System;

namespace MeuTodo_net5.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Realizado { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
