using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVC.Models;

#nullable disable

namespace MVC.Context
{
    public partial class BancoContext : DbContext
    {
        public BancoContext()
        {
        }

        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Contum> Conta { get; set; }
        public virtual DbSet<TipoCli> TipoClis { get; set; }
        public virtual DbSet<TipoContum> TipoConta { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=DESKTOP-8HAU83C\\SQLEXPRESS;database=Banco;integrated security=yes;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodCli)
                    .HasName("PK_Cli");

                entity.ToTable("Cliente");

                entity.Property(e => e.CodCli).HasColumnName("Cod_Cli");

                entity.Property(e => e.CodTipoCli).HasColumnName("Cod_TipoCli");

                entity.Property(e => e.DataNascFund)
                    .HasColumnType("datetime")
                    .HasColumnName("Data_NascFund")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Documento)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RendaLucro)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Renda_Lucro");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('M')")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoEmpresa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodTipoCliNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.CodTipoCli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cli");
            });

            modelBuilder.Entity<Contum>(entity =>
            {
                entity.HasKey(e => e.CodConta)
                    .HasName("PK_Cta");

                entity.Property(e => e.CodConta).HasColumnName("Cod_Conta");

                entity.Property(e => e.Agencia)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CodCli).HasColumnName("Cod_Cli");

                entity.Property(e => e.CodTipoCta).HasColumnName("Cod_TipoCta");

                entity.Property(e => e.CodigoBanco)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Codigo_Banco");

                entity.Property(e => e.NumeroConta)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Numero_Conta");

                entity.Property(e => e.SaldoAtual)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("Saldo_Atual");

                entity.Property(e => e.SaldoInicial)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("Saldo_Inicial");

                entity.HasOne(d => d.CodCliNavigation)
                    .WithMany(p => p.Conta)
                    .HasForeignKey(d => d.CodCli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cli1");

                entity.HasOne(d => d.CodTipoCtaNavigation)
                    .WithMany(p => p.Conta)
                    .HasForeignKey(d => d.CodTipoCta)
                    .HasConstraintName("FK_Tp_Cta");
            });

            modelBuilder.Entity<TipoCli>(entity =>
            {
                entity.HasKey(e => e.CodTipoCli);

                entity.ToTable("TipoCli");

                entity.HasIndex(e => e.NomeTipoCli, "UQ_TipoCli")
                    .IsUnique();

                entity.Property(e => e.CodTipoCli).HasColumnName("Cod_TipoCli");

                entity.Property(e => e.NomeTipoCli)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Nome_TipoCli")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TipoContum>(entity =>
            {
                entity.HasKey(e => e.CodTipoCta)
                    .HasName("PK_TipoCta");

                entity.HasIndex(e => e.NomeTipoCta, "UQ_TipoCta")
                    .IsUnique();

                entity.Property(e => e.CodTipoCta).HasColumnName("Cod_TipoCta");

                entity.Property(e => e.NomeTipoCta)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nome_TipoCta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
