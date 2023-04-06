using Biblioteca_Api.Models;
using System.Collections;

namespace Biblioteca_Api.Interfaces
{
    public interface IClient 
    {
        Task<IEnumerable> GetCollection();
        List<Livro> GetLivro(int Id);
        List<Livro> getPorCategoria(int Id);
        Task DeleteLivro(int Id);
        Task UpdateLivroAsync(Livro livro);   
    }
}
