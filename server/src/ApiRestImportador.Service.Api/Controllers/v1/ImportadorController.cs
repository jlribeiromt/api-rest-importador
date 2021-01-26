using ApiRestImportador.Domain.Core.Bus;
using ApiRestImportador.Domain.Core.Notifications;
using ApiRestImportador.Domain.DTO;
using ApiRestImportador.Domain.Interfaces.Repository;
using ApiRestImportador.Domain.Models;
using ApiRestImportador.Domain.Validations.Importacao;
using ApiRestImportador.Service.Api.Configuration;
using ExcelDataReader;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestImportador.Service.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/importador/")]
    public class ImportadorController : BaseController
    {
        private readonly IImportacaoRepository _repository;
        private readonly ISelectSearchRepository<ItemImportacao> _selectSearchItemImportacaoRepository;
        private LayoutPlanilhaExcel _layoutPlanilhaExcel;

        public ImportadorController(IImportacaoRepository repository,
                                    LayoutPlanilhaExcel layoutPlanilhaExcel,
                                    ISelectSearchRepository<ItemImportacao> selectSearchItemImportacaoRepository,
                                    INotificationHandler<DomainNotification> notifications,
                                    IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _repository = repository;
            _layoutPlanilhaExcel = layoutPlanilhaExcel;
            _selectSearchItemImportacaoRepository = selectSearchItemImportacaoRepository;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> Get()
        {
            var listaImportacao = await _repository.GetAllAsync(c => new ImportacaoDTO
            {
                ImportacaoId = c.ImportacaoId,
                TotalItens = c.TotalItens,
                ValorTotal = c.ValorTotal,
                DataImportacao = c.DataImportacao
            });

            return Ok(new
            {
                Success = true,
                Data = listaImportacao
            });
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> Get(int id)
        {
            var listaItemImportacao = await _selectSearchItemImportacaoRepository
                .GetAll(c => new
                {
                    c.ItemImportacaoId,
                    c.ImportacaoId,
                    c.NomeProduto,
                    c.Quantidade,
                    c.ValorUnitario,
                    c.DataEntrega
                }, p => p.ImportacaoId == id);


            return Ok(new
            {
                Success = true,
                Data = listaItemImportacao
            });
        }

        [HttpPost]
        public IActionResult Post([FromForm] IFormFile file)
        {
            // Mapear os dados da planilha
            var result = _layoutPlanilhaExcel.MapearPlanilhaParaEntidade(file);

            if (!result)
            {
                // Quando há erros no layout base.
                _layoutPlanilhaExcel.Erros
                    .ForEach(item => NotifyError(string.Empty, item));

                return Response();
            }

            var registerCommand = _layoutPlanilhaExcel.NewImportacaoCommand;
            _mediator.SendCommand(registerCommand);
            return Response("Importação realizada com sucesso!");
        }
    }
}
