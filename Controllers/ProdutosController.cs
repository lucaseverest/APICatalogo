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
using Microsoft.AspNetCore.JsonPatch;

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

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDTO);
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
                return NotFound();

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDTO);
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

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProdutoDTOUpdateResponse> Patch(int id,
        JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
        {
            //valida input 
            if (patchProdutoDto == null || id <= 0)
                return BadRequest();

            //obtem o produto pelo Id
            var produto = _uof.ProdutoRepository.GetById(c => c.Id == id);

            //se não econtrou retorna
            if (produto == null)
                return NotFound();

            //mapeia produto para ProdutoDTOUpdateRequest
            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            //aplica as alterações definidas no documento JSON Patch ao objeto ProdutoDTOUpdateRequest
            patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
                return BadRequest(ModelState);

            // Mapeia as alterações de volta para a entidade Produto
            _mapper.Map(produtoUpdateRequest, produto);

            // Atualiza a entidade no repositório
            _uof.ProdutoRepository.Update(produto);
            // Salve as alterações no banco de dados
            _uof.Commit();

            //retorna ProdutoDTOUpdateResponse
            return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
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
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            _uof.ProdutoRepository.Delete(produto);
            await _uof.Commit();

            return NoContent();
        }
    }
}
