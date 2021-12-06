﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentAndGroup> StudentsAndGroups { get; set; }
        public DbSet<StudentTestSolution> StudentTestSolutions { get; set; }
        public DbSet<StudentTestResult> StudentTestResults { get; set; }

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

        private void SetStudentsAndGroups(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAndGroup>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        private void SetStudentsTestSolutions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentTestSolution>().Property(e => e.ID).ValueGeneratedOnAdd();
        }

        private void SetStudentsTestResults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentTestResult>().Property(e => e.ID).ValueGeneratedOnAdd();
        }


        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
