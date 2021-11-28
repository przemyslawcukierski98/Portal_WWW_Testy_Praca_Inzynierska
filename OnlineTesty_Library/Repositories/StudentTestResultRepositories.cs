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
    public class StudentTestResultRepositories : BaseRepositoryEF, IStudentTestResultRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentTestResultRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            return this.GetDbSet<StudentTestResult>().Where(e => e.StudentEmail == userEmail);
        }

        public StudentTestResult Read(Guid? ID)
        {
            return this.GetDbSet<StudentTestResult>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public Guid SaveExamResult(StudentTestResult model)
        {
            this.GetDbSet<StudentTestResult>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }
    }

    public interface IStudentTestResultRepositories
    {
        StudentTestResult Read(Guid? ID);
        Guid SaveExamResult(StudentTestResult model);
        IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent();
    }
}
