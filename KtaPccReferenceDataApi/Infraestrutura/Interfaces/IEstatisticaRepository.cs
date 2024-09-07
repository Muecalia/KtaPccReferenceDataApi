using KtaPccReferenceDataApi.Domain.Queries.Requests;
using KtaPccReferenceDataApi.Domain.Queries.Responses;
using TotalAgilityApi.Wrappers;

namespace KtaPccReferenceDataApi.Infraestrutura.Interfaces
{
    public interface IEstatisticaRepository
    {
        Task<PagedResponse<EstatisticaRegistoKtaResponse>> GetEstatisticaRegistoKta(Request request, CancellationToken cancellationToken);
        Task<PagedResponse<EstatisticaMotivoRejeicaoResponse>> GetEstatisticaMotivoRejeicao(Request request, CancellationToken cancellationToken);
        Task<PagedResponse<EstatisticaEntradaProcessoKtaResponse>> GetEstatisticaEntradaProcessoKta(Request request, CancellationToken cancellationToken);
    }
}
