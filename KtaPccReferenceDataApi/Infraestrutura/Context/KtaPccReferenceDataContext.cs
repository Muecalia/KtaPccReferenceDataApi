using KtaPccReferenceDataApi.Domain.Queries.Responses;
using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Infraestrutura.Context
{
    public class KtaPccReferenceDataContext : DbContext
    {
        public KtaPccReferenceDataContext(DbContextOptions<KtaPccReferenceDataContext> options) : base(options) { }

        public virtual DbSet<ClientesElevadosMsisdnResponse> ClientesElevadosMsisdn { get; set; }
        public virtual DbSet<EstatisticaRegistoKtaResponse> EstatisticaRegistoKta { get; set; }
        public virtual DbSet<EstatisticaMotivoRejeicaoResponse> EstatisticaMotivoRejeicao { get; set; }
        public virtual DbSet<EstatisticaEntradaProcessoKtaResponse> EstatisticaEntradaProcessoKta { get; set; }
        public virtual DbSet<ClientesMenorIdadeResponse> ClientesMenorIdade { get; set; }
        public virtual DbSet<ProcessosRejeitadosCanal> ProcessosRejeitadosCanal { get; set; }
        public virtual DbSet<ProcessosDocumentosCaducadosResponse> ProcessosDocumentosCaducados { get; set; }

    }
}
