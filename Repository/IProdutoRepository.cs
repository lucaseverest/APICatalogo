using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;
using X.PagedList;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IPagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
        Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id);
    }
}
