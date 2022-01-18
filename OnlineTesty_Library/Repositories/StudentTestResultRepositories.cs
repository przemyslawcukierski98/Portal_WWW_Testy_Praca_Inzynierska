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
    public class StudentTestResultRepositories : BaseRepositoryEF, IStudentTestResultRepositories
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IStudentAndGroupRepositories _studentAndGroupRepositories;
        private readonly ILecturerProfileDetailsRepositories _lecturerProfileDetailsRepositories;
        
        public StudentTestResultRepositories(IUnitOfWork unitOfWork, AuthenticationStateProvider authenticationStateProvider,
            IStudentAndGroupRepositories studentAndGroupRepositories,
            ILecturerProfileDetailsRepositories lecturerProfileDetailsRepositories) : base(unitOfWork)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _studentAndGroupRepositories = studentAndGroupRepositories;
            _lecturerProfileDetailsRepositories = lecturerProfileDetailsRepositories;
        }

        public IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent(string titleFilter, string lecturerFilter)
        {
            IQueryable<StudentTestResult> evaluatedExams = Enumerable.Empty<StudentTestResult>().AsQueryable();
            LecturerProfileDetails lecturerProfileDetails = new LecturerProfileDetails();
            string studentEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;
            string lecturerEmail = string.Empty;

            if (titleFilter == null) titleFilter = string.Empty;
            if (lecturerFilter == null) lecturerFilter = string.Empty;

            lecturerProfileDetails = _lecturerProfileDetailsRepositories.ReadByLastName(lecturerFilter);

            if (lecturerProfileDetails != null)
            {
                lecturerEmail = lecturerProfileDetails.EmailAddress;
            }

            // walidacja
            ValidationObject validation = ValidationMethods.ValidationForExams(titleFilter, lecturerFilter, null);

            if (validation.TitleFilterIsFilled && validation.StudentFilterIsFilled)
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter))
                .Where(e => e.StudentEmail == studentEmail);
            }
            else if (validation.TitleFilterIsFilled && validation.StudentFilterIsNullOrEmpty)
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.StudentEmail == studentEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter));
            }
            else if (validation.TitleFilterIsNullOrEmpty && validation.StudentFilterIsFilled)
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.StudentEmail == studentEmail);
            }

            return evaluatedExams;
        }

        public IEnumerable<StudentTestResult> FindEvaluatedExamsForLecturer(string titleFilter, string studentFilter)
        {
            IQueryable<StudentTestResult> evaluatedExams = Enumerable.Empty<StudentTestResult>().AsQueryable();
            StudentAndGroup studentAndGroup = new StudentAndGroup();
            string studentEmail = string.Empty;
            string lecturerEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            if (titleFilter == null) titleFilter = string.Empty;
            if (studentFilter == null) studentFilter = string.Empty;

            studentAndGroup = _studentAndGroupRepositories.ReadByLastName(studentFilter);

            if (studentAndGroup != null)
            {
                studentEmail = studentAndGroup.EmailAddress;
            }

            // walidacja
            ValidationObject validation = ValidationMethods.ValidationForExams(titleFilter, studentFilter, null);

            if (validation.TitleFilterIsFilled && validation.StudentFilterIsFilled)
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter))
                .Where(e => e.StudentEmail == studentEmail);
            }
            else if(validation.TitleFilterIsFilled && validation.StudentFilterIsNullOrEmpty)
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter));
            }
            else if(validation.TitleFilterIsNullOrEmpty && validation.StudentFilterIsFilled)
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.StudentEmail == studentEmail);
            }

            return evaluatedExams;
        }

        public bool IsThereResult(Guid? ID, string userEmail)
        {
            var results = this.GetDbSet<StudentTestResult>().Where(e => e.StudentEmail == userEmail)
                .Where(e => e.ExamId == ID);

            if (results.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        bool IsThereResult(Guid? ID, string userEmail);
        IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent(string titleFilter, string lecturerFilter);
        IEnumerable<StudentTestResult> FindEvaluatedExamsForLecturer(string titleFilter, string studentFilter);
    }
}
