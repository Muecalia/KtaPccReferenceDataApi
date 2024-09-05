using KtaPccReferenceDataApi.Domain.Queries.Responses;
using Microsoft.EntityFrameworkCore;

namespace KtaPccReferenceDataApi.Infraestrutura.Context
{
    public class KtaPccReferenceDataContext : DbContext
    {
        public KtaPccReferenceDataContext(DbContextOptions<KtaPccReferenceDataContext> options) : base(options) { }

        public virtual DbSet<ClientesElevadosMsisdnResponse> ClientesElevadosMsisdn { get; set; }
        public virtual DbSet<RegistoSadcResponse> RegistoSadc { get; set; }
        public virtual DbSet<ClientesMenorIdadeResponse> ClientesMenorIdade { get; set; }
        public virtual DbSet<ProcessosRejeitadosCanal> ProcessosRejeitadosCanal { get; set; }
        public virtual DbSet<ProcessosDocumentosCaducadosResponse> ProcessosDocumentosCaducados { get; set; }

    }
}
