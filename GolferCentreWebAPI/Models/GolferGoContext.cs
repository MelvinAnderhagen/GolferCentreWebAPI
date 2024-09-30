using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GolferCentreWebAPI.Models;

public partial class GolferGoContext : DbContext
{
    public GolferGoContext()
    {
    }

    public GolferGoContext(DbContextOptions<GolferGoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Golfer> Golfers { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AUD-R90YPCMA\\SQLEXPRESS;Initial Catalog=golferGo;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D7187211242A1");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(200);
            entity.Property(e => e.Location).HasMaxLength(200);
        });

        modelBuilder.Entity<Golfer>(entity =>
        {
            entity.HasKey(e => e.GolferId).HasName("PK__Golfer__DB1FA05C294F7370");

            entity.ToTable("Golfer");

            entity.Property(e => e.GolferId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GolferID");
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.GolferImage).HasMaxLength(255);
            entity.Property(e => e.Handicap).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__Score__7DD229F18A19048B");

            entity.ToTable("Score");

            entity.Property(e => e.ScoreId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ScoreID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.GolferId).HasColumnName("GolferID");
            entity.Property(e => e.Score1).HasColumnName("Score");
            entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Scores)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Score");

            entity.HasOne(d => d.Golfer).WithMany(p => p.Scores)
                .HasForeignKey(d => d.GolferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Golfer_Score");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Scores)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tournament_Score");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.TournamentId).HasName("PK__Tourname__AC631333EF9B05D6");

            entity.ToTable("Tournament");

            entity.Property(e => e.TournamentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TournamentID");
            entity.Property(e => e.TournamentName).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACFC28008C");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4A2B52848").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053414FABF26").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
