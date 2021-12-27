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
    public class LecturerProfileDetailsRepositories : BaseRepositoryEF, ILecturerProfileDetailsRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LecturerProfileDetailsRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid AssignProfileDetailsToLecturer(LecturerProfileDetails model)
        {
            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
            model.EmailAddress = userEmail.Substring(67).Trim();

            this.GetDbSet<LecturerProfileDetails>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public IEnumerable<LecturerProfileDetails> FindAll()
        {
            return this.GetDbSet<LecturerProfileDetails>();
        }

        public LecturerProfileDetails Read(string emailAddress)
        {
            return this.GetDbSet<LecturerProfileDetails>()
                .Where(e => e.EmailAddress == emailAddress).FirstOrDefault();
        }

        public LecturerProfileDetails ReadByLastName(string lastName)
        {
            return this.GetDbSet<LecturerProfileDetails>()
                .Where(e => e.LastName == lastName).FirstOrDefault();
        }

        public bool WhetherEmailIsInTheDb(LecturerProfileDetails model)
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
    }

    public interface ILecturerProfileDetailsRepositories
    {
        Guid AssignProfileDetailsToLecturer(LecturerProfileDetails model);
        LecturerProfileDetails Read(string emailAddress);
        LecturerProfileDetails ReadByLastName(string lastName);
        IEnumerable<LecturerProfileDetails> FindAll();
        bool WhetherEmailIsInTheDb(LecturerProfileDetails model);
    }
}
