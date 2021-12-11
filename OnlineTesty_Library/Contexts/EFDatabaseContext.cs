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

        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<LecturerProfileDetails> LecturerProfileDetails { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentAndGroup> StudentsAndGroups { get; set; }
        public DbSet<StudentTestSolution> StudentTestSolutions { get; set; }
        public DbSet<StudentTestResult> StudentTestResults { get; set; }

        public DbSet<StudentTestToBeSolved> StudentTestToBeSolved { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetExams(modelBuilder);
        }

        private void SetExams(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
