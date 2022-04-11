using lab3.model;
using Microsoft.EntityFrameworkCore;

namespace lab3.db
{
    public partial class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Diagnosis> Diagnoses { get; set; } = null!;
        public virtual DbSet<Hospitalization> Hospitalizations { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql($"server=localhost;uid=root;pwd=87654321;database=hospital", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasKey(e => e.IdDiagnosis)
                    .HasName("PRIMARY");

                entity.ToTable("diagnoses");

                entity.HasIndex(e => e.Name, "name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdDiagnosis).HasColumnName("id_diagnosis");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Hospitalization>(entity =>
            {
                entity.HasKey(e => e.IdHospitalizations)
                    .HasName("PRIMARY");

                entity.ToTable("hospitalizations");

                entity.HasIndex(e => e.FkDiagnosis, "fk_hospitalizations_diagnoses_idx");

                entity.HasIndex(e => e.FkPatient, "fk_hospitalizations_patients_idx");

                entity.Property(e => e.IdHospitalizations).HasColumnName("id_hospitalizations");

                entity.Property(e => e.AdmissionDate).HasColumnName("admission_date");

                entity.Property(e => e.DischargeDate).HasColumnName("discharge_date");

                entity.Property(e => e.FkDiagnosis).HasColumnName("fk_diagnosis");

                entity.Property(e => e.FkPatient).HasColumnName("fk_patient");

                entity.HasOne(d => d.FkDiagnosisNavigation)
                    .WithMany(p => p.Hospitalizations)
                    .HasForeignKey(d => d.FkDiagnosis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_hospitalizations_diagnoses");

                entity.HasOne(d => d.FkPatientNavigation)
                    .WithMany(p => p.Hospitalizations)
                    .HasForeignKey(d => d.FkPatient)
                    .HasConstraintName("fk_hospitalizations_patients");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatients)
                    .HasName("PRIMARY");

                entity.ToTable("patients");

                entity.Property(e => e.IdPatients).HasColumnName("id_patients");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(45)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Surname)
                    .HasMaxLength(45)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
