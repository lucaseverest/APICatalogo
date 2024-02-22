using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = await Get();
            var produtosCategoria = produtos.Where(c => c.CategoriaId == id);
            return produtosCategoria;
        }
        public async Task<IPagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = await Get();

            var produtosOrdenados = produtos.OrderBy(p => p.Id).AsQueryable();

            var resultado = await produtosOrdenados.ToPagedListAsync(produtosParameters.PageNumber,
                                                               produtosParameters.PageSize);

            return resultado;
        }


    }
}
