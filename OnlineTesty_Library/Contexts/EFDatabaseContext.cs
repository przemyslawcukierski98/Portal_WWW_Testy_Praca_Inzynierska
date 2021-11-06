using Microsoft.EntityFrameworkCore;
using OnlineTesty_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Contexts
{
    public class EFDatabaseContext : DbContext, IUnitOfWork
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetExams(modelBuilder);
        }

        private void SetExams(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        private void SetExamQuestions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamQuestion>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        private void SetStudentGroups(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
