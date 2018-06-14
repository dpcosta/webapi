using System.Collections.Generic;

namespace Alura.WebAPI.WinFormsApp
{
    class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }

        public override string ToString()
        {
            return $"{Titulo} - {Autor}";
        }
    }

    class ListaLivros
    {
        public IEnumerable<Livro> Livros { get; set; }
    }
}