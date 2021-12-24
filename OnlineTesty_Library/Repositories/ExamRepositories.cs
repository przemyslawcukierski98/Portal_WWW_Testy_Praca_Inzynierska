﻿using Microsoft.AspNetCore.Http;
using OnlineTesty_Library.Contexts;
using OnlineTesty_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Repositories
{
    public class ExamRepositories : BaseRepositoryEF, IExamRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentAndGroupRepositories _studentAndGroupRepositories;

        public ExamRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            IStudentAndGroupRepositories studentAndGroupRepositories) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _studentAndGroupRepositories = studentAndGroupRepositories;
        }

        public Guid Create(Exam model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();

            this.GetDbSet<Exam>().Add(model);
            model.UserEmail = userEmail.Substring(67).Trim();
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<Exam>().Remove(remove);
            this.UnitOfWork.SaveChanges();
        }

        // listy egzaminów dla wykładowcy i studenta
        public IEnumerable<Exam> FindAssignedExams(string titleFilter, string groupFilter)
        {
            IQueryable<Exam> assignedExams;
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            if(titleFilter != null && groupFilter != null)
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail).Where(e => e.Name.Contains(titleFilter)).Where(e => e.StudentGroupName.Contains(groupFilter));
            }
            else if(titleFilter != null && groupFilter == null)
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail).Where(e => e.Name.Contains(titleFilter));
            }
            else if(titleFilter == null && groupFilter != null)
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail).Where(e => e.StudentGroupName.Contains(groupFilter));
            }
            else
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail);
            }

            return assignedExams;
        }

        public IEnumerable<Exam> FindAssignedExamsForStudent()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();
            var userDetails = _studentAndGroupRepositories.Read(userEmail);
            List<Exam> emptylist = new List<Exam>();

           if (userDetails != null)
           {
                return this.GetDbSet<Exam>().Where(e => e.StudentGroupName == userDetails.StudentGroupName);
           }
           else
           {
                return emptylist;
           } 
        }

        public Exam Read(Guid? ID)
        {
            return this.GetDbSet<Exam>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public string GetExamName(Guid? ID)
        {
            Exam exam = this.GetDbSet<Exam>().Where(e => e.ID == ID).FirstOrDefault();
            return exam.Name;
        }
    }

    public interface IExamRepositories
    {
        Exam Read(Guid? ID);
        Guid Create(Exam model);
        string GetExamName(Guid? ID);
        void Delete(Guid? ID);
        IEnumerable<Exam> FindAssignedExams(string nameFilter, string groupFilter);
        IEnumerable<Exam> FindAssignedExamsForStudent();
    }
}
