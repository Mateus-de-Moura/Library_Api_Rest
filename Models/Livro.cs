namespace Biblioteca_Api.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Editora { get; set; }
        public string? Autor { get; set; }
        public string? UrlImagem { get; set; }
        public decimal? Valor { get; set; }
        public string? Categoria { get; set; }
        public int Quantidade { get; set; }      

    }
}
