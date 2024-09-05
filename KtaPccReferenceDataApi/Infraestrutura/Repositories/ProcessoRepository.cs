using KtaPccReferenceDataApi.Config;
using KtaPccReferenceDataApi.Domain.Queries.Requests;
using KtaPccReferenceDataApi.Domain.Queries.Responses;
using KtaPccReferenceDataApi.Infraestrutura.Context;
using KtaPccReferenceDataApi.Infraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TotalAgilityApi.Wrappers;

namespace KtaPccReferenceDataApi.Infraestrutura.Repositories
{
    public class ProcessoRepository : IProcessoRepository
    {
        readonly KtaPccReferenceDataContext _context;
        readonly ILogger<ProcessoRepository> _logger;
        private readonly CultureInfo customCulture;

        public ProcessoRepository(KtaPccReferenceDataContext context, ILogger<ProcessoRepository> logger)
        {
            _logger = logger;

            customCulture = new CultureInfo("en-US", false);
            customCulture = new CultureInfo("pt-PT", true);

            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";

            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;

            _context = context;
        }

        /*************************************************************************************************
        * Objectivo: Listar os processos com documentos caducados em KTA
        * Parametros: request (DataInicial e DataFinal)
        * Retorno: A lista contendo os dados ou lista vazia
        *************************************************************************************************/
        public async Task<PagedResponse<ProcessosDocumentosCaducadosResponse>> GetProcessosDocumentosCaducados(Request request, CancellationToken cancellationToken)
        {
            var Entidade = "Processos com Documentos Caducados";
            try
            {
                var DataActual = DateTime.Now;
                DateTime FirstDate = Convert.ToDateTime(request.DataInicial);
                DateTime EndDate = Convert.ToDateTime(request.DataFinal);

                if ((FirstDate.CompareTo(DataActual) > 0) || (EndDate.CompareTo(DataActual) > 0))
                    return new PagedResponse<ProcessosDocumentosCaducadosResponse>(MessageError.DataError());

                if (FirstDate.CompareTo(EndDate) > 0)
                    return new PagedResponse<ProcessosDocumentosCaducadosResponse>(MessageError.DataError(FirstDate.ToString("d"), EndDate.ToString("d")));

                var response = await _context.ProcessosDocumentosCaducados.FromSqlInterpolated($"EXEC [dbo].[sp_GetAllProcessoDocumentoCaducado] @FirstDate={FirstDate}, @EndDate={EndDate}").ToListAsync(cancellationToken);

                _logger.LogInformation(MessageError.CarregamentoSucesso(Entidade, response.Count));
                return new PagedResponse<ProcessosDocumentosCaducadosResponse>(response, MessageError.CarregamentoSucesso(Entidade));
            }
            catch (Exception ex)
            {
                _logger.LogError(MessageError.BadRequest(Entidade, ex.Message));
                return new PagedResponse<ProcessosDocumentosCaducadosResponse>(Entidade);
            }
        }

        /*************************************************************************************************
        * Objectivo: Listar os processos rejeitados em KTA
        * Parametros: request (DataInicial e DataFinal)
        * Retorno: A lista contendo os dados ou lista vazia
        ************************************************************************************************/
        public async Task<CustomResponse<ProcessosRejeitadosResponse>> GetProcessosRejeitados(Request request, CancellationToken cancellationToken)
        {
            var Entidade = "Processos Rejeitados";
            try
            {
                var DataActual = DateTime.Now;
                DateTime FirstDate = Convert.ToDateTime(request.DataInicial);
                DateTime EndDate = Convert.ToDateTime(request.DataFinal);

                if ((FirstDate.CompareTo(DataActual) > 0) || (EndDate.CompareTo(DataActual) > 0))
                    return new PagedResponse<ProcessosRejeitadosResponse>(MessageError.DataError());

                if (FirstDate.CompareTo(EndDate) > 0)
                    return new PagedResponse<ProcessosRejeitadosResponse>(MessageError.DataError(FirstDate.ToString("d"), EndDate.ToString("d")));

                var responseAgentes = await _context.ProcessosRejeitadosCanal.FromSqlInterpolated($"EXEC [dbo].[sp_GetProcessosRejeitadosAgentes] @DataInicio={FirstDate}, @DataFinal={EndDate}").ToListAsync(cancellationToken);
                var responseLojas = await _context.ProcessosRejeitadosCanal.FromSqlInterpolated($"EXEC [dbo].[sp_GetProcessosRejeitadosLojas] @DataInicio={FirstDate}, @DataFinal={EndDate}").ToListAsync(cancellationToken);

                //System.Diagnostics.Debug.Print($"responseAgentes: {responseAgentes.Count}, responseLojas: {responseLojas.Count}");

                var response = new ProcessosRejeitadosResponse
                {
                    RejeitadosAgentes = responseLojas,
                    RejeitadosLoja = responseLojas
                };

                _logger.LogInformation(MessageError.CarregamentoSucesso(Entidade, responseLojas.Count+responseAgentes.Count));
                return new CustomResponse<ProcessosRejeitadosResponse>(response, MessageError.CarregamentoSucesso(Entidade));

            }
            catch (Exception ex)
            {
                _logger.LogError(MessageError.BadRequest(Entidade, ex.Message));
                return new PagedResponse<ProcessosRejeitadosResponse>(Entidade);
            }
        }

    }
}
