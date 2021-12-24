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

        public StudentTestResultRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            IStudentAndGroupRepositories studentAndGroupRepositories) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _studentAndGroupRepositories = studentAndGroupRepositories;
        }

        public IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent()
        {
            var studentEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            return this.GetDbSet<StudentTestResult>().Where(e => e.StudentEmail == studentEmail);
        }

        public IEnumerable<StudentTestResult> FindEvaluatedExamsForLecturer(string titleFilter, string studentFilter)
        {
            IQueryable<StudentTestResult> evaluatedExams = Enumerable.Empty<StudentTestResult>().AsQueryable();
            StudentAndGroup studentAndGroup = new StudentAndGroup();
            string studentEmail = string.Empty;

            if (titleFilter == null) titleFilter = string.Empty;
            if (studentFilter == null) studentFilter = string.Empty;

            studentAndGroup = _studentAndGroupRepositories.ReadByLastName(studentFilter);

            if (studentAndGroup != null)
            {
                studentEmail = studentAndGroup.EmailAddress;
            }

            var lecturerEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            if ((titleFilter != null && !titleFilter.Equals("")) && (studentFilter != null && !studentFilter.Equals("")))
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter))
                .Where(e => e.StudentEmail == studentEmail);
            }
            else if((titleFilter != null && !titleFilter.Equals("")) && (studentFilter == null || studentFilter.Equals("")))
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.ExamTitle.Contains(titleFilter));
            }
            else if((titleFilter == null || titleFilter.Equals("")) && (studentFilter != null && !studentFilter.Equals("")))
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail)
                .Where(e => e.StudentEmail == studentEmail);
            }
            else
            {
                evaluatedExams = this.GetDbSet<StudentTestResult>().Where(e => e.LecturerEmail == lecturerEmail);
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
        IEnumerable<StudentTestResult> FindEvaluatedExamsForStudent();
        IEnumerable<StudentTestResult> FindEvaluatedExamsForLecturer(string titleFilter, string studentFilter);
    }
}
