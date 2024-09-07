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
    public class EstatisticaRepository : IEstatisticaRepository
    {
        readonly KtaPccReferenceDataContext _context;
        readonly ILogger<EstatisticaRepository> _logger;
        private readonly CultureInfo customCulture;

        public EstatisticaRepository(KtaPccReferenceDataContext context, ILogger<EstatisticaRepository> logger)
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
        * Objectivo: Listar a estatística da entrada de processos em KTA
        * Parametros: request (DataInicial e DataFinal)
        * Retorno: A lista contendo os dados ou lista vazia
        *************************************************************************************************/
        public async Task<PagedResponse<EstatisticaEntradaProcessoKtaResponse>> GetEstatisticaEntradaProcessoKta(Request request, CancellationToken cancellationToken)
        {
            var Entidade = "Estatística de entrada de processos em KTA";
            try
            {
                var DataActual = DateTime.Now;
                DateTime DataInicio = Convert.ToDateTime(request.DataInicial);
                DateTime DataFinal = Convert.ToDateTime(request.DataFinal);

                if ((DataInicio.CompareTo(DataActual) > 0) || (DataFinal.CompareTo(DataActual) > 0))
                    return new PagedResponse<EstatisticaEntradaProcessoKtaResponse>(MessageError.DataError());

                if (DataInicio.CompareTo(DataFinal) > 0)
                    return new PagedResponse<EstatisticaEntradaProcessoKtaResponse>(MessageError.DataError(DataInicio.ToString("d"), DataFinal.ToString("d")));

                var response = await _context.EstatisticaEntradaProcessoKta.FromSqlInterpolated($"EXEC [dbo].[sp_GetEstatisticaEntradaProcessoKta] @DataInicio ={DataInicio}, @DataFinal ={DataFinal}").ToListAsync(cancellationToken);

                _logger.LogInformation(MessageError.CarregamentoSucesso(Entidade, response.Count));
                return new PagedResponse<EstatisticaEntradaProcessoKtaResponse>(response, MessageError.CarregamentoSucesso(Entidade));
            }
            catch (Exception ex)
            {
                _logger.LogError(MessageError.BadRequest(Entidade, ex.Message));
                return new PagedResponse<EstatisticaEntradaProcessoKtaResponse>(Entidade);
            }
        }

        /*************************************************************************************************
        * Objectivo: Listar a estatística do motivo de rejeição de processos em KTA
        * Parametros: request (DataInicial e DataFinal)
        * Retorno: A lista contendo os dados ou lista vazia
        *************************************************************************************************/
        public async Task<PagedResponse<EstatisticaMotivoRejeicaoResponse>> GetEstatisticaMotivoRejeicao(Request request, CancellationToken cancellationToken)
        {
            var Entidade = "Estatística de Motivo de Rejeição em KTA";
            try
            {
                var DataActual = DateTime.Now;
                DateTime DataInicio = Convert.ToDateTime(request.DataInicial);
                DateTime DataFinal = Convert.ToDateTime(request.DataFinal);

                if ((DataInicio.CompareTo(DataActual) > 0) || (DataFinal.CompareTo(DataActual) > 0))
                    return new PagedResponse<EstatisticaMotivoRejeicaoResponse>(MessageError.DataError());

                if (DataInicio.CompareTo(DataFinal) > 0)
                    return new PagedResponse<EstatisticaMotivoRejeicaoResponse>(MessageError.DataError(DataInicio.ToString("d"), DataFinal.ToString("d")));

                var response = await _context.EstatisticaMotivoRejeicao.FromSqlInterpolated($"EXEC [dbo].[sp_GetEstatisticaMotivoRejeicao] @DataInicio ={DataInicio}, @DataFinal ={DataFinal}").ToListAsync(cancellationToken);

                _logger.LogInformation(MessageError.CarregamentoSucesso(Entidade, response.Count));
                return new PagedResponse<EstatisticaMotivoRejeicaoResponse>(response, MessageError.CarregamentoSucesso(Entidade));
            }
            catch (Exception ex)
            {
                _logger.LogError(MessageError.BadRequest(Entidade, ex.Message));
                return new PagedResponse<EstatisticaMotivoRejeicaoResponse>(Entidade);
            }
        }

        /*************************************************************************************************
        * Objectivo: Listar a estatística dos registo em KTA
        * Parametros: request (DataInicial e DataFinal)
        * Retorno: A lista contendo os dados ou lista vazia
        *************************************************************************************************/
        public async Task<PagedResponse<EstatisticaRegistoKtaResponse>> GetEstatisticaRegistoKta(Request request, CancellationToken cancellationToken)
        {
            var Entidade = "Estatística de registo em KTA";
            try
            {
                var DataActual = DateTime.Now;
                DateTime DataInicio = Convert.ToDateTime(request.DataInicial);
                DateTime DataFinal = Convert.ToDateTime(request.DataFinal);

                if ((DataInicio.CompareTo(DataActual) > 0) || (DataFinal.CompareTo(DataActual) > 0))
                    return new PagedResponse<EstatisticaRegistoKtaResponse>(MessageError.DataError());

                if (DataInicio.CompareTo(DataFinal) > 0)
                    return new PagedResponse<EstatisticaRegistoKtaResponse>(MessageError.DataError(DataInicio.ToString("d"), DataFinal.ToString("d")));

                var response = await _context.EstatisticaRegistoKta.FromSqlInterpolated($"EXEC [dbo].[sp_GetEstatisticaRegistoKta] @DataInicio ={DataInicio}, @DataFinal ={DataFinal}").ToListAsync(cancellationToken);

                _logger.LogInformation(MessageError.CarregamentoSucesso(Entidade, response.Count));
                return new PagedResponse<EstatisticaRegistoKtaResponse>(response, MessageError.CarregamentoSucesso(Entidade));
            }
            catch (Exception ex)
            {
                _logger.LogError(MessageError.BadRequest(Entidade, ex.Message));
                return new PagedResponse<EstatisticaRegistoKtaResponse>(Entidade);
            }
        }


    }
}
