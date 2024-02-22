using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;
using X.PagedList;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IPagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
    }
}
