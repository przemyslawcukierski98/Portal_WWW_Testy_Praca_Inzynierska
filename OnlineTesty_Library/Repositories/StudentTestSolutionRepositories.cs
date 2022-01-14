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
    public class StudentTestSolutionRepositories : BaseRepositoryEF, IStudentTestSolutionRepositories
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IStudentAndGroupRepositories _studentAndGroupRepositories;

        public StudentTestSolutionRepositories(IUnitOfWork unitOfWork, AuthenticationStateProvider authenticationStateProvider,
            IStudentAndGroupRepositories studentAndGroupRepositories) : base(unitOfWork)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _studentAndGroupRepositories = studentAndGroupRepositories;
        }

        public Guid SaveExamSolution(StudentTestSolution model)
        {
            string userEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            model.StudentEmail = userEmail;
            this.GetDbSet<StudentTestSolution>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public IEnumerable<StudentTestSolution> FindResolvedExamsForLecturer(string titleFilter, string groupFilter, string studentFilter)
        {
            IQueryable<StudentTestSolution> resolvedExams = Enumerable.Empty<StudentTestSolution>().AsQueryable();
            StudentAndGroup studentAndGroup = new StudentAndGroup();
            string userEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;
            string studentEmail = string.Empty;
            string lecturerEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            if (titleFilter == null) titleFilter = string.Empty;
            if (studentFilter == null) studentFilter = string.Empty;
            if (groupFilter == null) groupFilter = string.Empty;

            studentAndGroup = _studentAndGroupRepositories.ReadByLastName(studentFilter);

            if (studentAndGroup != null)
            {
                studentEmail = studentAndGroup.EmailAddress;
            }

            // ten warunek nie może tak wyglądać - zrobić metodę w tej klasie lub klase statyczną?
            ValidationObject validation = ValidationMethods.ValidationForExams(titleFilter, studentFilter, groupFilter);

            if ((validation.TitleFilterIsFilled) && (validation.StudentFilterIsFilled))
            {
                resolvedExams = this.GetDbSet<StudentTestSolution>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter))
                .Where(e => e.StudentEmail == studentEmail);
            }
            else if ((validation.TitleFilterIsFilled) && (validation.StudentFilterIsNullOrEmpty))
            {
                resolvedExams = this.GetDbSet<StudentTestSolution>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter));
            }
            else if ((validation.TitleFilterIsNullOrEmpty) && (validation.StudentFilterIsFilled))
            {
                resolvedExams = this.GetDbSet<StudentTestSolution>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.StudentEmail == studentEmail);
            }

            return resolvedExams;
        }

        public StudentTestSolution GetSolution(Guid? ID)
        {
            return this.GetDbSet<StudentTestSolution>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public bool IsThereSolution(Guid? ID, string userEmail)
        {
            var solutions = this.GetDbSet<StudentTestSolution>().Where(e => e.StudentEmail == userEmail);

            if (solutions.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public interface IStudentTestSolutionRepositories
    {
        Guid SaveExamSolution(StudentTestSolution model);
        StudentTestSolution GetSolution(Guid? ID);
        IEnumerable<StudentTestSolution> FindResolvedExamsForLecturer(string titleFilter, string groupFilter, string studentFilter);
        bool IsThereSolution(Guid? ID, string userEmail);
    }
}
