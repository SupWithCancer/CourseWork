using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class FilmLibraryContext : DbContext
{
    public FilmLibraryContext() : base() {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public FilmLibraryContext(DbContextOptions options) : base(options)
    {


        //Database.EnsureCreated();
    }

    public virtual DbSet<Genre> Genre { get; set; }

    public virtual DbSet<GenreFilm> GenreFilm { get; set; }

    public virtual DbSet<Mark> Mark { get; set; }

    public virtual DbSet<Person> Person { get; set; }

    public virtual DbSet<Comment> Comment { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Film> Film { get; set; }


    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<FilmPerson> FilmPerson { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FilmLibDB;Username=postgres;Password=123");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genre");

            entity.Property(e => e.Id)
                  .HasColumnName("genre_id");

            entity.Property(e => e.Name)
                  .HasColumnName("name")
                  .HasMaxLength(50)
                  .IsUnicode(true)
                  .IsRequired();
            entity.HasIndex(e => e.Name)
                  .IsUnique();

            entity.Property(e => e.Description)
                  .HasColumnName("description")
                  .IsUnicode(true);

            entity.Property(e => e.IsDeleted)
                  .HasColumnName("is_deleted");

            entity.HasMany(g => g.GenreFilmPairs)
                  .WithOne(gf => gf.Genre);
        });

        modelBuilder.Entity<GenreFilm>(entity =>
        {
            entity.ToTable("genre_film");

            entity.HasOne(gf => gf.Genre)
                  .WithMany(g => g.GenreFilmPairs)
                  .HasForeignKey(id => id.GenreId);

            entity.HasOne(gf => gf.Film)
                  .WithMany(g => g.GenreFilmPairs)
                  .HasForeignKey(id => id.FilmId);

            entity.HasKey(id => new { id.GenreId, id.FilmId });
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.ToTable("mark");

            entity.Property(e => e.Id)
                  .HasColumnName("mark_id");

            entity.Property(e => e.Grade)
                  .HasColumnName("grade")
                  .IsRequired();

            entity.HasOne(m => m.Film)
                  .WithMany(f => f.Marks)
                  .HasForeignKey(id => id.FilmId);


            entity.HasOne(m => m.User)
                  .WithMany(u => u.Marks)
                  .HasForeignKey(id => id.UserId);

        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("person");

            entity.Property(e => e.Id)
                .HasColumnName("person_id");

            entity.Property(e => e.Name)
                 .HasColumnName("name")
                 .HasMaxLength(50)
                 .IsUnicode(true)
                 .IsRequired();
            entity.HasIndex(e => e.Name)
                  .IsUnique();

            entity.Property(e => e.Age)
                  .HasColumnName("age");

            entity.Property(e => e.ImagePath)
                 .HasColumnName("image_path")
                 .IsUnicode(true);


            entity.Property(e => e.Description)
                 .HasColumnName("description")
                 .IsUnicode(true);


            entity.HasMany(p => p.FilmPersonPairs)
                  .WithOne(fp => fp.Person);
        });


        modelBuilder.Entity<FilmPerson>(entity =>
        {
            entity.ToTable("film_person");

            entity.HasOne(fp => fp.Person)
                  .WithMany(p => p.FilmPersonPairs)
                  .HasForeignKey(id => id.PersonId);

            entity.HasOne(fp => fp.Film)
                  .WithMany(f => f.FilmPersonPairs)
                  .HasForeignKey(id => id.FilmId);

            entity.HasOne(fp => fp.Role)
                 .WithMany(r => r.FilmPersonPairs)
                 .HasForeignKey(id => id.RoleId);

            entity.HasKey(id => new { id.PersonId, id.FilmId, id.RoleId });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnName("role_id");

            entity.Property(e => e.Title)
                 .HasColumnName("title")
                 .HasMaxLength(50)
                 .IsUnicode(true)
                 .IsRequired();
            entity.HasIndex(e => e.Title)
                  .IsUnique();

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .IsUnicode(true);


            entity.HasMany(r => r.FilmPersonPairs)
                  .WithOne(fp => fp.Role);
        });



        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("comment");

            entity.Property(e => e.Id)
                  .HasColumnName("comment_id");

            entity.Property(e => e.Body)
              .HasColumnName("body")
              .IsUnicode(true);

            entity.HasOne(c => c.User)
                  .WithMany(u => u.Comments)
                  .HasForeignKey(id => id.UserId);


            entity.HasOne(c => c.Film)
                  .WithMany(f => f.Comments)
                  .HasForeignKey(id => id.FilmId);

        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id)
                  .HasColumnName("user_id");

            entity.Property(e => e.Email)
                  .HasColumnName("email")
                  .HasMaxLength(254)
                  .IsUnicode(true)
                  .IsRequired();
            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.Property(e => e.PasswordHash)
                  .HasColumnName("password_hash");

            entity.Property(e => e.PasswordSalt)
                  .HasColumnName("password_salt");

            entity.Property(e => e.Name)
                  .HasColumnName("first_name")
                  .HasMaxLength(50)
                  .IsUnicode(true)
                  .IsRequired();

            

         

            entity.HasMany(u => u.Comments)
                  .WithOne(cm => cm.User);

            entity.HasMany(u => u.Marks)
                  .WithOne(m => m.User);
        });


        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("film");

            entity.Property(e => e.Id)
                  .HasColumnName("film_id");

            entity.Property(e => e.Name)
                  .HasColumnName("name")
                  .HasMaxLength(50)
                  .IsUnicode(true)
                  .IsRequired();
            entity.HasIndex(e => e.Name)
                  .IsUnique();

            entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(true);

            entity.Property(e => e.IsDeleted)
                  .HasColumnName("is_deleted");

            entity.Property(e => e.Year)
                 .HasColumnName("year")
                 .IsRequired();

            entity.Property(e => e.Rank)
               .HasColumnName("rank");

            entity.Property(e => e.Theme)
              .HasColumnName("theme")
              .IsUnicode(true);

            entity.Property(e => e.ImagePath)
                .HasColumnName("image_path")
                .IsUnicode(true);

            entity.HasMany(f => f.GenreFilmPairs)
                 .WithOne(gf => gf.Film);

            entity.HasMany(f => f.Comments)
                  .WithOne(cm => cm.Film);

            entity.HasMany(f => f.Marks)
                  .WithOne(m => m.Film);

            entity.HasMany(f => f.FilmPersonPairs)
                 .WithOne(fp => fp.Film);


        });

   
    }

    private static void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
}