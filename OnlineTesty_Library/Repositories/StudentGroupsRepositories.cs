﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILogger<StudentGroupsRepositories> _logger;

        public StudentGroupsRepositories(IUnitOfWork unitOfWork, AuthenticationStateProvider authenticationStateProvider,
            ILogger<StudentGroupsRepositories> logger) : base(unitOfWork)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _logger = logger;
        }

        public Guid Create(StudentGroup model)
        {
            string lecturerEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            model.LecturerEmail = lecturerEmail;
            this.GetDbSet<StudentGroup>().Add(model);
            this.UnitOfWork.SaveChanges();
            _logger.LogInformation("Create new student group");
            return model.ID;
        }

        public void Delete(Guid? ID)
        {
            var remove = Read(ID);
            this.GetDbSet<StudentGroup>().Remove(remove);
            this.UnitOfWork.SaveChanges();
            _logger.LogInformation("Delete student group");
        }

        public StudentGroup Read(Guid? ID)
        {
            _logger.LogInformation("Read student group");
            return this.GetDbSet<StudentGroup>()
                .Where(e => e.ID == ID).FirstOrDefault();
        }

        public void Update(StudentGroup model)
        {
            var update = Read(model.ID);
            update.Name = model.Name;
            this.UnitOfWork.SaveChanges();
            _logger.LogInformation("Update student group");
        }

        public IEnumerable<StudentGroup> FindAllForLecturer()
        {
            string lecturerEmail = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;
            _logger.LogInformation("Find all student groups for lecturer");

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
