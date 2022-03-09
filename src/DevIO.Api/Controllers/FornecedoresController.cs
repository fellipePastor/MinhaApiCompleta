using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> GetAll()
        {

            var fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());


            return Ok(fornecedores);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> GetById(Guid id)
        {
            var fornecedor =  _mapper.Map<FornecedorViewModel>(await ObterFornecedorProdutosEndereco(id));

            if (fornecedor == null) return NotFound();            
            
            return Ok(fornecedor);
        }



        public async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }

    }

}
