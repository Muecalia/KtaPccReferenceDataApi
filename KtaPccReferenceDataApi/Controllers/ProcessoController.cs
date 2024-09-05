using KtaPccReferenceDataApi.Domain.Queries.Requests;
using KtaPccReferenceDataApi.Domain.Queries.Responses;
using KtaPccReferenceDataApi.Infraestrutura.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using TotalAgilityApi.RabbitMq;
using TotalAgilityApi.Wrappers;

namespace KtaPccReferenceDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IProcessoRepository _iProcessoRepository;
        private static readonly Counter RequestProcessoDocumentoCaducadoCounter = Metrics.CreateCounter("processo_doc_caducado_total", "Total requisições processos com documento caducado endpoint", ["status_code"]);
        private static readonly Counter RequestProcessoRejeitadoCounter = Metrics.CreateCounter("processo_rejeitado_total", "Total requisições processos rejeitados documento endpoint", ["status_code"]);

        public ProcessoController(IProcessoRepository iProcessoRepository, IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
            _iProcessoRepository = iProcessoRepository;
        }

        [HttpGet("processoDocumentoCaducado")]
        public async Task<ActionResult<PagedResponse<ProcessosDocumentosCaducadosResponse>>> GetProcessosDocumentosCaducados([FromQuery] Request request, CancellationToken cancellationToken)
        {
            string Queue = "ProcessoDocumentoCaducadoQueue";
            var response = await _iProcessoRepository.GetProcessosDocumentosCaducados(request, cancellationToken);
            if (response.Succeeded)
            {
                _rabbitMqService.SendMessage(response, Queue);
                RequestProcessoDocumentoCaducadoCounter.Labels(StatusCodes.Status200OK.ToString()).Inc();
                return Ok(response);
            }
            RequestProcessoDocumentoCaducadoCounter.Labels(StatusCodes.Status400BadRequest.ToString()).Inc();
            return BadRequest(response);
        }

        [HttpGet("processosRejeitados")]
        public async Task<ActionResult<CustomResponse<ProcessosDocumentosCaducadosResponse>>> GetProcessosRejeitados([FromQuery] Request request, CancellationToken cancellationToken)
        {
            string Queue = "ProcessoRejeitadoQueue";
            var response = await _iProcessoRepository.GetProcessosRejeitados(request, cancellationToken);
            if (response.Succeeded)
            {
                _rabbitMqService.SendMessage(response, Queue);
                RequestProcessoRejeitadoCounter.Labels(StatusCodes.Status200OK.ToString()).Inc();
                return Ok(response);
            }
            RequestProcessoRejeitadoCounter.Labels(StatusCodes.Status400BadRequest.ToString()).Inc();
            return BadRequest(response);
        }

    }
}
