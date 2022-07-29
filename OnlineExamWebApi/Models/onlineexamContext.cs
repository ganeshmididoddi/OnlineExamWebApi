using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineExamWebApi.Models
{
    public partial class onlineexamContext : DbContext
    {
        public onlineexamContext()
        {
        }

        public onlineexamContext(DbContextOptions<onlineexamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Attempt> Attempts { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questionsadvanced> Questionsadvanceds { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-QNA6DVP;database=onlineexam;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Attempt>(entity =>
            {
                entity.Property(e => e.AttemptId).HasColumnName("attempt_id");

                entity.Property(e => e.LOneMarks).HasColumnName("L_one_marks");

                entity.Property(e => e.LThreeMarks).HasColumnName("L_three_marks");

                entity.Property(e => e.LTwoMarks).HasColumnName("L_two_marks");

                entity.Property(e => e.LevelCleared).HasColumnName("level_cleared");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Attempts)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attempts__test_i__3F466844");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attempts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attempts__user_i__3E52440B");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.LevelId).HasColumnName("level_id");

                entity.Property(e => e.OptionsCorrect)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_correct");

                entity.Property(e => e.OptionsFour)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_four");

                entity.Property(e => e.OptionsOne)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_one");

                entity.Property(e => e.OptionsThree)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_three");

                entity.Property(e => e.OptionsTwo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_two");

                entity.Property(e => e.Question1)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("question");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__test___44FF419A");
            });

            modelBuilder.Entity<Questionsadvanced>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Question__2EC2154997C5E28B");

                entity.ToTable("Questionsadvanced");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.IsSingleCorrect).HasColumnName("is_single_correct");

                entity.Property(e => e.LevelId).HasColumnName("level_id");

                entity.Property(e => e.OptionsCorrect)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_correct");

                entity.Property(e => e.OptionsFour)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_four");

                entity.Property(e => e.OptionsOne)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_one");

                entity.Property(e => e.OptionsThree)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_three");

                entity.Property(e => e.OptionsTwo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("options_two");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("question");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questionsadvanceds)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Questions__test___47DBAE45");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.LOneReq).HasColumnName("L_one_req");

                entity.Property(e => e.LThreeReq).HasColumnName("L_three_req");

                entity.Property(e => e.LTwoReq).HasColumnName("L_two_req");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("subject_name");

                entity.Property(e => e.TestDate)
                    .HasColumnType("date")
                    .HasColumnName("test_date");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__admin_id__3B75D760");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164B57EE59F")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.College)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("college");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("qualification");

                entity.Property(e => e.Verified).HasColumnName("verified");

                entity.Property(e => e.YearOfPassing).HasColumnName("year_of_passing");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
