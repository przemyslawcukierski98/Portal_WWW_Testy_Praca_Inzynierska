using Microsoft.AspNetCore.Http;
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

        public ExamRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddQuestionToExam(ExamQuestion question)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Exam> FindAll()
        {
            return this.GetDbSet<Exam>();
        }

        public IEnumerable<Exam> FindAssignedExamsForLecturer()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();

            return this.GetDbSet<Exam>().Where(e => e.ExamStatus == "Utworzony")
                .Where(e => e.UserEmail == userEmail.Substring(67).Trim());
        }

        public IEnumerable<Exam> FindResolvedExamsForLecturer()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();

            return this.GetDbSet<Exam>().Where(e => e.ExamStatus == "Rozwiązany")
                .Where(e => e.UserEmail == userEmail.Substring(67).Trim());
        }

        public IEnumerable<Exam> FindEvaluatedExamsForLecturer()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();

            return this.GetDbSet<Exam>().Where(e => e.ExamStatus == "Oceniony")
                .Where(e => e.UserEmail == userEmail.Substring(67).Trim());
        }

        public Exam Read(Guid? ID)
        {
            return this.GetDbSet<Exam>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public void Update(Exam model)
        {
            throw new NotImplementedException();
        }
    }

    public interface IExamRepositories
    {
        Exam Read(Guid? ID);
        Guid Create(Exam model);
        void Delete(Guid? ID);
        void Update(Exam model);
        void AddQuestionToExam(ExamQuestion question);
        IEnumerable<Exam> FindAll();
        IEnumerable<Exam> FindAssignedExamsForLecturer();
        IEnumerable<Exam> FindEvaluatedExamsForLecturer();
        IEnumerable<Exam> FindResolvedExamsForLecturer();
    }
}
