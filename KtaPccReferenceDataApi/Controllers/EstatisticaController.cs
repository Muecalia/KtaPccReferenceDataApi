using KtaPccReferenceDataApi.Domain.Queries.Requests;
using KtaPccReferenceDataApi.Domain.Queries.Responses;
using KtaPccReferenceDataApi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using TotalAgilityApi.RabbitMq;
using TotalAgilityApi.Wrappers;

namespace KtaPccReferenceDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatisticaController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IEstatisticaRepository _iEstatisticaRepository;
        private static readonly Counter RequestEstatisticaEntradaProcessoKtaCounter = Metrics.CreateCounter("estatistica_entrada_processo_total", "Total requisições Estatistica Entrada de Processo em Kta endpoint", ["status_code"]);
        private static readonly Counter RequestEstatisticaRegistoKtaCounter = Metrics.CreateCounter("estatistica_registo_processo_total", "Total requisições Estatistica Registo de Processo em Kta endpoint", ["status_code"]);
        private static readonly Counter RequestEstatisticaMotivoRejeicaoCounter = Metrics.CreateCounter("estatistica_ewjwicao_processo_total", "Total requisições Estatistica Motivo de Rejeição em Kta endpoint", ["status_code"]);

        public EstatisticaController(IEstatisticaRepository iEstatisticaRepository, IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
            _iEstatisticaRepository = iEstatisticaRepository;
        }

        [HttpGet("estatisticaEntradaProcessoKta")]
        public async Task<ActionResult<PagedResponse<EstatisticaEntradaProcessoKtaResponse>>> GetEstatisticaEntradaProcessoKta([FromQuery] Request request, CancellationToken cancellationToken)
        {
            string Queue = "EstatisticaEntradaProcessoKtaQueue";
            var response = await _iEstatisticaRepository.GetEstatisticaEntradaProcessoKta(request, cancellationToken);
            if (response.Succeeded)
            {
                _rabbitMqService.SendMessage(response, Queue);
                RequestEstatisticaEntradaProcessoKtaCounter.Labels(StatusCodes.Status200OK.ToString()).Inc();
                return Ok(response);
            }
            RequestEstatisticaEntradaProcessoKtaCounter.Labels(StatusCodes.Status400BadRequest.ToString()).Inc();
            return BadRequest(response);
        }

        [HttpGet("estatisticaRegistoKta")]
        public async Task<ActionResult<PagedResponse<EstatisticaRegistoKtaResponse>>> GetEstatisticaRegistoKta([FromQuery] Request request, CancellationToken cancellationToken)
        {
            string Queue = "EstatisticaRegistoKtaQueue";
            var response = await _iEstatisticaRepository.GetEstatisticaRegistoKta(request, cancellationToken);
            if (response.Succeeded)
            {
                _rabbitMqService.SendMessage(response, Queue);
                RequestEstatisticaRegistoKtaCounter.Labels(StatusCodes.Status200OK.ToString()).Inc();
                return Ok(response);
            }
            RequestEstatisticaRegistoKtaCounter.Labels(StatusCodes.Status400BadRequest.ToString()).Inc();
            return BadRequest(response);
        }

        [HttpGet("estatisticaMotivoRejeicao")]
        public async Task<ActionResult<PagedResponse<EstatisticaMotivoRejeicaoResponse>>> GetEstatisticaMotivoRejeicao([FromQuery] Request request, CancellationToken cancellationToken)
        {
            string Queue = "EstatisticaMotivoRejeicaoQueue";
            var response = await _iEstatisticaRepository.GetEstatisticaMotivoRejeicao(request, cancellationToken);
            if (response.Succeeded)
            {
                _rabbitMqService.SendMessage(response, Queue);
                RequestEstatisticaMotivoRejeicaoCounter.Labels(StatusCodes.Status200OK.ToString()).Inc();
                return Ok(response);
            }
            RequestEstatisticaMotivoRejeicaoCounter.Labels(StatusCodes.Status400BadRequest.ToString()).Inc();
            return BadRequest(response);
        }

    }
}
