﻿using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Domain.Queries.Responses
{
    [Keyless]
    public class EstatisticaEntradaProcessoKtaResponse
    {
        public int Qtd { get; set; } = 0;
        public string CreatedOn { get; set; } = string.Empty;
        public string Job_Status { get; set; } = string.Empty;
    }
}
