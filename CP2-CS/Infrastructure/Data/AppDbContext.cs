using Microsoft.EntityFrameworkCore;
using CP2_CS.Domain.Entities;
using CP2_CS.Domain.Enums;

namespace CP2_CS.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<PessoaFisica> PessoasFisicas { get; set; }
    public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
    public DbSet<Agencia> Agencias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<MaquinaDeCartao> MaquinasDeCartao { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
    public DbSet<Contratacao> Contratacoes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agencia>(entity =>
        {
            entity.ToTable("TB_AGENCIA");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Numero).IsRequired();
            entity.Property(e => e.Cidade).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Estado).HasMaxLength(2).IsRequired();
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.HasIndex(e => e.Numero).IsUnique();
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasDiscriminator<string>("Tipo")
                .HasValue<PessoaFisica>("PF")
                .HasValue<PessoaJuridica>("PJ");
            entity.Property(e => e.Nome).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Telefone).HasMaxLength(20).IsRequired();
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.HasOne(e => e.Agencia)
                .WithMany(a => a.Clientes)
                .HasForeignKey(e => e.AgenciaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PessoaFisica>(entity =>
        {
            entity.Property(e => e.Cpf).HasMaxLength(11).IsRequired();
            entity.Property(e => e.DataNascimento).IsRequired();
            entity.HasIndex(e => e.Cpf).IsUnique();
        });

        modelBuilder.Entity<PessoaJuridica>(entity =>
        {
            entity.Property(e => e.Cnpj).HasMaxLength(14).IsRequired();
            entity.Property(e => e.RazaoSocial).HasMaxLength(200).IsRequired();
            entity.HasIndex(e => e.Cnpj).IsUnique();
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasDiscriminator<string>("Tipo")
                .HasValue<MaquinaDeCartao>("MAQUINA_CARTAO")
                .HasValue<Emprestimo>("EMPRESTIMO");
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descricao).HasMaxLength(500).IsRequired();
        });

        modelBuilder.Entity<MaquinaDeCartao>(entity =>
        {
            entity.Property(e => e.TipoPagamento)
                .HasConversion<int>()
                .IsRequired();
            entity.Property(e => e.TaxaMDR).HasColumnType("NUMBER(10,4)").IsRequired();
        });

        modelBuilder.Entity<Emprestimo>(entity =>
        {
            entity.Property(e => e.ValorSolicitado).HasColumnType("NUMBER(15,2)").IsRequired();
            entity.Property(e => e.PrazoMeses).IsRequired();
            entity.Property(e => e.TaxaJurosMensal).HasColumnType("NUMBER(10,4)").IsRequired();
        });

        modelBuilder.Entity<Contratacao>(entity =>
        {
            entity.ToTable("TB_CONTRATACAO");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasConversion<int>().IsRequired();
            entity.Property(e => e.DataSolicitacao).IsRequired();
            entity.Property(e => e.Observacao).HasMaxLength(500);
            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Contratacoes)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Produto)
                .WithMany(p => p.Contratacoes)
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
