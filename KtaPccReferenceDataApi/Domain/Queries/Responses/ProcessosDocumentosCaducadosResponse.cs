using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class ProcessosDocumentosCaducadosResponse
    {
        public string Job_Id { get; set; } = string.Empty;
        public string Msisdn { get; set; } = string.Empty;
        public string Job_Status { get; set; } = string.Empty;
        public string DocType { get; set; } = string.Empty;
        public string CreatedOn { get; set; } = string.Empty;
    }
}
