using APICatalogo.Models;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
