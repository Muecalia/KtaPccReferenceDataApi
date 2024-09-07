using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class EstatisticaMotivoRejeicaoResponse
    {
        public string CreatedOn { get; set; } = string.Empty;
        public string MotivoRejeicao { get; set; } = string.Empty;
        public int Qtd { get; set; } = 0;
    }
}
