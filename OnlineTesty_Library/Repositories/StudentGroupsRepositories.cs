using Microsoft.AspNetCore.Http;
using OnlineTesty_Library.Contexts;
using OnlineTesty_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Repositories
{
    public class StudentGroupsRepositories : BaseRepositoryEF, IStudentGroupsRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentGroupsRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Create(StudentGroup model)
        {
            var lecturerEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            model.LecturerEmail = lecturerEmail;
            this.GetDbSet<StudentGroup>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<StudentGroup>().Remove(remove);
            this.UnitOfWork.SaveChanges();
        }

        public StudentGroup Read(Guid? ID)
        {
            return this.GetDbSet<StudentGroup>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public void Update(StudentGroup model)
        {
            var update = Read(model.ID);
            update.Name = model.Name;
            this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<StudentGroup> FindAllForLecturer()
        {
            var lecturerEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString().Substring(67).Trim();

            return this.GetDbSet<StudentGroup>().Where(e => e.LecturerEmail == lecturerEmail);
        }

        public IEnumerable<StudentGroup> FindAll()
        {
            return this.GetDbSet<StudentGroup>();
        }
    }

    public interface IStudentGroupsRepositories
    {
        StudentGroup Read(Guid? ID);
        Guid Create(StudentGroup model);
        void Update(StudentGroup model);
        void Delete(Guid? ID);
        IEnumerable<StudentGroup> FindAll();
        IEnumerable<StudentGroup> FindAllForLecturer();
    }
}
