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

        // zapis rozwiązanego egzaminu do tabeli w bazie danych
        public Guid SaveExamSolution(StudentTestSolution model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();

            model.StudentEmail = userEmail.Substring(67).Trim();
            this.GetDbSet<StudentTestSolution>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }
    }

    public interface IStudentTestSolutionRepositories
    {
        Guid SaveExamSolution(StudentTestSolution model);
    }
}
