using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class ProcessosRejeitadosCanal
    {
        public string Msisdn { get; set; } = string.Empty;
        public string Job_Status { get; set; } = string.Empty;
        public string SapReference { get; set; } = string.Empty;
        public string ControlActionReason { get; set; } = string.Empty;
        public string CreatedOn { get; set; } = string.Empty;
    }
}
