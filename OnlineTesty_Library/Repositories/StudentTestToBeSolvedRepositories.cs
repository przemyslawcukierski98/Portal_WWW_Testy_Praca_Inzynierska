using Microsoft.AspNetCore.Components.Authorization;
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
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public StudentTestToBeSolvedRepositories(IUnitOfWork unitOfWork, AuthenticationStateProvider authenticationStateProvider) : base(unitOfWork)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public IEnumerable<StudentTestToBeSolved> FindSolutionsForStudent()
        {
            string userEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            return this.GetDbSet<StudentTestToBeSolved>().Where(e => e.StudentEmail == userEmail);
        }

        public Guid SaveInfoAboutSolution(StudentTestToBeSolved model)
        {
            string userEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            model.StudentEmail = userEmail;
            this.GetDbSet<StudentTestToBeSolved>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }
    }

    public interface IStudentTestToBeSolvedRepositories
    {
        Guid SaveInfoAboutSolution(StudentTestToBeSolved model);
        IEnumerable<StudentTestToBeSolved> FindSolutionsForStudent();
    }
}
