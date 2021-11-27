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

        public StudentTestResultRepositories(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public StudentTestResult GetResult(Guid? ID)
        {
            throw new NotImplementedException();
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
        Guid SaveExamResult(StudentTestResult model);
        StudentTestResult GetResult(Guid? ID);
    }
}
