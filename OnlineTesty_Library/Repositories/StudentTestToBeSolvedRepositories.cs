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
    public class StudentTestToBeSolvedRepositories : BaseRepositoryEF, IStudentTestToBeSolvedRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentTestToBeSolvedRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<StudentTestToBeSolved> FindSolutionsForStudent(string userEmail)
        {
            return this.GetDbSet<StudentTestToBeSolved>().Where(e => e.StudentEmail == userEmail);
        }

        public Guid SaveInfoAboutSolution(StudentTestToBeSolved model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            model.StudentEmail = userEmail;
            this.GetDbSet<StudentTestToBeSolved>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }
    }

    public interface IStudentTestToBeSolvedRepositories
    {
        Guid SaveInfoAboutSolution(StudentTestToBeSolved model);
        IEnumerable<StudentTestToBeSolved> FindSolutionsForStudent(string userEmail);
    }
}
