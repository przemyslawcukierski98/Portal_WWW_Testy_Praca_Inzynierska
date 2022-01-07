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
    public class StudentAndGroupRepositories : BaseRepositoryEF, IStudentAndGroupRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentAndGroupRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool WhetherEmailIsInTheDb(StudentAndGroup model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
            var allRecords = FindAll();
            bool emailIsInDatabase = false;

            model.EmailAddress = userEmail.Substring(67).Trim();

            foreach (var item in allRecords)
            {
                if (item.EmailAddress.Equals(model.EmailAddress))
                {
                    emailIsInDatabase = true;
                }
            }

            if (emailIsInDatabase)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Guid AssignStudentToGroup(StudentAndGroup model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
            model.EmailAddress = userEmail.Substring(67).Trim();

            this.GetDbSet<StudentAndGroup>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public IEnumerable<StudentAndGroup> FindAll()
        {
            return this.GetDbSet<StudentAndGroup>();
        }

        public StudentAndGroup Read(string emailAddress)
        {
            return this.GetDbSet<StudentAndGroup>()
                .Where(e => e.EmailAddress == emailAddress).FirstOrDefault();
        }

        public StudentAndGroup ReadByLastName(string lastName)
        {
            return this.GetDbSet<StudentAndGroup>()
                .Where(e => e.LastName == lastName).FirstOrDefault();
        }
    }

    public interface IStudentAndGroupRepositories
    {
        Guid AssignStudentToGroup(StudentAndGroup model);
        StudentAndGroup Read(string emailAddress);
        StudentAndGroup ReadByLastName(string lastName);
        IEnumerable<StudentAndGroup> FindAll();
        bool WhetherEmailIsInTheDb(StudentAndGroup model);
    }
}
