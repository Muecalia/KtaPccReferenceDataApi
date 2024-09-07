using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class EstatisticaRegistoKtaResponse
    {
        public int QtdTotal { get; set; } = 0;
        public string CreatedOn { get; set; } = string.Empty;
        public bool Attachment { get; set; } = false;
        //public int Attachment { get; set; } = 0;
        public int Qtd_imagem { get; set; } = 0;
    }
}
