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
        private readonly IStudentAndGroupRepositories _studentAndGroupRepositories;
        private readonly ILecturerProfileDetailsRepositories _lecturerProfileDetailsRepositories;
        
        public StudentTestResultRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            IStudentAndGroupRepositories studentAndGroupRepositories,
            ILecturerProfileDetailsRepositories lecturerProfileDetailsRepositories) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _studentAndGroupRepositories = studentAndGroupRepositories;
            _lecturerProfileDetailsRepositories = lecturerProfileDetailsRepositories;
        }

        public IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent(string titleFilter, string lecturerFilter)
        {
            IQueryable<StudentTestResult> evaluatedExams = Enumerable.Empty<StudentTestResult>().AsQueryable();
            LecturerProfileDetails lecturerProfileDetails = new LecturerProfileDetails();
            string studentEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();
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
            string lecturerEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

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
        IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent(string titleFilter, string lecturerFilter);
        IEnumerable<StudentTestResult> FindEvaluatedExamsForLecturer(string titleFilter, string studentFilter);
    }
}
