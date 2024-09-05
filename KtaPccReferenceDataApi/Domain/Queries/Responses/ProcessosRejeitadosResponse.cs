using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    
    public class ProcessosRejeitadosResponse
    {
        //public ProcessosRejeitadosResponse()
        //{
        //    RejeitadosAgentes = RejeitadosLoja = new List<ProcessosRejeitadosCanal>();
        //}

        public required List<ProcessosRejeitadosCanal> RejeitadosAgentes { get; set; } = [];
        public required List<ProcessosRejeitadosCanal> RejeitadosLoja { get; set; } = [];
    }
}
