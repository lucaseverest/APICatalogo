using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Authorization;
using APICatalogo.Repository;
using AutoMapper;
using APICatalogo.DTOs;
using APICatalogo.Pagination;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }

        // GET: api/Produtos
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutos([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);
            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDTO;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPreco()
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDTO;
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public ActionResult<ProdutoDTO> GetProduto(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return produtoDTO;
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.Id)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDTO);
            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ProdutoDTO> PostProduto(ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);
            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var newProdutoDTO = _mapper.Map<ProdutoDTO>(produto);
            return CreatedAtAction("GetProduto", new { id = produto.Id }, newProdutoDTO);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            return NoContent();
        }
    }
}
