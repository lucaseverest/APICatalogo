using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<IPagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters)
        {
            var categorias = await Get();

            // OrderBy síncrono
            var categoriasOrdenadas = categorias.OrderBy(p => p.Id).ToList();

            //var resultado =  PagedList<Categoria>.ToPagedList(categoriasOrdenadas,
            //                         categoriasParams.PageNumber, categoriasParams.PageSize);
            var resultado = await categoriasOrdenadas.ToPagedListAsync(categoriasParameters.PageNumber,
                                                                       categoriasParameters.PageSize);

            return resultado;
        }
    }
}
