using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_Linea_de_captura_BBVA.Models;

public partial class LpBbvaContext : DbContext
{
    public LpBbvaContext()
    {
    }

    public LpBbvaContext(DbContextOptions<LpBbvaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PeticionesLp> PeticionesLps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=LP_BBVA;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.38-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<PeticionesLp>(entity =>
        {
            entity.HasKey(e => e.IdLp).HasName("PRIMARY");

            entity.ToTable("peticiones_lp");

            entity.Property(e => e.IdLp)
                .HasColumnType("int(11)")
                .HasColumnName("Id_LP");
            entity.Property(e => e.Anio).HasColumnType("int(11)");
            entity.Property(e => e.Dia).HasColumnType("int(11)");
            entity.Property(e => e.Establecimiento).HasColumnType("int(11)");
            entity.Property(e => e.FechaPeticion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Peticion");
            entity.Property(e => e.Firma).HasMaxLength(64);
            entity.Property(e => e.Importe).HasColumnType("double(10,2)");
            entity.Property(e => e.IpCliente)
                .HasMaxLength(20)
                .HasColumnName("Ip_Cliente");
            entity.Property(e => e.LineaCaptura)
                .HasMaxLength(100)
                .HasColumnName("Linea_Captura");
            entity.Property(e => e.Mes).HasColumnType("int(11)");
            entity.Property(e => e.Navegador).HasMaxLength(225);
            entity.Property(e => e.NumeroAleatorio)
                .HasColumnType("int(11)")
                .HasColumnName("Numero_Aleatorio");
            entity.Property(e => e.ReferenciaAdicional)
                .HasMaxLength(16)
                .HasColumnName("Referencia_Adicional");
            entity.Property(e => e.ReferenciaPrincipal)
                .HasMaxLength(30)
                .HasColumnName("Referencia_Principal");
            entity.Property(e => e.TipoPago)
                .HasPrecision(2)
                .HasColumnName("Tipo_Pago");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
