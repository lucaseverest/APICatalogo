using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;
    private readonly IMapper _mapper;



    public CategoriasController(IUnitOfWork uof,
        ILogger<CategoriasController> logger,
        IMapper mapper)
    {

        _logger = logger;
        _uof = uof;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtem uma lista de objetos Categoria
    /// </summary>
    /// <returns>Uma lista de objetos Categoria</returns>

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
    {
        var categorias = await _uof.CategoriaRepository.Get();

        var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

        return Ok(categoriasDTO);
    }

    [HttpGet("pagination")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias([FromQuery] CategoriasParameters categoriasParameters)
    {
        var categorias = await _uof.CategoriaRepository.GetCategorias(categoriasParameters);
        var metadata = new
        {
            categorias.Count,
            categorias.PageSize,
            categorias.PageCount,
            categorias.TotalItemCount,
            categorias.HasNextPage,
            categorias.HasPreviousPage

        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

        return Ok(categoriasDTO);
    }

    /// <summary>
    /// Obtem uma Categoria pelo seu Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Objetos Categoria</returns>
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetById(c => c.Id == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoria);
    }

    /// <summary>
    /// Inclui uma nova categoria
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     POST api/categorias
    ///     {
    ///        "categoriaId": 1,
    ///        "nome": "categoria1",
    ///        "imagemUrl": "http://teste.net/1.jpg"
    ///     }
    /// </remarks>
    /// <param name="categoriaDTO">objeto Categoria</param>
    /// <returns>O objeto Categoria incluida</returns>
    /// <remarks>Retorna um objeto Categoria incluído</remarks>
    [HttpPost]
    public ActionResult Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = _mapper.Map<Categoria>(categoriaDTO);


        var categoriaCriada = _uof.CategoriaRepository.Add(categoria);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriada.Id },
            categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoriaDTO)
    {
        if (id != categoriaDTO.Id)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }
        var categoria = _mapper.Map<Categoria>(categoriaDTO);

        _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        var newCategoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(newCategoriaDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetById(c => c.Id == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        _uof.CategoriaRepository.Delete(categoria);
        await _uof.Commit();

        return Ok();

    }
}