using Microsoft.EntityFrameworkCore;
using CesiZenBackEnd.Core.Entities;

namespace CesiZenBackEnd.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Diagnostic> Diagnostics { get; set; } = null!;
        public DbSet<EvenementStress> EvenementsStress { get; set; } = null!;
        public DbSet<Posseder> Posseder { get; set; } = null!;
        public DbSet<PageInformation> PageInformations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Utilisateur
            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("utilisateur");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id_utilisateur");
                entity.Property(e => e.Nom).HasColumnName("nom").HasMaxLength(100);
                entity.Property(e => e.Prenom).HasColumnName("prenom").HasMaxLength(100);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(150);
                entity.Property(e => e.Active).HasColumnName("est_actif_util");
                entity.Property(e => e.PasswordHash).HasColumnName("mot_de_passe").HasMaxLength(255);
                entity.Property(e => e.DateCreation).HasColumnName("date_creation");
                entity.Property(e => e.RoleId).HasColumnName("id_role");

                entity.HasOne(e => e.Role)
                      .WithMany(r => r.Utilisateurs)
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Table Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id_role");
                entity.Property(e => e.libelRole).HasColumnName("libel_role").HasMaxLength(50);
            });

            // Table Diagnostic
            modelBuilder.Entity<Diagnostic>(entity =>
            {
                entity.ToTable("diagnostic");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id_diagnostic");
                entity.Property(e => e.DateDiagnostic).HasColumnName("date_diagnostic");
                entity.Property(e => e.ScoreTotal).HasColumnName("score_total");
                entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");

                entity.HasOne(d => d.Utilisateur)
                      .WithMany(u => u.Diagnostics)
                      .HasForeignKey(d => d.IdUtilisateur)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Table EvenementStress
            modelBuilder.Entity<EvenementStress>(entity =>
            {
                entity.ToTable("evenementstress");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id_evenement");
                entity.Property(e => e.LibelEvenement).HasColumnName("libel_evenement").HasMaxLength(255);
                entity.Property(e => e.Score).HasColumnName("score");
            });

            // Table Posseder (liaison Diagnostic <-> EvenementStress)
            modelBuilder.Entity<Posseder>(entity =>
            {
                entity.ToTable("posseder");
                entity.HasKey(e => new { e.IdDiagnostic, e.IdEvenement });

                entity.Property(e => e.IdDiagnostic).HasColumnName("id_diagnostic");
                entity.Property(e => e.IdEvenement).HasColumnName("id_evenement");

                entity.HasOne(p => p.Diagnostic)
                      .WithMany(d => d.Possessions)
                      .HasForeignKey(p => p.IdDiagnostic);

                entity.HasOne(p => p.EvenementStress)
                      .WithMany(e => e.Possessions)
                      .HasForeignKey(p => p.IdEvenement);
            });

            // Table PageInformation
            modelBuilder.Entity<PageInformation>(entity =>
            {
                entity.ToTable("page_information");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id_page");
                entity.Property(e => e.Titre).HasColumnName("titre").HasMaxLength(255);
                entity.Property(e => e.Contenu).HasColumnName("contenu");
            });
        }
    }
}
