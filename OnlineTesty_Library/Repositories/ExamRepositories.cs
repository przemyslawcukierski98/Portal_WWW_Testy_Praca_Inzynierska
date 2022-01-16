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
    public class ExamRepositories : BaseRepositoryEF, IExamRepositories
    {
        private readonly IStudentAndGroupRepositories _studentAndGroupRepositories;
        private readonly ILecturerProfileDetailsRepositories _lecturerProfileDetailsRepositories;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ExamRepositories(IUnitOfWork unitOfWork, 
            IStudentAndGroupRepositories studentAndGroupRepositories,
            ILecturerProfileDetailsRepositories lecturerProfileDetailsRepositories,
            AuthenticationStateProvider authenticationStateProvider) : base(unitOfWork)
        {
            _studentAndGroupRepositories = studentAndGroupRepositories;
            _lecturerProfileDetailsRepositories = lecturerProfileDetailsRepositories;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public Guid Create(Exam model)
        {
            string userEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            this.GetDbSet<Exam>().Add(model);
            model.UserEmail = userEmail;
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<Exam>().Remove(remove);
            this.UnitOfWork.SaveChanges();
        }

        // listy egzaminów dla wykładowcy i studenta
        public IEnumerable<Exam> FindAssignedExams(string titleFilter, string studentFilter, string groupFilter)
        {
            IQueryable<Exam> assignedExams = Enumerable.Empty<Exam>().AsQueryable();
            string userEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            ValidationObject validation = ValidationMethods.ValidationForExams(titleFilter, null, groupFilter);

            if (validation.TitleFilterIsFilled && validation.GroupFilterIsFilled)
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail).Where(e => e.Name.Contains(titleFilter)).
                    Where(e => e.StudentGroupName.Contains(groupFilter));
            }
            else if(validation.TitleFilterIsFilled && validation.GroupFilterIsNullOrEmpty)
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail).Where(e => e.Name.Contains(titleFilter));
            }
            else if(validation.TitleFilterIsNullOrEmpty && validation.GroupFilterIsFilled)
            {
                assignedExams = this.GetDbSet<Exam>().Where(e => e.UserEmail == userEmail).Where(e => e.StudentGroupName.Contains(groupFilter));
            }

            return assignedExams;
        }

        public IEnumerable<Exam> FindAssignedExamsForStudent(string titleFilter, string lecturerFilter)
        {
            IQueryable<Exam> assignedExams = Enumerable.Empty<Exam>().AsQueryable();
            LecturerProfileDetails lecturerProfileDetails = new LecturerProfileDetails();
            string studentEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;
            string lecturerEmail = string.Empty;
            var userDetails = _studentAndGroupRepositories.Read(studentEmail);
            List<Exam> emptylist = new List<Exam>();

            if (titleFilter == null) titleFilter = string.Empty;
            if (lecturerFilter == null) lecturerFilter = string.Empty;

            lecturerProfileDetails = _lecturerProfileDetailsRepositories.ReadByLastName(lecturerFilter);

            if(lecturerProfileDetails != null)
            {
                lecturerEmail = lecturerProfileDetails.EmailAddress;
            }

            ValidationObject validation = ValidationMethods.ValidationForExams(titleFilter, lecturerFilter, null);

            if (userDetails != null)
            {
                if(validation.TitleFilterIsFilled && validation.StudentFilterIsFilled)
                {
                    assignedExams = this.GetDbSet<Exam>().Where(e => e.StudentGroupName == userDetails.StudentGroupName)
                        .Where(e => e.Name.Contains(titleFilter))
                        .Where(e => e.UserEmail == lecturerEmail);
                }
                else if(validation.TitleFilterIsFilled && validation.StudentFilterIsNullOrEmpty)
                {
                    assignedExams = this.GetDbSet<Exam>().Where(e => e.StudentGroupName == userDetails.StudentGroupName)
                        .Where(e => e.Name.Contains(titleFilter));
                }
                else if(validation.TitleFilterIsNullOrEmpty && validation.StudentFilterIsFilled)
                {
                    assignedExams = this.GetDbSet<Exam>().Where(e => e.StudentGroupName == userDetails.StudentGroupName)
                        .Where(e => e.UserEmail == lecturerEmail);
                }

                return assignedExams;
            }
            else
            {    
                return emptylist;
            } 
        }

        public Exam Read(Guid? ID)
        {
            return this.GetDbSet<Exam>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public string GetExamName(Guid? ID)
        {
            Exam exam = this.GetDbSet<Exam>().Where(e => e.ID == ID).FirstOrDefault();
            return exam.Name;
        }
    }

    public interface IExamRepositories
    {
        Exam Read(Guid? ID);
        Guid Create(Exam model);
        string GetExamName(Guid? ID);
        void Delete(Guid? ID);
        IEnumerable<Exam> FindAssignedExams(string nameFilter, string studentFilter, string groupFilter);
        IEnumerable<Exam> FindAssignedExamsForStudent(string titleFilter, string lecturerFilter);
    }
}
