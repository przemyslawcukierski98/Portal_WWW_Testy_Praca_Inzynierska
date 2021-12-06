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
    public class StudentTestSolutionRepositories : BaseRepositoryEF, IStudentTestSolutionRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentTestSolutionRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid SaveExamSolution(StudentTestSolution model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim(); 

            model.StudentEmail = userEmail;
            this.GetDbSet<StudentTestSolution>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public IEnumerable<StudentTestSolution> FindResolvedExams()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            return this.GetDbSet<StudentTestSolution>().Where(e => e.LecturerEmail == userEmail)
                .Where(e => e.IsEvaluated == false);
        }

        public StudentTestSolution GetSolution(Guid? ID)
        {
            return this.GetDbSet<StudentTestSolution>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }
    }

    public interface IStudentTestSolutionRepositories
    {
        Guid SaveExamSolution(StudentTestSolution model);
        StudentTestSolution GetSolution(Guid? ID);
        IEnumerable<StudentTestSolution> FindResolvedExams();
    }
}
