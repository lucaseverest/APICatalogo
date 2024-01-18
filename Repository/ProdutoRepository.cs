using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco);
        }
        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            //return Get()
            //        .OrderBy(on => on.Nome)
            //        .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            //        .Take(produtosParameters.PageSize)
            //        .ToList();

            return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.Id),
                produtosParameters.PageNumber, produtosParameters.PageSize);
        }
    }
}
