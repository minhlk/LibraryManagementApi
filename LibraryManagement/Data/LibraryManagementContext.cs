using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public partial class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext()
        {
        }

        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookGenre> BookGenre { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBook> UserBook { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.YearOfBirth)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.IdAuthor)
                    .HasConstraintName("FK_Book_Author");
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.BookGenre)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("FK_BookGenre_Book");

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany(p => p.BookGenre)
                    .HasForeignKey(d => d.IdGenre)
                    .HasConstraintName("FK_BookGenre_Genre");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasMaxLength(150);

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.YearOfBirth)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<UserBook>(entity =>
            {
                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.UserBook)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("FK_UserBook_Book");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserBook)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_UserBook_User");
            });
        }
    }
}
