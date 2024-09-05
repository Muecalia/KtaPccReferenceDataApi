using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class ClientesMenorIdadeResponse
    {
        public string Job_Id { get; set; } = string.Empty;
        public string Msisdn { get; set; } = string.Empty;
        public string Job_Status { get; set; } = string.Empty;
        public string DocType { get; set; } = string.Empty;
        public string DataCriacao { get; set; } = string.Empty;
    }
}
