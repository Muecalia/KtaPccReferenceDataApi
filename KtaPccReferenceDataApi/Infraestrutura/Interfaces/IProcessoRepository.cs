using KtaPccReferenceDataApi.Domain.Queries.Requests;
using KtaPccReferenceDataApi.Domain.Queries.Responses;
using TotalAgilityApi.Wrappers;

namespace KtaPccReferenceDataApi.Infraestrutura.Interfaces
{
    public interface IProcessoRepository
    {
        Task<CustomResponse<ProcessosRejeitadosResponse>> GetProcessosRejeitados(Request request, CancellationToken cancellationToken);
        Task<PagedResponse<ProcessosDocumentosCaducadosResponse>> GetProcessosDocumentosCaducados(Request request, CancellationToken cancellationToken);
    }
}
