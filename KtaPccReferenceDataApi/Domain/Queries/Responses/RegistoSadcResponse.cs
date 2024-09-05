using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class RegistoSadcResponse
    {
        public string Job_id { get; set; } = string.Empty;
        public string Msisdn { get; set; } = string.Empty;
        public string Job_Status { get; set; } = string.Empty;
        //public string Attachment { get; set; } = string.Empty;
        public string CreatedOn { get; set; } = string.Empty;
        public string SapReference { get; set; } = string.Empty;
        public string SireAgentId { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public string ControlActionReason { get; set; } = string.Empty;
    }
}
