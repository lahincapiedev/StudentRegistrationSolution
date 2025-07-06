using Microsoft.EntityFrameworkCore;
using StudentRegistration.Core.Entities;

namespace StudentRegistration.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<Profesor> Profesores => Set<Profesor>();
        public DbSet<Materia> Materias => Set<Materia>();
        public DbSet<EstudianteMateria> EstudianteMaterias => Set<EstudianteMateria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EstudianteMateria>()
                .HasIndex(em => new { em.EstudianteId, em.MateriaId })
                .IsUnique();

            modelBuilder.Entity<EstudianteMateria>()
                .HasOne(em => em.Estudiante)
                .WithMany(e => e.EstudianteMaterias)
                .HasForeignKey(em => em.EstudianteId);

            modelBuilder.Entity<EstudianteMateria>()
                .HasOne(em => em.Materia)
                .WithMany(m => m.EstudianteMaterias)
                .HasForeignKey(em => em.MateriaId);
        }
    }
}