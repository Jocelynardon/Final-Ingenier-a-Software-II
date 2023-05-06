using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIFinalSoftwareII.Models;

public partial class FinalSoftwareContext : DbContext
{
    public FinalSoftwareContext()
    {
    }

    public FinalSoftwareContext(DbContextOptions<FinalSoftwareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Voto> Votos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;userid=root;password=#JED100pre;database=finalSoftware;TreatTinyAsBoolean=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuario1).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.Usuario1)
                .HasMaxLength(500)
                .HasColumnName("usuario");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(275)
                .HasColumnName("apellidos");
            entity.Property(e => e.Genero)
                .HasMaxLength(100)
                .HasColumnName("genero");
            entity.Property(e => e.Nombres)
                .HasMaxLength(275)
                .HasColumnName("nombres");
            entity.Property(e => e.Partido)
                .HasMaxLength(300)
                .HasColumnName("partido");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RealizoVoto)
                .HasColumnType("tinyint(1)")
                .HasColumnName("realizo_voto");
            entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");
        });

        modelBuilder.Entity<Voto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("voto");

            entity.HasIndex(e => e.Usuario, "usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaVoto)
                .HasColumnType("datetime")
                .HasColumnName("fecha_voto");
            entity.Property(e => e.Hora)
                .HasMaxLength(50)
                .HasColumnName("hora");
            entity.Property(e => e.Ip)
                .HasMaxLength(100)
                .HasColumnName("ip");
            entity.Property(e => e.Partido)
                .HasMaxLength(300)
                .HasColumnName("partido");
            entity.Property(e => e.Usuario)
                .HasMaxLength(500)
                .HasColumnName("usuario");
            entity.Property(e => e.VotosFraude).HasColumnName("votos_fraude");
            entity.Property(e => e.VotosValidosTotales).HasColumnName("votos_validos_totales");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Votos)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voto_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
